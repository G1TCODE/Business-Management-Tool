using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BMT.Domain.Abstractions
{
    public class Result
    {
        protected Result(bool successful, Error error)
        {
            if (ResultSuccessful && Error != Error.NoError)
            {
                throw new InvalidOperationException();
            }

            if (ResultSuccessful &&  Error == Error.NoError)
            {
                throw new InvalidOperationException();
            }

            ResultSuccessful = successful;
            Error = error;
        }

        public bool ResultSuccessful { get; }

        public bool ResultFailed => !ResultSuccessful;

        public Error Error { get; }

        public static Result ActionSuccessful() => new(true, Error.NoError);

        public static Result ActionFailed(Error error) => new(false, error);

        public static Result<T> Success<T>(T value) => new (value, true, Error.NoError);

        public static Result<T> Failure<T>(Error error) => new (default, false, error);

        public static Result<T> Create<T>(T value) =>
            value is not null ? Success(value) : Failure<T>(Error.Null);
    }

    public class Result<T> : Result
    {
        private readonly T? _tValue;

        protected internal Result(T? value, bool successful, Error error) : base(successful, error)
        {
            _tValue = value;
        }

        [NotNull]
        public T Value => ResultSuccessful
            ? _tValue! 
            : throw new InvalidOperationException("No value for failure result is availabe.");

        public static implicit operator Result<T>(T? value) => Create(value);
    }
}
