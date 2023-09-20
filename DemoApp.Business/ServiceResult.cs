namespace DemoApp.Business;

public class ServiceResult
{
    public ServiceResult(params string[] messages)
        : this(ResultState.Success, messages)
    {
    }

    public ServiceResult(ResultState resultState, params string[] messages)
    {
        ResultState = resultState;
        Messages = messages;
    }

    public ResultState ResultState { get; }

    public ICollection<string> Messages { get; }
}

public class ServiceResult<T> : ServiceResult
{
    public ServiceResult(T data, params string[] messages)
        : this(data, ResultState.Success, messages)
    {
        
    }

    public ServiceResult(T data, ResultState resultState, params string[] messages) 
        : base(resultState, messages)
    {
        Data = data;
    }

    public T Data { get; }
}