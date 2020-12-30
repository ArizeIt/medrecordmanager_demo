using AdvancedMDDomain.DTOs.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PVAMCommon;

namespace AdvancedMDServiceTest
{
    [TestClass]
    public class DTOSerializationTests
    {
        [TestInitialize]
        public void Initialize()
        {

        }
        [TestMethod]
        public string CanSerializAddVisit()
        {
            var addvisitrequest = new PpmAddVisitRequest();
            var xml = addvisitrequest.Serialize();
            return xml;
        }
    }
}
