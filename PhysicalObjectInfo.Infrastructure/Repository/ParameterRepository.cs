using PhysicalObjectInfo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhysicalObjectInfo.Infrastructure.Repository
{
    public class ParameterRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public ParameterRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //get параметр
        public async Task<List<Parameter>> GetAllAsyncParameter()
        {
            return await _context.Parameters.ToListAsync();
        }
        //get параметры
        public async Task<List<Parameter>> GetByIdAsync(Guid id)
        {
            //var parameters = await _context.Parameters.ToListAsync();
            List<Parameter> targetparameters = await _context.Parameters.ToListAsync();//parameters.RemoveAll(p => p.ObjectId != id);
            targetparameters.RemoveAll(p => p.PhysicalObjectId != id);
            return targetparameters;
        }
        //post
        public async Task AddAsyncParameter(Parameter targetparameter)
        {
            //var targetparameter = new Parameter() { Value = parameter.Value, Dimension = parameter.Dimension, Type = parameter.Type, PollingTime = DateTime.UtcNow, Id = Guid.NewGuid(), PhysicalObjectId = new Guid("ef113185-77e0-470c-9598-fcb1768f18c9"), ObjectId = Guid.Empty };
            _context.Parameters.Add(targetparameter);
            await _context.SaveChangesAsync();
        }
        /*
        public async Task UpdateAsync(PhysicalObject PhObject)//, Parameter parameters)
        {
            var exist = await _context.PhysicalObjects.FindAsync(PhObject.URL);
            _context.Entry(PhObject).CurrentValues.SetValues(PhObject);//parameters);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string URL)
        {
            PhysicalObject PhObject = await _context.PhysicalObjects.FindAsync(URL);
            _context.Remove(PhObject);
            await _context.SaveChangesAsync();
        }
        //get параметры
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
        }*/
    }
}
