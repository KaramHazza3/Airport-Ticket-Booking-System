namespace FTSAirportTicketBookingSystem.Common.Validators;

public class ValidationError
{
    public int RowNumber { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}