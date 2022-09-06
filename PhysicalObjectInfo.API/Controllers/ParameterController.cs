using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysicalObjectInfo.Domain;
using PhysicalObjectInfo.Infrastructure;
using PhysicalObjectInfo.Infrastructure.Repository;

namespace PhysicalObjectInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly Context _context;
        private readonly ParameterRepository _ParameterRepository;
        //private readonly PollRepository _PollRepository;
        public ParameterController(Context context)
        {
            _context = context;
            _ParameterRepository = new ParameterRepository(_context);
            //_PollRepository = new PollRepository(_context);
        }
        // GET: api/Parameter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parameter>>> GetParameters()
        {
            //return await _context.Persons.ToListAsync();
            return await _ParameterRepository.GetAllAsyncParameter();
        }
        // GET: api/Parameter/id
        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<Parameter>>> GetParametersById(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var parameters = await _ParameterRepository.GetByIdAsync(id);
            if (parameters == null)
            {
                return NotFound("Не нашел");
            }
            return await _ParameterRepository.GetByIdAsync(id);
        }

        // POST: api/PhysicalObject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parameter>> PostParameter(Parameter parameter)
        {
            //Console.WriteLine(parameter);
            //_context.Persons.Add(person);
            //await _context.SaveChangesAsync();
            await _ParameterRepository.AddAsyncParameter(parameter);
            return CreatedAtAction("GetParameter", new { Id = parameter.Id }, parameter);
        }
        // DELETE: api/PhysicalObject/url
        /*[HttpDelete("{}")]
        public async Task<IActionResult> DeletePhysicalObject(string URL)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _PhysicalObjectRepository.GetByURLAsync(URL);
            if (person == null)
            {
                return NotFound();
            }

            //_context.Persons.Remove(person);
            //await _context.SaveChangesAsync();
            await _PhysicalObjectRepository.DeleteAsync(URL);

            return NoContent();
        }
        */
    }
}
