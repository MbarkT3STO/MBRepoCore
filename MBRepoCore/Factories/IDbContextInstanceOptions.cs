﻿using MBRepoCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Factories
{
    public interface IDbContextInstanceOptions
    { 
        DbContextOptionsBuilder OptionsBuilder { get; set; }
        string ConnectionString { get; set; }
        RdbmsProvider RdbmsProvider { get; set; }
    }
}
