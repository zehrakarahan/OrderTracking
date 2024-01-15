1)proje içinde webapplication4 katmanında elasticsearch loglama olayı yapılıyor 2. olarak ise xml dosyasının class formatına gönüşüp ayıklaması saglanıyor.
2)webapplication2 katmanında captcha olayı ile veri üretimi saglanıyor 
3)Server katmanı ise login kısmını gerceklestirir ve rediscache kısmı ise forget token olan kısımda gerceklestirildi.Identity ve jwt ile token olusturma olayıda bu server katmanında gercekleştirildi.
https://ethereal.email/ bu web sitesi üzerinden mail gönderim olayı nasıl yapılır onun testi saglandı.

Elasticsearch Bilgiler

1) Önce asagıda verdigim yml uzantılı olarka bu dosyayı calıstırın.(Dosyayı çalıştırmak için  docker-compose -f {compose file name} up bunu kullanabilirsiniz.)

.yml file
version: '3.4'

services:
  elasticsearch:
    container_name: elasticsearch
    image: docker.elastic.co/elasticsearch/elasticsearch:7.9.1
    ports:
      - 9200:9200
    volumes:
      - elasticsearch-data:/usr/share/elasticsearch/data
    environment:
      - xpack.monitoring.enabled=true
      - xpack.watcher.enabled=false
      - "ES_JAVA_OPTS=-Xms512m -Xmx512m"
      - discovery.type=single-node
    networks:
      - elastic

  kibana:
    container_name: kibana
    image: docker.elastic.co/kibana/kibana:7.9.1
    ports:
      - 5601:5601
    depends_on:
      - elasticsearch
    environment:
      - ELASTICSEARCH_URL=http://localhost:9200
    networks:
      - elastic

networks:
  elastic:
    driver: bridge

volumes:
  elasticsearch-data:
2) Aşagıdaki bilgiler ise genel elastic indexlerini kontrol etmek için biraz bilgi 
elasticsearchte http://localhost:9200/_cat/indices?v bu tüm indexlerin listesini bakmanı saglar 
elasticsearchte index olusturduktan sonra kibana ekranında index olusturup bakabilirsin.Bu işlem asaması soyle kibana kurulduktan sonra 
discoverd tıklıyoruz sonra kibana altından index patern kısmına tıklıyoruz sonra 
create index alttan elasticsearchte olusan index isimleri yazar onu kopyalayın aynısını yazın sonra devam devam deyin bitti bu kadar sonra discover kısmına tekrar geri dön
sonra hangi db yi sececeksen onu sec ve ona göre loglara bakabilirsin bu kadar

eger yeni index olusturacaktan discover acılıyor once yukardaki menuden manage space kısmına tıklıyoruz bu kadar sonra işlemler aynı 

 

