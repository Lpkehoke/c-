using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_3_2 {
    class Student : Person, IDateAndCopy {
        public Student(Person person, Education educ, int group) : base(person.Name, person.Surname, person.Date) {
            m_educ  = educ;
            m_group = group;
        }

        public Student() : base() {
            var random = new Random();
            int key    = random.Next(0, 3);

            switch (key) {
                case 0:
                    m_educ  = Education.Specialist;
                    break;
                case 1:
                    m_educ  = Education.Bachelor;
                    break;
                case 2:
                    m_educ = Education.SecondEducation;
                    break;
            }
            
            m_group = 1;
        }

        public Person InfoPerson {
            get => new Person(Name, Surname, Date);
            set {
                Name    = value.Name;
                Surname = value.Surname;
                Date    = value.Date;
            }
        }

        public Education Educ {
            get => m_educ;
            set => m_educ = value;
        }

        public int Group {
            get => m_group;
            set => m_group = value;
        }

        public List<Exam> Exams {
            get => m_exams;
            set => m_exams = value;
        }

        public List<Test> Tests {
            get => m_tests;
            set => m_tests = value;
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

        public void AddTests (params Test[] tests) {
            if (m_tests == null) {
                m_tests = new List<Test>();
            }
            m_tests.AddRange(tests);
        }

        public override string ToString() {
            string a = base.ToString() + " " + m_group + " " + m_educ;

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
            return base.ToString() + " " + m_group + " " + MiddleMark + " " + m_educ;
        }

        public object DeepCopy() {
            var newStudent = new Student(new Person(Name, Surname, Date), m_educ, m_group);
            newStudent.Exams = new List<Exam>(m_exams);
            newStudent.Tests = new List<Test>(m_tests);

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

        public void SortOnName() {
            m_exams.Sort();
        }

        public void SortOnMark() {
            var c = new Exam();
            m_exams.Sort(c);
        }

        public void SortOnDate() {
            var c = new ExamOnDate();
            m_exams.Sort(c);
        }

        private Education  m_educ;
        private int        m_group;
        private List<Test> m_tests;
        private List<Exam> m_exams;
    };
};
