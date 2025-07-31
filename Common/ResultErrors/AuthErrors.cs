namespace FTSAirportTicketBookingSystem.Common.Errors;

public class AuthErrors
{
    public static readonly Error Unauthorized = new("Auth.Unauthorized", "Invalid credentials");
}