using MBRepoCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    public class DbContextInstanceOptions:IDbContextInstanceOptions
    {
        public DbContextOptionsBuilder OptionsBuilder   { get; set; }
        public string                  ConnectionString { get; set; }
        public RdbmsProvider           RdbmsProvider    { get; set; }
    }
}
