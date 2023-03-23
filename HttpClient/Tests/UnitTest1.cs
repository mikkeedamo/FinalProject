using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project1.DataModels;
using Project1.Helpers;
using Project1.Resources;
using Project1.Tests;
using Project1.Tests.TestData;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    [TestClass]
    public class UnitTest1 : BaseTests
    {
        private readonly List<BookingResponseModel> cleanUpList = new List<BookingResponseModel>();
        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Delete, Endpoints.DeleteBookingById(data.Bookingid), null, token);

            }

        }

        [TestMethod]
        public async Task Validate_CreateBooking()
        {
            //Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Post, Endpoints.CreateBooking(), GenerateBooking.newBooking());
            var postData = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponse.Content.ReadAsStringAsync().Result);
            cleanUpList.Add(postData);

            //Act
            var getResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Get, Endpoints.GetBookingById(postData.Bookingid));
            var getData = JsonConvert.DeserializeObject<BookingModels>(getResponse.Content.ReadAsStringAsync().Result);


            //Assert
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK );
            Assert.AreEqual(postData.BookingDetails.Firstname, getData.Firstname);
            Assert.AreEqual(postData.BookingDetails.Lastname, getData.Lastname);
            Assert.AreEqual(postData.BookingDetails.Totalprice, getData.Totalprice);
            Assert.AreEqual(postData.BookingDetails.Depositpaid, getData.Depositpaid);
            Assert.AreEqual(postData.BookingDetails.Bookingdates.Checkout, getData.Bookingdates.Checkout);
            Assert.AreEqual(postData.BookingDetails.Bookingdates.Checkin, getData.Bookingdates.Checkin); ;


        }

        [TestMethod]
        public async Task Validate_UpdateBooking()
        {
            //Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Post, Endpoints.CreateBooking(), GenerateBooking.newBooking());
            var postData = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponse.Content.ReadAsStringAsync().Result);
            cleanUpList.Add(postData);

            var updateBooking = new BookingModels()
            {
                Firstname = "Michael",
                Lastname = "Jordan",
                Totalprice = 5099,
                Depositpaid = true,
                Bookingdates = new Bookingdates
                {
                    Checkin = "2023-03-19",
                    Checkout = "2023-03-20"
                },
                Additionalneeds = "Breakfast"

            };

            var updateResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Put, Endpoints.UpdateBookingById(postData.Bookingid), updateBooking, token);
            var updateData = JsonConvert.DeserializeObject<BookingModels>(updateResponse.Content.ReadAsStringAsync().Result);


            //Act
            var getResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Get, Endpoints.GetBookingById(postData.Bookingid));
            var getData = JsonConvert.DeserializeObject<BookingModels>(getResponse.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(updateResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(updateData.Firstname, getData.Firstname);
            Assert.AreEqual(updateData.Lastname, getData.Lastname);
            Assert.AreEqual(updateData.Totalprice, getData.Totalprice);
            Assert.AreEqual(updateData.Depositpaid, getData.Depositpaid);
            Assert.AreEqual(updateData.Bookingdates.Checkout, getData.Bookingdates.Checkout);
            Assert.AreEqual(updateData.Bookingdates.Checkin, getData.Bookingdates.Checkin); ;
        }

        [TestMethod]
        public async Task Validate_DeleteBooking()
        {
            //Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Post, Endpoints.CreateBooking(), GenerateBooking.newBooking());
            var postData = JsonConvert.DeserializeObject<BookingResponseModel>(httpResponse.Content.ReadAsStringAsync().Result);

            //Act
            var deleteResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Delete, Endpoints.DeleteBookingById(postData.Bookingid), null, token);


            //Assert
            Assert.AreEqual(deleteResponse.StatusCode, HttpStatusCode.Created);
            

        }

        [TestMethod]
        public async Task Validate_GetBooking()
        {

            var getResponse = await bookingHelper.SendAsyncFunction(httpClient, HttpMethod.Get, Endpoints.GetBookingById(-4231));

            Assert.AreEqual(getResponse.StatusCode, HttpStatusCode.NotFound);
        }

    }
}