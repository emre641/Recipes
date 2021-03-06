using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key);             //Cache te var mı
        void Remove(string key);            //Cache i uçur
        void RemoveByPattern(string pattern);

    }
}
