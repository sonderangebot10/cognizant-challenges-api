using System;
using System.Collections.Generic;
using System.Text;

namespace Cognizant.Infrastructure.Services.Rextester
{
    public class Context
    {
        public string Uri { get; }

        public Context(string uri) : base()
        {
            Uri = uri;
        }
    }
}
