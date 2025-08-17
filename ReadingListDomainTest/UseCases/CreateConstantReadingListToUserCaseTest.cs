using Microsoft.Extensions.Logging;
using Moq;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingListDomainTest.UseCases
{
    public class CreateConstantReadingListToUserCaseTest
    {

        private readonly Mock<ILogger<CreateConstantReadingListToUserCase>> _logger;
        private readonly Mock<ICreateReadingListUnit> _createReadingListUnit;
        private readonly CreateConstantReadingListToUserCase _createConstantReadingListToUserCase;

        public CreateConstantReadingListToUserCaseTest() 
        {
            _logger = new Mock<ILogger<CreateConstantReadingListToUserCase>>();
            _createReadingListUnit = new Mock<ICreateReadingListUnit>();
            _createConstantReadingListToUserCase = new CreateConstantReadingListToUserCase(_logger.Object, _createReadingListUnit.Object);


        }

       
    }
}
