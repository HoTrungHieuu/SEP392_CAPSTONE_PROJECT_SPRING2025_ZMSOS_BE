using BO.Models;
using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ITaskMealRepository : IGenericRepository<TaskMeal>
    {
        public Task<TaskMeal?> GetTaskMealByAnimalAssignId(int animalAssignId);
        public Task<TaskMeal> AddTaskMeal(int animalAssignId, TaskMealAdd key);
    }
}
