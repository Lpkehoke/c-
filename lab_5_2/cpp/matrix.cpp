#include "matrix.hpp"

#include <vector>
#include <cstddef>
#include <thread>
#include <cstdint>
#include <algorithm>

namespace lab {
    Matrix::Matrix(const std::size_t n) noexcept {
        m_matrixVector.resize(n);
        m_matrixVector[0] = 10 * n; // diagonal elemnts need be large
        for (std::size_t i = 1; i < n; ++i) {
            m_matrixVector[i] = i;
        }

        m_matrix = m_matrixVector.data();
    }

    Matrix::Matrix(const std::vector<double>& matrix) noexcept
        : m_matrix(matrix.data())
    {}

    Matrix::Matrix(const double* matrix) noexcept
        : m_matrix(matrix)
    {}

    void Matrix::solve(const std::vector<double>& right, std::vector<double>& res) const {
        solve(right.size(), right.data(), res.data());
    }

    void Matrix::solve(std::int64_t size, const double* right, double* res) const {
        std::vector<double> x(size, 0);

		if (m_matrix[0]) {
			x[0] = 1 / m_matrix[0];
		} else {
			throw "no solutions";
		}

		double f, r, s;

		for (std::int64_t i = 1; i < size; ++i) {
			f = r = s = 0;
			for (std::int64_t j = 0; j < i; ++j) {
				f += m_matrix[i - j] * x[j];
			}

			if (1 - f*f) {
				r = 1 / (1 - f*f);
			} else {
				throw "no solutions";
			}

			s = r * f;

			x[0] = x[0] * r;
			for (std::int64_t j = 1; j < i + 1; ++j) {
				x[j] = x[j] * r + x[j-1] * s;
			}
		}

		if (x[0] == 0) {
			throw "no solutions";
		}

		double t;

		for (std::int64_t i = 0; i < size; ++i) {
			res[i] = t = 0;
			for (std::int64_t j = 0; j < size; ++j) {
				for (std::int64_t k = 0; k < std::min(i, j) + 1; ++k) {
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
    }
};
