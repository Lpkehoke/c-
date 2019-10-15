using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_3_2 {
    class Exam : IDateAndCopy, IComparable, IComparer<Exam> {
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

        object IDateAndCopy.DeepCopy() {
            return new Exam(Name, Mark, Date);
        }

        public int CompareTo(object obj) {
            return Name.CompareTo(((Exam)obj).Name);
        }

        public int Compare(Exam a, Exam b) {
            return a.Mark.CompareTo(b.Mark);
        }

        public string          Name;
        public int             Mark;
        public System.DateTime Date {
            get => m_date;
            set => m_date = value;
        }

        private System.DateTime m_date;
    };

    class ExamOnDate : IComparer<Exam> {
        public int Compare(Exam a, Exam b) {
            return a.Date.CompareTo(b.Date);
        }
    }
}
