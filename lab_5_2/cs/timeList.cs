using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab_5_2 {
    class TimeList {
        public TimeList() {
            m_list = new List<TimeItem>();
        }

        public void Add(TimeItem item) {
            m_list.Add(new TimeItem(item));
        }

        public bool Save(string fn) {
            try {
                using (var fs = new FileStream(fn, FileMode.Create)) {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, m_list);
                }

                return true;
            } catch {
                return false;
            }
        }

        public bool Load(string fn) {
            try {
                if (File.Exists(fn)) {
                    using (var fs = new FileStream(fn, FileMode.Open)) {
                        var deserializer = new BinaryFormatter();
                        m_list = (List<TimeItem>) deserializer.Deserialize(fs);
                    }
                } else {
                    return false;
                }

                return true;
            } catch {
                return false;
            }
        }

        public override string ToString() {
            string ans = "";
            foreach (var el in m_list) {
                ans += (el + "\n");
            }

            return ans;
        }

        private List<TimeItem> m_list;
    };
}
