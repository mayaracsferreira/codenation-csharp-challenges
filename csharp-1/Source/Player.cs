using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    class Player
    {
        public long ID;
        public long TeamID;
        public string Name;
        public DateTime BirthDate;
        public int SkillLevel;
        public decimal Salary;

        public Player(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            ID = id;
            TeamID = teamId;

            if (name.Equals(null)) throw new ArgumentNullException("name is null.");
            Name = name;

            if (birthDate.Equals(null)) throw new ArgumentNullException("birthDate is null.");
            BirthDate = birthDate;

            SkillLevel = skillLevel;            
            Salary = salary;
        }

    }
}
