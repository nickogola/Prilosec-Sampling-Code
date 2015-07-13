﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POTC.Models
{
    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }

        public Address() { }
    }
}