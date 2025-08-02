using System.ComponentModel.DataAnnotations;

namespace FTSAirportTicketBookingSystem.Common;

public static class AttributeConstraintsGenerator
{

    public static List<AttributeConstraint> GetAttributesConstraints<T>()
    {
        var properties = typeof(T).GetProperties();
        var constraints = new List<AttributeConstraint>();

        foreach (var property in properties)
        {
            var propertyConstraint = new AttributeConstraint
            {
                PropertyName = property.Name,
                PropertyType = property.PropertyType.Name,
                Constraints = new List<string>()
            };

            var validationAttributes = property.GetCustomAttributes(true)
                .OfType<ValidationAttribute>();

            foreach (var attribute in validationAttributes)
            {
                switch (attribute)
                {
                    case RequiredAttribute:
                        propertyConstraint.Constraints.Add("Required");
                        break;
                }
                
                if (!string.IsNullOrWhiteSpace(attribute.ErrorMessage))
                {
                    propertyConstraint.Constraints.Add(attribute.ErrorMessage);
                }
            }
            
            constraints.Add(propertyConstraint);
        }
        
        return constraints;
    }
}