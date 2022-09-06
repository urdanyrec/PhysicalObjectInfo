using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysicalObjectInfo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PhysicalObjectInfo.Infrastructure.Repository;
using PhysicalObjectInfo;


namespace TestProject
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "PhysicalObjectInfoDb");

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public PhysicalObjectRepository physicalobjectRepository
        {
            get
            {
                return new PhysicalObjectRepository(_context);
            }
        }
    }
}
