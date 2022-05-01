namespace API.Errors
{
    public class ApiResponse
    {

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageFromStatusCode(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageFromStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request, You have made",
                401 => "Authorize, You are not",
                404 => "Resource forund, it wasn't",
                500 => "Errors are path to the dark side",
                _ => null
            };
        }

    }
}
