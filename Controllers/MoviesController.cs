using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
       
        private new List<string> _AllowedExtention = new List<string> { ".jpg", ".png" };
        private long _MaxAllowedPosterSize = 1048576;
        private readonly IMoviesService _moviesService;
        private readonly IGenrasService genrasService;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesService moviesService ,IGenrasService genrasService,IMapper mapper)
        {
            _moviesService = moviesService;
            this.genrasService = genrasService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllasync()
        {
            var movie = await _moviesService.GetAll();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movie);

            //TODo Map Movie to DTO
            return Ok(data);


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            //if (movie == null)
            //    return NotFound();
            //var dto = new MovieDetailsDto {
            //    Id = movie.Id,
            //    GenraName = movie.Genra.Name,
            //    GenreId = movie.Genra.Id,
            //    Poster = movie.Poster,
            //    Rate = movie.Rate,
            //    Storeline = movie.Storeline,
            //    Title = movie.Title,
            //    Year = movie.Year

            //}; 
           
                return Ok(movie);



        }
        [HttpGet("GetGenraById")]
        public async Task<IActionResult> GetGenraByIdAsync(byte Genraid)
        {
            var movie = await _moviesService.GetAll(Genraid);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movie);

            return Ok(data);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto Dto)
        {
            if (Dto.Poster == null)
                return BadRequest("poster is required");
            if (!_AllowedExtention.Contains(Path.GetExtension(Dto.Poster.FileName).ToLower()))
                return BadRequest("only .png and.jpg are allowed");
            if (Dto.Poster.Length > _MaxAllowedPosterSize)

                return BadRequest("max allowed size for poster is 1MB!");

            var isvalidGenra = await genrasService.IsvalidGenre( Dto.GenraId);


            if (!isvalidGenra)
            {


                return BadRequest("Invalid Genra ID!");

            }
            using var datastream = new MemoryStream();
            await Dto.Poster.CopyToAsync(datastream);
            var movie = _mapper.Map<Movie>(Dto);
            movie.Poster = datastream.ToArray();
            await _moviesService.Add(movie);
            return Ok(movie);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            var isvalidGenra = await genrasService.IsvalidGenre( dto.GenraId);

            if (dto.Poster != null)
            {
                if (!_AllowedExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _MaxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.GenraId = dto.GenraId;
            movie.Year = dto.Year;
            movie.Storeline = dto.Storeline;
            movie.Rate = dto.Rate;

           _moviesService.Update(movie);
         
            return Ok(movie);
        }

       

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById( id);

            if (movie == null)
                return NotFound($"No movie was found with ID: {id}");



            _moviesService.Delete(movie);
           
            return Ok(movie);
        }
    }

}
