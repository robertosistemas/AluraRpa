namespace AluraRpa.Domain
{
    public class Result<TValue, TError>
    {
        public TValue Value { get; private set; }

        public TError ErrorValue { get; private set; }

        public bool IsOk { get; private set; }

        private Result(TValue value, TError errorValue, bool success)
        {
            this.Value = value;
            this.ErrorValue = errorValue;
            this.IsOk = success;
        }

        public static Result<TValueResult, TErrorResult> Ok<TValueResult, TErrorResult>(TValueResult valueResult)
        {
            return new Result<TValueResult, TErrorResult>(valueResult, default!, true);
        }

        public static Result<TValueResult, TErrorResult> Err<TValueResult, TErrorResult>(TErrorResult errorResult)
        {
            return new Result<TValueResult, TErrorResult>(default!, errorResult, false);
        }

        public static implicit operator Result<TValue, TError>(TValue value)
        {
            return new Result<TValue, TError>(value, default!, true);
        }

        public static implicit operator Result<TValue, TError>(TError valueError)
        {
            return new Result<TValue, TError>(default!, valueError, false);
        }

        public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> failure)
        {
            if (IsOk)
                return success(this.Value);
            else
                return failure(this.ErrorValue);
        }
    }
}
