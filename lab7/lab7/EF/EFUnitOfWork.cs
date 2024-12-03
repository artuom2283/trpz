using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab7.Repositories.Implementation;
using lab7.Repositories.Interfaces;
using lab7.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace lab7.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DatabaseContext db;
        private BusRepository busRepository;
        private UserRepository userRepository;
        public EFUnitOfWork(DbContextOptions options)
        {
            db = new DatabaseContext(options);
        }
        public IBusRepository Busses
        {
            get
            {
                if (busRepository == null)
                    busRepository = new BusRepository(db);
                return busRepository;
            }
        }
        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
