using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_1 {

    enum Education {
        Specialist,
        Bachelor,
        SecondEducation
    };

    interface IDateAndCopy {
        Object DeepCopy();

        System.DateTime Date {
            get;
            set;
        }
    }

    class StudentEnumerator : IEnumerator {
        private ArrayList m_st       = new ArrayList();
        private int       m_position = -1;

        public StudentEnumerator(ArrayList a, ArrayList b) {
            if (a != null) {
                m_st = (ArrayList)a.Clone();
            }

            if (b != null) {
                m_st.AddRange(b);
            }
        }

        public void Reset() {
            m_position = -1;
        }

        public Object Current {
            get {
                return m_st[m_position];
            }
        }

        public bool MoveNext() {
            if (m_position < m_st.Count -1) {
                ++m_position;
                return true;
            } else {
                return false;
            }
        }
    };

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
            get {
                return m_name;
            }

            set {
                m_name = value;
            }
        }

        public string Surname {
            get {
                return m_surname;
            }

            set {
                m_surname = value;
            }
        }

        public System.DateTime Date {
            get {
                return m_date;
            }

            set {
                m_date = value;
            }
        }

        public int Year {
            get {
                return m_date.Year;
            }

            set {
                m_date = new System.DateTime(value, m_date.Month, m_date.Day);
            }
        }

        public override string ToString() {
            return m_name + " " + m_surname + " " + m_date;
        }

        public virtual string ToShortString() {
            return m_name + " " + m_surname;
        }

        public override bool Equals(Object other) {
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

        Object IDateAndCopy.DeepCopy() {
            return new Person(m_name, m_surname, m_date);
        }

        protected string          m_name;
        protected string          m_surname;
        protected System.DateTime m_date;
    };

    class Exam : IDateAndCopy {
        public Exam(string name, int mark, System.DateTime date) {
            Name = name;
            Mark = mark;
            Date = date;
        }

        public Exam() {
            Name = "DefaultExam";
            Mark = 0;
            Date = new System.DateTime(2000, 1, 22);
        }

        public override string ToString() {
            return Name + " " + Mark + " " + Date;
        }

        Object IDateAndCopy.DeepCopy() {
            return new Exam(Name, Mark, Date);
        }

        public string          Name;
        public int             Mark;
        public System.DateTime Date {
            get {
                return m_date;
            }

            set {
                m_date = value;
            }
        }

        private System.DateTime m_date;
    };

    class Test {
        public Test(string title, bool isPass) {
            Name   = title;
            IsPass = isPass;
        }

        public Test() {
            Name = "Math";
            IsPass = true;
        }

        public override string ToString() {
            return Name + " " + IsPass;
        }

        public string Name;
        public bool   IsPass;
    };

    class Student : Person, IDateAndCopy {
        public Student(Person person, Education educ, int group) : base(person.Name, person.Surname, person.Date) {
            m_educ          = educ;
            m_group         = group;
        }

        public Student() : base() {
            m_educ          = Education.Specialist;
            m_group         = 1;
        }

        public Person InfoPerson {
            get {
                return new Person(Name, Surname, Date);
            }

            set {
                Name = value.Name;
                Surname = value.Surname;
                Date = value.Date;
            }
        }

        public Education Educ {
            get {
                return m_educ;
            }

            set {
                m_educ = value;
            }
        }

        public int Group {
            get {
                return m_group;
            }

            set {
                if (value <= 100 || value >= 559) {
                    throw new Exception("value need be more 599 or less 100");
                } else {
                    m_group = value;
                }
            }
        }

        public ArrayList Exams {
            get {
                return m_exams;
            }

            set {
                m_exams = value;
            }
        }

        public ArrayList Tests {
            get {
                return m_tests;
            }

            set {
                m_tests = value;
            }
        }

        public double MiddleMark {
            get {
                double sum = 0;
                int    i   = 0;
                if (m_exams != null) {
                    foreach (Exam el in m_exams) {
                        sum += el.Mark;
                        ++i;
                    }
                }

                return (i == 0 ? 0 : sum / i);
            }
        }

        public void AddExams (params Exam[] exams) {
            if (m_exams == null) {
                m_exams = new ArrayList();
            }
            m_exams.AddRange(exams);
        }

        public void AddTests (params Test[] tests) {
            if (m_tests == null) {
                m_tests = new ArrayList();
            }
            m_tests.AddRange(tests);
        }

        public override string ToString() {
            string a = base.ToString() + " " + m_group;

            if (m_exams != null) {
                foreach(Exam i in m_exams) {
                    a += ("\n" + i);
                }
            }

            if (m_tests != null) {
                foreach(Test i in m_tests) {
                    a += ("\n" + i);
                }
            }

            return a;
        }

        public override string ToShortString() {
            return base.ToString() + " " + m_group + " " + MiddleMark;
        }

        public Object DeepCopy() {
            var newStudent = new Student(new Person(Name, Surname, Date), m_educ, m_group);
            newStudent.Exams = (ArrayList)m_exams.Clone();
            newStudent.Tests = (ArrayList)m_tests.Clone();
            return newStudent;
        }

        public IEnumerator GetEnumerator() {
            return new StudentEnumerator(m_tests, m_exams);
        }

        public IEnumerable SortByMark(int l) {
            foreach(Exam i in m_exams) {
                if (i.Mark > l) {
                    yield return i;
                }
            }
        }

        public IEnumerable GetPassExams() {
            foreach (Exam i in m_exams) {
                if (i.Mark > 2) {
                    yield return i;
                }
            }

            foreach(Test i in m_tests) {
                if (i.IsPass) {
                    yield return i;
                }
            }
        }

        public IEnumerable GetEqualsTestWithExams(bool cond) {
            foreach (Exam i in m_exams) {
                foreach (Test j in m_tests) {
                    if (i.Name == j.Name) {
                        if (!cond || i.Mark > 2) {
                            yield return j;
                        }
                    }
                }
            }
        }

        private Education m_educ;
        private int       m_group;
        private ArrayList m_tests = new ArrayList();
        private ArrayList m_exams = new ArrayList();
    };

    static class Program {
        static void Main(string[] args) {
            var a = new Person("asd", "qwe", new DateTime(2000, 1, 22));
            var b = new Person("asd", "qwe", new DateTime(2000, 1, 22));

            Console.WriteLine(a.Equals(b));
            Console.WriteLine(Object.ReferenceEquals(a, b));
            Console.WriteLine(a.GetHashCode());
            Console.WriteLine(b.GetHashCode());

            // 2
            var c = new Student();
            c.AddExams(
                new Exam("Soc", 2, new DateTime(2000, 1, 22)),
                new Exam("Astr", 5, new DateTime(2000, 1, 22)),
                new Exam("Fis", 10, new DateTime(2000, 1, 22))
            );
            c.AddTests(
                new Test("Soc", true),
                new Test("Astr", true),
                new Test("Fi", false)
            );

            Console.WriteLine("\n2");
            Console.WriteLine(c.ToString());

            // 3
            Console.WriteLine("\n3");
            Console.WriteLine(c.ToShortString());

            // 4
            Console.WriteLine("\n4");
            var c2 = (Student)c.DeepCopy();
            c.Name = "Fafa";
            c.AddExams(new Exam("Soccc", 5, new DateTime(2000, 1, 22)));
            Console.WriteLine(c.ToString());
            Console.WriteLine(c2.ToString());

            // 5
            Console.WriteLine("\n5");
            try {
                c.Group = 1;
            } catch (Exception e) {
                Console.WriteLine(e);
            }

            // 6
            Console.WriteLine("\n6");
            foreach(var el in c) {
                Console.WriteLine(el);
            }

            // 7
            Console.WriteLine("\n7");
            foreach(var el in c.SortByMark(2)) {
                Console.WriteLine(el);
            }

            // 8
            Console.WriteLine("\n8");
            foreach(var el in c.GetEqualsTestWithExams(false)) {
                Console.WriteLine(el);
            }

            // 9
            Console.WriteLine("\n9");
            foreach(var el in c.GetPassExams()) {
                Console.WriteLine(el);
            }

            // 10
            Console.WriteLine("\n10");
            foreach(var el in c.GetEqualsTestWithExams(true)) {
                Console.WriteLine(el);
            }
        }
    };

};
