using System;
using System.Collections.Generic;
using System.Text;

namespace Cognizant.Domain
{
    public class DomainException : Exception
    {
        internal DomainException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}
