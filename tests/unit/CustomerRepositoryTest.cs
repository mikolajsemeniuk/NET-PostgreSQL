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
    public class CustomerRepositoryTest
    {
        private readonly Mock<ICustomerRepository> _repository;
        private readonly CustomerController _controller;

        public CustomerRepositoryTest()
        {
            _repository = new Mock<ICustomerRepository>();
            _controller = new CustomerController(_repository.Object);
        }

        [Fact]
        public async Task GetCustomersAsync()
        {
            // Arrange
            var expected = 1;
            var _customers = new[]
            {
                new CustomerPayload
                {
                    Id = 2,
                    Name = "john",
                    FullName = "mock",
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
    }
}

