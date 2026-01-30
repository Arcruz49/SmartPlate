namespace SmartPlate.Application.DTOs;

public class RetornoGenerico<T>
{
    public bool success { get; set; }
    public string message { get; set; } = string.Empty;
    public T data { get; set; } = default!;
    
}