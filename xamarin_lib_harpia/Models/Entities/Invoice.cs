using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Invoice
    {
        public List<string> Content { get; set; }

        public Invoice(List<string> content)
        {
            Content = content;
        }
    }
}
