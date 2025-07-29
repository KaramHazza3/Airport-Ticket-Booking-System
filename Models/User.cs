namespace FTSAirportTicketBookingSystem.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public UserRole Role { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
    }

    public User(string name, string email, string password, UserRole role)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Role = role;
    }

    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}, Email: {this.Email}, Role: {this.Role}";
    }
}