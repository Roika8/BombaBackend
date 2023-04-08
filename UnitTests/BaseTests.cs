using BLL.MainPortfolio.Validators;
using DAL;
using DATA.Instruments;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class BaseTests
    {
        protected MainDataContext _dbContext;
        protected ICommandValidator<PortfolioInstrument> _validator;
        public BaseTests()
        {
            var dbOptions = new DbContextOptionsBuilder<MainDataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new MainDataContext(dbOptions.Options);
            _validator = new PortfolioInstrumentValidator();
        }
        protected static DbSet<T> CreateDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());


            return mockDbSet.Object;
        }
    }
}
