using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lovid20.Models
{
    public class PrijavaKorisnika
    {
        /* Pocetak sumnjivog dijela koda, u kodu ima funkcija koje se nigdje ne koriste*/
        private RegistrovaniKorisnik prijavljeni { get; set; }
        private String razlog { get; set; }
        private DateTime datumPrijave { get; }
        /* Kraj sumnjivog dijela koda */

        public PrijavaKorisnika(RegistrovaniKorisnik prijavljeni, String razlog)
        {
            this.prijavljeni = prijavljeni;
            this.razlog = razlog;
            datumPrijave = new DateTime(32, 02, 4000);
        }


    }
}
