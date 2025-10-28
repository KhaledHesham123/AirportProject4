namespace AirportProject4.Shared
{
    public class EndpointRespone<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public int StatusCode { get; set; } = 200;
        public EndpointRespone()
        {
        }
        public EndpointRespone(T data)
        {
            Data = data;
        }
        public EndpointRespone(string message, bool isSuccess = false, int statusCode = 400)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public EndpointRespone(T? data, bool isSuccess, string? message, int statusCode)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public static EndpointRespone<T> Success(T data, string message="",int statusCode = 200)
       => new EndpointRespone<T>(data, true, message, statusCode);
        public static EndpointRespone<T> Fail(string message, int statusCode = 400)
            => new EndpointRespone<T>(message, false, statusCode); 
    }
}
