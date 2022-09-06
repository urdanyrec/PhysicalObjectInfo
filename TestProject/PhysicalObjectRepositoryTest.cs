using Xunit;
using PhysicalObjectInfo.Domain;

namespace TestProject
{
    public class PhysicalObjectRepositoryTest
    {
        [Fact]
        public void TestAdd()
        {
            var testHelper = new TestHelper();
            var physicalobjectRepository = testHelper.physicalobjectRepository;

            PhysicalObject physicalobject = new PhysicalObject { Id = System.Guid.NewGuid(), ObjectId = System.Guid.NewGuid(), Series = "238", State = "On", URL = "uraura", ObjectTechId = System.Guid.Empty};
            physicalobjectRepository.AddAsyncPhObject(physicalobject).Wait();

            Assert.Equal("uraura", physicalobjectRepository.GetByURLAsync("uraura").Result.URL);
            Assert.True(physicalobjectRepository.GetAllAsyncPhObject().Result.Count == 1);
            Assert.Equal("238", physicalobjectRepository.GetByURLAsync("uraura").Result.Series);
        }
        [Theory]
        [InlineData("192.168.1.3", "192.168.1.3")]
        [InlineData("192.168.1.43", "192.168.1.43")]
        [InlineData("192.168.3.3", "192.168.3.3")]
        public void TestMultipleAdd(string url, string expected)
        {
            var testHelper = new TestHelper();
            var physicalobjectRepository = testHelper.physicalobjectRepository;
            
            PhysicalObject physicalobject = new PhysicalObject { Id = System.Guid.NewGuid(), ObjectId = System.Guid.NewGuid(), Series = "238", State = "On", URL = url, ObjectTechId = System.Guid.Empty };
            physicalobjectRepository.AddAsyncPhObject(physicalobject).Wait();

            Assert.Equal(expected, physicalobjectRepository.GetByURLAsync(url).Result.URL);
        }
    }
}