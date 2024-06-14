using Convey.WebApi.Exceptions;
using Convey;
using System;
using System.Collections.Concurrent;
using System.Net;


namespace Tours.Infrastructure.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new ConcurrentDictionary<Type, string>();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                _ => new ExceptionResponse(new { code = "error", reason = "There was an error." },
                    HttpStatusCode.BadRequest)
            };

        private static string GetCode(Exception exception)
        {
            var type = exception.GetType();
            if (Codes.TryGetValue(type, out var code))
            {
                return code;
            }

            var exceptionCode = exception switch
            {
                _ => exception.GetType().Name.Underscore().Replace("_exception", string.Empty)
            };

            Codes.TryAdd(type, exceptionCode);

            return exceptionCode;
        }
    }
}