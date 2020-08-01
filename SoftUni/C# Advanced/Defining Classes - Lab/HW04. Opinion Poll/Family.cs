using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        public List<Person> People { get; set; }
        public void AddMember(Person member)
        {
            People.Add(member);
        }

        public Person GetOldestMember()
        {
            int oldestMember = 0;
            int oldestMemberIndex = 0;
            for (int i = 0; i < People.Count; i++)
            {
                Person person = People[i];
                if (person.Age > oldestMember)
                {
                    oldestMember = person.Age;
                    oldestMemberIndex = i;
                }
            }

            return People[oldestMemberIndex];
        }
    }
}
