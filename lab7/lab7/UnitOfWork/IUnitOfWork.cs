using lab7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBusRepository Busses { get; }
        IUserRepository Users { get; }
        void Save();
    }
}
