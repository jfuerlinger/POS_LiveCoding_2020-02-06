using System;

namespace LiveCoding.Core
{
    public class Pupil
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClassName { get; set; }

        public override string ToString() => $"{Id,3}{LastName,15}{FirstName,15}{ClassName,7}";
    }
}
