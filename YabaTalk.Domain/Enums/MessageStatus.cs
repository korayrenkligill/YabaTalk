using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YabaTalk.Domain.Enums
{
    public enum MessageStatus
    {
        /// Mesaj henüz gönderilmemiş veya gönderim işlemi kuyrukta bekliyor.
        /// Genellikle bir saat ikonu ile gösterilir.
        Pending,

        /// Mesaj sunucuya başarıyla ulaştı.
        /// Genellikle tek bir tik işareti ile gösterilir.
        Sent,

        /// Mesaj alıcının cihazına başarıyla iletildi.
        /// Genellikle çift gri tik işareti ile gösterilir.
        Delivered,

        /// Mesaj alıcı tarafından okundu.
        /// Genellikle çift mavi tik işareti ile gösterilir.
        Read,

        /// Mesaj gönderilirken bir hata oluştu.
        /// Genellikle kırmızı bir ünlem işareti ile gösterilir.
        Failed
    }
}
