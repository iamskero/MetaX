using System;

namespace MetaX.Models
{
    public class Order
    {
        private string _id { get; set; }


        /// <summary>
        /// faking id since they were omitted in the data
        /// no collisions assumed
        /// </summary>
        public string Id
        {
            get
            {
                return _id.ToString();
            }
            set { this._id = Guid.NewGuid().ToString(); }
        }

        public DateTime Time { get; set; }

        public string Kind { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        // circ ref todo: possible other ways..more clunky but more "economic"
        public Exchange ownerExchange { get; set; }
    }
}
