using SpaceParkAPI.Models;
using System;
using System.Threading.Tasks;

namespace SpaceParkAPI.Repos
{
    public interface IPersonRepo : IRepository
    {
        public Task<PersonModel> GetPersonByName(String name);
    }
}
