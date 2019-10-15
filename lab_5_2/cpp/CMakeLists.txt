project(Lab5_2 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)

add_library(Matrix SHARED index.cpp matrix.cpp index.hpp)
