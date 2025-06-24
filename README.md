
# Okul Yönetim Sistemi - MVC Projesi

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş bir okul yönetim sistemi örneğidir.  
Proje, Entity Framework Core ile veritabanı işlemlerini gerçekleştiren Entity sınıflarına, UnitOfWork, Repository ve Service katmanlarına sahiptir.

---

<img width="2240" alt="Image" src="https://github.com/user-attachments/assets/0253f46a-5e59-4206-8ab2-d5f0b94f7f64" />


<img width="2240" alt="Image" src="https://github.com/user-attachments/assets/c75ee09d-5d71-4c1e-adcf-e5d88d89eb58" />


<img width="2240" alt="Image" src="https://github.com/user-attachments/assets/28ea81fe-d767-45a3-ba56-f2bb6dfbb046" />


<img width="2238" alt="Image" src="https://github.com/user-attachments/assets/b7977d84-6372-41d5-9f9a-d2835f95c744" />


---

## Özellikler

- Bölümler, Öğrenciler, Öğretmenler, Dersler ve Notlar gibi temel okul varlıklarının yönetimi  
- Katmanlı mimari: Repository pattern ve Unit of Work ile veri erişim katmanı soyutlanmıştır  
- Servis katmanı ile iş kuralları ve operasyonlar yönetilir  
- Asenkron (async/await) veri sorguları ve işlemleri  
- Kullanıcı girişi ve kimlik doğrulama yok (basit erişim modeli)  
- LINQ sorguları ile raporlama ve istatistiksel veri çekme  
- Entity Framework Core üzerinden ilişkisel veritabanı yönetimi

---

## Proje Yapısı

/Controllers --> MVC Controllerlar
/Models --> Entity ve DTO modelleri
/Repositories --> Veri erişim katmanı (Repository pattern)
/Services --> İş katmanı (Service pattern)
/UnitOfWork --> Birim işlemleri yönetimi
/Views --> Razor View sayfaları

---

## Kullanılan Teknolojiler

- ASP.NET Core MVC  
- Entity Framework Core (Code First / Database First)  
- LINQ  
- Asenkron Programlama (async/await)  
- C# 10+  
- SQL Server (veya tercihe göre başka ilişkisel veritabanı)  

---
