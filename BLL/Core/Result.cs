using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Errors { get; set; }
        public static Result<T> Success(T value)
        {
            return new Result<T> { Value = value, IsSuccess = true };
        }
        public static Result<T> Failure(string error)
        {
            return new Result<T> { Errors = error, IsSuccess = false };
        }
    }
}
