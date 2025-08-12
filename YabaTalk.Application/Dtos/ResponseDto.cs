using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YabaTalk.Application.Dtos
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public static ResponseDto<T> Ok(T data, string? message = null, int statusCode = 200)
        {
            return new ResponseDto<T>
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }
        public static ResponseDto<T> Fail(string message, int statusCode = 400)
        {
            return new ResponseDto<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Data = default
            };
        }
    }

    public class ResponseDto
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Data { get; set; }
        public static ResponseDto Ok(string? message = null, int statusCode = 200)
        {
            return new ResponseDto
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = null
            };
        }
        public static ResponseDto Fail(string message, int statusCode = 400)
        {
            return new ResponseDto
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Data = default
            };
        }
    }
}
