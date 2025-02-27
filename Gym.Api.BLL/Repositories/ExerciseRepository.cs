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
    public class ExerciseRepository:IExerciseRepository
    {
        private readonly ApplicationDbContext context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await context.Exercise.ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int Id)
        {
            return await context.Exercise.FindAsync(Id);
        }

        public async Task<int> AddAsync(Exercise entity)
        {
            await context.Exercise.AddAsync(entity);
             return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(Exercise entity)
        {
            context.Exercise.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(Exercise entity)
        {
            context.Exercise.Update(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exercise>> SearchByName(string Name)
        {
            return await context.Exercise.Where(E => E.Name.Contains(Name)).ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> SearchByTargetMuscle(string TargetMuscle)
        {
            return await context.Exercise.Where(E => E.TargetMuscle.Contains(TargetMuscle)).ToListAsync();
        }
    }
}
