using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Persistence
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        void Add(User user);
    }
}
