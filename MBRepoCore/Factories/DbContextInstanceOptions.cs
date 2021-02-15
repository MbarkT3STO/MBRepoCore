using System;
using System.Collections.Generic;
using System.Text;
using MBRepoCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    public class DbContextInstanceOptions:IDbContextInstanceOptions
    {
        public DbContextOptionsBuilder OptionsBuilder   { get; set; }
        public string                  connectionString { get; set; }
        public RdbmsProvider           RdbmsProvider    { get; set; }
    }
}
