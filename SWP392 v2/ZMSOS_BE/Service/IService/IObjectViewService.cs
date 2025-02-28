using BO.Models;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IObjectViewService
    {
        public Task<List<AnimalView>> GetListAnimalView(List<Animal> animals);
        public Task<AnimalView> GetAnimalView(Animal animal);
        public Task<FlockView> GetFlockView(Flock flock);
        public Task<IndividualView> GetIndividualView(Individual individual);
        public Task<List<AnimalTypeView>> GetListAnimalTypeView(List<AnimalType> animalTypes);
        public Task<AnimalTypeView> GetAnimalTypeView(AnimalType animalType);
        public Task<List<CageView>> GetListCageView(List<Cage> cages);
        public Task<CageView> GetCageView(Cage cage);
        public Task<List<ZooAreaView>> GetListZooAreaView(List<ZooArea> zooAreas);
        public Task<ZooAreaView> GetZooAreaView(ZooArea zooArea);
        public Task<List<TaskView>> GetListTaskView(List<BO.Models.Task> tasks);
        public Task<TaskView> GetTaskView(BO.Models.Task task);
        public Task<List<TaskTypeView>> GetListTaskTypeView(List<TaskType> taskTypes);
        public Task<TaskTypeView> GetTaskTypeView(TaskType taskType);
        public Task<List<AccountView>> GetListAccountView(List<Account> accounts);
        public Task<AccountView> GetAccountView(Account account);
        public Task<UserView> GetUserView(User user);
        public Task<List<TeamView>> GetListTeamView(List<Team> teams);
        public Task<TeamView> GetTeamView(Team team);
        public Task<LeaderAssignView> GetLeaderAssignView(LeaderAssign leaderAssign);
        public Task<List<MemberAssignView>> GetListMemberAssignView(List<MemberAssign> memberAssigns);
        public Task<MemberAssignView> GetMemberAssignView(MemberAssign memberAssign);
        public Task<List<ReportView>> GetListReportView(List<Report> reports);
        public Task<ReportView> GetReportView(Report report);
        public Task<List<ApplicationView>> GetListApplicationView(List<Application> applications);
        public Task<ApplicationView> GetApplicationView(Application application);
        public Task<List<ApplicationTypeView>> GetListApplicationTypeView(List<ApplicationType> applicationTypes);
        public Task<ApplicationTypeView> GetApplicationTypeView(ApplicationType applicationType);
        public Task<List<IncompatibleAnimalTypeView>> GetListIncompatibleAnimalTypeView(List<IncompatibleAnimalType> incompatibleAnimalTypes);
        public Task<IncompatibleAnimalTypeView> GetIncompatibleAnimalTypeView(IncompatibleAnimalType incompatibleAnimalType);
    }
}
