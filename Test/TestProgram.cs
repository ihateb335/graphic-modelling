using GraphicModellingLibrary;
using System.Numerics;

namespace Test1
{
    internal class TestProgram
    {
        static void Main(string[] args)
        {
            KinematicPair previous_pair = null;
            for (int i = 0; i < 7; i++)
            {
                var pair = new KinematicPair(1.0f)
                {
                    LinkHolder = previous_pair
                };
                previous_pair = pair;
            }
            while (previous_pair != null)
            {
                previous_pair.AbsLink().Print();
                previous_pair.A.Print("A");
                previous_pair = previous_pair.LinkHolder;
            }

        }
    }
}
