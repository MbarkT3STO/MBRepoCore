using MBRepoCore.Repo.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MBRepoCore.Repo.Generic
{
    public interface IGenericRepo<TContext>:IBasicRepository,IAdvancedRepository,IBasicRepositoryAsync,IAdvancedRepositoryAsync where TContext : DbContext
    {

        
    }
}