using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Shared.Response
{
    public class ErrorList<T>
    {
        public static Error Duplicates(string name) => new Error($"Duplicates {typeof(T)}", StatusCodes.Status400BadRequest, $"Duplicates of Records for {typeof(T)} of name {name}");
        public static Error NotFound(int Id) => new Error($"NotFound {typeof(T)}", StatusCodes.Status404NotFound, $"{typeof(T)} of id {Id} not found");
        public static Error Missing(List<string> missingflds) => new Error($"Invalid Data {typeof(T)}", StatusCodes.Status406NotAcceptable, $"The following are missing {missingflds}");
        public static Error CreationFailed(string name) => new Error($"CreationFailed {typeof(T)}", StatusCodes.Status406NotAcceptable, $"Failed to create product {name}");
        public static Error UpdateFailed(string name) => new Error($"UpdateFailed {typeof(T)}", StatusCodes.Status400BadRequest, $"Failed to update {typeof(T)} of name {name}");
        public static Error DeleteFailed(int id) => new Error($"DeleteFailed {typeof(T)}", StatusCodes.Status304NotModified, $"{typeof(T)} with ID {id} failed to delete");
    }
}
