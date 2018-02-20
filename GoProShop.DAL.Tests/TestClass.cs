

using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Repositories;
using InMemoryDbSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoProShopTests
{
    [TestClass]
    public class TestClass
    {
        private IBaseRepository<Customer> _baseRepository;

        private InMemoryDbSet<Customer> _customersDbSet;
        private Mock<IGoProShopContext> _contextMock;

        [TestInitialize]
        public void SetUp()
        {
            _contextMock = new Mock<IGoProShopContext>();
            _customersDbSet = new InMemoryDbSet<Customer>();
      
            _contextMock.Setup(x => x.Customers).Returns(_customersDbSet);
            _contextMock.Setup(x => x.Set<Customer>()).Returns(_customersDbSet);
            _baseRepository = new BaseRepository<Customer>(_contextMock.Object);
        }

        [TestMethod]
        public void Create_When_Customer_Is_Not_Null_Returns_Entity()
        {
            // Arrange
            var customer = GetCustomer();

            // Act
            var result = _baseRepository.Create(customer);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_When_Customer_Is_Not_Null_Returns_Null()
        {
            // Act
            var result = _baseRepository.Create(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_When_Customer_Is_Not_Null_Returns_Null()
        {
            // Arrange
            var customer = GetCustomer();
            var entity = _baseRepository.Create(customer);

            // Act
            var result = await _baseRepository.GetAsync(entity.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAll_When_Context_Contains_Customers_Returns_Customer_List()
        {
            // Arrange
            var entity = _baseRepository.Entities.AddRange(GetCustomers());

            // Act
            var result = _baseRepository.GetAll().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
        }


        [TestMethod]
        public async Task Delete_When_Customer_Is_Not_Exists_Removes_Customer()
        {
            // Arrange
            var entity = _baseRepository.Create(GetCustomer());

            // Act
            await _baseRepository.DeleteAsync(entity);
            var result = await _baseRepository.GetAsync(entity.Id);

            // Assert
            Assert.IsNull(result);
        }

        private Customer GetCustomer()
        {
            return new Customer
            {
                Id = 1,
                Name = "testname",
                Address = "testaddress",
                Email = "test@email.com",
                Phone = "testphone"
            };
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                GetCustomer(),
                GetCustomer()
            };
        }
    }
}
