using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace GraphicModellingLibrary._3D_Display
{
    public delegate void InvalidateDelegate();
    public class DirectX9Facade :  IFormDisplayer
    {
        private Device d3d = null;

        private List<IObserver<Device>> observers = new List<IObserver<Device>>();

        public event InvalidateDelegate On_Invalidate;

        private Vector3 CameraPosition { get; set; } = new Vector3();
        private Vector3 CameraTarget { get; set; } = new Vector3();

        public int Width { get; set; }
        public int Height { get; set; }

        public Matrix POV { get; set; }
        public Matrix Target { get; set; }


        public bool BuildUp(Control render_object)
        {
            try
            {
                //// Устанавливаем режим отображения трехмерной графики
                PresentParameters d3dpp = new PresentParameters();
                d3dpp.BackBufferCount = 1;
                d3dpp.SwapEffect = SwapEffect.Discard;
                d3dpp.Windowed = true; // Выводим графику в окно
                d3dpp.MultiSample = MultiSampleType.None; // Выключаем антиалиасинг
                d3dpp.EnableAutoDepthStencil = true; // Разрешаем создание z-буфера

                d3dpp.AutoDepthStencilFormat = DepthFormat.D16; // Z-буфер в 16 бит
                d3d = new Device(0, // D3D_ADAPTER_DEFAULT - видеоадаптер по
                                    // умолчанию
                DeviceType.Hardware, // Тип устройства - аппаратный ускоритель
                render_object, // Окно для вывода графики
                CreateFlags.SoftwareVertexProcessing, // Геометрию обрабатывает CPU
                d3dpp);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка инициализации");
                return true; // Закрываем окно
            }

            POV = Matrix.PerspectiveFovLH((float)Math.PI / 4, Width / Height, 1.0f, 20.0f);
            Target = Matrix.LookAtLH(CameraPosition, CameraTarget, new Vector3(0.0f, 1.0f, 0.0f));

            Paint();
            return false;
        }

        public void RetrieveCamera(Vector3 vector)
        {
            CameraPosition = vector;
        }

        public void RetrieveFocus(Vector3 vector)
        {
            CameraTarget = vector;
        }

        /// <summary>
        /// Расположение камеры
        /// </summary>
        private void SetupCamera()
        {
            d3d.Transform.Projection = POV;
            d3d.Transform.View = Target;
        }
        /// <summary>
        /// Расположение света
        /// </summary>

        private void SetLight()
        {
            d3d.Lights[0].Position = new Vector3(0.0f, 0.0f, 0.0f);
            d3d.Lights[0].Diffuse = Color.White;
            d3d.Lights[0].Enabled = true;
        }

        /// <summary>
        /// Прорисовка сцены
        /// </summary>

        public void Paint()
        {
            if (d3d == null) return;
            // Очищаем буфер глубины и дублирующий буфер
            d3d.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.White, 1.0f, 0);

            d3d.BeginScene();

            SetLight();

            SetupCamera();

            observers.ForEach(o => o.OnNext(d3d));

            d3d.EndScene();

            if (On_Invalidate != null) On_Invalidate();

            //Обновление состояния дисплея
            d3d.Present();
        }


        public IDisposable Subscribe(IObserver<Device> observer)
        {
            observers.Add(observer);
            return new Subscription();
        }

        /// <summary>
        /// Возвращение использования ресурсов
        /// </summary>

        public void Dispose()
        {
            if (d3d != null)
                d3d.Dispose();
            if (observers.Count > 0)
            {
                foreach (var observer in observers)
                {
                    observer.OnCompleted();
                }
            }
        }

        public void UserControl(KeyEventArgs e)
        {
            Vector3 Forward = Vector3.Subtract(CameraTarget, CameraPosition); Forward.Normalize();
            Vector3 Left = new Vector3(-Forward.Z, 0.0f, Forward.X); Left.Normalize();
            Vector3 Up = Vector3.Cross(Left, Forward); Up.Normalize();

            Forward.Multiply(0.05f);
            Left.Multiply(0.05f);
            Up.Multiply(0.05f);

            Vector3 camera_vector = new Vector3();
            Vector3 camera_target = new Vector3();

            switch (e.KeyData)

            {
                case Keys.W:
                    {
                        camera_vector = Forward;
                        camera_target = Forward;
                        break;
                    }
                case Keys.S:
                    {
                        camera_vector = -Forward;
                        camera_target = -Forward;
                        break;
                    }
                case Keys.A:
                    {
                        camera_vector = Left;
                        camera_target = Left;
                        break;
                    }
                case Keys.D:
                    {
                        camera_vector = -Left;
                        camera_target = -Left;
                        break;
                    }
                case Keys.R:
                    {
                        camera_vector = Up;
                        camera_target = Up;
                        break;
                    }
                case Keys.F:
                    {
                        camera_vector = -Up;
                        camera_target = -Up;
                        break;
                    }

                case Keys.I:
                    {
                        camera_vector = new Vector3(0f, 0.05f, 0f);
                        break;
                    }
                case Keys.K:
                    {
                        camera_vector = new Vector3(0f, -0.05f, 0f);
                        break;
                    }
                case Keys.J:
                    {
                        camera_vector = new Vector3( 0.05f, 0f, 0f);
                        break;
                    }
                case Keys.L:
                    {
                        camera_vector = new Vector3( 0.05f, 0f, 0f);
                        break;
                    }

                default: { break; }
            }


            CameraPosition.Add(camera_vector);
            CameraTarget.Add(camera_target);
        }

        private class Subscription : IDisposable
        {
            public void Dispose()
            {
               throw new NotImplementedException();
            }
        }

    }
}
