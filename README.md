# phonebook

# Amaç
Telefon defterine kullanıcı eklenebilmesi, kullanıcıya ait iletişim bilgilerinin eklenip background servisler ile raporlanıp görüntülenmesi

## Api Layer

### PhoneBook.PersonOperationService
Kullanıcı operasynonun gerçekleştiği katmandır.  
**Persons Controller**    
- GET api/Persons/     => Kişi listesini getirir.  
- GET api/Persons/{id} => Kişi + kişiye ait iletişim bilgilerini döndürür.   
- POST api/Persons/ => Yeni kişi oluşturur.  
- DELETE api/Persons/ => Mevcud kişiyi siler.  
  
**Contacts Controller**   
- POST api/Contacts => Kişiye iletişim bilgisi ekler  
- DELETE api/Contacts => İletişim bilgisinin silinmesini sağlar  

### PhoneBook.ReportService
- GET api/Reports/getall => Tüm raporları listeler  
- GET api/Reports/ => Rapor talebi oluşturur.  
- POST api/Upload => Worker servis'in oluşturduğu exceli kendi assembly'sine kayıt eder  

### PhoneBook.Common => Models  
Data transfer objelerin'in yer aldığı katmandır.  

### PhoneBook.Utils  
- Rabbit MQ => Kuyruk mekanizması lagic'leri bulunmaktadır.  
- Excel Helper => Excel oluşturma logicleri yer almaktadır.  

