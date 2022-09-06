using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysicalObjectInfo.Domain;
using Microsoft.EntityFrameworkCore;

namespace PhysicalObjectInfo.Infrastructure.Repository
{
    public class SampleValueRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public SampleValueRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<SampleValue>> GetAllAsyncSampleValue()
        {
            return await _context.SampleValues.OrderBy(p => p.PollingTime).ToListAsync();
        }
        public async Task<List<SampleValue>> GetByTypeAsyncParameter(Guid ObjectId)
        {
            return await _context.SampleValues.OrderBy(p => p.ObjectId).ToListAsync();
        }
    }
}
