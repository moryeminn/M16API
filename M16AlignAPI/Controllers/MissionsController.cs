using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using M16AlignAPI.Models;
using M16AlignAPI.Service;
using System.Drawing;
using MongoDB.Driver.GeoJsonObjectModel;


namespace M16AlignAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class MissionsController : ControllerBase
    {
        private readonly MissionService _missionService;
        private readonly GeoCodeAPI _geoCodeAPI;

        public MissionsController(MissionService MissionService, GeoCodeAPI GeoCodeAPI)
        {
            _missionService = MissionService;
            _geoCodeAPI = GeoCodeAPI;
        }

        [HttpPost]
        [Route("/mission")]
        public IActionResult Create(Mission mission)
        {
            try
            {
                _missionService.Create(mission);
                return Ok();
            }
            catch (Exception e)
            {
                // will log e.message
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("/get-missions")]
        public ActionResult<List<Mission>> Get() =>
            _missionService.Get();


        [HttpGet]
        [Route("/countries-by-isolation")]
        public IActionResult GetCountryByIsolation()
        {
            try
            {
                var country = _missionService.GetCountryByIsolation();
                return Ok(country);
            }
            catch (Exception exception)
            {
                return StatusCode(500);
            }
        }
      
        [HttpPost]
        [Route("/find-closest")]
        public IActionResult FindClosestCountry([FromBody]InputAddress address)
        {
            string latitude, longitude;          
            try
            {
                 _geoCodeAPI.GetCoordinates(out latitude, out longitude, address.TargetLocation);
                if (latitude is null || longitude is null)
                {
                    return NotFound("cannot find the coordinates of the address");
                }
                var mission = _missionService.FindClosestMission(double.Parse(latitude), double.Parse(longitude));
                return Ok(mission);
            }
            catch (Exception exception)
            {
                return StatusCode(500);
            }
        }
    }
}