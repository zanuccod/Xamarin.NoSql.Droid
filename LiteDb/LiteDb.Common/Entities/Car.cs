using System;

namespace LiteDb.Common.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Productor { get; set; }

        public int Year { get; set; }

        public bool Equals(Car item)
        {
            var result = true;
            result &= (Id == item.Id);
            result &= (Model.Equals(item.Model));
            result &= (Productor.Equals(item.Productor));
            result &= (Year == item.Year);
            return result;
        }
    }
}
