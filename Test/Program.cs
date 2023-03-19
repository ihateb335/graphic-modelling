﻿using GraphicModellingLibrary;
using System.Numerics;

namespace Test1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CinematicPair previous_pair = null;
            for (int i = 0; i < 7; i++)
            {
                var pair = new CinematicPair
                {
                    Fi = 0,
                    Link = Vector3.UnitY,
                    LinkHolder = previous_pair
                };
                previous_pair = pair;
            }
            while (previous_pair != null)
            {
                previous_pair.AbsLink.Print();
                previous_pair.A.Print("A");
                previous_pair = previous_pair.LinkHolder;
            }

        }
    }
}