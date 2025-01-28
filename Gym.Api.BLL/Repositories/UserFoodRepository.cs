using Gym.Api.BLL.Interfaces;
using Gym.Api.DAL.Data.Contexts;
using Gym.Api.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Repositories
{
    public class UserFoodRepository : IUserFoodRepository
    {
        private readonly ApplicationDbContext _Context;

        public UserFoodRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<int> AddFoodToUser(ApplicationUserFood userFood)
        {
            _Context.applicationUserFoods.Add(userFood);
            return await _Context.SaveChangesAsync();
        }

        public async Task<int> RemoveFoodFromUser(string userId, int FoodId)
        {
           var userFood = _Context.applicationUserFoods.FirstOrDefault(e=>e.applicationUserId == userId && e.foodId==FoodId);
            if (userFood != null) 
            {
                _Context.applicationUserFoods.Remove(userFood);
                return await _Context.SaveChangesAsync();
            }
            return 0 ;
        }

        public async Task<IEnumerable<ApplicationUserFood>> GetAllFoodsOfUser(string userId)
        {
            return await _Context.applicationUserFoods.Where(e=>e.applicationUserId==userId).ToListAsync();
        }
    }
}
