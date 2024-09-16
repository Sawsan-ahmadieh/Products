using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Shared.Response
{
    public record Error(string title,int status, string Message)
    {
    }
}
