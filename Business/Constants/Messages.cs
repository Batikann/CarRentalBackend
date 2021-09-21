using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string SystemAlert = "System is now off";
        public static string RentalAdded = "Car Rental Successful ";
        public static string RentalNotAdded = "Car Rental Failed ";
        public static string NotExistCar;
        public static string InvalidFileExtension;
        public static string NotExist;
        public static string ImageNumberLimitExceeded;
        public static string AddImageSuccessful;
        public static string UpdateImageSuccessful;
        public static string DeleteImage;
        public static string AuthorizationDenied = "Kullanıcının Yetkisi Bulunmamaktadır.";
        public static string UserUpdated = "Kullanıcı Bilgileriniz Başarıyla Güncellenmiştir.";
        public static string CreditCardAdded = "Kredi Kartınız Başarıyla Eklenmiştir";
        public static string CreditCarDeleted = "Kredi Kartı Başarıyla Silinmiştir";
        public static string SuccessRentalUpdate = "Araba Başarıyla  Geri Alınmıştır.";
        public static string ErrorRentalUpdate = "Araba İadesi Başarısız.";
        public static string CarIsNotAvailable="Araba Mevcut Değil";
    }
}
