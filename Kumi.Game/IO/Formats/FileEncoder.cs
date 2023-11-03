﻿using Kumi.Game.Extensions;

namespace Kumi.Game.IO.Formats;

public abstract class FileEncoder<TInput, TSection> : FileEncoder<TInput>, IFileHandler<TInput, TSection>
    where TInput : class
    where TSection : struct
{
    public TSection CurrentSection { get; set; }
    protected abstract IFileHandler<TInput, TSection>.SectionHeaderValues SectionHeader { get; }

    protected FileEncoder(int version, bool closeStreamUponProcessed = true)
        : base(version, closeStreamUponProcessed)
    {
    }

    protected void WriteSection(TSection section)
    {
        CurrentSection = section;
        HandleSection(section);
    }

    protected override void Process(TInput input)
    {
        base.Process(input);

        foreach (var section in Enum.GetValues(typeof(TSection)).Cast<TSection>())
        {
            // Write the section header
            var sectionName = section.ToString();
            WriteLine($"{SectionHeader.Start}{sectionName!.ToScreamingSnakeCase()}{SectionHeader.End}");
            WriteSection(section);
        }
    }

    protected abstract void HandleSection(TSection section);

    #region IFileHandler implementation

    IFileHandler<TInput, TSection>.SectionHeaderValues IFileHandler<TInput, TSection>.SectionHeader => SectionHeader;
    
    bool IFileHandler<TInput, TSection>.CloseStreamUponProcessed => CloseStreamUponProcessed;

    #endregion
}

public abstract class FileEncoder<TInput> : FileHandler<TInput>
    where TInput : class
{
    protected bool CloseStreamUponProcessed { get; }

    protected FileEncoder(int version, bool closeStreamUponProcessed = true)
        : base(version)
    {
        CloseStreamUponProcessed = closeStreamUponProcessed;
    }

    protected TextWriter Writer = null!;
    private Stream stream = null!;

    public void Encode(TInput input) => Encode(input, new MemoryStream());
    
    public void Encode(TInput input, Stream writableStream)
    {
        stream = writableStream;
        Writer = new StreamWriter(stream);

        Process(input);
        PostProcess();

        Writer.Flush();
        
        if (CloseStreamUponProcessed)
            Writer.Close();
    }


    protected override void Process(TInput input)
    {
        Current = input;

        HandleHeader();
    }

    protected void WriteLine(string line) => Writer.WriteLine(line);

    protected virtual void HandleHeader() => Writer.WriteLine($"#KUMI v{Version}");

    public override void Dispose()
    {
        stream.Dispose();
        Writer.Dispose();
    }
}