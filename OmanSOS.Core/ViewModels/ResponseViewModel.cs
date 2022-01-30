using System.Net;

namespace OmanSOS.Core.ViewModels;

public class ResponseViewModel<T>
{
    public ResponseViewModel()
    {
        StatusCode = HttpStatusCode.OK;
        Message = "Operation done successfully";
        Time = DateTime.Now;
    }

    // Main Properties
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorStackTrace { get; set; }
    public T? Data { get; set; }
    public DateTime Time { get; set; }
}
