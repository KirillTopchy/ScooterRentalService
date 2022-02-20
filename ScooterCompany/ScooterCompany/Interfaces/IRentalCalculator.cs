using ScooterCompany.Models;

namespace ScooterCompany.Interfaces
{
    public interface IRentalCalculator
    {
        decimal CalculateRent(RentedScooter scooter);
    }
}
