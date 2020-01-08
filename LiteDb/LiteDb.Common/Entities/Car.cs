using System;
using SQLite;

namespace LiteDb.Common.Entities
{
    public class Car : IEquatable<Car>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Model { get; set; }

        public string Productor { get; set; }

        public int Year { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }

            return Equals(obj as Car);
        }

        public bool Equals(Car item)
        {
            if (item != null)
            {
                bool result = true;
                result &= (Id == item.Id);
                result &= string.Compare(Model, item.Model, StringComparison.Ordinal) == 0;
                result &= string.Compare(Productor, item.Productor, StringComparison.Ordinal) == 0;
                result &= (Year == item.Year);

                return result;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return new { Id, Model, Productor, Year }.GetHashCode();
        }
    }
}
