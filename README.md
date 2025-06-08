# Mayın Tarlası Oyunu 💣

Bu proje, C# Windows Forms kullanılarak geliştirilmiş klasik Mayın Tarlası (Minesweeper) oyununun bir uygulamasıdır.

## 🎮 Oyun Hakkında

Mayın Tarlası, 10x10'luk bir ızgarada gizlenmiş 10 mayını bulmaya çalıştığınız klasik bir bulmaca oyunudur.
Amacınız tüm güvenli alanları açmak ve mayınlara basmamaktır.

## 🚀 Özellikler

- **10x10 Oyun Tahtası**: Toplam 100 kare
- **10 Mayın**: Rastgele yerleştirilmiş mayınlar
- **Sayı İpuçları**: Her kare etrafındaki mayın sayısını gösterir
- **Bayrak Sistemi**: Sağ tık ile şüpheli kareleri işaretleyebilirsiniz
- **Otomatik Açılma**: Boş alanlara tıkladığınızda komşu alanlar da otomatik açılır
- **Kazanma/Kaybetme Durumu**: Oyun sonunda uygun mesajlar

## 📸 Ekran Görüntüleri

### Oyun Kaybedildiğinde
![Oyun Kaybetti](screenshot1.png)
*Mayına bastığınızda tüm mayınlar görünür hale gelir ve oyun biter*

### Oyun Devam Ederken
![Oyun Devam Ediyor](screenshot2.png)
*Güvenli alanları açarken sayılar size yardımcı olur*

## 🎯 Nasıl Oynanır?

1. **Sol Tık**: Bir kareyi açmak için
2. **Sağ Tık**: Şüpheli bir kareye bayrak koymak/kaldırmak için
3. **Sayılar**: Açılan karelerdeki sayılar, o karenin etrafındaki 8 komşu karede kaç mayın olduğunu gösterir
4. **Amaç**: Tüm güvenli alanları açmak (mayın olmayan kareler)

## 🛠️ Teknik Detaylar

- **Dil**: C#
- **Framework**: .NET Framework 4.7.2
- **UI**: Windows Forms
- **IDE**: Visual Studio

### Proje Yapısı
```
deneme/
├── Form1.cs              # Ana oyun formu
├── Form1.Designer.cs     # Form tasarım dosyası
├── Program.cs            # Uygulama giriş noktası
├── HSDButton.cs          # Özel buton sınıfı (Form1.cs içinde)
└── Properties/           # Proje özellikleri
```

## 🔧 Kurulum ve Çalıştırma

1. **Gereksinimler**:
   - Visual Studio 
   - .NET Framework 4.7.2

2. **Çalıştırma**:
   ```bash
   git clone https://github.com/rmeysaaltay/mayinTarlasi.git
   cd mayinTarlasi
   ```
   - `deneme.sln` dosyasını Visual Studio ile açın
   - F5 tuşuna basarak çalıştırın

## 🏗️ Kod Yapısı

### Button Sınıfı
Özel buton sınıfı aşağıdaki özelliklere sahiptir:
- `row`, `column`: Butonun konumu
- `point`: Etrafındaki mayın sayısı
- `mine`: Mayın var mı?
- `flag`: Bayrak konmuş mu?
- `revealed`: Açılmış mı?

### Ana Fonksiyonlar
- `MayinYerlestir()`: Mayınları rastgele yerleştirir
- `PuanlariEkle()`: Her kare için komşu mayın sayısını hesaplar
- `ButonTiklandi()`: Sol tık olayı
- `ButonSagTiklandi()`: Sağ tık olayı (bayrak)
- `KomsuAlanlariAc()`: Boş alanların komşularını otomatik açar

## 🎊 Oyun Kuralları

- Mayına basarsanız oyunu kaybedersiniz
- Tüm güvenli alanları açarsanız oyunu kazanırsınız
- Bayraklar yanlış yerleştirilse bile oyunu etkilemez
- Sayılar size en büyük ipucunu verir

## 👨‍💻 Geliştirici

**Rumeysa Altay** - [GitHub Profili](https://github.com/rmeysaaltay)

## 📝 Lisans

Bu proje Nesne Tabanlı Programlama dersinde eğitim amaçlı geliştirilmiştir.

---

**İyi Eğlenceler! 🎮**
