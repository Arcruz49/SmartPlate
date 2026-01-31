namespace SmartPlate.Application.DTOs;

public class Retorno
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    
    public Retorno(bool success, string message)
    {
        this.Success = success;
        this.Message = message;
    }

    public Retorno(){}
}