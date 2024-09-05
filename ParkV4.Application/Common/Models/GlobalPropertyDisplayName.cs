using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkV4.Application.Common.Models
{
    public class GlobalPropertyDisplayName
    {
        public static readonly string UpdateId = "ID";

        public static readonly string BrandId = "Marka ID";
        public static readonly string BrandName = "Marka Adı";

        public static readonly string ModelId = "Model ID";
        public static readonly string ModelName = "Model Adı";
        public static readonly string ModelBrandId = "Modelin Marka ID";

        public static readonly string CompanyId = "Şirket ID";
        public static readonly string CompanyName = "Şirket Adı";
        public static readonly string CompanyPhoto = "Şirket Fotoğraf";

        public static readonly string CustomerId = "Müşteri ID";
        public static readonly string CustomerIdentityNumber = "Müşteri Kimlik Numarası";
        public static readonly string CustomerName = "Müşteri Adı";
        public static readonly string CustomerSurname = "Müşteri Soyadı";
        public static readonly string CustomerTelephoneNumber = "Müşteri Telefon Numarası";

        public static readonly string VehicleId = "Araç ID";
        public static readonly string VehicleType = "Araç Tipi";
        public static readonly string VehiclePlate = "Araç Plakası";
        public static readonly string VehicleColor = "Araç Rengi";
        public static readonly string VehicleBrandId = "Araç Marka ID";
        public static readonly string VehicleModelId = "Araç Model ID";

        public static readonly string UserId = "Kullanıcı ID";
        public static readonly string UserName = "Kullanıcı İsim";
        public static readonly string UserSurname = "Kullanıcı Soyisim";
        public static readonly string UserProfilePhoto = "Kullanıcı Fotoğrafı";
        public static readonly string UserUsername = "Kullanıcı Adı";
        public static readonly string UserPassword = "Kullanıcı Parola";
        public static readonly string UserEmail = "Kullanıcı E-Posta";
        public static readonly string UserTelephoneNumber = "Kullanıcı Telefon Numarası";
        public static readonly string UserToken = "Kullanıcı Token";
        public static readonly string UserTokenExpireTime = "Kullanıcı Token Expire Time";
        public static readonly string UserRefreshToken = "Kullanıcı RefreshToken";
        public static readonly string UserRefreshTokenExpireTime = "Kullanıcı RefreshToken Expire Time";
        public static readonly string UserCompanyId = "Kullanıcı Şirket ID";

        public static readonly string LocationId = "Lokasyon ID";
        public static readonly string LocationDescription = "Lokasyon Açıklaması";
        public static readonly string LocationCompanyId = "Lokasyon Şirket ID";

        public static readonly string EntryId = "İşlem ID";
        public static readonly string EntryReceiptId = "İşlem Fiş Numarası";
        public static readonly string EntryVehicleId = "İşlem Araç ID";
        public static readonly string EntryCustomerId = "İşlem Müşteri ID";
        public static readonly string EntryLocationId = "İşlem Lokasyon ID";
        public static readonly string EntryFirstPrice = "İşlem Giriş Fiyatı";
        public static readonly string EntryLastPrice = "İşlem Son Fiyatı";
        public static readonly string EntryPriceDifference = "İşlem Fiyat Farkı";
        public static readonly string EntryFirstDuration = "İşlem İlk Ayar Zamanı";
        public static readonly string EntryLastDuration = "İşlem Son Ayar Zamanı";
        public static readonly string EntryFirstDate = "İşlem İlk Tarih";
        public static readonly string EntryLastDate = "İşlem Son Tarih";
        public static readonly string EntryStatus = "İşlem Durumu";
        public static readonly string EntryDescription = "İşlem Açıklaması";
    }
}