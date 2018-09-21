using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

    }
}
