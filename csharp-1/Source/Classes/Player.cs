using System;
namespace Source.Classes
{
    public class Player
    {
        public long TeamId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int SkillLevel { get; set; }
        public decimal Salary { get;  set; }
        public bool Captain { get; set; }
        public long Id { get;  set; }
    }
}
