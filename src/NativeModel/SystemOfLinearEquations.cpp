
#include <Eigen/Dense>

namespace
{
    static constexpr auto RELATIVE_ERROR_THRESHOLD = 1.0e-8;

    template<typename Matrix, typename Vector>
    inline bool IsFailed(const Matrix& A, const Vector& result, const Vector& solution)
    {
        auto solutionNorm = solution.norm();
        auto relativeError = solutionNorm != 0.0 ?
            (A * solution - result).norm() / solutionNorm :
            1.0;

        return relativeError > RELATIVE_ERROR_THRESHOLD;
    }
}

extern "C"
{
    typedef struct _matrix matrix_t;

    typedef Eigen::Matrix<double, Eigen::Dynamic, Eigen::Dynamic> matrix_impl_t;

    struct _matrix
    {
        matrix_impl_t matrix;
    };

    __declspec(dllexport) matrix_t* __stdcall Create(int rows, int columns, double initialValue) noexcept
    {
        auto m = new _matrix;
        m->matrix = matrix_impl_t::Constant(rows, columns, initialValue);
        return m;
    }

    __declspec(dllexport) int __stdcall Set(matrix_t* ctx, int row, int column, double value) noexcept
    {
        try
        {
            ctx->matrix(row, column) = value;
        }
        catch (std::exception&)
        {
            return 1;
        }

        return 0;
    }

    __declspec(dllexport) int __stdcall Get(matrix_t* ctx, int row, int column, double& value) noexcept
    {
        try
        {
            value = ctx->matrix(row, column);
        }
        catch (std::exception&)
        {
            return 1;
        }

        return 0;
    }

    __declspec(dllexport) int __stdcall Free(matrix_t* ctx) noexcept
    {
        delete ctx;
        return 0;
    }

    struct Result
    {
        enum Impl
        {
            Ok = 0,
            Singular = 1,
            Failed = 2
        };
    };

    __declspec(dllexport) int __stdcall Solve(matrix_t* A, matrix_t* result, matrix_t* solution) noexcept
    {
        try
        {
            // http://eigen.tuxfamily.org/dox/group__TutorialLinearAlgebra.html

            // Note: For colPivHouseholderQr do NOT use .info(), because
            // -> This function always returns Success. It is provided for compatibility with other factorization routines.

            // For small sizes (< 100) prefer fullPivLu over colPivHouseholderQr due to speed and accuracy!
            // auto decompositon = A->data.colPivHouseholderQr();
            auto decompositon = A->matrix.fullPivLu();
            solution->matrix = decompositon.solve(result->matrix);

            if (IsFailed(A->matrix, result->matrix, solution->matrix))
            {
                // TOOD: It this really required? Tests say so!
                solution->matrix = Eigen::VectorXd::Zero(result->matrix.rows());
                if (!decompositon.isInvertible())
                    return Result::Singular;
                return Result::Failed;
            }

            return Result::Ok;
        }
        catch (std::exception&)
        {
            return 1;
        }

        return 0;
    }
}
