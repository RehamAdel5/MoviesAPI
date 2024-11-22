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
    public class GenrasController : ControllerBase
    {
        private readonly IGenrasService genrasService;

        public GenrasController(IGenrasService genrasService)
        {
            this.genrasService = genrasService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genras = await genrasService.GetAll();
            return Ok(genras);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenrasDTO dto)
        {
            var genra = new Genra { Name = dto.Name };
            await genrasService.Add(genra);
            
            return Ok(genra);


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] GenrasDTO dto)
        {
            var genre = await genrasService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            genre.Name = dto.Name;

            genrasService.Update(genre);
          
            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await genrasService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");



            genrasService.Delete(genre);
           

            return Ok(genre);
        }
    }
}
