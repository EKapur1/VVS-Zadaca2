using System;
using System.Collections.Generic;
using System.Text;

namespace Filmoteka
{
    public interface IReziser
    {
        bool DaLiJeReziraoFilm(Film f);
    }
    public class Reziser : IReziser
    {
        public bool DaLiJeReziraoFilm(Film f)
        {
            throw new NotImplementedException();
        }
    }
}
