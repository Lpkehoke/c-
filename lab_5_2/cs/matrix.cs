using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace lab_5_2 {
    class Matrix {
        public Matrix(int n) {
            m_matrix = new List<double>(n);
            m_matrix.Add(10.0 * n);
            for (int i = 1; i < n; ++i) {
                m_matrix.Add(1.0 * i);
            }
        }

        public Matrix(List<double> arr) {
            m_matrix = new List<double>(arr);
        }

        public List<double> Solve(List<double> right) {
			var size = m_matrix.Count;
			var x = new List<double>();
			var res = new List<double>();

			for (int i = 0; i < size; ++i) {
				x.Add(0);
				res.Add(0);
			}

			if (m_matrix[0] != 0) {
				x[0] = 1 / m_matrix[0];
			} else {
				throw new ArgumentNullException("no solutions");
			}

			double f, r, s;

			for (int i = 1; i < size; ++i) {
				f = r = s = 0;
				for (int j = 0; j < i; ++j) {
					f += m_matrix[i - j] * x[j];
				}

				if (1 - f*f != 0) {
					r = 1 / (1 - f*f);
				} else {
					throw new ArgumentNullException("no solutions");
				}

				s = r * f;

				x[0] = x[0] * r;
				for (int j = 1; j < i + 1; ++j) {
					x[j] = x[j] * r + x[j-1] * s;
				}
			}

			if (x[0] == 0) {
				throw new ArgumentNullException("no solutions");
			}

			double t;

			for (int i = 0; i < size; ++i) {
				res[i] = t = 0;
				for (int j = 0; j < size; ++j) {
					for (int k = 0; k < Math.Min(i, j) + 1; ++k) {
						t += x[i - k] * x[size - 1 - j + k]; // el in first matrix
						if (i != 0 && j != 0 && i != k && j != k) {
							t -= x[i - k - 1] * x[size - j + k]; // el in second matrix
						}
					}

					res[i] += t * right[j];
					t = 0;
				}
				res[i] /= x[0];
			}
            return res;
        }

        public override string ToString() {
            return "matrix";
        }

        private List<double> m_matrix;
    };
}
