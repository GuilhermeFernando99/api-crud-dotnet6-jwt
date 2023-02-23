using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Apivscode2.Models;
using Dapper;
using Npgsql;

namespace Apivscode2.Repository
{
    public class FilmesRepository : IFilmesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public FilmesRepository(IConfiguration configuration){
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("Default");
        }
        public async Task<IEnumerable<FilmesResponse>> BuscaFilmesAsync()
        {
            string sql = @"SELECT id, nome, ano, id_produtora FROM public.tb_filmes;";
            using (var con = new NpgsqlConnection(connectionString))
            {
                return await con.QueryAsync<FilmesResponse>(sql);
            }
        }
        public async Task<FilmesResponse> BuscaFilmesAsyncId(int id)
        {
            string sql = @"SELECT id, nome, ano, id_produtora FROM public.tb_filmes where id= @Id";
            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<FilmesResponse>(sql, new { Id = id });
        }
        public async Task<bool> AdicionaAsync(FilmesResquest resquest)
        {
            string sql = @"INSERT INTO public.tb_filmes(id, nome, ano, id_produtora) VALUES ("+ resquest.Id + ",'" + resquest.Nome + "'," + resquest.Ano + "," + resquest.ProdutoraId + ")";
            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, resquest) > 0;
        }

        public async Task<bool> AtualizaFilme(FilmesResquest resquest, int id)
        {
            string sql = @"UPDATE public.tb_filmes SET nome='"+resquest.Nome+"',"+"ano="+resquest.Ano+" WHERE id="+id+"";
            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, resquest) > 0;
        }
        public async Task<bool> DeletarAsync(int id)
        {
            string sql = @"DELETE FROM public.tb_filmes WHERE id="+id+"";
            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql) > 0;
        }
        public async Task<FilmesResponse> Authentic(string name)
        {
            string sql = @"SELECT id, nome, ano, id_produtora FROM public.tb_filmes where nome= @Nome";
            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<FilmesResponse>(sql, new { Nome = name });
        }
    }
}