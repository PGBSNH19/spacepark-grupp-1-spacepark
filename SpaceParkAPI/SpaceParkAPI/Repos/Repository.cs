using Microsoft.Extensions.Logging;
using SpaceParkAPI.Db_Context;
using System.Threading.Tasks;

namespace SpaceParkAPI.Repos
{
    public class Repository : IRepository
    {
        protected readonly SpaceParkContext _spaceParkContext;
        protected readonly ILogger<Repository> _logger;

        public Repository(SpaceParkContext context, ILogger<Repository> logger)
        {
            _spaceParkContext = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _spaceParkContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _spaceParkContext.Remove(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _spaceParkContext.SaveChangesAsync()) >= 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _spaceParkContext.Update(entity);
        }
    }
}
