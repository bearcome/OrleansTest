using KWKY.WebClient.Controllers;
using Moq;
using Orleans;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitClientTest;

namespace XUnitTest
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：ValuesControllerTest
	// 文件功能描述：
	//
	// 
	// 创建者：杨明
	// 时间：2019/5/15 10:29:47
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion

    [Collection(ClientCollection.Name)]
    public class ValuesControllerTest
    {
        public HttpClient Client { get; }
        public ValuesControllerTest (ClientFixture clientFixture)
        {
            Client = clientFixture.Client;
        }

        [Theory]
        [InlineData("/api/values/",1)]
        [InlineData("/api/values/",2)]
        public async void GetTest (string uri,int id)
        {
            Console.WriteLine(uri);

            HttpResponseMessage res = await Client.DeleteAsync(uri+id);
            res.EnsureSuccessStatusCode();
            string strRes = await res.Content.ReadAsStringAsync();
            Assert.True(int.TryParse(strRes, out int resInt));
            Assert.Equal(resInt, id*100);
        }



        [Fact]
        public async Task DeleteTest ()
        {
            // Arrange
            var mockRepo = new Mock<IClusterClient>();
            //mockRepo.Setup(repo => repo.ListAsync())
            //    .ReturnsAsync(GetTestSessions());
            var controller = new ValuesController(mockRepo.Object);
            // Act
            var res = await controller.Delete(1);
            
            // Assert
            Assert.Equal(res, 1 * 100);

        }
       
    }
}
