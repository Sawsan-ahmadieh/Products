using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Shared.Response;

public class Result
{
    public bool Succeeded { get; set; }
    public int Status { get; set; }
    public Error? error { get; set; }
    public Object? Data { get; set; }
    public Result(bool suc, int st, Error? err, object? dt)
    {
        Succeeded = suc;
        Status = st;
        error = err;
        Data = dt;
    }
}
