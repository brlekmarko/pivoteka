CREATE TABLE korisnik
(
  korisnicko_ime VARCHAR(32) NOT NULL,
  lozinka VARCHAR(256) NOT NULL,
  email VARCHAR(64) NOT NULL,
  ime VARCHAR(64) NOT NULL,
  prezime VARCHAR(64) NOT NULL,
  PRIMARY KEY (korisnicko_ime)
);

CREATE TABLE kupac
(
  adresa_dostave VARCHAR(256) NOT NULL,
  korisnicko_ime VARCHAR(32) NOT NULL,
  PRIMARY KEY (korisnicko_ime),
  FOREIGN KEY (korisnicko_ime) REFERENCES korisnik(korisnicko_ime)
);

CREATE TABLE zaposlenik
(
  datum_zaposljenja DATE NOT NULL,
  kraj_zaposlenja DATE,
  korisnicko_ime VARCHAR(32) NOT NULL,
  PRIMARY KEY (korisnicko_ime),
  FOREIGN KEY (korisnicko_ime) REFERENCES korisnik(korisnicko_ime)
);

CREATE TABLE dobavljac
(
  ime VARCHAR(64) NOT NULL,
  adresa VARCHAR(256) NOT NULL,
  email VARCHAR(64) NOT NULL,
  PRIMARY KEY (ime)
);

CREATE TABLE vrsta
(
  ime VARCHAR(64) NOT NULL,
  PRIMARY KEY (ime)
);

CREATE TABLE pivo
(
  ime VARCHAR(64) NOT NULL,
  cijena NUMERIC(8,2) NOT NULL,
  količina INT NOT NULL,
  opis VARCHAR(512) NOT NULL,
  zemlja_podrijetla VARCHAR(64) NOT NULL,
  neto_volumen INT NOT NULL,
  ime_dobavljaca VARCHAR(64) NOT NULL,
  vrsta VARCHAR(64) NOT NULL,
  PRIMARY KEY (ime),
  FOREIGN KEY (ime_dobavljaca) REFERENCES dobavljac(ime),
  FOREIGN KEY (vrsta) REFERENCES vrsta(ime)
);

CREATE TABLE narudzba
(
  id INT NOT NULL,
  datum DATE NOT NULL,
  ukupna_cijena NUMERIC(8,2) NOT NULL,
  korisnicko_ime VARCHAR(32) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (korisnicko_ime) REFERENCES kupac(korisnicko_ime)
);

CREATE TABLE ocjena
(
  ocjena INT NOT NULL CHECK (ocjena >= 1 AND ocjena <= 5),
  tekst VARCHAR(512),
  korisnicko_ime_kupca VARCHAR(32) NOT NULL,
  ime_piva VARCHAR(64) NOT NULL,
  PRIMARY KEY (korisnicko_ime_kupca, ime_piva),
  FOREIGN KEY (korisnicko_ime_kupca) REFERENCES kupac(korisnicko_ime),
  FOREIGN KEY (ime_piva) REFERENCES pivo(ime)
);

CREATE TABLE narucio_pivo
(
  količina INT NOT NULL,
  cijena_piva NUMERIC(8,2) NOT NULL,
  ime_piva VARCHAR(64) NOT NULL,
  id_narudzbe INT NOT NULL,
  PRIMARY KEY (ime_piva, id_narudzbe),
  FOREIGN KEY (ime_piva) REFERENCES pivo(ime),
  FOREIGN KEY (id_narudzbe) REFERENCES narudzba(id)
);


INSERT INTO korisnik(korisnicko_ime, lozinka, email, ime, prezime) VALUES 
	('testkupac1', 'lozinka', 'testkupac@gmail.com', 'Test', 'Kupac'),
	('testzaposlenik1', 'lozinka', 'testzaposlenik@pivoteka.hr', 'Test', 'Zaposlenik');

INSERT INTO kupac(korisnicko_ime, adresa_dostave) VALUES ('testkupac1', 'Adresa Prvog Kupca 12a');
INSERT INTO zaposlenik(korisnicko_ime, datum_zaposljenja, kraj_zaposlenja) VALUES ('testzaposlenik1', '2024-03-29', NULL);

INSERT INTO dobavljac(ime, adresa, email) VALUES 
	('Dobavljac Piva 1', 'Adresa prvog dobavljaca pive', 'emaildobavljac1@pivo.hr'),
	('Samo dobra piva', 'Ulica hrvatskih velikana 17', 'dobropivo@gmail.com');
	

INSERT INTO pivo(ime, cijena, količina, opis, zemlja_podrijetla, neto_volumen, ime_dobavljaca) VALUES
	('Staropramen', 1.50, 100, 'Poznato pivo iz Češke', 'Češka', 500, 'Dobavljac Piva 1'),
	('Guinness', 3, 20, 'Svi znate za Guinness', 'Irska', 333, 'Samo dobra piva');

INSERT INTO narudzba(id, datum, ukupna_cijena, korisnicko_ime) VALUES
	(1, '2024-04-03', 6, 'testkupac1');

INSERT INTO narucio_pivo(ime_piva, id_narudzbe, količina, cijena_piva) VALUES
	('Staropramen', 1, 2, 1.50),
	('Guinness', 1, 1, 3);

INSERT INTO ocjena(korisnicko_ime_kupca, ime_piva, ocjena, tekst) VALUES
	('testkupac1', 'Guinness', 3, 'Očekivao sam bolje za tu cijenu, radije pijem žuju');