using System;
using System.Collections.Generic;
using System.Text;

namespace MBRepoCore.Exceptions
{
    class MBRepoCoreExceptions
    {

        class NotMathDBContext<TContext_A, TContextB>:Exception
        {
            public NotMathDBContext():base($"The {typeof(TContext_A).Name} does not match with {typeof(TContextB).Name}") { }
        }

    }
}
