using Project2_Restsharp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Restsharp.Tests.TestData
{
    public class GenerateBooking
    {

        public static BookingModels newBooking()
        {
            return new BookingModels
            {
                Firstname = "NoobFromUA",
                Lastname = "NoobFromUA",
                Totalprice = 9123,
                Depositpaid = true,
                Bookingdates = new Bookingdates
                {
                    Checkin = "2024-06-29",
                    Checkout = "2024-06-29"
                },
                Additionalneeds = "Breakfast"

            };
        }
    }
}
