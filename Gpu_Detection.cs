//i will add hip later...

using System;
using System.Runtime.InteropServices;

public static class GpuDetection
{
    // CUDA
    //-----------------------------------------------------------------------
    static int cuInit(uint flags)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return cuInit_Windows(flags);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return cuInit_Linux(flags);
        }
        catch
        {
            // CUDA driver not installed...
            return -1;
        }

        return -1;
    }

    [DllImport("nvcuda.dll", EntryPoint = "cuInit")]
    private static extern int cuInit_Windows(uint flags);

    [DllImport("libcuda.so.1", EntryPoint = "cuInit")]
    private static extern int cuInit_Linux(uint flags);

    // ROCm
    //-----------------------------------------------------------------------
    static int hsa_init()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return -1;

        try
        {
            return hsa_init_Linux();
        }
        catch
        {
            // ROCm not installed...
            return -1;
        }
    }

    [DllImport("libhsa-runtime64.so", EntryPoint = "hsa_init")]
    private static extern int hsa_init_Linux();

    public static void RunDetection()
    {
        int result;

        result = cuInit(0);
        if (result == 0){
            Console.WriteLine("Cuda Detected :D");
            Environment.SetEnvironmentVariable("CUDA_VISIBLE_DEVICES", "0");
    }
        result = hsa_init();
        if (result == 0){
            Console.WriteLine("ROCm Detected :D");
            Environment.SetEnvironmentVariable("ROCR_VISIBLE_DEVICES", "0");
    }
    }
}
