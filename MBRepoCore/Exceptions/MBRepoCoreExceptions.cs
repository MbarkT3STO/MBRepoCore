using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MBRepoCore.Exceptions
{
    public class MBRepoCoreExceptions
    {

       public class NotMathDBContext:Exception
       {
           public NotMathDBContext(Type TContextType, Type RepoContextType) :
               base($"The {TContextType.Name} does not match with {RepoContextType.Name}")
            {
                
            }
        }

    }
}
