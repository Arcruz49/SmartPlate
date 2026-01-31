namespace SmartPlate.Application.DTOs;

public class RetornoGenerico<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } = default!;
    
}