using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace lab_1 {

    enum Education {
        Specialist,
        Bachelor,
        SecondEducation
    };

    class Person {
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

        private string          m_name;
        private string          m_surname;
        private System.DateTime m_date;
    };

    class Exam {
        public Exam(string name, int mark, System.DateTime date) {
            Name = name;
            Mark = mark;
            Date = date;
        }

        public Exam() {
            Name = "DefaultExam";
            Mark = 0;
            Date = new DateTime(2000, 1, 22);
        }

        public override string ToString() {
            return Name + " " + Mark + " " + Date;
        }

        public string          Name;
        public int             Mark;
        public System.DateTime Date;
    };

    class Student {
        public Student(Person person, Education educ, int group) {
            m_infoPerson    = person;
            m_educ          = educ;
            m_group         = group;
        }

        public Student() {
            m_infoPerson    = new Person();
            m_educ          = Education.Specialist;
            m_group         = 1;
        }

        public Person InfoPerson {
            get {
                return m_infoPerson;
            }

            set {
                m_infoPerson = value;
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
            get { return m_group; }

            set {
                m_group = value;
            }
        }

        public Exam[] Exams {
            get {
                return m_exams;
            }

            set {
                m_exams = value;
            }
        }

        public double MiddleMark {
            get {
                double sum = 0;
                int    i   = 0;
                if (m_exams != null) {
                    foreach (var el in m_exams) {
                        sum += el.Mark;
                        ++i;
                    }
                }
                return (i == 0 ? 0 : sum / i);
            }
        }

        public bool this[Education ed] {
            get {
                return ed == m_educ;
            }
        }

        public void AddExams (params Exam[] exams) {
            var list = new List<Exam>();
            if (m_exams != null) {
                list.AddRange(m_exams);
            }
            list.AddRange(exams);
            m_exams = list.ToArray();
        }

        public override string ToString() {
            string a = m_infoPerson + " " + m_group;
            if (m_exams != null) {
                foreach(var i in m_exams) {
                    a += ("\n" + i);
                }
            }
            return a;
        }

        public string ToShortString() {
            return m_infoPerson + " " + m_group + " " + MiddleMark;
        }

        private Person    m_infoPerson;
        private Education m_educ;
        private int       m_group;
        private Exam[]    m_exams;
    };

    class Program {
        static void Main(string[] args) {
            var inner = new Person("Name", "Surname", new DateTime(2000, 1, 22));
            var a     = new Student(inner, Education.Bachelor, 999);
            Console.WriteLine(a.ToShortString());

            Console.WriteLine(a[Education.Bachelor]);
            Console.WriteLine(a[Education.Specialist]);
            Console.WriteLine(a[Education.SecondEducation]);

            a.Group = -999;
            Console.WriteLine(a);

            Exam[] exams = {
                new Exam("lala", 2, new DateTime(2000, 1, 20)),
                new Exam("larw", 4, new DateTime(2000, 1, 21)),
                new Exam("lqwr", 3, new DateTime(2000, 1, 22))
            };

            a.AddExams(exams);
            Console.WriteLine(a);
            Console.WriteLine(a.ToShortString());

            // Console.WriteLine("separators: \"@, #, $\"");
            // string input = Console.ReadLine();
            //
            // char[] sep = {'@', '#', '$'};
            // var n_m = input.Split(sep);

            // var n = int.Parse(n_m[0]);
            // var m = int.Parse(n_m[1]);
            var n = 10;
            var m = 10;

            var time1 = Environment.TickCount;
            // first

            var ex1 = new Exam[n*m];
            for (int i = 0; i < n*m; ++i) {
                ex1[i] = new Exam();
                ex1[i].Name = "asd";
                ex1[i].Mark = 2;
            }
            var time2 = Environment.TickCount;
            Console.WriteLine(time2 - time1);
            var time2_1 = Environment.TickCount;
            // second

            var ex2 = new Exam[n, m];
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    ex2[i, j] = new Exam();
                    ex2[i, j].Name = "asd";
                    ex2[i, j].Mark = 2;
                }
            }

            var time3 = Environment.TickCount;
            Console.WriteLine(time3 - time2_1);
            // third

            var ex3 = new Exam[n][];
            var t   = m;
            for (int i = 0; i < n; ++i) {
                if (n % 2 == 1 && i == n - 1) {
                    t = m;
                } else {
                    if (i % 2 == 0) {
                        t = m - 1;
                    } else {
                        t = m + 1;
                    }
                }
                ex3[i] = new Exam[t];
                for (int j = 0; j < t; ++j) {
                    ex3[i][j] = new Exam();
                    ex3[i][j].Name = "asd";
                    ex3[i][j].Mark = 2;
                }
            }

            var time4 = Environment.TickCount;
            Console.WriteLine(time4 - time3);

            time4 = Environment.TickCount;
            Stopwatch st = new Stopwatch();
            st.Start();
            for (int k = 0; k < 10; k++)
            {
                var some = 1 + 2;
            }
            st.Stop();
            var time5 = Environment.TickCount;
            Console.WriteLine(st.Elapsed.Ticks);
            Console.WriteLine(time5 - time4);
            Console.WriteLine(some);
        }
    };

};
