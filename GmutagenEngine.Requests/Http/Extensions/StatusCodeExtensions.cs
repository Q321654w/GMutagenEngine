namespace GmutagenEngine.Requests.Http.Extensions
{
    public static class StatusCodeExtensions
    {
        public static bool IsSuccess(this int statusCode)
            => statusCode >= 200 && statusCode < 300;

        public static bool IsRedirect(this int statusCode)
            => statusCode >= 300 && statusCode < 400;

        public static bool IsClientError(this int statusCode)
            => statusCode >= 400 && statusCode < 500;

        public static bool IsServerError(this int statusCode)
            => statusCode >= 500 && statusCode < 600;

        public static bool IsError(this int statusCode)
            => statusCode >= 400 && statusCode < 600;

        public static string GetStatusCategory(this int statusCode)
        {
            if (statusCode.IsSuccess()) return "Success";
            if (statusCode.IsRedirect()) return "Redirect";
            if (statusCode.IsClientError()) return "ClientError";
            if (statusCode.IsServerError()) return "ServerError";
            return "Unknown";
        }

        public static string GetDefaultStatusMessage(this int statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                201 => "Created",
                202 => "Accepted",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                422 => "Unprocessable Entity",
                500 => "Internal Server Error",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                _ => "Unknown Status"
            };
        }

        public static bool ShouldHaveBody(this int statusCode)
        {
            return statusCode != 204 && statusCode != 304;
        }

        public static bool CanCache(this int statusCode)
        {
            return statusCode == 200 || statusCode == 203 || statusCode == 204 ||
                   statusCode == 206 || statusCode == 300 || statusCode == 301 ||
                   statusCode == 308 || statusCode == 404 || statusCode == 405 ||
                   statusCode == 410 || statusCode == 414 || statusCode == 501;
        }
    }
}