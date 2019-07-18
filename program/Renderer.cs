using System;
using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using LanguageExt;
using Index = ILGPU.Index;
using Vector4 = System.Numerics.Vector4;

namespace Program
{
    public sealed class Renderer : IDisposable
    {
        private readonly Vector4[] _frameBuffer;
        private readonly int _canvasWidth;
        private readonly int _canvasHeight;
        private bool isDisposed = false;

        private Context _context;
        private Accelerator _accelerator;
        private MemoryBuffer2D<Vector4> _gpuFrameBuffer;

        private Renderer(int canvasWidth, int canvasHeight)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
            _frameBuffer = new Vector4[_canvasWidth * _canvasHeight];
        }

        public static Either<Renderer, Exception> Initialize(int width, int height)
        {
            try
            {
                var renderer = new Renderer(width, height);
                renderer._context = new Context();
                renderer._accelerator = new CudaAccelerator(renderer._context);
                renderer._gpuFrameBuffer = renderer._accelerator.Allocate<Vector4>(width, height);

                return renderer;
            }
            catch(Exception e)
            {
                return e;
            }
        }

        public Vector4[] NextFrame()
        {
            var kernel = _accelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<Vector4>, int, int>(RayTracerKernels.Frame);

            kernel(new Index2(_canvasWidth, _canvasHeight), _gpuFrameBuffer.View, _canvasWidth, _canvasHeight);
            _accelerator.Synchronize();
            _gpuFrameBuffer.CopyTo(_frameBuffer, Index2.Zero, 0, new Index2(_canvasWidth, _canvasHeight));

            return _frameBuffer;
        }

        private void Dispose(bool disposing)
        {
            if(!isDisposed)
            {
                if(disposing)
                {
                    _gpuFrameBuffer.Dispose();
                    _accelerator.Dispose();
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                isDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Renderer()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}