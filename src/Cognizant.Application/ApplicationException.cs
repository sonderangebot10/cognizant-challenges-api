using System;

namespace Cognizant.Application
{
    public class ApplicationException : Exception
    {
        internal ApplicationException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
