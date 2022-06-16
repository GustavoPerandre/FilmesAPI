using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Model;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context; //utiliza a classe FilmeContext para manipular os dados na base de dados
        private IMapper _mapper; //utiliza o modelo de automapper
        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost] //Indica que estou recebendo os dados via POST
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) //[FromBody] indica que os dados virão no corpo da requisição
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmeById), new { Id = filme.Id }, filme); //Além do status Created (201), mostra onde o recurso foi criado (no Headers>Location da chamada)
        }

        [HttpGet] //Indica que estou recebendo uma requisição via GET
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.filmes;
        }

        [HttpGet("{id}")] //Indica que estou recebendo uma requisição via GET com um parâmetro chamado id, com o mesmo nome do parâmetro do método (int id)
        public IActionResult RecuperaFilmeById(int id)
        {
            Filme filme = _context.filmes.FirstOrDefault(filme => filme.Id == id); //Busca na lista de filmes e retorna o filme com o mesmo id fornecido no Get. Se não encontrar nada, retorna null
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto); //utiliza o Ok da interface IActionResult
            }
            else
                return NotFound(); //utiliza o NotFound da interface IActionResult
        }

        [HttpPut("{id}")] //indica que estou recebendo uma requisiçao de atualização via PUT
        public IActionResult AtualizaFilmeById(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
                return NotFound();

            _mapper.Map(filmeDto, filme); //subrescreve filme usando as informações de filmeDto
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")] //Indica que estou recebendo uma requisição de exclusão via DELETE
        public IActionResult DeletaFilmeById(int id)
        {
            Filme filme = _context.filmes.FirstOrDefault(filme => filme.Id == id);
            
            if (filme == null)
                return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
