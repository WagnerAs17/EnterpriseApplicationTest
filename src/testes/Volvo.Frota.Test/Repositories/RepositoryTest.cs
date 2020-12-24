using Microsoft.EntityFrameworkCore;
using System;
using Volvo.Frota.API.Data;
using Volvo.Frota.API.Data.Repositories;

namespace Volvo.Frota.Test.Repositories
{
    public abstract class RepositoryTest
    {
        private VolvoContext _context { get; set; }
        public Entities Entities { get; set; }

        public RepositoryTest()
        {
            this._context = CreateContext();
            this.Entities = new Entities(this._context);
        }

        private VolvoContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<VolvoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new VolvoContext(options);
        }

    }
}
