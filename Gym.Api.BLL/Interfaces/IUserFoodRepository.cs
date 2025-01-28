using Gym.Api.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IUserFoodRepository
    {
        Task<int> AddFoodToUser(ApplicationUserFood userFood);
        Task<int> RemoveFoodFromUser(string userId , int FoodId);
        Task<IEnumerable<ApplicationUserFood>> GetAllFoodsOfUser(string userId);
    }
}