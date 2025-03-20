using BO.Models;
using DAO.OtherModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ObjectViewService : IObjectViewService
    {
        public IAnimalRepository animalRepo;
        public IFlockRepository flockRepo;
        public IIndividualRepository individualRepo;
        public IAnimalTypeRepository animalTypeRepo;
        public ICageRepository cageRepo;
        public IZooAreaRepository zooAreaRepo;
        public ITaskRepository taskRepo;
        public ITaskTypeRepository taskTypeRepo;
        public IAnimalCageRepository animalCageRepo;
        public IAnimalAssignRepository animalAssignRepo;
        public IAccountRepository accountRepo;
        public IUserRepository userRepo;
        public ITeamRepository teamRepo;
        public ILeaderAssignRepository leaderRepo;
        public IMemberAssignRepository memberRepo;
        public IReportRepository reportRepo;
        public IReportAttachmentRepository reportAttachmentRepo;
        public IApplicationRepository applicationRepo;
        public IApplicationTypeRepository applicationTypeRepo;
        public IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo;
        public INewsRepository newsRepo;
        public INotificationRepository notificationRepo;
        public IScheduleRepository scheduleRepo;
        public ITaskEstimateRepository taskEstimateRepo;
        public IAnimalImageRepository animalImageRepo;
        public IZooAreaImageRepository zooAreaImageRepo;
        public IRoleRepository roleRepo;
        public IFoodRepository foodRepo;
        public IMealDayRepository mealDayRepo;
        public IMealFoodRepository mealFoodRepo;
        public ITaskMealRepository taskMealRepo;
        public ObjectViewService(IAnimalRepository animalRepo, IAnimalTypeRepository animalTypeRepo, 
            ICageRepository cageRepo, IZooAreaRepository zooAreaRepo,
            ITaskRepository taskRepo, ITaskTypeRepository taskTypeRepo, IAnimalCageRepository animalCageRepo, IAnimalAssignRepository animalAssignRepo,
            IFlockRepository flockRepo, IIndividualRepository individualRepo,
            IAccountRepository accountRepo, IUserRepository userRepo,
            ITeamRepository teamRepo, ILeaderAssignRepository leaderRepo, IMemberAssignRepository memberRepo,
            IReportRepository reportRepo, IReportAttachmentRepository reportAttachmentRepo,
            IApplicationRepository applicationRepo, IApplicationTypeRepository applicationTypeRepo,
            IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo,
            INewsRepository newsRepo,
            INotificationRepository notificationRepo,
            IScheduleRepository scheduleRepo,
            ITaskEstimateRepository taskEstimateRepo,
            IAnimalImageRepository animalImageRepo, IZooAreaImageRepository zooAreaImageRepo,
            IRoleRepository roleRepo,
            IFoodRepository foodRepo, IMealDayRepository mealDayRepo, IMealFoodRepository mealFoodRepo, ITaskMealRepository taskMealRepo)
        {
            this.animalRepo = animalRepo;
            this.animalTypeRepo = animalTypeRepo;
            this.cageRepo = cageRepo;
            this.zooAreaRepo = zooAreaRepo;
            this.taskRepo = taskRepo;
            this.taskTypeRepo = taskTypeRepo;
            this.animalCageRepo = animalCageRepo;
            this.animalAssignRepo = animalAssignRepo;
            this.flockRepo = flockRepo;
            this.individualRepo = individualRepo;
            this.accountRepo = accountRepo;
            this.userRepo = userRepo;
            this.teamRepo = teamRepo;
            this.leaderRepo = leaderRepo;
            this.memberRepo = memberRepo;
            this.reportRepo = reportRepo;
            this.reportAttachmentRepo = reportAttachmentRepo;
            this.applicationRepo = applicationRepo;
            this.applicationTypeRepo = applicationTypeRepo;
            this.incompatibleAnimalTypeRepo = incompatibleAnimalTypeRepo;
            this.newsRepo = newsRepo;
            this.notificationRepo = notificationRepo;
            this.scheduleRepo = scheduleRepo;
            this.taskEstimateRepo = taskEstimateRepo;
            this.animalImageRepo = animalImageRepo;
            this.zooAreaImageRepo = zooAreaImageRepo;
            this.roleRepo = roleRepo;
            this.foodRepo = foodRepo;
            this.mealDayRepo = mealDayRepo;
            this.mealFoodRepo = mealFoodRepo;
            this.taskMealRepo = taskMealRepo;
        }
        public async Task<List<AnimalView>> GetListAnimalView(List<Animal> animals)
        {
            List<AnimalView> result = new();
            foreach (var animal in animals)
            {
                result.Add(await GetAnimalView(animal));
            }
            return result;
        }
        public async Task<AnimalView> GetAnimalView(Animal animal)
        {
            AnimalTypeView animalType = new();
            animalType = await GetAnimalTypeView(animalTypeRepo.GetById((int)animal.AnimalTypeId));
            List<string> urlImages = new List<string>();
            urlImages = await animalImageRepo.GetListAnimalImageUrlByAnimalId(animal.Id);
            CageView cage = new();
            var animalCage = await animalCageRepo.GetAnimalCageCurrentByAnimalId((int)animal.Id);
            if(animalCage != null)
            {
                cage = await GetCageView(cageRepo.GetById((int)animalCage.CageId));
            }
            else
            {
                cage = null;
            }
            if (animal.Classify == "Flock")
            {
                FlockView flock = new();
                flock = await GetFlockView(await flockRepo.GetFlockByAnimalId((int)animal.Id));
                var result = animalRepo.ConvertAnimalIntoAnimalView(animal, animalType, flock, null,cage,urlImages);
                return result;
            }
            else if(animal.Classify == "Individual")
            {
                IndividualView individual = new();
                individual = await GetIndividualView(await individualRepo.GetIndividualByAnimalId((int)animal.Id));
                var result = animalRepo.ConvertAnimalIntoAnimalView(animal, animalType, null, individual, cage, urlImages);
                return result;
            }
            else
            {
                var result = animalRepo.ConvertAnimalIntoAnimalView(animal, animalType, null, null, cage, urlImages);
                return result;
            }
        }
        public async Task<FlockView> GetFlockView(Flock flock)
        {
            var result = flockRepo.ConvertFlockIntoFlockView(flock);
            return result;
        }
        public async Task<IndividualView> GetIndividualView(Individual individual)
        {
            var result = individualRepo.ConvertIndividualIntoIndividualView(individual);
            return result;
        }
        public async Task<List<AnimalTypeView>> GetListAnimalTypeView(List<AnimalType> animalTypes)
        {
            List<AnimalTypeView> result = new();
            foreach (var animalType in animalTypes)
            {
                result.Add(await GetAnimalTypeView(animalType));
            }
            return result;
        }
        public async Task<AnimalTypeView> GetAnimalTypeView(AnimalType animalType)
        {
            var result = animalTypeRepo.ConvertAnimalTypeIntoAnimalTypeView(animalType);
            return result;
        }
        public async Task<List<CageView>> GetListCageView(List<Cage> cages)
        {
            List<CageView> result = new List<CageView>();
            foreach(var cage in cages)
            {
                result.Add(await GetCageView(cage));
            }
            return result;
        }
        public async Task<CageView> GetCageView(Cage cage)
        {
            ZooAreaView zooArea = new();
            zooArea = await GetZooAreaView(zooAreaRepo.GetById((int)cage.ZooAreaId));
            var result = cageRepo.ConvertCageIntoCageView(cage, zooArea);
            return result;
        }
        public async Task<List<ZooAreaView>> GetListZooAreaView(List<ZooArea> zooAreas)
        {
            List<ZooAreaView> result = new List<ZooAreaView>();
            foreach (var zooArea in zooAreas)
            {
                result.Add(await GetZooAreaView(zooArea));
            }
            return result;
        }
        public async Task<ZooAreaView> GetZooAreaView(ZooArea zooArea)
        {
            List<string> urlImages = new List<string>();
            urlImages = await zooAreaImageRepo.GetListZooAreaImageUrlByZooAreaId(zooArea.Id);
            var result = zooAreaRepo.ConvertZooAreaIntoZooAreaView(zooArea, urlImages);
            return result;
        }
        public async Task<List<TaskView>> GetListTaskView(List<BO.Models.Task> tasks)
        {
            List<TaskView> result = new List<TaskView>();
            foreach(var task in tasks)
            {
                result.Add(await GetTaskView(task));
            }
            return result;
        }
        public async Task<TaskView> GetTaskView(BO.Models.Task task)
        {
            TaskTypeView? taskType = new();
            if(task.TaskTypeId != null)
            {
                taskType = await GetTaskTypeView(taskTypeRepo.GetById((int)task.TaskTypeId));
            }
            var animalAssigns = await animalAssignRepo.GetListAnimalAssignByTaskId(task.Id);
            if(animalAssigns == null)
            {
                var resultTemp = taskRepo.ConvertTaskIntoTaskView(task, null, taskType);
                return resultTemp;
            }
            List<(AnimalView, CageView, AnimalAssign)> animalCageViews = new();
            List<AnimalCageTask> animalCageTasks = new();
            foreach (var animalAssign in animalAssigns)
            {
                var animalCage = animalCageRepo.GetById(animalAssign.AnimalCageId);
                animalCageViews.Add((await GetAnimalView(animalRepo.GetById(animalCage.AnimalId)), await GetCageView(cageRepo.GetById(animalCage.CageId)), animalAssign));
            }
            animalCageViews.OrderBy(l => l.Item2);
            AnimalCageTask animalCageTask = new();
            for (int i = 0; i < animalCageViews.Count; i++)
            {
                if (i > 0)
                {
                    if (animalCageViews[i].Item2.Id == animalCageViews[i - 1].Item2.Id)
                    {
                        animalCageTask.Animals.Add(new AnimalTaskMeal()
                        {
                            Animal = animalCageViews[i].Item1,
                            MealDay = await GetMealDayView(mealDayRepo.GetById((await taskMealRepo.GetTaskMealByAnimalAssignId(animalCageViews[i].Item3.Id)).MealDayId))
                        });
                    }
                    else
                    {
                        animalCageTasks.Add(animalCageTask);
                        animalCageTask = new()
                        {
                            Cage = animalCageViews[i].Item2,
                            Animals = new()
                        };
                        animalCageTask.Animals.Add(new AnimalTaskMeal()
                        {
                            Animal = animalCageViews[i].Item1,
                            MealDay = await GetMealDayView(mealDayRepo.GetById((await taskMealRepo.GetTaskMealByAnimalAssignId(animalCageViews[i].Item3.Id)).MealDayId))
                        });
                    }
                }
                else
                {
                    animalCageTask = new()
                    {
                        Cage = animalCageViews[i].Item2,
                        Animals = new()
                    };
                    animalCageTask.Animals.Add(new AnimalTaskMeal()
                    {
                        Animal = animalCageViews[i].Item1,
                        MealDay = await GetMealDayView(mealDayRepo.GetById((await taskMealRepo.GetTaskMealByAnimalAssignId(animalCageViews[i].Item3.Id)).MealDayId))
                    });
                }
            }
            animalCageTasks.Add(animalCageTask);
            var result = taskRepo.ConvertTaskIntoTaskView(task, animalCageTasks, taskType);
            return result;
        }
        public async Task<List<TaskTypeView>> GetListTaskTypeView(List<TaskType> taskTypes)
        {
            List<TaskTypeView> result = new List<TaskTypeView>();
            foreach (var taskType in taskTypes)
            {
                result.Add(await GetTaskTypeView(taskType));
            }
            return result;
        }
        public async Task<TaskTypeView> GetTaskTypeView(TaskType taskType)
        {
            var result = taskTypeRepo.ConvertTaskTypeIntoTaskTypeView(taskType);
            return result;
        }
        public async Task<List<AccountView>> GetListAccountView(List<Account> accounts)
        {
            List<AccountView> result = new List<AccountView>();
            foreach (var account in accounts)
            {
                result.Add(await GetAccountView(account));
            }
            return result;
        }
        public async Task<AccountView> GetAccountView(Account account)
        {
            RoleView role = new RoleView();
            role = await GetRoleView(roleRepo.GetById(account.RoleId));
            UserView user = new UserView();
            user = await GetUserView(await userRepo.GetUserByAccountId(account.Id));
            var result = accountRepo.ConvertAccountIntoAccountView(account, role, user);
            return result;
        }
        public async Task<UserView> GetUserView(User user)
        {
            var result = userRepo.ConvertUserIntoUserView(user);
            return result;
        }
        public async Task<List<TeamView>> GetListTeamView(List<Team> teams)
        {
            List<TeamView> result = new List<TeamView>();
            foreach (var team in teams)
            {
                result.Add(await GetTeamView(team));
            }
            return result;
        }
        public async Task<TeamView> GetTeamView(Team team)
        {
            var result = teamRepo.ConvertTeamIntoTeamView(team);
            return result;
        }
        public async Task<LeaderAssignView> GetLeaderAssignView(LeaderAssign leaderAssign)
        {
            TeamView team = new();
            team = await GetTeamView(teamRepo.GetById((int)leaderAssign.TeamId));
            UserView user = new();
            user = await GetUserView(userRepo.GetById((int)leaderAssign.LeaderId));
            var result = leaderRepo.ConvertLeaderAssignIntoLeaderAssignView(leaderAssign,team,user);
            return result;
        }
        public async Task<List<MemberAssignView>> GetListMemberAssignView(List<MemberAssign> memberAssigns)
        {
            List<MemberAssignView> result = new List<MemberAssignView>();
            foreach (var memberAssign in memberAssigns)
            {
                result.Add(await GetMemberAssignView(memberAssign));
            }
            return result;
        }
        public async Task<MemberAssignView> GetMemberAssignView(MemberAssign memberAssign)
        {
            TeamView team = new();
            team = await GetTeamView(teamRepo.GetById((int)memberAssign.TeamId));
            UserView user = new();
            user = await GetUserView(userRepo.GetById((int)memberAssign.MemberId));
            var result = memberRepo.ConvertMemberAssignIntoMemberAssignView(memberAssign, team, user);
            return result;
        }
        public async Task<List<ReportView>> GetListReportView(List<Report> reports)
        {
            List<ReportView> result = new List<ReportView>();
            foreach (var report in reports)
            {
                result.Add(await GetReportView(report));
            }
            return result;
        }
        public async Task<ReportView> GetReportView(Report report)
        {
            UserView sender = new();
            sender = await GetUserView(await userRepo.GetUserByAccountId((int)report.SenderId));
            UserView reciever = new();
            reciever = await GetUserView(await userRepo.GetUserByAccountId((int)report.ReceiverId));
            var attachs = await reportAttachmentRepo.GetListReportAttachmentByReportId(report.Id);
            List<string> fileUrl = reportAttachmentRepo.ConvertListReportAttachmentIntoListString(attachs);

            var result = reportRepo.ConvertReportIntoReportView(report, sender, reciever, fileUrl);
            return result;
        }
        public async Task<List<ApplicationView>> GetListApplicationView(List<Application> applications)
        {
            List<ApplicationView> result = new List<ApplicationView>();
            foreach (var application in applications)
            {
                result.Add(await GetApplicationView(application));
            }
            return result;
        }
        public async Task<ApplicationView> GetApplicationView(Application application)
        {
            UserView sender = new();
            sender = await GetUserView(await userRepo.GetUserByAccountId((int)application.SenderId));
            UserView reciever = new();
            reciever = await GetUserView(await userRepo.GetUserByAccountId((int)application.RecieverId));
            ApplicationTypeView applicationType = new();
            applicationType = await GetApplicationTypeView(applicationTypeRepo.GetById(application.Id));

            var result = applicationRepo.ConvertApplicationIntoApplicationView(application, sender, reciever, applicationType);
            return result;
        }
        public async Task<List<ApplicationTypeView>> GetListApplicationTypeView(List<ApplicationType> applicationTypes)
        {
            List<ApplicationTypeView> result = new List<ApplicationTypeView>();
            foreach (var applicationType in applicationTypes)
            {
                result.Add(await GetApplicationTypeView(applicationType));
            }
            return result;
        }
        public async Task<ApplicationTypeView> GetApplicationTypeView(ApplicationType applicationType)
        {
            var result = applicationTypeRepo.ConvertApplicationTypeIntoApplicationTypeView(applicationType);
            return result;
        }
        public async Task<List<IncompatibleAnimalTypeView>> GetListIncompatibleAnimalTypeView(List<IncompatibleAnimalType> incompatibleAnimalTypes)
        {
            List<IncompatibleAnimalTypeView> result = new List<IncompatibleAnimalTypeView>();
            foreach (var incompatibleAnimalType in incompatibleAnimalTypes)
            {
                result.Add(await GetIncompatibleAnimalTypeView(incompatibleAnimalType));
            }
            return result;
        }
        public async Task<IncompatibleAnimalTypeView> GetIncompatibleAnimalTypeView(IncompatibleAnimalType incompatibleAnimalType)
        {
            AnimalTypeView animalType1 = new();
            animalType1 = await GetAnimalTypeView(animalTypeRepo.GetById((int)incompatibleAnimalType.AnimalTypeId1));
            AnimalTypeView animalType2 = new();
            animalType2 = await GetAnimalTypeView(animalTypeRepo.GetById((int)incompatibleAnimalType.AnimalTypeId2));
            var result = incompatibleAnimalTypeRepo.ConvertIncompatibleAnimalTypeIntoIncompatibleAnimalTypeView(incompatibleAnimalType,animalType1,animalType2);
            return result;
        }
        public async Task<List<NewsView>> GetListNewsView(List<News> newss)
        {
            List<NewsView> result = new List<NewsView>();
            foreach (var news in newss)
            {
                result.Add(await GetNewsView(news));
            }
            return result;
        }
        public async Task<NewsView> GetNewsView(News news)
        {
            UserView user = new();
            user = await GetUserView(await userRepo.GetUserByAccountId((int)news.AccountId));
            var result = newsRepo.ConvertNewsIntoNewsView(news, user);
            return result;
        }
        public async Task<List<NotificationView>> GetListNotificationView(List<Notification> notifications)
        {
            List<NotificationView> result = new List<NotificationView>();
            foreach (var notification in notifications)
            {
                result.Add(await GetNotificationView(notification));
            }
            return result;
        }
        public async Task<NotificationView> GetNotificationView(Notification notification)
        {
            var result = notificationRepo.ConvertNotificationIntoNotificationView(notification);
            return result;
        }
        public async Task<List<ScheduleView>> GetListScheduleView(List<Schedule> schedules)
        {
            List<ScheduleView> result = new List<ScheduleView>();
            foreach (var schedule in schedules)
            {
                result.Add(await GetScheduleView(schedule));
            }
            return result;
        }
        public async Task<ScheduleView> GetScheduleView(Schedule schedule)
        {
            UserView user = new();
            user = await GetUserView(await userRepo.GetUserByAccountId((int)schedule.AccountId));
            var result = scheduleRepo.ConvertScheduleIntoScheduleView(schedule, user);
            return result;
        }
        public async Task<List<TaskEstimateView>> GetListTaskEstimateView(List<TaskEstimate> taskEstimates)
        {
            List<TaskEstimateView> result = new List<TaskEstimateView>();
            foreach (var taskEstimate in taskEstimates)
            {
                result.Add(await GetTaskEstimateView(taskEstimate));
            }
            return result;
        }
        public async Task<TaskEstimateView> GetTaskEstimateView(TaskEstimate taskEstimate)
        {
            TaskTypeView taskType = new();
            taskType = await GetTaskTypeView(taskTypeRepo.GetById((int)taskEstimate.TaskTypeId));
            AnimalTypeView animalType = new();
            animalType = await GetAnimalTypeView(animalTypeRepo.GetById((int)taskEstimate.AnimalTypeId));
            var result = taskEstimateRepo.ConvertTaskEstimateIntoTaskEstimateView(taskEstimate, taskType,animalType);
            return result;
        }
        public async Task<List<RoleView>> GetListRoleView(List<Role> roles)
        {
            List<RoleView> result = new List<RoleView>();
            foreach (var role in roles)
            {
                result.Add(await GetRoleView(role));
            }
            return result;
        }
        public async Task<RoleView> GetRoleView(Role role)
        {
            var result = roleRepo.ConvertRoleIntoRoleView(role);
            return result;
        }
        public async Task<List<FoodView>> GetListFoodView(List<Food> foods)
        {
            List<FoodView> result = new List<FoodView>();
            foreach (var food in foods)
            {
                result.Add(await GetFoodView(food));
            }
            return result;
        }
        public async Task<FoodView> GetFoodView(Food food)
        {
            var result = foodRepo.ConvertFoodIntoFoodView(food);
            return result;
        }
        public async Task<List<MealDayView>> GetListMealDayView(List<MealDay> mealDays)
        {
            List<MealDayView> result = new List<MealDayView>();
            foreach (var mealDay in mealDays)
            {
                result.Add(await GetMealDayView(mealDay));
            }
            return result;
        }
        public async Task<MealDayView> GetMealDayView(MealDay mealDay)
        {
            AnimalTypeView animalType = new();
            animalType = await GetAnimalTypeView(animalTypeRepo.GetById(mealDay.AnimalTypeId));
            List<MealFoodView> foods = new();
            foreach(var food in (await mealFoodRepo.GetListMealFoodByMealDayId(mealDay.Id)))
            {
                FoodView fo = new();
                fo = await GetFoodView(foodRepo.GetById(food.FoodId));
                foods.Add(new MealFoodView()
                {
                    Food = fo,
                    Quantitative = food.Quantitative
                });
            }
            var result = mealDayRepo.ConvertMealDayIntoMealDayView(mealDay, animalType,foods);
            return result;
        }
    }
}
