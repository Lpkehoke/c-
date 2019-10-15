#pragma once

#include <cstddef>
#include <cstdint>
#include <vector>

#ifdef __cplusplus
extern "C" {
#endif

#ifdef _WIN32
#  ifdef MODULE_API_EXPORTS
#    define MODULE_API __declspec(dllexport)
#  else
#    define MODULE_API __declspec(dllimport)
#  endif
#else
#  define MODULE_API
#endif
MODULE_API std::int64_t cppSolveOrderN(const std::int64_t order, const std::int64_t repeat);
MODULE_API std::int64_t cppSolveDefineMatrix(const std::int64_t size, const double* matrixVector, const double* right, double* res);

#ifdef __cplusplus
}
#endif
