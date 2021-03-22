using MBRepoCore.Repo.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo.Generic
{
    interface IGenericRepo<TContext>:IBasicRepository,IAdvancedRepository where TContext : DbContext
    {

        
    }
}