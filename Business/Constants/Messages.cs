using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Program ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductsListed="Ürünler Listelendi";
        public static string ProductCountOfCategoryError="Bir Kategoride en fazla ürün olabilir.";
        public static string ProductUpdate="Ürün güncellendi";
        public static string ProductNameAlreadyExist="Bu isimde bir ürün daha önce eklenmiş";
        public static string CategoryLimitExceded="Kategori limiti aşıldığı için yeni ürün eklenemiyor.";
        public static string AuthorizationDenied="Erişim izniniz yok";
        internal static string UserRegistered="Kullanıcı Eklendi";
        internal static string UserNotFound="Kullanıcı bulunamadı";
        internal static string PasswordError= "Şifre Hatalı";
        internal static string SuccessfulLogin="Login olundu";
        internal static string UserAlreadyExists="Kullanıcı zaten kayıtlı";
        internal static string AccessTokenCreated= "Token oluşturuldu";
    }
}
