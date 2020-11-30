using System;
using System.Collections.Generic;
using System.Text;

namespace Filmoteka
{
    public enum Zanr
    {
        Akcija,
        Triler,
        Kriminalisticki,
        Komedija,
        SciFi,
        Horor,
        Drama,
        None
    }

    public class Film
    {
        #region Atributi

        int id;
        static int brojač;
        string naziv;
        Zanr žanr;
        double ocjena;
        List<string> glumci;

        #endregion

        #region Properties

        public int Id
        {
            get => id;
        }

        public string Naziv
        {
            get => naziv;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Naziv ne smije biti prazan!");

                naziv = value;
            }
        }

        public double Ocjena
        {
            get => ocjena;
            set
            {
                if (value < 0 || value > 5)
                    throw new ArgumentException("Ocjena nije u dozvoljenom opsegu!");

                ocjena = value;
            }
        }

        public Zanr Žanr
        {
            get => žanr;
            set => žanr = value;
        }

        public List<string> Glumci
        {
            get => glumci;
            set
            {
                foreach (string glumac in value)
                    if (String.IsNullOrWhiteSpace(glumac))
                        throw new ArgumentException("Ime glumca je prazno!");

                glumci = value;
            }
        }

        #endregion

        #region Konstruktor

        public Film(string name, double rating, Zanr genre, List<string> actors)
        {
            brojač++;
            id = brojač.GetHashCode();

            Naziv = name;
            Ocjena = rating;
            Žanr = genre;

            if (actors == null)
                Glumci = new List<string>();
            else
                Glumci = actors;
        }

        #endregion

    }
}
