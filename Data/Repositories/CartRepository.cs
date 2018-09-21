using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CartRepository : BaseRepository<CartLine>
    {
        public CartRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<CartLine> Includes(IQueryable<CartLine> query)
        {
            return query.Include(l => l.Product);
        }
    }
}
