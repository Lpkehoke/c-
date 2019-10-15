#include "index.hpp"
#include "matrix.hpp"

#include <cstddef>
#include <chrono>
#include <vector>
#include <cstdint>

MODULE_API std::int64_t cppSolveOrderN(const std::int64_t order, const std::int64_t repeat) {
    auto begin = std::chrono::high_resolution_clock::now();

    lab::Matrix matrix(order);
    std::vector<double> res(order);

    //make right vector
    std::vector<double> right(order);
    for (std::int64_t i = 0; i < order; ++i) {
        right[i] = i+1;
    }

    // solving
    for (std::int64_t i = 0; i < order; ++i) {
        matrix.solve(right, res);
    }

    auto end = std::chrono::high_resolution_clock::now();
    return static_cast<std::int64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(end-begin).count());
}

MODULE_API std::int64_t cppSolveDefineMatrix(const std::int64_t size, const double* matrixVector, const double* right, double* res) {
    auto begin = std::chrono::high_resolution_clock::now();

    lab::Matrix matrix(matrixVector);
    matrix.solve(size, right, res);

    auto end = std::chrono::high_resolution_clock::now();
    return std::chrono::duration_cast<std::chrono::milliseconds>(end-begin).count();
    return 0;
}
