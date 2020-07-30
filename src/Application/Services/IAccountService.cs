using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccountAsync();
    }
}
