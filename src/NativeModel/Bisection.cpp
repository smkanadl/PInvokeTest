#include <boost/math/tools/roots.hpp>
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

    __declspec(dllexport) int __stdcall RunBisection(InputDataBisection input, ResultDataBisection& result) noexcept
    {
        using namespace std;
        using namespace boost::math::tools;
        try
        {
            boost::uintmax_t it = input.iterations;
            auto root = toms748_solve(input.f, input.lower, input.upper, eps_tolerance<double>{ 48 }, it);
            result.root = root.first;
        }
        catch (std::exception&)
        {
            return 1;
        }
        
        return 0;
    }
}
