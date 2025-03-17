using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TaskMealRepository : GenericRepository<TaskMeal>, ITaskMealRepository
    {
        public TaskMealRepository()
        {
        }
        public async Task<TaskMeal?> GetTaskMealByAnimalAssignId(int animalAssignId)
        {
            try
            {
                var taskMeal = (await GetAllAsync()).FirstOrDefault(l=>l.AnimalAssignId == animalAssignId);
                return taskMeal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskMeal> AddTaskMeal(int animalAssignId,TaskMealAdd key)
        {
            try
            {
                TaskMeal taskMeal = new()
                {
                    AnimalAssignId = animalAssignId,
                    MealDayId = key.MealDayId,
                    Percent = key.Percent
                };
                await CreateAsync(taskMeal);
                return taskMeal;
            }
            catch (Exception)
            {
                throw;
            }
        } 
    }
}
