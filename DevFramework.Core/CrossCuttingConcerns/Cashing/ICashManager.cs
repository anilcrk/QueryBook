using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Cashing
{
   public interface ICashManager//cashe interfacesi
    {
        T Get<T>(string key);
        void Add(string key, object data, int casheTime);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void clear();
    }
}
