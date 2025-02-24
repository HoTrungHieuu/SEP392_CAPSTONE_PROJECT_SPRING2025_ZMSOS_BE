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
    public class ZooAreaService : IZooAreaService
    {
        public IZooAreaRepository repo;
        public ZooAreaService(IZooAreaRepository repo)
        {
            this.repo = repo;
        }
        public async Task<ServiceResult> GetListZooArea()
        {
            try
            {
                var zooAreas = await repo.GetListZooArea();
                if (zooAreas == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = repo.ConvertListZooAreaIntoListZooAreaView(zooAreas);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Areas",
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
        public async Task<ServiceResult> GetZooAreaById(int id)
        {
            try
            {
                var zooArea = repo.GetById(id);
                if (zooArea == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = repo.ConvertZooAreaIntoZooAreaView(zooArea);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Area",
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
        public async Task<ServiceResult> AddZooArea(ZooAreaAdd key)
        {
            try
            {
                var zooArea = await repo.AddZooArea(key);
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
        public async Task<ServiceResult> UpdateZooArea(ZooAreaUpdate key)
        {
            try
            {
                var zooArea = await repo.UpdateZooArea(key);
                if (zooArea == null)
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
