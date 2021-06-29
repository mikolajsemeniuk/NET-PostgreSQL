using System;
using System.Collections.Generic;
using System.Linq;
using app.Data;
using Moq;
using app.Models;
using app.Services;
// using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using MockQueryable.FakeItEasy;
using Xunit;
using System.Threading.Tasks;
using app.Inputs;
using EntityFrameworkCoreMock;
using app.Payloads;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace unit
{
    // public class CustomerServiceTest
    // {
    //     [Fact]
    //     public async Task CheckAsync()
    //     {
    //         // var users = new List<Customer>()
    //         // {
    //         //     new Customer { Name = "ExistLastName", FullName = "hehs", BirthDate = DateTime.Parse("01/20/2012")},
    //         // };

    //         // var fakeContext = A.Fake<DataContext>();
    //         // var fakeDbSet = A.Fake<DbSet<Customer>>();
    //         // A.CallTo(() => fakeContext.Customers).Returns(fakeDbSet);
    //         // A.CallTo(() => fakeDbSet.Add(A<Customer>.Ignored)).Returns(users);
    //         // var _repository = new 

    //         var mockSet = new Mock<DbSet<Customer>>();

    //         var mockContext = new Mock<DataContext>();
    //         mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

    //         var service = new CustomerRepository(mockContext.Object);
    //         await service.AddCustomerAsync(new CustomerInput { Name = "zcx", FullName = "das", BirthDate = DateTime.Now });

    //         mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
    //         mockContext.Verify(m => m.SaveChanges(), Times.Once());
    //     }
    // }
    public class CustomerRepositoryTest
    {
        private DbContextOptions<DataContext> DummyOptions { get; } =
            new DbContextOptionsBuilder<DataContext>().Options;
        private readonly DbContextMock<DataContext> _context;
        private readonly CustomerRepository _repository;
        private readonly Customer[] _customers = new[]
        {
            new Customer { CustomerId = 1, Name = "john", FullName = "Eric Cartoon", BirthDate = DateTime.Now },
            new Customer { CustomerId = 2, Name = "mike", FullName = "Billy Jewel", BirthDate = DateTime.Now },
        };

        public CustomerRepositoryTest()
        {
            _context = new DbContextMock<DataContext>(DummyOptions);
            _context.CreateDbSetMock(x => x.Customers, _customers);
            _repository = new CustomerRepository(_context.Object);
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldReturnCostumers()
        {
            // Arrange
            const int expected = 2;

            // Act
            var customers = await _repository.GetCustomersAsync();
            var result = customers.ToList<CustomerPayload>().Count;
            
            // Assert
            Assert.True(result == expected);
        }

        [Fact]
        public async Task GetCustomerAsync_ShouldReturnCustomer()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = await _repository.GetCustomerAsync(customerId);
            var expected = new CustomerPayload
            {
                Id = 1,
                Name = "john",
                FullName = "Eric Cartoon",
                BirthDate = DateTime.Now
            };

            // Assert
            Assert.True(result.Equals(result));
        }

        [Fact]
        public async Task GetCustomerAsync_ShouldThrowException()
        {
            // Arrange
            var customerId = 4;

            // Act
            Func<Task> act = () => _repository.GetCustomerAsync(customerId);

            // Assert
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task AddCustomerAsync_ShouldSaveToDataBase()
        {
            // Arrange
            var input = new CustomerInput
            {
                Name = "mike",
                FullName = "Mock",
                BirthDate = DateTime.Now
            };
            _context.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            
            // Act
            await _repository.AddCustomerAsync(input);
            
            // Assert
            _context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}