using System;
using System.Collections.Generic;
using System.Text;

namespace Source
{
    public class Team
    {
        public long ID;
        public string Name;
        public DateTime CreateDate;
        public string MainShirtColor;
        public string SecondaryShirtColor;

        
        public Team(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            ID = id;

            if (name.Equals(null)) throw new ArgumentNullException("name is null.");
            Name = name;

            if(createDate.Equals(null)) throw new ArgumentNullException("createDate is null.");
            CreateDate = createDate;

            if (mainShirtColor.Equals(null)) throw new ArgumentNullException("mainShirtColor is null.");
            MainShirtColor = mainShirtColor;

            if (secondaryShirtColor.Equals(null)) throw new ArgumentNullException("secondaryShirtColor is null.");
            SecondaryShirtColor = secondaryShirtColor;
        }
     
    }
}
