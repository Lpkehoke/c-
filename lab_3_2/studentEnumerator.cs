using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_3_2 {
    class StudentEnumerator : IEnumerator {
        private ArrayList m_st       = new ArrayList();
        private int       m_position = -1;

        public StudentEnumerator(List<Test> a, List<Exam> b) {
            if (a != null) {
                m_st = new ArrayList(a);
            }

            if (b != null) {
                m_st.AddRange(b);
            }
        }

        public void Reset() {
            m_position = -1;
        }

        public object Current {
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
};
