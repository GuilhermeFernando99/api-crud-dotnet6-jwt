using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apivscode2.Models;

namespace Apivscode2.Repository
{
    public interface IFilmesRepository
    {
        Task<IEnumerable<FilmesResponse>> BuscaFilmesAsync();
        Task<FilmesResponse> BuscaFilmesAsyncId(int id);
        Task<bool> AdicionaAsync(FilmesResquest resquest);
        Task<bool> AtualizaFilme(FilmesResquest resquest, int id);
        Task<bool> DeletarAsync(int id);
        Task<FilmesResponse> Authentic(string name);
    }
}