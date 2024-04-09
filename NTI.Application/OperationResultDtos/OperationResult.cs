using System.Net;

namespace NTI.Application.OperationResultDtos;

public class OperationResult
{
    #region Constructors
    public OperationResult()
    {

    }

    public OperationResult AddError(string message)
    {
        Errors.Add(message);
        return this;
    }

    #endregion

    public int Code { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool Succeeded { get; set; }
    public bool IsSuccessfulWithNoErrors { get => Succeeded && !HasErrors; }
    public List<string> Errors { get; set; } = new List<string>();
    public bool HasErrors { get { return Errors.Any(); } }

    #region Methods
    public OperationResult AddErrors(ICollection<string> messages)
       => messages?.Any() == true ? AddErrors(messages.ToArray()) : this;

    public OperationResult AddErrors(OperationResult OperationResult)
        => OperationResult?.Errors?.Any() == true ? AddErrors(OperationResult.Errors) : this;

    public OperationResult AddErrors(params string[] messages)
    {
        if (Errors == null) Errors = new List<string>();
        if (messages != null) Errors.AddRange(messages.Where(c => !string.IsNullOrEmpty(c)));
        return this;
    }

    public OperationResult SetCode(int code)
    {
        Code = code;
        return this;
    }

    public OperationResult SetStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        return this;
    }

    public OperationResult SetFrom(OperationResult result)
    {
        Code = result.Code;
        Errors = Errors ?? new List<string>();
        if (result.Errors?.Any() == true)
            Errors.AddRange(result.Errors);

        return this;
    }

    public static OperationResult Success()
    {
        var result = new OperationResult();
        result.Succeeded = true;
        result.Code = (int)HttpStatusCode.OK;
        result.StatusCode = HttpStatusCode.OK;
        return result;
    }

    public static OperationResult Failed(ICollection<string> errors) => Failed(errors.ToArray());

    public static OperationResult Failed(params string[] errors)
    {
        var result = new OperationResult();
        result.Succeeded = false;
        result.AddErrors(errors);
        result.Code = (int)HttpStatusCode.BadRequest;
        result.StatusCode = HttpStatusCode.BadRequest;
        return result;
    }

    #endregion
}

public class OperationResult<T> : OperationResult
{
    protected OperationResult() { }

    public T Payload { get; set; }


    #region Methods

    public new static OperationResult<T> Failed(ICollection<string> errors) => Failed(errors.ToArray());

    public new static OperationResult<T> Failed(params string[] errors)
    {
        var result = new OperationResult<T>();
        result.Succeeded = false;
        if (errors?.Any() == true) result.AddErrors(errors);
        return result;
    }

    public static OperationResult<T> Success(T payload)
    {
        var result = new OperationResult<T>();
        result.Succeeded = true;
        result.Payload = payload;
        result.Code = (int)HttpStatusCode.OK;
        result.StatusCode = HttpStatusCode.OK;
        return result;
    }

    public new static OperationResult<T> Success()
    {
        var result = new OperationResult<T>();
        result.Succeeded = true;
        result.Code = (int)HttpStatusCode.OK;
        result.StatusCode = HttpStatusCode.OK;
        return result;
    }

    public new OperationResult<T> AddError(string message)
    {
        base.AddError(message);
        return this;
    }

    public new OperationResult<T> AddErrors(ICollection<string> messages) => AddErrors(messages.ToArray());

    public new OperationResult<T> AddErrors(params string[] messages)
    {
        base.AddErrors(messages);
        return this;
    }

    public new OperationResult<T> AddErrors(OperationResult OperationResult) => AddErrors(OperationResult.Errors);

    public new OperationResult<T> SetCode(int code)
    {
        Code = code;
        return this;
    }

    public new OperationResult<T> SetStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        return this;
    }

    public OperationResult<T> SetFrom<TResult>(TResult result) where TResult : OperationResult
    {
        Code = result.Code;

        Errors = Errors ?? new List<string>();
        if (result.Errors?.Any() == true)
            Errors.AddRange(result.Errors);

        return this;
    }

    public OperationResult<T> SetSucceeded(T payload)
    {
        Succeeded = true;
        Payload = payload;
        return this;
    }
    #endregion
}

