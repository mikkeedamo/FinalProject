using Project1.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Tests.TestData
{
    public class GenerateBooking
    {

        public static BookingModels newBooking()
        {
            return new BookingModels
            {
                Firstname = "Mikkee",
                Lastname = "Damo",
                Totalprice = 5099,
                Depositpaid = true,
                Bookingdates = new Bookingdates
                {
                    Checkin = "2023-03-19",
                    Checkout = "2023-03-20"
                },
                Additionalneeds = "Breakfast"

            };
        }
    }
}
