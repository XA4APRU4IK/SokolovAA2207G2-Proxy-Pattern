using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_pattern
{
    public class Proxy : ISubject
    {
        private readonly RealSubject _realSubject;
        private readonly Dictionary<string, (string result, DateTime timestamp)> _cache;
        private readonly TimeSpan _cacheDuration;

        public Proxy()
        {
            _realSubject = new RealSubject();
            _cache = new Dictionary<string, (string result, DateTime timestamp)>();
            _cacheDuration = TimeSpan.FromSeconds(5);
        }
        public string Request(string request)
        {
            if (!HasAccess())
            {
                return "доступ запрещен";
            }
            if (_cache.TryGetValue(request, out var cacheEntry))
            {
                if (DateTime.Now - cacheEntry.timestamp > _cacheDuration)
                {
                    Console.WriteLine("Proxy: Возвращение результата из кэша.");
                    return cacheEntry.result;
                }
                else
                {
                    _cache.Remove(request);
                }
            }
            string result = _realSubject.Request(request);
            _cache[request] = (result, DateTime.Now);
            return result;
        }
        private bool HasAccess()
        {
            return true;
        }
        
    }
}
