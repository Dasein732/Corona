using System.Numerics;
using Program;

namespace program
{
    internal class Program
    {
        private static void Main()
        {
            using(var engine = new Engine(800, 600))
            {
                engine.Run();
            }
        }
    }
}