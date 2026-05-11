namespace HealthCare.Application.Models
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResponseModel<T> Ok(T data, string message = "") => new ResponseModel<T> { Success = true, Data = data, Message = message };
        public static ResponseModel<T> Fail(string message) => new ResponseModel<T> { Success = false, Message = message, Data = default };
    }
}
