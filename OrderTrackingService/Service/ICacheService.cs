using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingService.Service
{

    public interface ICacheService
    {
        /// <summary>
        /// Belirtilen anahtar için veriyi cache'e kaydeder.
        /// </summary>
        /// <param name="key">Cache anahtarı.</param>
        /// <param name="value">Cache'lenecek değer.</param>
        /// <param name="expiry">Cache süresi. Null ise varsayılan değer kullanılır.</param>
        /// <typeparam name="T">Cache'lenecek verinin tipi.</typeparam>
        Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// Belirtilen anahtara karşılık gelen cache verisini alır.
        /// </summary>
        /// <param name="key">Alınacak cache verisinin anahtarı.</param>
        /// <typeparam name="T">Döndürülecek verinin tipi.</typeparam>
        /// <returns>Cache'den alınan veri. Eğer veri yoksa default değer döndürülür.</returns>
        Task<T> GetCacheAsync<T>(string key);
    }

}
