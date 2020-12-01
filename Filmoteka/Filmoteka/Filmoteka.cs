using System;
using System.Collections.Generic;
using System.Text;

namespace Filmoteka
{
    public class Filmoteka
    {
        #region Atributi

        static List<Gost> gosti = new List<Gost>();
        static List<Clan> clanovi = new List<Clan>();
        static List<Film> filmovi = new List<Film>();

        #endregion

        #region Properties

        public List<Gost> Gosti
        {
            get => gosti;
        }

        public List<Clan> Clanovi
        {
            get => clanovi;
        }

        public List<Film> Filmovi
        {
            get => filmovi;
        }

        #endregion

        #region Konstruktor

        public Filmoteka()
        {

        }

        #endregion

        #region Metode

        public void RadSaKorisnicima(Gost korisnik, int opcija)
        {
            if (opcija == 0)
            {
                Gost postojeci = gosti.Find(k => k.Id == korisnik.Id);
                if (postojeci == null)
                    postojeci = clanovi.Find(k => k.Id == korisnik.Id);

                if (postojeci == null)
                    if (korisnik is Clan)
                        clanovi.Add((Clan)korisnik);
                    else
                        gosti.Add(korisnik);

                else
                    throw new InvalidOperationException("Korisnik već postoji u sistemu!");
            }

            else if (opcija == 1)
            {
                Gost postojeci = gosti.Find(k => k.Id == korisnik.Id);
                if (postojeci == null)
                    postojeci = clanovi.Find(k => k.Id == korisnik.Id);
                else
                {
                    gosti.Remove(postojeci);
                    return;
                }
                if (postojeci == null)
                    throw new ArgumentNullException("Korisnik ne postoji u sistemu!");
                else
                    clanovi.Remove(clanovi.Find(k => k.Id == korisnik.Id));
            }
        }

        public void DodajWatchlistu(Clan c, List<Film> filmovi, string naziv)
        {
            foreach (Film film in filmovi)
                if (film.Naziv.Length < 1)
                    throw new ArgumentNullException("Ime filma je prazno!");

            c.Watchliste.Add(new Watchlist(naziv, filmovi));
        }

        public List<Film> DajSveFilmoveZaRezisera(IReziser reziser)
        {
            List<Film> rezirani = new List<Film>();

            foreach (Film f in Filmovi)
            {
                if (reziser.DaLiJeReziraoFilm(f))
                    rezirani.Add(f);
            }

            return rezirani;
        }

        /// <summary>
        /// Metoda za filtriranje liste filmova prema glumcima.
        /// Ukoliko je lista filmova ili glumaca prazna, baca se izuzetak.
        /// U suprotnom, vraćaju se svi filmovi koji u listi glumaca imaju sve glumce koji su proslijeđeni kao parametar.
        /// </summary>
        /// <param name="glumci"></param>
        /// <returns></returns>
        /// Emir Ensar i Meho
        public List<Film> DajSveFilmoveSGlumcima(List<string> glumci)
        {
            List<Film> filmoviFiltrirani = new List<Film>();
            if (filmovi.Count == 0) throw new InvalidOperationException("Lista filmova je prazna");

            for (int i = 0; i < glumci.Count; i++)
            {
                for (int j = 0; j < filmovi.Count; j++)
                {
                    if (filmovi[j].Glumci.Contains(glumci[i]))
                    {
                        Boolean ima = false;
                        for (int k = 0; k < filmoviFiltrirani.Count; k++)
                        {
                           
                            if (filmoviFiltrirani[k].Naziv.Equals(filmovi[j].Naziv))
                            {
                              
                                ima = true;
                                break;
                            }
                        }
                        if(!ima)
                            filmoviFiltrirani.Add(new Film(filmovi[j].Naziv, filmovi[j].Ocjena, filmovi[j].Žanr, filmovi[j].Glumci));
                       
                    }
                }

               
            }
            return filmoviFiltrirani;
        }

        public void DodajNastavak(Film film, double rating, bool istiGlumci, List<string> noviGlumci = null)
        {
            throw new NotImplementedException();
        }

        
        #endregion
    }
}
