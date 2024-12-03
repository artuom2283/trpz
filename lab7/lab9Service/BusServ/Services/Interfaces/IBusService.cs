using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusServ.DTOs;

namespace BusServ.Services.Interfaces
{
    public interface IBusService
    {
        IEnumerable<BusDto> GetAllBuses();
        BusDto GetBusById(int id);
        void CreateBus(BusDto busDto);
        void UpdateBus(BusDto busDto);
        void DeleteBus(int id);
    }
}