using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using System.Drawing;


namespace GraphicModellingLibrary._3D_Display
{
    public sealed class KinematicArmMesh: IObjectToDisplay, IDirectXDisplayer
    {
        List<IObserver<Device>> observers;

        List<KinematicPair> KinematicPairs;
        List<KinematicPairMesh> KinematicPairMeshes;


        private double[,] GetMatrix(double offsetAngle = 2 * Math.PI / 3)
        {
            double cosO = Math.Cos(offsetAngle);
            double sinO = Math.Sin(offsetAngle);

            return new double[,] {
                {1, 0, 0 },
                {0, cosO, -sinO },
                {0, sinO, cosO },
            };
        }

        private Vector3[] SetupFirstLayer(double length = 0.15 / 2, double heightAngle = Math.PI / 4)
        {
            var SegmentO1 = new Vector3(
                (float)(length * Math.Cos(heightAngle)),
                (float)(length * Math.Sin(heightAngle)),
                0
                );

            double[,] R1 = GetMatrix();
            double[,] R2 = R1.Transpose();

           var SegmentO2 = SegmentO1.RightMultiply(R1);
           var SegmentO3 = SegmentO1.RightMultiply(R2);

            return new Vector3[] { SegmentO1, SegmentO2, SegmentO3 };
        }

        private Vector3[] SetupSecondLayer(double length = 0.15 / 2)
        {
            var result = new Vector3[3];
            for (int i = 0; i < 3; i++) result[i] = new Vector3((float)length, 0, 0);
            return result;
        }

        private void PopulateKinematicPairs()
        {
            KinematicPairs = new List<KinematicPair>();

            double[,] TransformS1 =
            {
                {1, 0, 0 },
                {0, 1, 0 },
                {0, 0, 1 }
            };


            double[,] R1 = GetMatrix();
            double[,] R2 = R1.Transpose();


            var l1 = SetupFirstLayer();

            KinematicPairs.Add(
                new KinematicPair
                {
                    Link = l1[0].Vector3FromDX(),
                    LinkHolder = Parent,
                    A = TransformS1,
                }
            );

            KinematicPairs.Add(
                new KinematicPair
                {
                    Link = l1[1].Vector3FromDX(),
                    LinkHolder = Parent,
                    A = TransformS1.RightMultiply(R1),
                }
            );

            KinematicPairs.Add(
                new KinematicPair
                {
                    Link = l1[2].Vector3FromDX(),
                    LinkHolder = Parent,
                    A = TransformS1.RightMultiply(R1.Transpose()),
                }
            );

            var l2 = SetupSecondLayer();
            for (int i = 0; i < 3; i++)
            {
                KinematicPairs.Add(new KinematicPair { Link = l2[i].Vector3FromDX(), A = null, LinkHolder = KinematicPairs[i] });
            }
        }

        public KinematicArmMesh(IDirectXDisplayer observable, KinematicPair pair, Color color, float cylinder_radius = 0.0125f)
        {
            observable.Subscribe(this);
            d3d = observable.d3d;

            observers = new List<IObserver<Device>>();
            KinematicPairMeshes = new List<KinematicPairMesh>();

            Parent = pair;

            PopulateKinematicPairs();

            foreach (var item in KinematicPairs)
            {
                KinematicPairMeshes.Add(
                    new KinematicPairMesh(this, item, color, cylinder_radius)
                    );
            }


        }

        private KinematicPair Parent { get; set; }

        public Device d3d {get; private set;}

        public void Dispose()
        {
            observers.ForEach(x => x.OnCompleted());
        }

        public void OnCompleted()
        {
            Dispose();
        }

        public void OnError(Exception error)
        {
            Dispose();
        }

        public void OnNext(Device d3d)
        {

            for (int i = 0; i < 3; i++)
            {
                KinematicPairs[i].Fi = Fi;
            }
           observers.ForEach(x => x.OnNext(d3d));
        }

        public double Fi { get; set; }


        public IDisposable Subscribe(IObserver<Device> observer)
        {
            observers.Add(observer);

            return new Subscription();
        }
    }
}
