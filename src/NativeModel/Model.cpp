
#include <exception>

extern "C"
{
    typedef struct
    {
        double v1;
        double v2;
        double v3;
        bool shouldThrow;
    } InputData;

    typedef struct
    {
        double sum;
        double product;
    } ResultData;

    __declspec(dllexport) int __stdcall RunModel(InputData data, ResultData& result) noexcept
    {
        try
        {
            if (data.shouldThrow)
            {
                throw std::exception("Should throw");
            }
            result.sum = data.v1 + data.v2 + data.v3;
            result.product = data.v1 * data.v2 * data.v3;
        }
        catch (std::exception&)
        {
            return 1;
        }
        
        return 0;
    }
}
