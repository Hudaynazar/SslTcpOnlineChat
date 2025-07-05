## SSL Tabanlı TCP Sunucu-İstemci Mesajlaşma Uygulaması
Bu proje, .NET Framework kullanılarak geliştirilmiş bir SSL (güvenli) TCP sunucu ve istemci mesajlaşma sistemidir. Amacım socket programlamayı öğrenmek ve gerçek dünya senaryolarında nasıl kullanıldığını kavramaktı. Proje sonunda, kişisel bilgisayarımı gerçek bir sunucuya dönüştürerek dış dünyadan erişilebilir hale getirdim.

## Kullanılan Teknolojiler
C# (Windows Forms)

TcpClient / TcpListener

SslStream (SSL şifreleme)

X.509 Sertifika (PFX)

Cloudflare + No-IP (Dinamik DNS ve yönlendirme)

Modem DDNS Ayarı

## Proje Yapısı
- Server_Arayüzlü/
   - Form1.cs
   - cert.pfx
- Client1_Arayuzlu/
   - Form1.cs
- README.md

## Nasıl Çalışır?
  Sunucu (Server_Arayüzlü)
TCP portu üzerinden dinleme yapar (varsayılan: XXXX).

İstemci bağlandığında, SslStream ile güvenli bir iletişim başlatır.

İlk olarak istemciden adını ister.

Daha sonra mesajları alır ve diğer istemcilere iletir.

Arayüz üzerinden de mesaj gönderilebilir.

 İstemci (Client1_Arayuzlu)
IP adresi veya domain üzerinden sunucuya bağlanır.

SSL doğrulamasını bypass eder (ValidateServerCertificate → true döner).

Bağlantı sonrası kullanıcı arayüzüyle mesaj gönderebilir.

Mesaj alımı ve gönderimi ayrı protokol üzerinden yapılır.

## Projeyi Çevrimiçi Hale Getirme Süreci
Socket programlamanın ötesine geçerek bu projeyi global erişime açtım. İşte adımlar:

1. Statik IP yerine No-IP (Dinamik DNS) kullandım
noip.com üzerinden ücretsiz bir hostname aldım

Modemime DDNS ayarı ekleyerek IP değişse bile bu hostname üzerinden erişimi sağladım.

2. Modem Port Yönlendirme (Port Forwarding)
Modemin arayüzüne girerek XXXX numaralı portu kendi bilgisayarıma yönlendirdim.

Böylece dışarıdan gelen TCP bağlantıları bilgisayarıma ulaşıyor.

3. Ücretsiz SSL Sertifikası Edindim
Web sitem için SSL sertifikası aldım (cloudfare'dan)

Başkada .pfx formatında self-signed sertifikası aldım. Server projesine ekledim (cert.pfx). //Bunu sildim fakat sizde kendinizinkini yapmaslısınız ve eklemelisiniz

Bu sayede iletişim SSL şifreli hale geldi.

4. Cloudflare Ayarları
Domain’im için Cloudflare kullanarak DNS yönetimi sağladım.

A kaydındaki IP adresini kendi IP adresimle değiştirdim.

"Proxy" özelliğini kapatarak doğrudan bağlantıya izin verdim.

## Güvenlik Notu
Geliştirme ve test sürecinde sertifika doğrulaması bypass edilmiştir (ValidateServerCertificate → return true). Gerçek dünyada kullanacaksanız bu kısmı doğru şekilde doğrulama yapacak şekilde güncelleyin.

Ayrıca, .pfx dosyasını ve parolasını asla GitHub’a yüklemeyin. .gitignore dosyasıyla hariç tutmayı unutmayın.

## Öğrendiklerim
TCP/IP temelleri ve SslStream ile güvenli iletişim

Dinamik DNS yönetimi

Modem port yönlendirme

Sertifika kullanımı ve SSL şifreleme

Cloudflare DNS yapılandırması

Gerçek bir bilgisayarı sunucuya dönüştürme

## Başlatmak İçin

Sunucuyu başlat:
Start Server_Arayüzlü uygulamasını çalıştırın ve "Bağlan" butonuna basın.

İstemciyi başlat:
Start Client1_Arayuzlu uygulamasını çalıştırın.

Mesaj yazın, alıcı adı girin ve gönderin.
## Lisans
Bu proje eğitim amaçlı geliştirilmiştir. Dilerseniz kendi projelerinize entegre edebilirsiniz.

## İletişim
Sorularınız olursa (https://api.whatsapp.com/send/?phone=%2B905447722293&text&type=phone_number&app_absent=0) sekmesinden yazabilirsiniz.

## Not
Sunucuya uzaktan bağlanmak istiyorsan bilgisayarın açık kalmalı ve güvenlik duvarı gelen bağlantıları engellememeli.

## Sonuç
Bu projede sadece kod yazmakla kalmadım, aynı zamanda ağ altyapısını, DNS yapılandırmasını, güvenlik ayarlarını ve sertifika kullanımını da öğrendim. Gerçek dünya uygulamasıyla teorik bilgiyi birleştirdim.
