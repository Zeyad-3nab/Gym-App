﻿using Gym.Api.BLL.Interfaces;
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
    public class TrainerDataRepository:ITrainerDataRepository
    {
        private readonly ApplicationDbContext context;

        public TrainerDataRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<TrainerData>> GetAllAsync()
        {
            return await context.TrainersData.ToListAsync();
        }

        public async Task<TrainerData> GetByIdAsync(int Id)
        {
            return await context.TrainersData.FindAsync(Id);
        }

        public async Task AddAsync(TrainerData entity)
        {
            await context.TrainersData.AddAsync(entity);
            context.SaveChanges();
        }

        public void Delete(TrainerData entity)
        {
            context.TrainersData.Remove(entity);
            context.SaveChanges();
        }

        public void Update(TrainerData entity)
        {
            context.TrainersData.Update(entity);
            context.SaveChanges();
        }

        public async Task<IEnumerable<TrainerData>> SearchByName(string Name)
        {
            return await context.TrainersData.Where(E => E.UserName.Contains(Name)).ToListAsync();
        }
    }
}
