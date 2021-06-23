using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models
{
    public class Customer
    {
        [Key, Column(Order = 0)]
        public int CustomerId { get; set; }
        [Column(Order = 1)]
        public string Name { get; set; }
        [Column(Order = 2)]
        public string FullName { get; set; }
        [Column(Order = 3)]
        public DateTime BirthDate { get; set; }
    }
}