using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SpaceParkAPI.Models;
using SpaceParkAPI.Repos;
using Microsoft.AspNetCore.Cors;

namespace SpaceParkAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly IParkingSpaceRepo _parkingSpaceRepo;

        public ParkingSpaceController(IParkingSpaceRepo parkingSpaceRepo)
        {
            _parkingSpaceRepo = parkingSpaceRepo;
        }

        //api/v1.0/ParkingSpace/##
        [EnableCors("AllowFrontEnd")]

        [HttpGet("{id}")]
        public async Task<ActionResult> GetParkingSpaceById(int id)
        {
            try
            {
                var result = await _parkingSpaceRepo.GetParkingSpaceById(id);
                if (result == null)
                {
                    return NotFound($"ParkingSpace with ID: {id} could not be found");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            
        }
        [EnableCors("AllowFrontEnd")]

        [HttpPost]
        public async Task<ActionResult<ParkingSpaceModel>> PostParkingSpace(ParkingSpaceModel parkingSpaceModel)
        {
            try
            {
                _parkingSpaceRepo.Add(parkingSpaceModel);
                if (await _parkingSpaceRepo.Save())
                {
                    return Created($"/api/v1.0/Spaceship/{parkingSpaceModel.ID}", parkingSpaceModel);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }
        [EnableCors("AllowFrontEnd")]

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteParkingSpace(int id)
        {
            try
            {
                var parkingSpaceToDelete = await _parkingSpaceRepo.GetParkingSpaceById(id);
                if (parkingSpaceToDelete == null)
                {
                    return NotFound($"There is no parking space with that id (id: {id}) in the database");
                }

                _parkingSpaceRepo.Delete(parkingSpaceToDelete);

                if (await _parkingSpaceRepo.Save())
                {
                    return Ok($"You deleted the the parking space: {parkingSpaceToDelete} from the database");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure:{e.Message}");
            }
            return BadRequest();
        }

    }
}
