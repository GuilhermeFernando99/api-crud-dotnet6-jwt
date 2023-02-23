using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apivscode2.Models
{
    public class FilmesResquest
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public int Ano {get; set;}
        public int ProdutoraId {get; set;}
    }
}