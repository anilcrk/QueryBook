using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Cashing.Microsoft
{
    public class MemoryCasheManager : ICashManager
    {
        protected ObjectCache Cashe //microsoft memory cashe not:dışarıdan erişim sağlanamaması için protecden erişim bildirgeci ile yazıldı.
=> MemoryCache.Default;
        public void Add(string key, object data, int casheTime)
        {
          if(data==null)
            {
                return;
            }
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(casheTime) };//gelen time' a göre nekadar sürelik cashleme yapılacağını belirledik
            Cashe.Add(new CacheItem(key, data), policy);//policy belirlendikten sonra cashe ekleme işlemi yaptık
        }

        public void clear()
        {
            foreach (var item in Cashe)
            {
                Remove(item.Key);//bütün cashe datalarını siler
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cashe[key];  //Cashe nesnesi arasında gönderilen key'e göre olan cashi çek T'ye dönüştür geri gönder
        }

        public bool IsAdd(string key)
        {
            return Cashe.Contains(key);//key ile gönderilen data cashe'de varmı ?
        }

        public void Remove(string key)
        {
            Cashe.Remove(key);//gönderilen keye göre cashe bellekten siler
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysRemove = Cashe.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();
            foreach (var key in keysRemove)
            {
                Remove(key);
            }
        }
    }
}
