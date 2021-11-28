using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using M16AlignAPI.Models;
namespace M16AlignAPI.Service
{
    public class MissionService 
    {

        private readonly IMongoCollection<Mission> _missions;

        public MissionService(IMissionsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _missions = database.GetCollection<Mission>(settings.MissionsCollectionName);
        }

        public List<Mission> Get() =>
            _missions.Find(mission => true).ToList();

        public Mission Create(Mission mission)

        {
            _missions.InsertOne(mission);
            return mission;
        }

        private List<string> GetIsolatedAgents()
        {
            var isolatedAgents = _missions.AsQueryable()
                .GroupBy(c => c.Agent)
                .Select(d => new { d.Key, count = d.Count() })
                .Where(e => e.count == 1)
                .Select(a => a.Key);

            return isolatedAgents.ToList();
        }

        //select agent from Missions group by agent having count(agent) = 1;
        private string GetMostIsolatedCountry(List<string> isolatedAgents)
        {
            var mostIsloatedCountry = _missions
                .AsQueryable()
                .Where(a => isolatedAgents.Contains(a.Agent))
                .GroupBy(a => a.Country)
                .Select(a => new { a.Key, count = a.Count() })
                .OrderByDescending(a => a.count);

            return mostIsloatedCountry.ToList().First().Key;
        }

        public string GetCountryByIsolation()
        {
            var agents = GetIsolatedAgents();
            var country = GetMostIsolatedCountry(agents);
            return country;
        }



        public Mission FindClosestMission(double i_GivenLatitude, double i_GivebLongitude)
        {
            double distanceClosestCountry = 1.7976931348623157E+308; // biggest double
            Mission country = new Mission();
            double distance;
            var filter = Builders<Mission>.Filter.Empty;
            foreach (Mission mission_ in _missions.Find(filter).ToListAsync().Result)
            {
                distance = DistanceBetweenTwoCoordinates(mission_.Latitude, mission_.Longitude, i_GivenLatitude, i_GivebLongitude);
                if (distance < distanceClosestCountry)
                {
                    distanceClosestCountry = distance;
                    country = mission_;
                }
            }
            return country;
        }
            
        public double DistanceBetweenTwoCoordinates(double i_LatitudeMission, double i_LongitudeMission, double i_GivenLatitude, double i_GivebLongitude)
        {
            var baseRad = Math.PI * i_LatitudeMission / 180;
            var targetRad = Math.PI * i_GivenLatitude / 180;
            var theta = i_LongitudeMission - i_GivebLongitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist;
        }
    }
}