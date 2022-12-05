using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NoteController : ControllerBase
    {
        private readonly IDbContext _dbContext;

        public NoteController(IDbContext dbContext) =>
            _dbContext = dbContext;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _dbContext.Notes.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _dbContext.Notes.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetRange(int start, int end)
        {
            var data = await _dbContext.Notes.GetRangeAsync(start, end);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Note note)
        {
            var data = await _dbContext.Notes.AddAsync(note);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(Guid id)
        {
            var data = await _dbContext.Notes.CompleteAsync(id);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _dbContext.Notes.DeleteAsync(id);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Note note)
        {
            var data = await _dbContext.Notes.UpdateAsync(note);
            return Ok(data);
        }

    }
}
