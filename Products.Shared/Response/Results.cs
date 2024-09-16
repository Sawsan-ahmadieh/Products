using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Products.Shared.Response
{
    public static class Response
    {
        public static Result Success(object obj) => new Result(true, StatusCodes.Status200OK, null, obj);
        public static Result Failed(Error erro, int status) => new Result(false, erro.status, erro, null);
    }
}
