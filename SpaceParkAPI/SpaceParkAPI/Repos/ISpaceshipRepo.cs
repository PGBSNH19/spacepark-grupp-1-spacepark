using System.Threading.Tasks;
using SpaceParkAPI.Models;

namespace SpaceParkAPI.Repos
{
    public interface ISpaceshipRepo : IRepository
    {
        Task<SpaceshipModel> GetSpaceshipById(long id);
    }
}
