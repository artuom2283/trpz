using lab7.EF;
using lab7.Entities;
using lab7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7.Repositories.Implementation
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
