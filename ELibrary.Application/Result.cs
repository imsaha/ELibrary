using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Application
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }

        public static Result Ok() => new Result() { IsSuccess = true };
        public static Result Failed() => new Result() { IsSuccess = false };
        public static Result<T> Ok<T>(T data = default) => new Result<T>(data, true);
        public static Result<T> Failed<T>(T data = default) => new Result<T>(data, false);

    }

    public class ListResult : Result
    {
        public static ListResult<T> Ok<T>(IEnumerable<T> data = default) => new ListResult<T>(data, true);
        public static ListResult<T> Failed<T>(IEnumerable<T> data = default) => new ListResult<T>(data, false);
    }


    public sealed class Result<T> : Result
    {
        public Result(T data)
        {
            Data = data;
        }

        public Result(T data, bool isSuccess) : this(data)
        {
            Data = data;
            IsSuccess = isSuccess;
        }


        public T Data { get; }
    }




    public sealed class ListResult<T> : ListResult
    {
        public ListResult(IEnumerable<T> data)
        {
            Data = data;
        }

        public ListResult(IEnumerable<T> data, bool isSuccess) : this(data)
        {
            Data = data;
            IsSuccess = isSuccess;
        }

        public IEnumerable<T> Data { get; }
    }
}
