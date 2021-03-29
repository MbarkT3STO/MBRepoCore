using MBRepoCore.Repo.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo.Generic
{
    public interface IGenericRepo<TContext>:IBasicRepository,IAdvancedRepository where TContext : DbContext
    {

        
    }
}