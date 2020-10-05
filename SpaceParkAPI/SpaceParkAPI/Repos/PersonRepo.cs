using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceParkAPI.Db_Context;
using SpaceParkAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;



namespace SpaceParkAPI.Repos
{
    public class PersonRepo : Repository, IPersonRepo 
    {

        public PersonRepo(SpaceParkContext spaceParkContext, ILogger<PersonRepo> logger) : base(spaceParkContext, logger)
        { }
        private static IQueryable<PersonModel> PersonQuery(IQueryable<PersonModel> query)
        {
            return query;
        }
        public async Task<PersonModel> GetPersonByName(String name)
        {
            _logger.LogInformation($"Fetching person by the selected name {name}.");
            
            IQueryable<PersonModel> query = _spaceParkContext.Persons.Where(x => x.Name == name);

            query = PersonQuery(query);

            return await query.SingleOrDefaultAsync();

        }
    }
}
