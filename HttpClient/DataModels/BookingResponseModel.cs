using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataModels
{
    public class BookingResponseModel
    {
        [JsonProperty("bookingid")]
        public long Bookingid { get; set; }

        [JsonProperty("booking")]
        public BookingModels BookingDetails { get; set; }
    }
}
