using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project2_Restsharp.DataModels;
using Project2_Restsharp.Helpers;
using Project2_Restsharp.Resources;
using Project2_Restsharp.Tests;
using Project2_Restsharp.Tests.TestData;
using RestSharp;
using System.Collections.Generic;
using System.Net;
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
                var deleteResponse = await bookingHelper.DeleteBooking(restClient, data.Bookingid, token);

            }

        }

        [TestMethod]
        public async Task Validate_CreateBooking()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<BookingResponseModel>(response.Content);

            cleanUpList.Add(newBooking);

            //Act
            var getBooking = await bookingHelper.GetBookingById(restClient, newBooking.Bookingid);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(newBooking.BookingDetails.Firstname, getBooking.Firstname);
            Assert.AreEqual(newBooking.BookingDetails.Lastname, getBooking.Lastname);
            Assert.AreEqual(newBooking.BookingDetails.Totalprice, getBooking.Totalprice);
            Assert.AreEqual(newBooking.BookingDetails.Depositpaid, getBooking.Depositpaid);
            Assert.AreEqual(newBooking.BookingDetails.Bookingdates.Checkout, getBooking.Bookingdates.Checkout);
            Assert.AreEqual(newBooking.BookingDetails.Bookingdates.Checkin, getBooking.Bookingdates.Checkin);


        }

        [TestMethod]
        public async Task Validate_UpdateBooking()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<BookingResponseModel>(response.Content);
            cleanUpList.Add(newBooking);

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


            //Act
            var updateResponse = await bookingHelper.UpdateBookingById(restClient, newBooking.Bookingid, updateBooking, token);
            var updateData = JsonConvert.DeserializeObject<BookingModels>(updateResponse.Content);

      
            var getBooking = await bookingHelper.GetBookingById(restClient, newBooking.Bookingid);

            //Assert
            Assert.AreEqual(updateResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(updateData.Firstname, getBooking.Firstname);
            Assert.AreEqual(updateData.Lastname, getBooking.Lastname);
            Assert.AreEqual(updateData.Totalprice, getBooking.Totalprice);
            Assert.AreEqual(updateData.Depositpaid, getBooking.Depositpaid);
            Assert.AreEqual(updateData.Bookingdates.Checkout, getBooking.Bookingdates.Checkout);
            Assert.AreEqual(updateData.Bookingdates.Checkin, getBooking.Bookingdates.Checkin);
        }


        [TestMethod]
        public async Task Validate_DeleteBooking()
        {

            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<BookingResponseModel>(response.Content);

            //Act
            var deleteData = await bookingHelper.DeleteBooking(restClient, newBooking.Bookingid, token);

            //Assert
            Assert.AreEqual(deleteData.StatusCode, HttpStatusCode.Created);




        }

        [TestMethod]
        public async Task Validate_GetBooking()
        {
            //Arrange
            var request = new RestRequest(Endpoints.GetBookingById(-31233))
               .AddHeader("Accept", "application/json");

            //Act
            var response = await restClient.ExecuteGetAsync<BookingModels>(request);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

    }
}