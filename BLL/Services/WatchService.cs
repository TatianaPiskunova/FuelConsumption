using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class WatchService:IWatchService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Watch> _watchRepositry;

        public WatchService(IMapper carMapper,
            IRepository<Watch> watchRepositry)
        {

            _mapper = carMapper;
            _watchRepositry = watchRepositry;
        }

        public List<WatchDTO> GetAll()
        {
            return _mapper
                .Map<IQueryable<Watch>, List<WatchDTO>>(_watchRepositry.GetAll());

        }
    }
}
