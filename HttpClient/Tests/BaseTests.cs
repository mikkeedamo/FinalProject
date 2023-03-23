using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project1.DataModels;
using Project1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Tests
{
    public class BaseTests
    {
        public HttpClient httpClient { get; set; }
        public BookingModels bookingModel { get; set; }

        public BookingHelper bookingHelper { get; set; }

        public string token;

        [TestInitialize]
        public async Task Initialize()
        {
            httpClient = new HttpClient();
            bookingHelper = new BookingHelper();
            token = await bookingHelper.GetToken(httpClient);
        }

    }
}
