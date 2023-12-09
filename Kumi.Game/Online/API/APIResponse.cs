﻿using System.Net;
using Newtonsoft.Json;

namespace Kumi.Game.Online.API;

/// <summary>
/// An object that represents a response from the API, without any response data.
/// </summary>
public class APIResponse : IHasStatus
{
    [JsonProperty("code")]
    public int StatusCode { get; set; }
    
    [JsonProperty("message")]
    public string? Message { get; set; }
    
    [JsonProperty("data")]
    public Dictionary<string, object> Data { get; set; }
    
    [JsonIgnore]
    public bool IsSuccess => StatusCode == 200;
    
    [JsonIgnore]
    public CookieContainer Cookies { get; set; } = new CookieContainer();
   
    /// <summary>
    /// Gets the value of the specified key from the response data.
    /// </summary>
    /// <param name="key">The key to get the value of.</param>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <exception cref="KeyNotFoundException">An exception that states the key was not found.</exception>
    public T Get<T>(string key)
    {
        if (Data.TryGetValue(key, out var value))
        {
            return (T) value;
        }

        throw new KeyNotFoundException($"Key {key} was not found in the response data.");
    }
}
