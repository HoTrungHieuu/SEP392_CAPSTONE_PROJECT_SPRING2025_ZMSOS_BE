using BO.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AnimalImageRepository : GenericRepository<AnimalImage>, IAnimalImageRepository
    {
        public AnimalImageRepository()
        {
        }
        public async Task<List<AnimalImage>?> GetListAnimalImageByAnimalId(int animalId)
        {
            try
            {
                var animalImages = (await GetAllAsync()).FindAll(l=>l.AnimalId == animalId);
                return animalImages;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<string>?> GetListAnimalImageUrlByAnimalId(int animalId)
        {
            try
            {
                var animalImages = (await GetAllAsync()).FindAll(l => l.AnimalId == animalId);
                List<string> result = new();
                foreach (var animalImage in animalImages)
                {
                    result.Add(animalImage.UrlImage);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task AddAnimalImageByAnimalId(int animalId, List<string> listUrl)
        {
            try
            {
                if (listUrl == null)
                {
                    return;
                }
                foreach (var item in listUrl)
                {
                    AnimalImage animalImage = new AnimalImage()
                    {
                        AnimalId = animalId,
                        UrlImage = item,
                    };
                    await CreateAsync(animalImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task DeleteAnimalImageByAnimalId(int animalId)
        {
            try
            {
                var animalImages = (await GetAllAsync()).FindAll(l => l.AnimalId == animalId);
                foreach(var item in animalImages)
                {
                    await RemoveAsync(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
