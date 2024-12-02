# Web arkkitehtuurit ja sovelluskehykset projekti

Projekti on toteutettu ASP .Net Corella. Alla löytyy kaikki tiedot toteutetuista CRUD-toiminnallisuuksista.

Projektissa on käytetty ChatGtp:tä Program.cs tiedoston rakentamisessa. Tämä siksi, että sen käsittely oli hyvin hankala hahmottaa itselle vaikka nyt tehtävää tehdessä se on tullut tutummaksi. Ymmärrän kuitenkin mitä varten eri osat ovat ja Mapperin toimintoja osaan itse lisätä. 


## Cateogrian CRUD-toiminnot admin-roolilla

Categories-taulu sisältää UserId:n. Tämä antaa tiedon, kuka on lisännyt tietyn kategorian tauluun. Tämä tieto haetaan ohjelmassa kirjautumisen yhteydessä luodulla JWT-tokenilla. 

Kateogrian muokkausta varten en hakenut käyttäjän ID:tä, sillä en kokenut tätä tarpeelliseksi tässä toteutuksessa. Itse ajattelin sen niin, että token tulee hakea päivityksen yhteydessä, jos tietokantaan halutaan tieto, kuka on päivittänyt kategoriaa. Jos taas halutaan, että tietokannassa pidetään vain tieto, kuka kategorian on luonut, niin UserId-kenttää ei päivitetä kategorian päivityksen yhteydessä. 

### Kategorian toiminnot

1. Kategorian luominen
2. Kategorian osittainen päivittäminen id:n perusteella
3. Kategorian poisto id:n perusteella


