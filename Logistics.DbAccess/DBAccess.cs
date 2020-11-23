using Logistics.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Logistics.DbAccess
{
    public class DBAccess : IDBAccess
    {
        private LogisticContext Context { get; }

        public DBAccess(LogisticContext _context)
        {
            this.Context = _context;
        }

        public void AddCity(string name)
        {
            Context.Cities.Add(new Cities
            {
                Name = name,
                DateUpdated = DateTime.Now
            });
        }

        public void AddLogisticsCenter(LogisticsCenterDTO lcenter)
        {
            Context.LogisticsCenters.Add(new LogisticsCenters
            {
                CityId = lcenter.CityId,
                DateUpdated = DateTime.Now
            });
        }

        public void AddRout(int startId, int endId, decimal distance)
        {
            Context.Routes.Add(new Routes
            {
                StartCityId = startId,
                EndCityId = endId,
                DateUpdated = DateTime.Now,
                Distance = distance
            });
        }

        public void DeleteCity(int id)
        {
            var city = Context.Cities.Where(x => x.Id == id).FirstOrDefault();
            Context.Cities.Remove(city);
        }

        public void DeleteRout(int id)
        {
            var rout = Context.Routes.Where(x => x.Id == id).FirstOrDefault();
            Context.Routes.Remove(rout);
        }

        public void DeleteLogisticsCenter(int id)
        {
            var lcenter = Context.LogisticsCenters.Where(x => x.Id == id).FirstOrDefault();
            Context.LogisticsCenters.Remove(lcenter);
        }

        public void EditCity(CityDTO cityDto)
        {
            var city = Context.Cities.Where(x => x.Id == cityDto.Id).FirstOrDefault();
            city.Name = cityDto.Name;
            city.DateUpdated = DateTime.Now;
        }

        public void EditRout(RoutDTO routDto)
        {
            var rout = Context.Routes.Where(x => x.Id == routDto.Id).FirstOrDefault();
            rout.StartCityId = routDto.StartCityId;
            rout.EndCityId = routDto.EndCityId;
            rout.DateUpdated = DateTime.Now;
        }

        public List<CityDTO> GetCities()
        {
            return Context.Cities.Select(x => new CityDTO
            {
                Id = x.Id,
                Name = x.Name,
                DateUpdated = x.DateUpdated
            }).ToList();
        }

        public List<RoutDTO> GetRoutes()
        {
            return Context.Routes.Select(x => new RoutDTO
            {
                Id = x.Id,
                StartCityId = x.StartCityId,
                StartCityName = x.StartCity.Name,
                EndCityId = x.EndCityId,
                EndCityName = x.EndCity.Name,
                DateUpdated = x.DateUpdated
            }).ToList();
        }

        public List<LogisticsCenterDTO> GetLogisticsCenters()
        {
            return Context.LogisticsCenters.Select(x => new LogisticsCenterDTO
            {
                Id = x.Id,
                CityId = x.CityId,
                CityName = x.City.Name,
                DateUpdated = x.DateUpdated
            }).ToList();
        }

        public bool SaveChanges()
        {
            return Context.SaveChanges() > 0;
        }

        public CityDTO FindCity(int id)
        {
            return Context.Cities.Where(x => x.Id == id).Select(x => new CityDTO
            {
                Id = x.Id,
                Name = x.Name,
                DateUpdated = x.DateUpdated
            }).FirstOrDefault();
        }

        public RoutDTO FindRout(int id)
        {
            return Context.Routes.Where(x => x.Id == id).Select(x => new RoutDTO
            {
                Id = x.Id,
                StartCityId = x.StartCityId,
                StartCityName = x.StartCity.Name,
                EndCityId = x.EndCityId,
                EndCityName = x.EndCity.Name,
                DateUpdated = x.DateUpdated
            }).FirstOrDefault();
        }

        public LogisticsCenterDTO FindLogisticsCenter(int id)
        {
            return Context.LogisticsCenters.Where(x => x.Id == id).Select(x => new LogisticsCenterDTO
            {
                Id = x.Id,
                CityId = x.CityId,
                CityName = x.City.Name,
                DateUpdated = x.DateUpdated
            }).FirstOrDefault();
        }

        public void UpdateLogistics()
        {
            var lastUpdateOfRoutes = Context.Routes.OrderByDescending(x => x.DateUpdated).Select(x => x.DateUpdated).FirstOrDefault();
            var lastUpdateOfLogistics = Context.LogisticsCenters.OrderByDescending(x => x.DateUpdated).Select(x => x.DateUpdated).FirstOrDefault();

            if (lastUpdateOfLogistics > lastUpdateOfRoutes)
            {
                //the last update of the logistics centers is later than the last update of the routes
                //so no new changes have happend on the routes and no update is necesery on the logistics
                return;
            }

            var averageRoutesByStartCity = Context.Routes.GroupBy(x => x.StartCityId).Select(x => new CitiDistance()
            {
                CityId = x.Key,
                AverageDistance = (x.Select(y => y.Distance).Sum()) / x.Count()
            }).ToList();

            var averageRoutesByEndCity = Context.Routes.GroupBy(x => x.EndCityId).Select(x => new CitiDistance()
            {
                CityId = x.Key,
                AverageDistance = (x.Select(y => y.Distance).Sum()) / x.Count()
            }).ToList();

            averageRoutesByStartCity.AddRange(averageRoutesByEndCity);
            var averageDistances = averageRoutesByStartCity.GroupBy(x => x.CityId).Select(x => new CitiDistance()
            {
                CityId = x.Key,
                AverageDistance = x.Select(y => y.AverageDistance).Sum()
            }).ToList();

            var mostDistantCity = averageDistances.OrderByDescending(x => x.AverageDistance).FirstOrDefault();

            var ClosestToTheMostDistant = Context.Routes
                .Where(x => x.StartCityId == mostDistantCity.CityId || x.EndCityId == mostDistantCity.CityId)
                .OrderBy(x => x.Distance).FirstOrDefault();

            int newLogisticsCenterCityId;
            if (ClosestToTheMostDistant.StartCityId == mostDistantCity.CityId)
            {
                newLogisticsCenterCityId = ClosestToTheMostDistant.EndCityId;
            }
            else
            {
                newLogisticsCenterCityId = ClosestToTheMostDistant.StartCityId;
            }

            AddLogisticsCenter(new LogisticsCenterDTO { CityId = newLogisticsCenterCityId });
        }
    }
}
