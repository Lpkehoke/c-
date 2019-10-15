#pragma once

#include <vector>
#include <cstddef>
#include <cstdint>

namespace lab {
    class Matrix final {
        public:
            explicit Matrix(const std::size_t n) noexcept;
            explicit Matrix(const std::vector<double>& matrix) noexcept;
            explicit Matrix(const double* matrix) noexcept;


            Matrix(const Matrix&)            = default;
            Matrix(Matrix&&)                 = default;
            Matrix& operator=(const Matrix&) = default;
            Matrix& operator=(Matrix&&)      = default;
            ~Matrix()                        = default;

            void solve(const std::vector<double>& right, std::vector<double>& res) const;
            void solve(std::int64_t size, const double* right, double* res) const;

        private:
            const double*       m_matrix;
            std::vector<double> m_matrixVector;
    };

};
