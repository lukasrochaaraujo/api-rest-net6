using System;
using System.Threading.Tasks;

namespace Rest.Infrastructure.Cache;

public interface ICacheService
{
    Task AddAsync<TValue>(string key, TValue value, TimeSpan expiration);
    Task<TValue> GetAsync<TValue>(string key);
}
