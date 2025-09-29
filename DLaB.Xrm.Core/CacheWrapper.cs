#nullable enable
using System;
using System.Collections.Concurrent;

#if NET
using Microsoft.Extensions.Caching.Memory;
#else
using System.Runtime.Caching;
#endif

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    /// <summary>
    /// Provides a unified interface for caching across different .NET frameworks
    /// </summary>
    public interface ICacheWrapper
    {
        /// <summary>
        /// Gets an item from the cache
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <returns>The cached item, or null if not found</returns>
        object? Get(string key);

        /// <summary>
        /// Sets an item in the cache with an absolute expiration time
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="value">The value to cache</param>
        /// <param name="absoluteExpiration">The absolute expiration time</param>
        void Set(string key, object? value, DateTime absoluteExpiration);
    }

    internal class CacheWrapper : ICacheWrapper
    {
#if NET
        private readonly IMemoryCache _cache;

        public CacheWrapper(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public object? Get(string key)
        {
            return _cache.Get(key);
        }

        public void Set(string key, object? value, DateTime absoluteExpiration)
        {
            _cache.Set(key, value, absoluteExpiration);
        }
#else
        private readonly MemoryCache _cache;

        public CacheWrapper(MemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public object? Get(string key)
        {
            return _cache.Get(key);
        }

        public void Set(string key, object? value, DateTime absoluteExpiration)
        {
            _cache.Set(key, value, new CacheItemPolicy
            {
                AbsoluteExpiration = new DateTimeOffset(absoluteExpiration)
            });
        }
#endif
    }

    /// <summary>
    /// Extension methods for ICacheWrapper
    /// </summary>
    public static class CacheWrapperExtensions
    {
        private static readonly ConcurrentDictionary<string, object> LocksByKey = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Gets the item from the cache or adds it using the factory functions to both get the value and set the expiration time.
        /// </summary>
        /// <param name="cache">The Cache</param>
        /// <param name="key">The key</param>
        /// <param name="getValue">The getValue factory</param>
        /// <param name="getExpirationTime">The getExpirationTime factory</param>
        /// <returns></returns>
        public static T GetOrAdd<T>(this ICacheWrapper cache, string key, Func<string, T> getValue, Func<string, T, DateTime> getExpirationTime)
        {
            var value = (T?)cache.Get(key);
            if (value != null)
            {
                return value;
            }

            var lockForKey = LocksByKey.GetOrAdd(key, k => new object());
            lock (lockForKey)
            {
                value = (T?)cache.Get(key);
                if (value != null)
                {
                    return value;
                }

                value = getValue(key);
                cache.Set(key, value, getExpirationTime(key, value));
            }

            return value;
        }
    }
}