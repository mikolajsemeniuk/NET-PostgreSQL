using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Controllers;
using app.Interfaces;
using app.Payloads;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace unit
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerRepository> _repository;
        private readonly CustomerController _controller;

        public CustomerControllerTest()
        {
            _repository = new Mock<ICustomerRepository>(MockBehavior.Strict);
            _controller = new CustomerController(_repository.Object);
        }

        [Fact]
        public async Task GetCustomersAsync_ShouldReturnCustomers()
        {
            // Arrange
            const int expected = 2;
            var _customers = new[]
            {
                new CustomerPayload
                {
                    Id = 2,
                    Name = "john",
                    FullName = "mock",
                    BirthDate = DateTime.Now
                },
                new CustomerPayload
                {
                    Id = 3,
                    Name = "john",
                    FullName = "doe",
                    BirthDate = DateTime.Now
                }
            };
            _repository.Setup(_ => _.GetCustomersAsync()).ReturnsAsync(_customers);

            // Act
            var actionResult = await _controller.GetCustomersAsync();
            var actionObject = actionResult.Result as OkObjectResult;
            var customers = actionObject.Value as IEnumerable<CustomerPayload>;
            var result = customers.ToList<CustomerPayload>().Count;

            // Assert
            Assert.True(result == expected);
        }

        [Fact]
        public async Task GetCustomerAsync_ShouldReturnCustomer()
        {
            // Assert
            const int id = 2;
            var expected = new CustomerPayload
            {
                Id = 2,
                Name = "john",
                FullName = "mock",
                BirthDate = DateTime.Now
            };
            _repository.Setup(_ => _.GetCustomerAsync(id)).ReturnsAsync(expected);

            // Act
            var actionResult = await _controller.GetCustomerAsync(id);
            var actionObject = actionResult.Result as OkObjectResult;
            var result = actionObject.Value as CustomerPayload;

            // Assert
            Assert.True(result.Equals(expected));
        }

        [Fact]
        public async Task GetCustomerAsync_ShouldThrowException()
        {
            // Arrange
            const int customerId = 4;
            _repository.Setup(_ => _.GetCustomerAsync(It.IsAny<int>())).Throws(new Exception("something"));

            // Act
            Func<Task> act = () => _controller.GetCustomerAsync(customerId);

            // Assert
            await Assert.ThrowsAsync<Exception>(act);
        }
    }
}

