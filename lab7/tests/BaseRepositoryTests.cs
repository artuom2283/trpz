using System;
using System.Collections.Generic;
using System.Linq;
using lab7.EF;
using lab7.Entities;
using lab7.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests
{
    public class BaseRepositoryTests
    {
        // Helper method to create an in-memory database context
        public DatabaseContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DatabaseContext(options);
        }

        [Fact]
        public void Create_AddsBusToDbSet()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repository = new BusRepository(context);
            var newBus = new Bus { Id = 1, BusNumber = "TestBus" };

            // Act
            repository.Create(newBus);
            context.SaveChanges();

            // Assert
            Assert.Single(context.Set<Bus>());
            Assert.Equal("TestBus", context.Set<Bus>().Single().BusNumber);
        }

        [Fact]
        public void Delete_RemovesUserFromDbSet()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            context.Set<User>().AddRange(
                new User { Id = 1, Name = "User1" },
                new User { Id = 2, Name = "User2" }
            );
            context.SaveChanges();

            var repository = new UserRepository(context);

            // Act
            repository.Delete(1);
            context.SaveChanges();

            // Assert
            Assert.Single(context.Set<User>());
            Assert.Null(context.Set<User>().Find(1));
        }

        [Fact]
        public void Find_ReturnsFilteredAndPagedBuses()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repository = new BusRepository(context);

            // Add test data
            context.Busses.AddRange(
                new Bus { Id = 1, BusNumber = "Bus1", Capacity = 50 },
                new Bus { Id = 2, BusNumber = "Bus2", Capacity = 30 },
                new Bus { Id = 3, BusNumber = "Bus3", Capacity = 20 },
                new Bus { Id = 4, BusNumber = "Bus4", Capacity = 40 }
            );
            context.SaveChanges();

            // Define filter and pagination
            Func<Bus, bool> predicate = bus => bus.Capacity > 25; // Filter buses with Capacity > 25
            int pageIndex = 0;
            int pageSize = 2;

            // Act
            var result = repository.Find(predicate, pageIndex, pageSize);

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            Assert.Equal(2, result.Count()); // Expect 2 buses in the filtered and paginated result
            Assert.All(result, bus => Assert.True(bus.Capacity > 25)); // Ensure all buses match the filter
        }

        [Fact]
        public void Get_ReturnsUserById()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            context.Set<User>().AddRange(
                new User { Id = 1, Name = "User1" },
                new User { Id = 2, Name = "User2" }
            );
            context.SaveChanges();

            var repository = new UserRepository(context);

            // Act
            var result = repository.Get(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User2", result.Name);
        }

        [Fact]
        public void GetAll_ReturnsAllBuses()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            context.Set<Bus>().AddRange(
                new Bus { Id = 1, BusNumber = "Bus1" },
                new Bus { Id = 2, BusNumber = "Bus2" },
                new Bus { Id = 3, BusNumber = "Bus3" }
            );
            context.SaveChanges();

            var repository = new BusRepository(context);

            // Act
            var results = repository.GetAll().ToList();

            // Assert
            Assert.Equal(3, results.Count);
            Assert.Contains(results, b => b.BusNumber == "Bus1");
            Assert.Contains(results, b => b.BusNumber == "Bus2");
            Assert.Contains(results, b => b.BusNumber == "Bus3");
        }

        [Fact]
        public void Update_ChangesUserEntityStateToModified()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repository = new UserRepository(context);

            // Add a user to the context
            var user = new User { Id = 1, Name = "John Doe" };
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            var updatedUser = new User { Id = 1, Name = "Updated Name" };

            // Detach the existing tracked entity (avoids tracking conflict)
            var trackedUser = context.ChangeTracker.Entries<User>()
                                      .FirstOrDefault(e => e.Entity.Id == user.Id)?.Entity;
            if (trackedUser != null)
            {
                context.Entry(trackedUser).State = EntityState.Detached;
            }

            repository.Update(updatedUser);

            // Assert
            var result = context.Users.First(u => u.Id == 1);
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }
    }
}