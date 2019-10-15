using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab_5_2 {
    class Programm {
        [DllImport("libMatrix.so", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Int64 cppSolveOrderN(Int64 order, Int64 repeat);

        [DllImport("libMatrix.so", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Int64 cppSolveDefineMatrix(Int64 size, double[] matrixVector, double[] right, double[] res);

        private static int csSolveOrderN(int order, int repeat) {
            var sw = new Stopwatch();
            sw.Start();

            var matrix = new Matrix(order);

            var right = new List<double>(order);
            for (int i = 0; i < order; ++i) {
                right.Add(1.0 * (i+1));
            }

            for (int i = 0; i < repeat; ++i) {
                matrix.Solve(right);
            }

            sw.Stop();
            return (int)sw.ElapsedMilliseconds;
        }

        public static void Main() {
            // 1
            double[] mArray  = {10.0, 2.0, 3.0};
            double[] rArray  = {4.0, 5.0, 1.0};
            double[] res2    = {0.0, 0.0, 0.0};

            var mList = mArray.OfType<double>().ToList();
            var rList = rArray.OfType<double>().ToList();

            var m1   = new Matrix(mList);
            var res1 = m1.Solve(rList);

            // 2
            try {
                cppSolveDefineMatrix(3, mArray, rArray, res2);
            } catch {
                Console.WriteLine("error with cpp");
                System.Environment.Exit(1);
            }

            // Pirnting matrix and result
            Console.WriteLine("Matrix\tright\tres1\t\t\tres2");
            Console.WriteLine("" + mList[0] + " " + mList[1] + " " + mList[2] + "\t" + rList[0] + "\t" + res1[0] + "\t" + res2[0]);
            Console.WriteLine("" + mList[2] + " " + mList[0] + " " + mList[1] + "\t" + rList[1] + "\t" + res1[1] + "\t" + res2[1]);
            Console.WriteLine("" + mList[1] + " " + mList[2] + " " + mList[0] + "\t" + rList[2] + "\t" + res1[2] + "\t" + res2[2]);

            // 3
            TimeList tl = new TimeList();
            Console.WriteLine("Enter filename:");
            var fn = Console.ReadLine();
            if (!tl.Load(fn)) {
                Console.WriteLine("file not exist");
            }

            // 4
            while (true) {
                Console.WriteLine("Enter Order and repeat(int int) or enter -1 to exit");
                string input = Console.ReadLine();
                if (input == "-1") {
                    break;
                }

                char[] sep = {' '};
                var par = input.Split(sep);
                int n, repeat;
                try {
                    n = int.Parse(par[0]);
                    repeat = int.Parse(par[1]);
                } catch {
                    Console.WriteLine("error with input");
                    continue;
                }
                if (n <= 0 || repeat <= 0) {
                    Console.WriteLine("error with input");
                    continue;
                }

                // 5
                TimeItem ti = new TimeItem();
                ti.Order  = n;
                ti.Repeat = repeat;
                try {
                    ti.TimeCpp = (int)cppSolveOrderN(n, repeat);
                } catch {
                    Console.WriteLine("error with cpp");
                    continue;
                }

                ti.TimeCs = csSolveOrderN(n, repeat);
                ti.Factor = (double)ti.TimeCs / (double)ti.TimeCpp;

                tl.Add(ti);
            }

            // 6
            tl.Save(fn);
            Console.WriteLine(tl);
        }
    };
};
