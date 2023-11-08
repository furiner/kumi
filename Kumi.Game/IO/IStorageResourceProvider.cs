﻿using Kumi.Game.Database;
using osu.Framework.Audio;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace Kumi.Game.IO;

public interface IStorageResourceProvider
{
    /// <summary>
    /// The game renderer.
    /// </summary>
    IRenderer Renderer { get; }

    /// <summary>
    /// Retrieve the game-wide audio manager.
    /// </summary>
    AudioManager? AudioManager { get; }

    /// <summary>
    /// Access game-wide user files.
    /// </summary>
    IResourceStore<byte[]> Files { get; }

    /// <summary>
    /// Access game-wide resources.
    /// </summary>
    IResourceStore<byte[]> Resources { get; }

    /// <summary>
    /// Access realm.
    /// </summary>
    RealmAccess RealmAccess { get; }

    /// <summary>
    /// Create a texture loader store based on an underlying data store.
    /// </summary>
    /// <param name="underlyingStore">The underlying provider of texture data (in arbitrary image formats).</param>
    /// <returns>A texture loader store.</returns>
    IResourceStore<TextureUpload>? CreateTextureLoaderStore(IResourceStore<byte[]> underlyingStore);
}
