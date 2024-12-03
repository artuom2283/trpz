using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusServ.DTOs;
using BusServ.Services.Interfaces;
using lab7.Entities;
using lab7.Repositories.Interfaces;

namespace BusServ.Services.Implementation
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;

        public BusService(IBusRepository busRepository, IMapper mapper)
        {
            _busRepository = busRepository;
            _mapper = mapper;
        }

        public IEnumerable<BusDto> GetAllBuses()
        {
            var buses = _busRepository.GetAll();
            return _mapper.Map<IEnumerable<BusDto>>(buses);
        }

        public BusDto GetBusById(int id)
        {
            var bus = _busRepository.Get(id);
            return _mapper.Map<BusDto>(bus);
        }

        public void CreateBus(BusDto busDto)
        {
            var bus = _mapper.Map<Bus>(busDto);
            _busRepository.Create(bus);
        }

        public void UpdateBus(BusDto busDto)
        {
            var bus = _mapper.Map<Bus>(busDto);
            _busRepository.Update(bus);
        }

        public void DeleteBus(int id)
        {
            _busRepository.Delete(id);
        }
    }
}