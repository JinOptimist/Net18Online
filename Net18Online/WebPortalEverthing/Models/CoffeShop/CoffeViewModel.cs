﻿using WebPortalEverthing.Models.Brand;

namespace WebPortalEverthing.Models.CoffeShop
{
    public class CoffeViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Url { get; set; }

        public string Coffe { get; set; }

        public decimal Cost { get; set; }
    }
}