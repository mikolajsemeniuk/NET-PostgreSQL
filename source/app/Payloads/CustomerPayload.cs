using System;

namespace app.Payloads
{
    public class CustomerPayload
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}