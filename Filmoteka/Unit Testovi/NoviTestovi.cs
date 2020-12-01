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
        [ExpectedException(typeof(InvalidOperationException), "Lista filmova je prazna")]
        public void TestDajSveFilmoveSGLumcima()
        {
            var filmoteka = new Filmoteka.Filmoteka();
            filmoteka.Filmovi.Add(new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            filmoteka.Filmovi.Add(new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            filmoteka.Filmovi.Add(new Film("Need For Speed", 3.5, Zanr.Akcija, new List<string>() { "Aaron Paul", "Dominic Cooper" }));
            List<Film> filmoviFiltrirani = new List<Film>();
            var glumci = new List<string>() { "Aaron Paul", "Dominic Cooper" };
            filmoviFiltrirani = filmoteka.DajSveFilmoveSGlumcima(glumci);
            Assert.AreEqual(3, filmoviFiltrirani.Count); 
        }

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
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Watchlista je prazna")]
        public void TestDajSrednjuOcjenuSvihFilmovaPrazan()
        {
            var list = new Watchlist("Lista", null);
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

        #endregion
    }
}
