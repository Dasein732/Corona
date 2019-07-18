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
        private readonly Index2 _canvasSize;
        private bool isDisposed = false;

        private Context _context;
        private Accelerator _deviceAccelerator;
        private MemoryBuffer2D<Vector4> _deviceFrameBuffer;

        private Action<Index2, ArrayView2D<Vector4>, Index2> kernel;

        private Renderer(int canvasWidth, int canvasHeight)
        {
            _canvasSize = new Index2(canvasWidth, canvasHeight);
            _frameBuffer = new Vector4[canvasWidth * canvasHeight];
        }

        // TODO extract into a factory
        public static Either<Renderer, Exception> Initialize(int width, int height)
        {
            try
            {
                var renderer = new Renderer(width, height);
                renderer._context = new Context();
                renderer._deviceAccelerator = new CudaAccelerator(renderer._context);
                renderer._deviceFrameBuffer = renderer._deviceAccelerator.Allocate<Vector4>(width, height);
                renderer.LoadKernel();

                return renderer;
            }
            catch(Exception e)
            {
                return e;
            }
        }

        public Vector4[] NextFrame()
        {
            kernel(_canvasSize, _deviceFrameBuffer.View, _canvasSize);
            _deviceAccelerator.Synchronize();
            _deviceFrameBuffer.CopyTo(_frameBuffer, Index2.Zero, 0, _canvasSize);

            return _frameBuffer;
        }

        private void LoadKernel()
        {
            kernel = _deviceAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<Vector4>, Index2>(RayTracerKernels.Frame);
        }

        private void Dispose(bool disposing)
        {
            if(!isDisposed)
            {
                if(disposing)
                {
                    _deviceFrameBuffer.Dispose();
                    _deviceAccelerator.Dispose();
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