using PhysicalObjectInfo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhysicalObjectInfo.Infrastructure.Repository
{
    public class PhysicalObjectRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public PhysicalObjectRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //get физические объекты
        public async Task<List<PhysicalObject>> GetAllAsyncPhObject()
        {
            return await _context.PhysicalObjects.OrderBy(p => p.Series).ToListAsync();
        }
        //get по id
        public async Task<PhysicalObject> GetByIdAsync(Guid id)
        {
            return await _context.PhysicalObjects.FindAsync(id);
        }
        //get по url
        public async Task<PhysicalObject> GetByURLAsync(string url)
        {
            return await _context.PhysicalObjects.FindAsync(url);
        }
        //post
        public async Task AddAsyncPhObject(PhysicalObject PhObject)
        {
            _context.PhysicalObjects.Add(PhObject);
            await _context.SaveChangesAsync();
        }
        //put
        public async Task UpdateAsync(PhysicalObject physicalobject)//, Parameter parameters)
        {
            var exist = await _context.PhysicalObjects.FindAsync(physicalobject.Id);
            _context.Entry(physicalobject).CurrentValues.SetValues(physicalobject);//parameters);
            await _context.SaveChangesAsync();
        }
        //delete
        public async Task DeleteAsync(Guid id)
        {
            PhysicalObject PhObject = await _context.PhysicalObjects.FindAsync(id);
            _context.Remove(PhObject);
            await _context.SaveChangesAsync();
        }
        //get параметры
        /*
        public async Task<List<Parameter>> GetAllAsyncParameters()
        {
            return await _context.Parameters.OrderBy(p => p.PollingTime).ToListAsync();
        }
        public async Task<List<Parameter>> GetByTypeAsyncParameter(string Type)
        {
            return await _context.Parameters.OrderBy(p => p.Type).ToListAsync();
        }
        private bool URLExists(string url)
        {
            return _context.PhysicalObjects.Any(e => e.URL == url);
        }
        */
    }
}
