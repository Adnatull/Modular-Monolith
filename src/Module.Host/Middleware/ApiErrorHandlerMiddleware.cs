using Microsoft.AspNetCore.Http;
using Module.Shared.Response;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace Module.Host.Middleware {
    public class ApiErrorHandlerMiddleware {
        private readonly RequestDelegate _next;

        public ApiErrorHandlerMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            }
            catch (Exception error) {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = Response<string>.Fail(message: error?.Message);
                switch (error) {
                    
                    case KeyNotFoundException:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case Exception:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
