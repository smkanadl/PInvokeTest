#include <boost/math/tools/roots.hpp>
#include <type_traits>

extern "C"
{
    typedef struct
    {
        double lower;
        double upper;
        int iterations;
        void* f;
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
            using FunctionPtr = std::add_pointer_t<double __stdcall (double)>;
            auto func = (FunctionPtr)input.f;
            
            boost::uintmax_t it = input.iterations;
            auto root = toms748_solve(func, input.lower, input.upper, eps_tolerance<double>{ 48 }, it);
            result.root = root.first;
        }
        catch (std::exception&)
        {
            return 1;
        }
        
        return 0;
    }
}
