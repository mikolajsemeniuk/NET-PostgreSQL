using System;

namespace app.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}