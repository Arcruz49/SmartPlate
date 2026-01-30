namespace SmartPlate.Application.DTOs;

public class Retorno
{
    public bool success { get; set; }
    public string message { get; set; } = string.Empty;
    
    public Retorno(bool success, string message)
    {
        this.success = success;
        this.message = message;
    }

    public Retorno(){}
}