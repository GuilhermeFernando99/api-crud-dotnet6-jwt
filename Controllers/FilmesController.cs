using Apivscode2.Models;
using Apivscode2.Repository;
using Apivscode2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apivscode2.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private readonly IFilmesRepository _repository;
    public FilmesController(IFilmesRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var filmes = await _repository.BuscaFilmesAsync();
        return filmes.Any() ? Ok(filmes) : NoContent();
    }
    [HttpGet("id")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var filmes = await _repository.BuscaFilmesAsyncId(id);
        return filmes != null ? Ok(filmes) : NotFound("Filme não encontrado");
    }
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Authentic(FilmesResponse nome)
    {
        var user = await _repository.Authentic(nome.Nome);
        var token = TokenServices.GenerateToken(nome);
        object[] itens = { user, token };
        return user != null ? Ok(itens) : NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Post(FilmesResquest resquest)
    {
        if(string.IsNullOrEmpty(resquest.Nome) || resquest.Ano <= 0 || resquest.ProdutoraId <= 0)
        {
            return BadRequest("Informações inválidas");
        }
        var isAdd = await _repository.AdicionaAsync(resquest);
        return isAdd 
            ? Ok("Filme adicionado com sucesso!")
            : BadRequest("Erro ao adicionar filme"); 
    }
    [HttpPut("id")]
    public async Task<IActionResult> Put(FilmesResquest resquest, int id)
    {
        if(id <= 0) return BadRequest("Filme Invalido");
        var filmes = await _repository.BuscaFilmesAsyncId(id);
        if(filmes == null) NotFound("Filme não existe na base de dados");
        if(string.IsNullOrEmpty(resquest.Nome)) resquest.Nome = filmes.Nome;
        if(resquest.Ano <= 0) resquest.Ano = filmes.Ano;
        var update = await _repository.AtualizaFilme(resquest, id);
         return update 
            ? Ok("Filme atualizado com sucesso!")
            : BadRequest("Erro ao atualiza filme"); 
    }
    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id) 
    {
        if(id <= 0) return BadRequest("Filme não encontrado");
        var deleted = await _repository.DeletarAsync(id);
        return deleted 
            ? Ok("Filme deletado com sucesso!")
            : BadRequest("Erro ao deletar filme"); 
    }
}
