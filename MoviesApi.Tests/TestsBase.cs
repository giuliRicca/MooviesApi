using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Helpers;

namespace MoviesApi.Tests
{

    public class TestsBase
    {
       protected ApplicationDBContext BuildContext(string contextDB)
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(contextDB).Options;
            
            var dbContext = new ApplicationDBContext(options);
            return dbContext;
        }
        protected IMapper ConfigAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            options.AddProfile(new AutomapperProfiles()));
            return config.CreateMapper();
        }
    }
}