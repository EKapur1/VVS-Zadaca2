using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Filmoteka
{
    public class Gost
    {
        #region Atributi

        string username, password, ime, prezime, id;

        #endregion

        #region Properties

        public string Username
        {
            get => username;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length < 5 || value.Length > 20
                    || value.Any(char.IsDigit))
                    throw new InvalidOperationException("Neispravan format za username!");

                username = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Length < 10 || value.Length > 20
                    || !value.All(char.IsUpper))
                    throw new InvalidOperationException("Neispravan format za password!");

                password = value.GetHashCode().ToString(); ;
            }
        }

        public string Ime
        {
            get => ime;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || !value.Substring(0, 1).All(char.IsUpper)
                    || !value.Substring(1).All(char.IsLower))
                    throw new InvalidOperationException("Neispravan format za ime!");

                ime = value;
            }
        }

        public string Prezime
        {
            get => prezime;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || !value.Substring(0, 1).All(char.IsUpper)
                    || !value.Substring(1).All(char.IsLower))
                    throw new InvalidOperationException("Neispravan format za prezime!");

                prezime = value;
            }
        }

        public string Id
        {
            get => id;
        }

        #endregion

        #region Konstruktor

        public Gost(string user, string pass, string name, string surname)
        {
            Random rand = new Random();
            id = rand.Next(100000).ToString();
            Username = user;
            Password = pass;
            Ime = name;
            Prezime = surname;
        }

        public Gost()
        {

        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda kojom se vrši formiranje korisničkih podataka koristeći proslijeđene parametre.
        /// Ukoliko su jedan ili više parametara neispravni, baca se izuzetak, kao i u slučaju da se sastoje od bilo
        /// kojih znakova koji nisu slova.
        /// U suprotnom, korisničko ime se formira kao: prvadvaslovaimenaprezimeostalaslovaimena.
        /// Password se formira kao PRVADVASLOVAIMENAPREZIMEOSTALASLOVAIMENA.
        /// Ukoliko ima više karaktera od dozvoljenih u postojećoj programskoj logici (properties), oduzima se višak slova.
        /// Zatim se korisničko ime i password vraćaju kao rezultati metode respektivno.
        /// </summary>
        /// <param name="ime"></param>
        /// <param name="prezime"></param>
        /// <returns></returns>
        public static Tuple<string, string> AutomatskiKorisničkiPodaci(string ime, string prezime)
        {
            if (!Regex.Match(ime, "^[A-Z][a-zA-Z]*$").Success || !Regex.Match(ime, "^[A-Z][a-zA-Z]*$").Success)
            {
                throw new InvalidOperationException("Neispravni parametri");
            }
            else
            {
                return Tuple.Create<string, string>(ime, "");
            }
        }

        //Dodjjela random korisnickog imena i pasvorda, dio koda Meho
       /* public void AutomatskiKorisničkiPodaci()
        {
            //lista slova
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            //random generator
            Random rand = new Random();
            string userName = "";
            for (int j = 1; j <= 6; j++)
            {
                //random broj izmedju 0-25
                int ranodmBroj = rand.Next(0, letters.Length - 1);

                //dpdaj 
                userName += letters[ranodmBroj];
            }
            this.username = userName;
            string pass = "";
            for (int j = 1; j <= 12; j++)
            {
                //random broj izmedju 0-25
                int ranodmBroj = rand.Next(0, letters.Length - 1);

                //dpdaj 
                pass += letters[ranodmBroj];
            }
            this.password = password;
        }*/
            #endregion
        }
}
