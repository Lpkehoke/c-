using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace lab_4_2 {
    delegate TKey KeySelector<TKey>(Student st);
    delegate void StudentChangedHandler<TKey> (object source, StudentsChangedEventArgs<TKey> args);

    enum Education {
        Specialist,
        Bachelor,
        SecondEducation
    };

    enum Action {
        Add,
        Remove,
        Property
    };

    class StudentsChangedEventArgs<TKey> : EventArgs {
        public string NameCollection;
        public Action Info;
        public string PropertyName;
        public TKey   Key;

        public StudentsChangedEventArgs(string name, Action act, string prop, TKey key) {
            NameCollection = name;
            Info           = act;
            PropertyName   = prop;
            Key            = key;
        }

        public override string ToString() {
            return NameCollection + " " + PropertyName;
        }
    };

    class StudentCollection<TKey> {
        public event StudentChangedHandler<TKey> StudentsChanged;

        public StudentCollection(KeySelector<TKey> f, string name) {
            m_getKey = f;
            m_dict = new Dictionary<TKey, Student>();
            Name = name;
        }

        public void AddStudents(params Student[] st) {
            foreach(Student i in st) {
                m_dict.Add(m_getKey(i), i);
                StudentsChanged(this, new StudentsChangedEventArgs<TKey>(
                    Name,
                    Action.Add,
                    "",
                    m_getKey(i)
                ));

                i.PropertyChanged += PropertyHandler;
            }
        }

        public bool Remove(TKey k) {
            if (m_dict[k] != null) {
                m_dict[k].PropertyChanged -= PropertyHandler;
                m_dict.Remove(k);
                StudentsChanged(this, new StudentsChangedEventArgs<TKey>(
                    Name,
                    Action.Remove,
                    "",
                    k
                ));

                return true;
            } else {
                return false;
            }
        }

        public void PropertyHandler(object sender, PropertyChangedEventArgs e) {
            StudentsChanged(this, new StudentsChangedEventArgs<TKey>(
                Name,
                Action.Property,
                e.PropertyName,
                m_getKey((Student)sender)
            ));
        }

        public override string ToString() {
            string a = "";
            foreach (var pair in m_dict) {
                a += (pair.Value.ToString() + "\n");
            }

            return a;
        }

        public string Name;

        private Dictionary<TKey, Student> m_dict;
        private KeySelector<TKey>         m_getKey;
    };

    class Student : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public Student(Education educ, int group) {
            m_educ          = educ;
            m_group         = group;
            Id = Student._id++;
        }

        public Student() {
            m_educ          = Education.Specialist;
            m_group         = 1;
            Id = Student._id++;
        }

        public Education Educ {
            get {
                return m_educ;
            }

            set {
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Education"));
                }
                m_educ = value;
            }
        }

        public int Group {
            get {
                return m_group;
            }

            set {
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Group"));
                }
                m_group = value;
            }
        }

        public override string ToString() {
            return "" + Group;
        }

        private static int _id = 0;

        public  int        Id;
        private Education  m_educ;
        private int        m_group;
    };

    class Journal<TKey> {
        public Journal() {
            m_st = new List<JournalEntry>();
        }

        public void Subscribe(StudentCollection<TKey> a) {
            a.StudentsChanged += ((source, e) => {
                var note = new JournalEntry(e.NameCollection, e.Info, e.PropertyName, e.Key.ToString());
                m_st.Add(note);
            });
        }

        public override string ToString() {
            string a = "";
            foreach(var i in m_st) {
                a += (i.ToString() + "\n");
            }

            return a;
        }

        private List<JournalEntry> m_st;
    };

    class JournalEntry {
        public string Name;
        public Action Info;
        public string PropertyName;
        public string Key;

        public JournalEntry(string name, Action act, string prop, string key) {
            Name = name;
            Info           = act;
            PropertyName   = prop;
            Key            = key;
        }

        public override string ToString() {
            return Info + " " + Name + " " + PropertyName + " " + Key;
        }
    };

    class Programm {
        static void Main(string[] args) {
            // 1. Create Collections
            var a = new StudentCollection<int>((e) => e.Id, "a");
            var b = new StudentCollection<int>((e) => e.Id, "b");

            // 2. Journal and Subscribe
            var j = new Journal<int>();
            j.Subscribe(a);
            j.Subscribe(b);

            // 3. Change Collections
            var s1 = new Student(Education.Bachelor, 8);
            var s2 = new Student(Education.Bachelor, 8);
            a.AddStudents(s1);
            b.AddStudents(s2);
            s1.Educ = Education.SecondEducation;
            s2.Educ = Education.SecondEducation;

            s1.Group = 4;
            s2.Group = 4;

            a.Remove(s1.Id);
            b.Remove(s2.Id);

            s1.Educ = Education.Specialist;
            s2.Educ = Education.Specialist;

            s1.Group = 3;
            s2.Group = 3;

            // 4. Journal in Console
            // After deleting from Collections changing not write in journal
            Console.WriteLine(j.ToString());
        }
    };
};
