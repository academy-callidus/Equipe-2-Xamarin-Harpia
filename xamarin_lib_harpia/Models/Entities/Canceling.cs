using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Canceling
    {
        public string Nsu { get; set; }
        public int Code { get; set; }
        public string Date { get; set; }
        public float Price { get; set; }

        public Canceling(string nsu, int code, string date, float price)
        {
            Nsu = nsu;
            Code = code;
            Date = date;
            Price = price;
        }
    }
}
