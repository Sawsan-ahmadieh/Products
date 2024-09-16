using Products.Shared.Response;

namespace Products.backend.ExceptionHdl
{
    public static class MapErrors
    {
        public static Error MapException(Exception exception)
        {
            return exception switch
            {
                ArgumentOutOfRangeException => new Error ("OutOfRange",StatusCodes.Status400BadRequest,"Out of Range" ),
                _ => new Error ("Internal Error", StatusCodes.Status500InternalServerError, "Internal Server error" )

            };
        }
    }
}
