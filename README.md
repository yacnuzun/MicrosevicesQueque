# SupplyChainQueueSystem

**SupplyChainQueueSystem**, tedarik zinciri süreçleri gibi yüksek hacimli ve paralel işlem gerektiren senaryolarda mesaj tabanlı haberleşme sağlayan, .NET 8 ile geliştirilmiş mikroservis mimarisine sahip bir kuyruk yönetim sistemidir. RabbitMQ, Docker, Unit of Work ve Domain-Driven Design (DDD) prensipleriyle inşa edilmiştir.

## 🚀 Proje Amacı

- Mikroservis mimarisi üzerine gerçek dünyaya yakın bir yapı kurmak  
- RabbitMQ üzerinden asenkron haberleşmeyi yönetmek  
- SOLID prensiplerine uygun, modüler ve ölçeklenebilir bir yapı oluşturmak  
- Katmanlı mimari, validation, logging ve unit testing gibi yazılım geliştirme disiplinlerini uygulamak

---

## ⚙️ Kullanılan Teknolojiler ve Araçlar

| Teknoloji         | Açıklama                                      |
|------------------|-----------------------------------------------|
| .NET 8           | Temel backend framework                       |
| RabbitMQ         | Mesajlaşma altyapısı                          |
| Docker / Compose | Servis konteynerleştirme ve orkestrasyon      |
| FluentValidation | DTO seviyesinde veri doğrulama                |
| MailKit          | SMTP ile e-posta gönderimi                    |
| Unit of Work     | Veri erişim yönetimi                          |
| Serilog          | Loglama (planlandı)                           |
| xUnit            | Unit test altyapısı                           |

---

## 🧱 Proje Yapısı

```bash
/MicroservicesQueue
│
├── src
│ ├── OrderService.Api
│ ├── InventoryService.Api
│ ├── NotificationService.Api
│ ├── SharedLibraries
│ └── BuildingBlocks
│
├── docker-compose.yml
└── README.md
```
- **OrderService**: Sipariş işlemlerini yönetir ve kuyruklara mesaj bırakır.
- **InventoryService**: Stoğu günceller, mesaj kuyruğunu dinler.
- **NotificationService**: MailKit ile e-posta gönderimleri yapar.
- **SharedLibraries**: DTO’lar, BaseEntity, Result, Mail servisleri vb. içerir.
- **BuildingBlocks**: Core katmanı, Generic Repository, UoW, BaseResponse gibi altyapı kodlarını içerir.

---

## 🛠️ Kurulum (Docker ile Çalıştırma)

> Projeyi çalıştırmak için Docker ve .NET 8 SDK yüklü olmalıdır.

1. Repoyu klonlayın:

```bash
git clone https://github.com/kullanici-adi/MicroservicesQueue.git
cd MicroservicesQueue
```
2. Docker'ı Kurun:
```bash
docker-compose up --build
```
---
## ✅ Özellikler

-  RabbitMQ Publisher / Consumer yapısı
-  DTO validasyonları (FluentValidation)
-  Unit of Work ve Generic Repository altyapısı
-  Temel test altyapısı (xUnit)
---
## 🧪 Testler

- xUnit ile yazılmış unit test örnekleri mevcuttur.
- Test coverage ileride artırılacaktır.
---
## ✉️ Mail Servisi

- NotificationService, gelen mesajlara göre SMTP üzerinden e-posta gönderimi yapar.

- MailKit kullanılmaktadır, appsettings.json üzerinden SMTP ayarları yapılabilir.


