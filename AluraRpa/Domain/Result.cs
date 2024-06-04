namespace AluraRpa.Domain
{
    public class Result<TSuccessTypeResult, TErrorTypeResult>
    {
        public TSuccessTypeResult SuccessValue { get; private set; }

        public TErrorTypeResult ErrorValue { get; private set; }

        public bool IsOk { get; private set; }

        private Result(TSuccessTypeResult successValue)
        {
            this.SuccessValue = successValue;
            this.ErrorValue = default!;
            this.IsOk = true;
        }

        private Result(TErrorTypeResult errorValue)
        {
            this.SuccessValue = default!;
            this.ErrorValue = errorValue;
            this.IsOk = false;
        }

        public static Result<TSuccessType, TErrorType> Ok<TSuccessType, TErrorType>(TSuccessType valueResult)
        {
            return new Result<TSuccessType, TErrorType>(valueResult);
        }

        public static Result<TSuccessType, TErrorType> Err<TSuccessType, TErrorType>(TErrorType errorResult)
        {
            return new Result<TSuccessType, TErrorType>(errorResult);
        }

        public static implicit operator Result<TSuccessTypeResult, TErrorTypeResult>(TSuccessTypeResult successValue)
        {
            return new Result<TSuccessTypeResult, TErrorTypeResult>(successValue);
        }

        public static implicit operator Result<TSuccessTypeResult, TErrorTypeResult>(TErrorTypeResult errorValue)
        {
            return new Result<TSuccessTypeResult, TErrorTypeResult>(errorValue);
        }

        public TResult Match<TResult>(Func<TSuccessTypeResult, TResult> successFunc, Func<TErrorTypeResult, TResult> errorFunc)
        {
            if (IsOk)
                return successFunc(this.SuccessValue);
            else
                return errorFunc(this.ErrorValue);
        }
    }
}
