extern "C"
{
    typedef struct
    {
        double v1;
        double v2;
        double v3;
    } InputData;

    typedef struct
    {
        double sum;
        double product;
    } ResultData;

    __declspec(dllexport) ResultData __stdcall RunModel(InputData data)
    {
        ResultData r;
        r.sum = data.v1 + data.v2 + data.v3;
        r.product = data.v1 * data.v2 * data.v3;
        return r;
    }
}
