using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Entities;
using MoviesBackend.Services;
using MoviesBackend.Interfaces;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _service;
    public MoviesController(IMoviesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _service.GetAllAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movies = await _service.GetByIdAsync(id.Value);

        if (movies == null)
        {
            return NotFound();
        }

        return Ok(movies);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie movies)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(movies);
            return Ok(movies);
        }
        return BadRequest(movies);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int? id, Movie movies)
    {
        if (id != movies.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _service.UpdateAsync(movies);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(movies.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        return BadRequest(movies);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        await _service.DeleteAsync(id.Value);
        return Ok();
    }

    private bool MoviesExists(int? id)
    {
        return _service.IsExisting(id.Value);
    }
}