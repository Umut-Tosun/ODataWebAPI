# OData Query Parametreleri

## Temel Parametreler
```http
$count=true          // Toplam kayıt sayısını döndürür
$top=10              // İlk 10 kaydı getirir
$skip=0              // İlk 0 kaydı atlar (sayfalama için)
$select=Name,Price   // Sadece belirtilen alanları getirir
$orderby=Name desc   // Name alanına göre azalan sıralama (asc: artan)
$expand=Category     // İlişkili tabloyu join eder
```

## Filter Operatörleri

### Karşılaştırma Operatörleri
```http
$filter=Name eq 'Domates'           // Eşittir
$filter=Price ne 50                 // Eşit değildir
$filter=Quantity gt 100             // Büyüktür
$filter=Quantity ge 100             // Büyük eşittir
$filter=Quantity lt 50              // Küçüktür
$filter=Quantity le 50              // Küçük eşittir
```

### String Fonksiyonları
```http
$filter=startswith(Name, 'Dom')              // "Dom" ile başlar
$filter=endswith(Name, 'tes')                // "tes" ile biter
$filter=contains(Name, 'oma')                // "oma" içerir
$filter=length(Name) eq 7                    // Uzunluk 7 karakter
$filter=indexof(Name, 'oma') eq 1            // "oma" substring'inin başlangıç indexi 1
$filter=substring(Name, 1, 3) eq 'oma'       // 1. indexten itibaren 3 karakter al
$filter=tolower(Name) eq 'domates'           // Küçük harfe çevir
$filter=toupper(Name) eq 'DOMATES'           // Büyük harfe çevir
$filter=trim(Name) eq 'Domates'              // Baş/sondaki boşlukları temizle
$filter=concat(Name, ' Fresh') eq 'Domates Fresh'  // String birleştir
$filter=contains(tolower(Name), 'dom')       // Büyük/küçük harf duyarsız arama
```

### Aritmetik Operatörler
```http
$filter=Price add Quantity eq 150   // Toplama
$filter=Price sub 10 eq 40          // Çıkarma
$filter=Price mul 2 eq 100          // Çarpma
$filter=Price div 2 eq 25           // Bölme
$filter=Price mod 3 eq 0            // Mod (kalan)
```

### Mantıksal Operatörler
```http
$filter=(Price gt 50) and (Quantity lt 200)  // VE
$filter=(Price lt 50) or (Quantity gt 300)   // VEYA
$filter=not (Price eq 50)                    // DEĞİL
```

### Null Kontrol
```http
$filter=Name eq null    // Null'a eşit
$filter=Name ne null    // Null değil
```

### IN Operatörü
```http
$filter=Name in ('Domates', 'Biber', 'Patlıcan')
$filter=CategoryId in (1, 2, 3)
```

## Tarih/Saat Fonksiyonları

### Tarih Karşılaştırma
```http
$filter=OrderDate eq 2024-01-01T00:00:00Z              // Belirli tarih
$filter=OrderDate ge 2024-01-01T00:00:00Z              // Tarihten sonra
$filter=OrderDate le 2024-12-31T23:59:59Z              // Tarihten önce
$filter=OrderDate ge 2024-01-01 and OrderDate le 2024-12-31  // Aralık
```

### Tarih Parçaları
```http
$filter=year(OrderDate) eq 2024      // Yıl
$filter=month(OrderDate) eq 12       // Ay (1-12)
$filter=day(OrderDate) eq 29         // Gün (1-31)
$filter=hour(OrderDate) eq 14        // Saat (0-23)
$filter=minute(OrderDate) eq 30      // Dakika (0-59)
$filter=second(OrderDate) eq 15      // Saniye (0-59)
```

## Gelişmiş Örnekler

### Sayfalama
```http
// 1. sayfa (ilk 10 kayıt)
?$top=10&$skip=0&$count=true

// 2. sayfa (11-20 arası kayıtlar)
?$top=10&$skip=10&$count=true

// 3. sayfa (21-30 arası kayıtlar)
?$top=10&$skip=20&$count=true
```

### Kompleks Sorgular
```http
// Çoklu sıralama
?$orderby=Category/Name asc, Price desc

// Çoklu expand
?$expand=Category,Supplier

// Nested expand
?$expand=Category($expand=ParentCategory)

// Select ile expand
?$expand=Category($select=Name,Description)

// Filter ile expand
?$expand=Orders($filter=Status eq 'Completed')

// Kombinasyon örneği
?$filter=contains(tolower(Name), 'domates') and Price gt 20
&$orderby=Price desc
&$top=10
&$skip=0
&$count=true
&$select=Name,Price,Stock
&$expand=Category($select=Name)
```

### Özel Senaryolar
```http
// Bugünün kayıtları
$filter=date(OrderDate) eq date(now())

// Bu ayki kayıtlar
$filter=year(OrderDate) eq year(now()) and month(OrderDate) eq month(now())

// Fiyatı 50-100 arası
$filter=Price ge 50 and Price le 100

// İsmi 'A' ile başlayan ve stokta olan
$filter=startswith(Name, 'A') and Stock gt 0

// Kategori adına göre filtreleme (ilişkili tablo)
$filter=Category/Name eq 'Elektronik'
```

## Notlar

- String değerler **tek tırnak** içinde: `'Domates'`
- Tarih formatı: **ISO 8601** (`2024-01-01T00:00:00Z`)
- URL encoding gerekebilir: boşluk = `%20`, tek tırnak = `%27`
- `and`, `or`, `not` küçük harfle yazılmalı
- Operatör öncelikleri: `not` > `and` > `or`