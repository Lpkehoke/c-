using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab_3_2 {
    class StudentCollection<TKey> {
        public StudentCollection(KeySelector<TKey> f) {
            m_getKey = f;
            m_dict   = new Dictionary<TKey, Student>();
        }

        public void AddDefaults() {
            for (int i = 0; i < 5; ++i) {
                var a = new Student();
                a.AddExams(
                    new Exam("Sawdoca", i  , new DateTime(2000, 1, 22)),
                    new Exam("Astrawd", i*2, new DateTime(2001, 1, 22))
                );
                a.Group = (i+1)*100;
                m_dict.Add(m_getKey(a), a);
            }
        }

        public void AddStudents(params Student[] st) {
            foreach(Student i in st) {
                m_dict.Add(m_getKey(i), i);
            }
        }

        public override string ToString() {
            string a = "";
            foreach (var pair in m_dict) {
                a += (pair.Value.ToString() + "\n");
            }

            return a;
        }

        public string ToShortString() {
            string a = "";
            foreach (var pair in m_dict) {
                a += (pair.Value.ToShortString() + "\n");
            }

            return a;
        }

        public double MaxMiddleMark() {
            return m_dict.Values.Max( a => a.MiddleMark );
        }

        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education value) {
            return m_dict.Where( a => a.Value.Educ == value );
        }

        public IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> GroupingDictEducation {
            get => m_dict.GroupBy( a => a.Value.Educ );
        }

        private Dictionary<TKey, Student> m_dict;
        private KeySelector<TKey>         m_getKey;
    };
};
