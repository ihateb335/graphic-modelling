using GraphicModellingLibrary;
using System.Numerics;
using System.Linq;
using System;
namespace Test1
{
    internal class TestProgram
    {
        static void Main(string[] args)
        {
            KinematicPair previous_pair = null;
            for (int i = 0; i < 7; i++)
            {
                var pair = new KinematicPair()
                {
                    LinkHolder = previous_pair,
                    Link = new Vector3(0,1.0f,0)
                };
                previous_pair = pair;
            }

            var array = previous_pair.ToCollection().Reverse().ToArray();
            foreach (var item in array)
            {
                item.AbsLink().Print();
            }
           
            var a = KinematicPair.ReverseJob(previous_pair, new Vector3 { X = -1, Y = 0, Z = 0 });

            for (int i = 1; i < array.Length - 1; i++)
            {
                array[i].Fi = a[i];
            }
            foreach (var item in array)
            {
                item.AbsLink().Print(); Console.WriteLine(item.Fi * 180 / Math.PI);
            }
        }
    }
}
