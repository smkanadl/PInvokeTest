#pragma once

extern "C"
{
    typedef struct _matrix matrix_t;

    __declspec(dllexport) matrix_t* __stdcall Create(int rows, int columns, double initialValue) noexcept;

    __declspec(dllexport) int __stdcall Set(matrix_t* ctx, int row, int column, double value) noexcept;

    __declspec(dllexport) int __stdcall Get(matrix_t* ctx, int row, int column, double& value) noexcept;

    __declspec(dllexport) int __stdcall Free(matrix_t* ctx) noexcept;

    __declspec(dllexport) int __stdcall Solve(matrix_t* A, matrix_t* result, matrix_t* solution) noexcept;
}
