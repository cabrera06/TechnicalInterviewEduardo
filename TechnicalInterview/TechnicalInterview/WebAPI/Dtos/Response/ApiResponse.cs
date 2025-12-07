namespace TechnicalInterview.WebAPI.Dtos.Response
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data) => new() { Succeeded = true, Data = data };
        public static ApiResponse<T> Fail(string message) => new() { Succeeded = false, Message = message };
    }
}
