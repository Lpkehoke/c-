using System;

namespace lab_3_2 {
    class Person : IDateAndCopy {
        public Person(string name, string surname, System.DateTime date) {
            m_name    = name;
            m_surname = surname;
            m_date    = date;
        }

        public Person() {
            m_name    = "DefaultName";
            m_surname = "DefaultSurname";
            m_date    = new DateTime(2000, 1, 22);
        }

        public string Name {
            get => m_name;
            set => m_name = value;
        }

        public string Surname {
            get => m_surname;
            set => m_surname = value;
        }

        public System.DateTime Date {
            get => m_date;
            set => m_date = value;
        }

        public int Year {
            get => m_date.Year;
            set => m_date = new System.DateTime(value, m_date.Month, m_date.Day);
        }

        public override string ToString() {
            return m_name + " " + m_surname + " " + m_date;
        }

        public virtual string ToShortString() {
            return m_name + " " + m_surname;
        }

        public override bool Equals(object other) {
            var p = (Person) other;
            return (p.m_name == m_name && p.m_surname == m_surname && m_date == p.m_date);
        }

        public static bool operator==(Person p1, Person p2) {
            return p1.Equals(p2);
        }

        public static bool operator!=(Person p1, Person p2) {
            return !(p1 == p2);
        }

        public override int GetHashCode() {
            return (m_name.GetHashCode() + m_surname.GetHashCode() + m_date.GetHashCode());
        }

        object IDateAndCopy.DeepCopy() {
            return new Person(m_name, m_surname, m_date);
        }

        protected string          m_name;
        protected string          m_surname;
        protected System.DateTime m_date;
    };
};
