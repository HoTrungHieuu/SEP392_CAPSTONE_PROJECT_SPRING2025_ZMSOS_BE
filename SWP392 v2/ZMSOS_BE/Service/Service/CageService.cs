using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CageService : ICageService
    {
        public ICageRepository repo;
        public IZooAreaRepository areaRepo;
        public CageService(ICageRepository repo, IZooAreaRepository areaRepo)
        {
            this.repo = repo;
            this.areaRepo = areaRepo;
        }
        public async Task<ServiceResult> GetListCage()
        {
            try
            {
                var cages = await repo.GetListCage();
                if (cages == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                List<ZooAreaView> zooAreas = new();
                for (int i = 0; i < cages.Count; i++)
                {
                    ZooAreaView zooArea = new();
                    zooArea = areaRepo.ConvertZooAreaIntoZooAreaView(areaRepo.GetById((int)cages[i].ZooAreaId));
                    zooAreas.Add(zooArea);
                }

                var result = repo.ConvertListCageIntoListCageView(cages, zooAreas);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetListCageByZooAreaId(int zooAreaId)
        {
            try
            {
                var cages = await repo.GetListCageByAreaId(zooAreaId);
                if (cages == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                List<ZooAreaView> zooAreas = new();
                for (int i = 0; i < cages.Count; i++)
                {
                    ZooAreaView zooArea = new();
                    zooArea = areaRepo.ConvertZooAreaIntoZooAreaView(areaRepo.GetById((int)cages[i].ZooAreaId));
                    zooAreas.Add(zooArea);
                }

                var result = repo.ConvertListCageIntoListCageView(cages, zooAreas);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetCageById(int id)
        {
            try
            {
                var cage = repo.GetById(id);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                ZooAreaView zooArea = new();
                zooArea = areaRepo.ConvertZooAreaIntoZooAreaView(areaRepo.GetById((int)cage.ZooAreaId));

                var result = repo.ConvertCageIntoCageView(cage, zooArea);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddCage(CageAdd key)
        {
            try
            {
                var cage = await repo.AddCage(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateCage(CageUpdate key)
        {
            try
            {
                var cage = await repo.UpdateCage(key);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
