using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MBRepoCore.Exceptions
{
    public class MBRepoCoreExceptions
    {

       public class NotMatchDBContext:Exception
       {
           public NotMatchDBContext(Type contextType, Type repoContextType) :
               base($"The {contextType.Name} does not match with {repoContextType.Name}")
            {
                
            }
        }

    }
}
