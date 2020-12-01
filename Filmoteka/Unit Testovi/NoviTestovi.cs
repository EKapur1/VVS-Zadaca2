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

            IReziser r = new Reziser();

            List<Film> rezirani = filmoteka.DajSveFilmoveZaRezisera(r);

            Assert.IsTrue(rezirani.Contains(film));
        }

        #endregion

        #region TDD

        [TestMethod]
        public void TestDodajNastavakIstiGlumci()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();
            filmoteka.Filmovi.Add(film);

            filmoteka.DodajNastavak(film, 4.0, true);

            Assert.IsTrue(filmoteka.Filmovi.Find(f => f.Naziv == "Need For Speed 2" && f.Žanr == Zanr.Akcija && f.Glumci.Count == 2) != null);
        }

        [TestMethod]
        public void TestDodajNastavakRazlicitiGlumci()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();
            filmoteka.Filmovi.Add(film);

            filmoteka.DodajNastavak(film, 4.0, false, new List<string>() { "Brad Pitt", "Chris Hemsworth", "Antonio Banderas" });

            Assert.IsTrue(filmoteka.Filmovi.Find(f => f.Naziv == "Need For Speed 2" && f.Žanr == Zanr.Akcija && f.Glumci.Count == 3) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDodajNastavakIzuzetak()
        {
            Film film = new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" });

            var filmoteka = new Filmoteka.Filmoteka();

            filmoteka.DodajNastavak(film, 4.0, false, new List<string>() { "Brad Pitt", "Chris Hemsworth", "Antonio Banderas" });
        }

        [TestMethod]
        public void TestDajSveFilmoveSGLumcima()
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
            
        }

        #endregion
    }
}
