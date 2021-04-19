using System;

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
