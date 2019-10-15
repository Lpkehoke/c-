using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab_3_2 {
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    delegate TKey KeySelector<TKey> (Student st);

    class Programm {
        static void Main(string[] args) {
            ///1
            Console.WriteLine("\n#############1#############"); // test sorting
            var c = new Student();
            c.AddExams(
                new Exam("Sawdoca", 1, new DateTime(2000, 1, 22)),
                new Exam("Astrawd", 2, new DateTime(2001, 1, 22)),
                new Exam("Astwadr", 4, new DateTime(1900, 1, 22)),
                new Exam("wadAstr", 3, new DateTime(2000, 1, 22)),
                new Exam("wAwstrd", 5, new DateTime(2010, 1, 22)),
                new Exam("aAAstrw", 1, new DateTime(1999, 1, 22)),
                new Exam("Asdwatr", 5, new DateTime(2020, 1, 22)),
                new Exam("Fiswadw", 6, new DateTime(2100, 1, 22))
            );
            c.Educ = Education.Bachelor;

            c.SortOnMark();
            Console.WriteLine("--------------------Sort On Mark-----------------------------");
            Console.WriteLine(c);

            c.SortOnName();
            Console.WriteLine("--------------------Sort On Name-----------------------------");
            Console.WriteLine(c);


            c.SortOnDate();
            Console.WriteLine("--------------------Sort On Date-----------------------------");
            Console.WriteLine(c);

            //2
            Console.WriteLine("\n#############2#############"); // test student Collections
            var coll = new StudentCollection<string>(a => a.ToString());
            coll.AddDefaults();
            Console.WriteLine("Student Collections:");
            Console.WriteLine(coll.ToShortString());

            //3
            Console.WriteLine("\n#############3#############");
            Console.WriteLine("Max MiddleMark: " + coll.MaxMiddleMark());

            Console.WriteLine("\nAll Bachelor: ");
            foreach (var i in coll.EducationForm(Education.Bachelor)) {
                Console.WriteLine(i.Value.ToShortString());
            }

            Console.WriteLine("\nGrouping: ");
            foreach (var i in coll.GroupingDictEducation) {
                foreach (var j in i) {
                    Console.WriteLine(j.Value.ToShortString());
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("End grouping.");

            //4
            Console.WriteLine("\n#############4#############");
            int n = 1000000;
            var test = new TestCollections<Person, Student>(n, j => { // func to generate unique element
                var a = new KeyValuePair<Person, Student>(
                    new Person(j + "_name", j + "_surname", new DateTime(2000, 1, 22)),
                    new Student()
                );
                return a;
            });

            test.CheckTime(1); // first element
            test.CheckTime(n/2); // middle element
            test.CheckTime(n); // last element
            test.CheckTime(n * 2); // element out of range
        }
    };
};
