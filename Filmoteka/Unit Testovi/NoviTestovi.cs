using Filmoteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Unit_Testovi
{
    [TestClass]
    public class NoviTestovi
    {
        #region Zamjenski Objekti

        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();
            filmoteka.Filmovi.Add(film);

            IReziser r = new Stub();
            
            List<Film> rezirani = filmoteka.DajSveFilmoveZaRezisera(r);

            Assert.IsTrue(rezirani.Contains(film));
        }

        #endregion

        #region TDD
        // GRESKA var filmoteka pamti vrijednosti iz drugih testova samim tim testovi padaju kada se sve pokrene a prolaze samostalno

        [TestMethod]
        public void TestDodajNastavakIstiGlumci()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();
            filmoteka.Filmovi.Add(film);

            filmoteka.DodajNastavak(film, 4.0, true);

            Assert.IsTrue(filmoteka.Filmovi.Find(f => f.Naziv == "Need For Speed 2" && f.Žanr == Zanr.Akcija && f.Glumci.Count == 2) != null);
        }
        // GRESKA var filmoteka pamti vrijednosti iz drugih testova samim tim testovi padaju kada se sve pokrene a prolaze samostalno

        [TestMethod]
        public void TestDodajNastavakRazlicitiGlumci()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();
            filmoteka.Filmovi.Add(film);

            filmoteka.DodajNastavak(film, 4.0, false, new List<string>() { "Brad Pitt", "Chris Hemsworth", "Antonio Banderas" });

            Assert.IsTrue(filmoteka.Filmovi.Find(f => f.Naziv == "Need For Speed 2" && f.Žanr == Zanr.Akcija && f.Glumci.Count == 3) != null);
        }
        // GRESKA var filmoteka pamti vrijednosti iz drugih testova samim tim testovi padaju kada se sve pokrene a prolaze samostalno

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDodajNastavakIzuzetak()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();

            filmoteka.DodajNastavak(film, 4.0, false, new List<string>() { "Brad Pitt", "Chris Hemsworth", "Antonio Banderas" });
        }

        [TestMethod]
        public void TestDajSveFilmoveSGLumcima2()
        {
            var filmo = new Filmoteka.Filmoteka();
            filmo.Filmovi.Add(new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            filmo.Filmovi.Add(new Film("Bajazit", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            filmo.Filmovi.Add(new Film("Otac na sluzbenom putu", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
         

            List<Film> filmoviFiltrirani = new List<Film>();
            var glumci = new List<string>() { "Aaron Paul", "Dominic Cooper" };
            filmoviFiltrirani = filmo.DajSveFilmoveSGlumcima(glumci);
            Console.WriteLine(filmoviFiltrirani.Count);

            Assert.IsTrue( filmo.DajSveFilmoveSGlumcima(glumci).Count==3); 
        }


        /// <summary>
        /// Test metode DajSrednjuOcjenuSvihFilmova 
        /// </summary>
        [TestMethod]
        public void TestDajSrednjuOcjenuSvihFilmova()
        {
            var filmovi = new List<Film>();
            filmovi.Add(new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            filmovi.Add(new Film("Sniper", 4.7, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            filmovi.Add(new Film("Dictator", 4.2, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            var nova = new Watchlist("Lista2", filmovi);
            var s2 = nova.DajSrednjuOcjenuSvihFilmova();
            Assert.AreEqual(4.1, s2);
        }
        /// <summary>
        /// Test izuzetka da li je watchlist-a prazna
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Watchlista je prazna")]
        public void TestDajSrednjuOcjenuSvihFilmovaPrazan()
        {
            var list = new Watchlist("Lista", null);
            var s = list.DajSrednjuOcjenuSvihFilmova();
        }

        /// <summary>
        /// Test izuzetka za neispravno ime watchlist-e
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException), " Neispravno ime za watchlistu!")]
        public void TestWatchlistKonstruktorIzuzetak()
        {
            var list = new Watchlist(" ", null);
            var s = list.DajSrednjuOcjenuSvihFilmova();
        }

        [TestMethod]
        public void TestProdužiRokValidan()
        {
            DateTime pom = new DateTime(2020, 9, 20, 0, 0, 0, 0);
            DateTime novi = new DateTime(2020, 12, 25, 0, 0, 0, 0);
            var clan = new Clan("testic", "TESTICTESTIC","Emir", "Feratovic", pom);
            clan.ProdužiRok(novi);
            var provjera = clan.RokPretplate;
            Assert.IsTrue(provjera.Equals(novi));
        }

        /// <summary>
        /// Test izuzetka za neispravne parametre metode AutomatskiKorisnickiPodaci
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Neispravni parametri")]
        public void TestAutomatskiKorisničkiPodaciIzuzetak()
        {
            Tuple<string, string> e = Gost.AutomatskiKorisničkiPodaci("3nsar", "Ka9ur5");
        }

        /// <summary>
        /// Test da li metoda AutomatskiKorisnickiPodaci vrati ispravan username
        /// </summary>
        [TestMethod]
        public void TestAutomatskiKorisničkiPodaciUsername()
        {
            Tuple<string, string> e = Gost.AutomatskiKorisničkiPodaci("Ensar", "Kapur");
            Tuple<string, string> novi = Tuple.Create("EnKapursar","ENKAPURSAR");
            Assert.AreEqual(e.Item1, novi.Item1);
        }

        /// <summary>
        /// Test da li metoda AutomatskiKorisnickiPodaci vrati ispravan password
        /// </summary>
        [TestMethod]
        public void TestAutomatskiKorisničkiPodaciPassword()
        {
            Tuple<string, string> e = Gost.AutomatskiKorisničkiPodaci("Ensar", "Kapur");
            Tuple<string, string> novi = Tuple.Create("EnKapursar", "ENKAPURSAR");
            Assert.AreEqual(e.Item2, novi.Item2);
        }

        /// <summary>
        /// Test izuzetka kada je pogresan atribut username prilikom poziva konstruktora
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Neispravan format za username!")]
        public void TestGostKonstruktorIzuzetakUsername()
        {
            var gost = new Gost("usr123", "pass123", "Ensar", "Prezime");
        }

        /// <summary>
        /// Test izuzetka kada je pogresan atribut password prilikom poziva konstruktora
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Neispravan format za password!")]
        public void TestGostKonstruktorIzuzetakPassword()
        {
            var gost = new Gost("useruser", "pass123", "Ensar", "Prezime");
        }
        /// <summary>
        /// Test izuzetka kada je pogresan atribut ime prilikom poziva konstruktora
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Neispravan format za ime!")]
        public void TestGostKonstruktorIzuzetakIme()
        {
            var gost = new Gost("useruser", "ENKAPURSAR", "11Ensar", "Prezime");
        }
        /// <summary>
        /// Test izuzetka kada je pogresan atribut prezime prilikom poziva konstruktora
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Neispravan format za prezime!")]
        public void TestGostKonstruktorIzuzetakPrezime()
        {
            var gost = new Gost("useruser", "ENKAPURSAR", "Ensar", "11Prezime");
        }
        /// <summary>
        /// Test konstruktora
        /// </summary>
        [TestMethod]
        public void TestGostKonstruktor()
        {
            var gost = new Gost("useruser", "ENKAPURSAR", "Ensar", "Prezime");
        }

        /// <summary>
        /// Test izuzetka za metodu ProduziRokIzuzetka
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Nemoguce produziti clanarinu!")]
        public void TestProduziRokIzuzetak()
        {
            DateTime pom = new DateTime(2020, 12, 20, 0, 0, 0, 0);
            DateTime novi = new DateTime(2020, 12, 25, 0, 0, 0, 0);
            var clan = new Clan("testic", "TESTICTESTIC", "Emir", "Feratovic", pom);
            clan.ProdužiRok(novi);
        }

        /// <summary>
        /// Test konstruktora klase Clan
        /// </summary>
        [TestMethod]
        public void TestClanKonstruktor()
        {
            DateTime pom = new DateTime(2020, 9, 20, 0, 0, 0, 0);
            var clan = new Clan(pom);
            Assert.IsTrue(pom.Equals(clan.RokPretplate));
        }
        /// <summary>
        /// Test metode ResetujListe klase Clan
        /// </summary>
        [TestMethod]
        public void TestResetujListe()
        {
            DateTime pom = new DateTime(2020, 9, 20, 0, 0, 0, 0);
            var clan = new Clan(pom);
            clan.ResetujListe();
            Assert.IsTrue(clan.Watchliste.Count.Equals(0));
        }

        /// <summary>
        /// Test metode DodajWatchlistu klase Filmoteka
        /// </summary>
        [TestMethod]
        public void TestDodajWatchlistu()
        {
            var filmoteka = new Filmoteka.Filmoteka();
            DateTime pom = new DateTime(2020, 9, 20, 0, 0, 0, 0);
            var clan = new Clan("testic", "TESTICTESTIC", "Emir", "Feratovic", pom);
            List<Film> filmovi = new List<Film> { new Film("Testic", 2.5, Zanr.Akcija, new List<string> { "Mustafa Nadarevic" })};
            filmoteka.DodajWatchlistu(clan, filmovi, "Testna");
            Assert.IsTrue(clan.Watchliste.Count.Equals(1));
        }

        /// <summary>
        /// Test metode DodajWatchlistu klase Filmoteka sa greskom
        /// </summary>
        ///string name, double rating, Zanr genre, List<string> actors
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Naziv ne smije biti prazan!")]
        public void TestDodajWatchlistuNevalidnoIme()
        {
            var filmoteka = new Filmoteka.Filmoteka();
            DateTime pom = new DateTime(2020, 9, 20, 0, 0, 0, 0);
            var clan = new Clan("testic", "TESTICTESTIC", "Emir", "Feratovic", pom);
            List<Film> filmovi = new List<Film> {new Film("", 2.5, Zanr.Akcija, new List<string> { "Mustafa Nadarevic" }) };
            filmoteka.DodajWatchlistu(clan, filmovi, "Testna");
        }

        /// <summary>
        ///  testiranje dodavanja gosta u listu
        /// </summary>
        [TestMethod]
        public void TestRadSaKorisnicima2()
        {
            Gost gost1 = new Gost("testic", "TESTICTESTIC", "Emir", "Feratovic");

            Film film1 = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });
            var filmoteka1 = new Filmoteka.Filmoteka();
            filmoteka1.Filmovi.Add(film1);
            filmoteka1.RadSaKorisnicima(gost1, 0);
            Assert.IsTrue(filmoteka1.Gosti.Count==1);
        }

        /// <summary>
        /// testiranje izbaccivanja gosta iz liste 
        /// </summary>
        [TestMethod]
        public void TestRadSaKorisnicima1()
        {
            Gost gost1 = new Gost("testic", "TESTICTESTIC", "Meho", "Aliefendic");

            Film film1 = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });
            var filmoteka1 = new Filmoteka.Filmoteka();
            filmoteka1.Filmovi.Clear();
            filmoteka1.Filmovi.Add(film1);
            filmoteka1.RadSaKorisnicima(gost1, 0);
            filmoteka1.RadSaKorisnicima(gost1, 1);
            Assert.IsTrue(0 == filmoteka1.Gosti.Count);
        }

        /// <summary>
        /// metodat baca iuztetak kada se dodaje isti korisnik
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), " Korisnik već postoji u sistemu!")]
        public void TestRadSaKorisnicima3()
        {
            Gost gost1 = new Gost("testic", "TESTICTESTIC", "Emir", "Feratovic");

            Film film1 = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });
            var filmoteka1 = new Filmoteka.Filmoteka();
            filmoteka1.Filmovi.Add(film1);
            filmoteka1.RadSaKorisnicima(gost1, 0);
            filmoteka1.RadSaKorisnicima(gost1, 0);
        }

        /// <summary>
        /// metodat baca iuztetak kada se brise  korisnik kiji ne postoji u listi
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Korisnik ne postoji u sistemu!")]
        public void TestRadSaKorisnicima4()
        {
            Gost gost1 = new Gost("testic", "TESTICTESTIC", "Emir", "Feratovic");

            Film film1 = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });
            var filmoteka1 = new Filmoteka.Filmoteka();
            filmoteka1.Filmovi.Add(film1);
            filmoteka1.RadSaKorisnicima(gost1, 1);
        }

        /// <summary>
        /// test metode DajSveFilmoveSGLumcima u slucaju kada baca izuzetak
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Lista filmova je prazna")]
        public void TestDajSveFilmoveSGLumcima1()
        {
            var filmo = new Filmoteka.Filmoteka();

            List<Film> filmoviFiltrirani = new List<Film>();
            var glumci = new List<string>() { "Aaron Paul", "Dominic Cooper" };
            filmoviFiltrirani = filmo.DajSveFilmoveSGlumcima(glumci);
            Console.WriteLine(filmoviFiltrirani.Count);
        }

        /// <summary>
        /// test getera klase Watchlist
        /// </summary>
        [TestMethod]
        public void TestWatchlistGetters()
        {
            List<Film> filmovi = new List<Film> { new Film("Testic", 2.5, Zanr.Akcija, new List<string> { "Mustafa Nadarevic" }) };
            var wlista = new Watchlist("Testna",filmovi);
            Assert.IsTrue(wlista.Naziv.Equals("Testna"));
            Assert.IsTrue(wlista.Filmovi.Equals(filmovi));
        }

        /// <summary>
        /// test ocjena filma
        /// </summary>
        [TestMethod]
        public void TestOcjenaFilma()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });
            Assert.IsTrue(film.Glumci.Count.Equals(2));
            Assert.IsTrue(film.Ocjena.Equals(3.5));
            Assert.IsTrue(film.Naziv.Equals("Need For Speed"));
        }


        /// <summary>
        /// test ocjena filma
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Ocjena nije u dozvoljenom opsegu!")]
        public void TestOcjenaFilmaNevalidna()
        {
            Film film = new Film("Need For Speed", -3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });
        }

        /// <summary>
        /// test kada je ime glumca prazan string 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Ime glumca je prazno!")]
        public void TestGlumciFilmaGreska()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "", "Dominic Cooper" });
        }

        /// <summary>
        /// test konstruktor sa null filma
        /// </summary>
        [TestMethod]
        public void TestKonstruktorFilmaNull()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija,null);
            Assert.IsTrue(film.Glumci.Count.Equals(0));
        }

        /// <summary>
        /// test id filma
        /// </summary>
        [TestMethod]
        public void TestFilmIdGet()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, null);
            var x = film.Id;
            Assert.IsTrue(film.Id.Equals(x));
        }

        /// <summary>
        /// test geteri Gosta
        /// </summary>
        [TestMethod]
        public void TestGostGetters()
        {
            var gost = new Gost("testtest", "TESTTESTTEST", "Test", "Testativc");
            Assert.IsTrue(gost.Ime.Equals("Test"));
            Assert.IsTrue(gost.Prezime.Equals("Testativc"));
            Assert.IsTrue(gost.Username.Equals("testtest"));
            var x= gost.Password;
            Assert.IsTrue(gost.Password.Equals(x));
        }

        #endregion
    }
}
