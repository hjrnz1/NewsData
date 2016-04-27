using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PriceJob.Model
{
    public class BTCE
    {
        public Btc_Usd btc_usd { get; set; }
    }

    public class Btc_Usd
    {
        [Key]
        public int index { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float avg { get; set; }
        public float vol { get; set; }
        public float vol_cur { get; set; }
        public float last { get; set; }
        public float buy { get; set; }
        public float sell { get; set; }
        public int updated { get; set; }
    }
}

