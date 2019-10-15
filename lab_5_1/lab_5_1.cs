using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab_5_1 {
    enum Education {
        Specialist,
        Bachelor,
        SecondEducation
    };

    [Serializable()]
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

        protected string          m_name;
        protected string          m_surname;
        protected System.DateTime m_date;
    };

    [Serializable()]
    class Exam {
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

    [Serializable()]
    class Student : Person {
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
                m_group = value;
            }
        }

        public List<Exam> Exams {
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
                m_exams = new List<Exam>();
            }
            m_exams.AddRange(exams);
        }

        public override string ToString() {
            string a = base.ToString() + " " + m_group;

            if (m_exams != null) {
                foreach(Exam i in m_exams) {
                    a += ("\n" + i);
                }
            }

            return a;
        }

        public override string ToShortString() {
            return base.ToString() + " " + m_group + " " + MiddleMark;
        }

        public Student Deepcopy() {
            using (var ms = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Student) formatter.Deserialize(ms);
            }
        }

        public bool Save(string fn) {
            return Student.Save(fn, this);
        }

        public bool Load(string fn) {
            return Student.Load(fn, this);
        }

        public bool AddFromConsole() {
            Console.WriteLine("Enter title(string)$mark(int)$date(year(int)$month(int)$day(int))");
            string input = Console.ReadLine();
            char[] sep = {'$'};
            var par = input.Split(sep);
            try {
                this.AddExams(new Exam(par[0], int.Parse(par[1]), new DateTime(int.Parse(par[2]), int.Parse(par[3]), int.Parse(par[4]))));
                return true;
            } catch {
                Console.WriteLine("Wrong input!");
                return false;
            }
        }

        public static bool Save(string fn, Student obj) {
            try {
                using (var fs = new FileStream(fn, FileMode.Create)) {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                    return true;
                }
            } catch {
                return false;
            }
        }

        public static bool Load(string fn, Student obj) {
            try {
                if (File.Exists(fn)) {
                    using (var fs = new FileStream(fn, FileMode.Open)) {
                        var deserializer = new BinaryFormatter();
                        var a = (Student) deserializer.Deserialize(fs);
                        obj.Assign(a);
                        return true;
                    }
                } else {
                    return false;
                }
            } catch {
                return false;
            }
        }

        public void Assign(Student a) {
            this.m_exams = a.m_exams;
        }

        private Education  m_educ;
        private int        m_group;
        private List<Exam> m_exams;
    };

    class Programm {
        static void Main(string[] args) {
            Console.WriteLine("\n#############1#############");
            var c1 = new Student();
            c1.AddExams(
                new Exam("Sawdoca", 1, new DateTime(2000, 1, 22)),
                new Exam("Asdwatr", 5, new DateTime(2020, 1, 22))
            );

            var c2 = c1.Deepcopy();
            Console.WriteLine(c1 + "\n");
            Console.WriteLine(c2);


            Console.WriteLine("\n#############2, 3#############");
            Console.WriteLine("Enter name file.");
            string name = Console.ReadLine();
            var a = new Student();
            if (File.Exists(name)) {
                a.Load(name);
                Console.WriteLine("obj from file");
                Console.WriteLine(a);
            } else {
                Console.WriteLine("File not exist, creating.");
                var f = File.Create(name);
                f.Close();
            }

            Console.WriteLine("\n#############4#############");
            a.AddFromConsole();
            Console.WriteLine(a);
            a.Save(name);

            Console.WriteLine("\n#############5#############");
            a.Load(name);
            a.AddFromConsole();
            a.Save(name);

            Console.WriteLine("\n#############6#############");
            Console.WriteLine(a);
        }
    };
};
