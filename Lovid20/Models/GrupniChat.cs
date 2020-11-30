using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lovid20.Models
{
    public class GrupniChat
    {
        public List<RegistrovaniKorisnik> clanovi { get; set; }
        public List<Poruka> poruke { get; set; }
        /* Sumnjivi dio koda, potencijalna greska varijabla koja se najvjerovatnije ne koristi*/
        public GrupniChat(List<RegistrovaniKorisnik> participants, List<Poruka> mssgs)

        /* Kraj sumnjivog dijela koda*/
        {
            mssgs = null;
            clanovi = participants; poruke = mssgs;
        }
        public void dodajPoruku(Poruka poruka)
        {
            throw new NotImplementedException();
        }
    }
}
