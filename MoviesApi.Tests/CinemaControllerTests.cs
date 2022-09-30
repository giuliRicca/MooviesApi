using Microsoft.AspNetCore.Mvc;
using MoviesApi.Controllers;
using MoviesApi.DTOs;
using MoviesApi.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Diagnostics;

namespace MoviesApi.Tests
{
    [TestClass]
    public class CinemaControllerTests : TestsBase
    {

        [TestMethod]
        public async Task Get_sendPlazaColonLocationAnd2kmDistance_returns3NearbyCinemas()
        {

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Setup
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);


            using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            {
                var cinemas = new List<Cinema>()
                {
                    new Cinema{ Name = "Test", Location = geometryFactory.CreatePoint(new Coordinate(-64.205345, -31.412223)) }
                };

                context.AddRange(cinemas);
                await context.SaveChangesAsync();
            }

            var dto = new CinemaLocationFilterDTO
            {
                latitude = -31.409237806617867,
                longitude = -64.1957959933835,
                distance = 2000
            };


            using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            {
                var mapper = ConfigAutoMapper();
                var controller = new CinemasController(context, mapper, geometryFactory);

                // Test
                var response = await controller.Nearby(dto);
                var objectResult = response.Result as ObjectResult;
                var responseValue = objectResult.Value;

                Trace.WriteLine(responseValue);
                Assert.IsNotNull(responseValue);

            }
        }

    }
}
