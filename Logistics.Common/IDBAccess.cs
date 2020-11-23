using System.Collections.Generic;

namespace Logistics.Common.Models
{
    public interface IDBAccess
    {
        void UpdateLogistics();

        void AddCity(string name);
        CityDTO FindCity(int id);
        void EditCity(CityDTO city);
        void DeleteCity(int id);
        List<CityDTO> GetCities();

        void AddRout(int startId, int endId, decimal distance);
        RoutDTO FindRout(int id);
        void EditRout(RoutDTO city);
        void DeleteRout(int id);
        List<RoutDTO> GetRoutes();

        void AddLogisticsCenter(LogisticsCenterDTO lcenter);
        LogisticsCenterDTO FindLogisticsCenter(int id);
        void DeleteLogisticsCenter(int id);
        List<LogisticsCenterDTO> GetLogisticsCenters();

        bool SaveChanges();
    }
}
