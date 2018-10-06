#pragma once

#include <type_traits>

extern "C"
{
    using FunctionPtr = std::add_pointer_t<double __stdcall (double)>;

    typedef struct
    {
        double lower;
        double upper;
        int iterations;
        FunctionPtr f;
    } InputDataBisection;

    typedef struct
    {
        double root;
    } ResultDataBisection;

    __declspec(dllexport) int __stdcall RunBisection(InputDataBisection input, ResultDataBisection& result) noexcept;
}
