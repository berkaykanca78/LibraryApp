using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LibraryApp.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetMonthName(this int ay)
        {
            ay++;

            if (ay >= 1 && ay <= 12)
            {
                CultureInfo customCulture = new CultureInfo("tr-TR");
                return customCulture.DateTimeFormat.GetMonthName(ay);
            }
            return "";
        }

        public static int? YasHesapla(this DateTime? dogumTarihi)
        {
            if (dogumTarihi == null)
            {
                return null;
            }
            else
            {
                int yas = DateTime.Now.Year - dogumTarihi.Value.Year;
                if (dogumTarihi > DateTime.Now.AddYears(-yas))
                    yas--;
                return yas;
            }
        }

        public static TimeSpan TarihAralikHesaplaTimeSpan(this DateTime baslangicTarihi, DateTime bitisTarih)
        {
            return bitisTarih - baslangicTarihi; //create TimeSpan object
        }

        public static bool TarihAralikHesaplaAyBuyuklukKontrol(this int aySayisi, DateTime? baslangicTarihi, DateTime? bitisTarih)
        {

            TimeSpan tarihFarki = bitisTarih.Value - baslangicTarihi.Value;
            var toplamDakikaFarki = tarihFarki.TotalMinutes;
            //30 Gündeki dakika = 43200
            //7088 nolu taska göre tarih hesaplaması dakika hassasiyetinde olacaktır.
            return toplamDakikaFarki > 43200 * aySayisi;
        }


        public static bool TarihAralikHesaplaAyBuyuklukKontrolNtp(this int aySayisi, DateTime? baslangicTarihi, DateTime? bitisTarih)
        {
            var gunSayisi = aySayisi * 30;
            return baslangicTarihi.Value.AddDays(gunSayisi) <= bitisTarih;

            //TimeSpan tarihFarki = bitisTarih.Value - baslangicTarihi.Value;
            //var toplamDakikaFarki = tarihFarki.TotalMinutes;
            //30 Gündeki dakika = 43200
            //7088 nolu taska göre tarih hesaplaması dakika hassasiyetinde olacaktır.
            // return toplamDakikaFarki > 43200 * aySayisi;
        }

        public static bool TarihAralikHesaplaYilBuyuklukKontrol(this int yilSayisi, DateTime? baslangicTarihi, DateTime? bitisTarih)
        {
            if (baslangicTarihi == null || bitisTarih == null)
                return false;

            TimeSpan tarihFarki = bitisTarih.Value - baslangicTarihi.Value;
            var toplamGun = tarihFarki.TotalDays;
            return toplamGun > 365 * yilSayisi;
        }

        public static int CalculateAge(this DateTime dateOfBirth)
        {
            int age = 0;

            age = DateTime.Now.Year - dateOfBirth.Year;

            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
