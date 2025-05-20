namespace MediatRBlogAPI.Application.Base;

public class ResponseDto<T>
{
	public string message { get; set; }
	public T? data { get; set; }
	public bool is_success { get; set; }
	public long response_code { get; set; }
}

