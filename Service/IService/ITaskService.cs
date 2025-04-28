using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITaskService
    {
        public Task<ServiceResult> GetListTaskByDateByTeamId(int teamId, DateOnly fromDate, DateOnly toDate);
        public Task<ServiceResult> GetListTaskAnimalByDateByTeamId(int teamId, DateOnly fromDate, DateOnly toDate);
        public Task<ServiceResult> GetListTaskByDateByAccountId(int accountId, DateOnly fromDate, DateOnly toDate);
        public Task<ServiceResult> GetListAccountSuitableTranfer(int teamId, int taskId);
        public Task<ServiceResult> GetListTaskByScheduleId(int scheduleId);
        public Task<ServiceResult> GetTaskById(int id);
        public Task<ServiceResult> AddTask(TaskAdd key);
        public Task<ServiceResult> AddTaskManual(TaskAddManual key);
        public Task<ServiceResult> UpdateTask(TaskUpdate key);
        public Task<ServiceResult> TranferTask(TaskTranfer key);
        public Task<ServiceResult> ClearTaskStaff(ClearTask key);
        public Task<ServiceResult> StartTask(int id);
        public Task<ServiceResult> UpdateTaskStaff(TaskStaffUpdate key);
        public Task<ServiceResult> UpdateTaskLeader(TaskLeaderUpdate key);
        public Task<ServiceResult> AddTaskCleaningAutomatic(AnimalTaskCleaningSchedule key);
        public Task<ServiceResult> AddTaskAutomatic(AnimalTaskMealSchdule key);
        public Task<ServiceResult> AddTaskHealthAutomatic(AnimalTaskNormalScheldule key);
    }
}
