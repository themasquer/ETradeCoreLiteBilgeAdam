﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=ETradeCoreLite;user id=sa;password=sa;multipleactiveresultsets=true");
            return new Db(optionsBuilder.Options);
        }
    }
}
