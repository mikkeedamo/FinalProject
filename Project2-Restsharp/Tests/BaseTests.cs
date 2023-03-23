using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2_Restsharp.DataModels;
using Project2_Restsharp.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Restsharp.Tests
{
    public class BaseTests
    {
        public RestClient restClient { get; set; }
        public BookingModels bookingModel { get; set; }

        public BookingHelper bookingHelper { get; set; }

        public string token;

        [TestInitialize]
        public async Task Initialize()
        {
            restClient = new RestClient();
            bookingHelper = new BookingHelper();
            token = await bookingHelper.GetToken(restClient);
        }

    }
}
