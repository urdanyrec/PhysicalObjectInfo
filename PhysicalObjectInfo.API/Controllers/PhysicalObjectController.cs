using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysicalObjectInfo.API.Service;
using PhysicalObjectInfo.Domain;
using PhysicalObjectInfo.Infrastructure;
using PhysicalObjectInfo.Infrastructure.Repository;

namespace PhysicalObjectInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalObjectController : ControllerBase
    {
        private readonly Context _context;
        private readonly PhysicalObjectRepository _PhysicalObjectRepository;
        private readonly ILogger<PhysicalObjectController> _logger;
        private readonly IpollService _singletonService1;
        //private readonly PollRepository _PollRepository;
        /*
        public PhysicalObjectController(Context context)
        {
            _context = context;
            _PhysicalObjectRepository = new PhysicalObjectRepository(_context);
            //_PollRepository = new PollRepository(_context);
        }*/
        public PhysicalObjectController(Context context, ILogger<PhysicalObjectController> logger, IpollService singletonService1)
        {
            _context = context;
            _PhysicalObjectRepository = new PhysicalObjectRepository(_context);
            _logger = logger;
            _singletonService1 = singletonService1;
            //_PollRepository = new PollRepository(_context);
        }
        // GET: api/PhysicalObject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalObject>>> GetPhysicalObjects()
        {
            _singletonService1.PollObjects();
            //return await _context.Persons.ToListAsync();
            return await _PhysicalObjectRepository.GetAllAsyncPhObject();
            
        }
        /*
        // GET: api/PhysicalObject/url
        [HttpGet("{URL}")]
        public async Task<ActionResult<PhysicalObject>> GetPhysicalObjectByUrl(string url)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _PhysicalObjectRepository.GetByURLAsync(url);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }
        */
        
        // GET: api/PhysicalObject/id
        [HttpGet("{Id}")]
        public async Task<ActionResult<PhysicalObject>> GetPhysicalObjectById(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var phobject = await _PhysicalObjectRepository.GetByIdAsync(id);
            if (phobject == null)
            {
                return NotFound();
            }
            return phobject;
        }
        
        // PUT: api/PhysicalObject/url
       /*
        [HttpPut("{URL}")]
        public async Task<IActionResult> PutPhysicalObject(string url, List<Parameter> parameters)//, PhysicalObject PhObject)//, Parameter parameters)
        {
            /*if (url != PhObject.URL)
            {
                return BadRequest("Не нашел");
            }
            var PhObjectToUpdate = await _PhysicalObjectRepository.GetByURLAsync(url);
            //var PhObjectToUpdate = new PhysicalObject() { URL = url, Parameters = parameters };//await _PhysicalObjectRepository.GetByURLAsync(url);
            if (PhObjectToUpdate == null) 
                return NotFound("Не нашел url");
            PhObjectToUpdate.Parameters = parameters;
            await _PhysicalObjectRepository.UpdateAsync(PhObjectToUpdate); //, parameters);

            return NoContent();
        }
        */

        public async Task<IActionResult> PutPhysicalObject(Guid id, PhysicalObject physicalobject)
        {
            if (id != physicalobject.Id)
            {
                return BadRequest();
            }
            await _PhysicalObjectRepository.UpdateAsync(physicalobject);
            return NoContent();
        }
        // POST: api/PhysicalObject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhysicalObject>> PostPhysicalObject(PhysicalObject PhObject)
        {
            //_context.Persons.Add(person);
            //await _context.SaveChangesAsync();
            await _PhysicalObjectRepository.AddAsyncPhObject(PhObject);
            return CreatedAtAction("GetPhysicalObject", new { URL = PhObject.URL }, PhObject);
        }
        // DELETE: api/PhysicalObject/url
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePhysicalObject(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var phobject = await _PhysicalObjectRepository.GetByIdAsync(id);
            if (phobject == null)
            {
                return NotFound();
            }
            await _PhysicalObjectRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
