using System;
using System.Collections.Generic;
using System.Text;

namespace Filmoteka
{
    public class Watchlist
    {
        #region Atributi

        string naziv;
        List<Film> filmovi;

        #endregion

        #region Properties

        public string Naziv
        {
            get => naziv;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new FormatException("Neispravno ime za watchlistu!");

                naziv = value;
            }
        }
        public List<Film> Filmovi
        {
            get => filmovi;
        }

        #endregion

        #region Konstruktor

        public Watchlist(string name, List<Film> movies = null)
        {
            Naziv = name;

            if (movies == null)
                filmovi = new List<Film>();
            else
                filmovi = movies;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda za formiranje i vraćanje srednje ocjene svih filmova iz watchliste.
        /// Ako je watchlista prazna, baca se izuzetak.
        /// U suprotnom, formira se zbir svih ocjena filmova a zatim dijeli sa brojem filmova i vraća kao rezultat.
        /// </summary>
        /// <returns></returns>
        // * Metoda DajSrednjuOcjenuSvihFilmova dio Ensar Kapur *
        public double DajSrednjuOcjenuSvihFilmova()
        {
            if (filmovi.Count == 0) throw new InvalidOperationException("Watchlista je prazna");
            double s = 0;
            for(int i = 0; i < filmovi.Count; i++)
            {
                s += filmovi[i].Ocjena;
            }
            s /= filmovi.Count;
            return Math.Round((Double)s, 1);
            
        }

        #endregion
    }
}
