﻿using Kumi.Game.Charts.Data;
using Kumi.Game.Charts.Timings;
using Kumi.Game.IO.Formats;

namespace Kumi.Game.Charts;

public interface IChart : IDecodable
{
    /// <summary>
    /// The <see cref="ChartInfo"/> of this <see cref="IChart{T}"/>.
    /// </summary>
    ChartInfo ChartInfo { get; set; }
    
    /// <summary>
    /// The <see cref="ChartMetadata"/> of this <see cref="IChart{T}"/>.
    /// </summary>
    ChartMetadata Metadata { get; }
    
    /// <summary>
    /// The <see cref="IEvent"/>s that occur throughout this <see cref="IChart{T}"/>.
    /// </summary>
    IReadOnlyList<IEvent> Events { get; }
    
    /// <summary>
    /// A <see cref="TimingPointHandler"/> that handles all timing points in this <see cref="IChart{T}"/>.
    /// </summary>
    TimingPointHandler TimingPoints { get; }
}

public interface IChart<out T> : IChart
{
    // todo...
}
