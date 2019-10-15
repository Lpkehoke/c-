using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace lab_3_2 {
    class TestCollections<TKey, TValue> {
        public TestCollections(int num, GenerateElement<TKey, TValue> f) {
            m_f = f;
            m_num = num;
            for (int i = 0; i < num; ++i) {
                var el = f(i+1);
                m_dict1.Add(el.Key, el.Value);
                m_dict2.Add("" + i, el.Value);
                m_keys.Add(el.Key);
                m_strings.Add("" + i);
            }
        }

        public void CheckTime(int i) {
            Console.Write("" + i + " element\n");
            var sw = new Stopwatch();
            var el = m_f(i);

            sw.Start();
            m_keys.Contains(el.Key);
            sw.Stop();
            Console.WriteLine("List<TKey>:\t\t\t " + sw.ElapsedTicks);

            sw.Start();
            m_strings.Contains("" + i);
            sw.Stop();
            Console.WriteLine("List<string>:\t\t\t " + sw.ElapsedTicks);

            sw.Start();
            m_dict1.ContainsKey(el.Key);
            sw.Stop();
            Console.WriteLine("Dictionary<TKey, TValue>:\t " + sw.ElapsedTicks);

            sw.Start();
            m_dict2.ContainsKey("" + i);
            sw.Stop();
            Console.WriteLine("Dictionary<string, TValue>:\t " + sw.ElapsedTicks);
        }

        private List<TKey>                    m_keys     = new List<TKey>();
        private List<string>                  m_strings  = new List<string>();
        private Dictionary<TKey, TValue>      m_dict1    = new Dictionary<TKey, TValue>();
        private Dictionary<string, TValue>    m_dict2    = new Dictionary<string, TValue>();
        private GenerateElement<TKey, TValue> m_f;
        private int                           m_num;
    };
};
