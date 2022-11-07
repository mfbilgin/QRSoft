# QRSoft
- QR Soft bir QR Menü yazılımıdır. Veri tabanı olarak **[MSSQL](https://www.microsoft.com/tr-tr/sql-server/sql-server-2019)** kullanılmıştır ve bağlantılar **[Entity Framework](https://docs.microsoft.com/tr-tr/ef/core/get-started)** ile yapılmıştır.
- Yazılımın temel amacı işletmelerin dijital ortamda menü oluşturup bu menüleri daha az masraf ile müşterilerle buluşturmasını sağlamaktır.
- Gerekli bilgilendirmeler ilgili kodların başlangıçlarında yorum olarak belirtilmiştir.

- [Business](https://github.com/mfbilgin42/QRSoft/tree/master/Business) : Gelen bilgileri gerekli koşullara göre işlemek için oluşturulan katmandır.Abstract klasörü soyut nesneleri(interface),Concrete klasörü somut nesneleri(class),ValidationRules ve Utilities klasörleri doğrulama işlemlerini gerçekleştiren sınıfları içerisinde tutar.
- [Core](https://github.com/mfbilgin42/QRSoft/tree/master/Core) : Genel tüm projelerde kullanım için uygun, temel alt yapıyı oluituran sınıf ve methodları içerir.
- [DataAccess](https://github.com/mfbilgin42/QRSoft/tree/master/DataAccess) : Veri tabanı CRUD operasyonlarını gerçekleştirir. Abstract klasörü interfaceleri,Concrete klasörü classları tutar.
- [Entities](https://github.com/mfbilgin42/QRSoft/tree/master/Entities) : Bu katman veritabanı tablosundaki verileri tutar.
- [WebAPI](https://github.com/mfbilgin42/QRSoft/tree/master/WebAPI) : Yazılımın dış dünyaya açıldığı katmandır. 
