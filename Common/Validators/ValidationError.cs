namespace FTSAirportTicketBookingSystem.Common.Validators;

public class ValidationError
{
    public int RowNumber { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    
    public ValidationError() { } // required for deserialization or model binding

    public ValidationError(int rowNumber, string propertyName, string errorMessage)
    {
        RowNumber = rowNumber;
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
}