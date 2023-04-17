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
    public class DirectX9Facade : IDirectXFormDisplayer
    {
        public Device d3d { get; private set; } =  null;

        private List<IObserver<Device>> observers = new List<IObserver<Device>>();

        public event InvalidateDelegate On_Invalidate;

        private Vector3 CameraPosition { get; set; } = new Vector3();
        private Vector3 CameraTarget { get; set; } = new Vector3();

        public int Width { get; set; }
        public int Height { get; set; }

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
            d3d.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, Width / Height, 1.0f, 50.0f);

            d3d.Transform.View = Matrix.LookAtLH(CameraPosition, CameraTarget, new Vector3(0.0f, 1.0f, 0.0f));
        }
        /// <summary>
        /// Расположение света
        /// </summary>

        private void SetLight()
        {
            d3d.Lights[0].Diffuse = Color.White;
            d3d.Lights[0].Position= CameraPosition;
            d3d.Lights[0].Direction=CameraTarget;
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

            SetupCamera();

            SetLight();

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
        private readonly Keys[] _keys = new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D, Keys.C, Keys.Space
        , Keys.I, Keys.K, Keys.J, Keys.L, Keys.Y, Keys.H};

        public Keys[] keys => _keys;

        public void UserControl(IEnumerable<Keys> keyValues)
        {
            const float camera_multiplier = 0.05f;
            const float camera_targer_multiplier = 1;


            Vector3 Forward = Vector3.Subtract(CameraTarget, CameraPosition); Forward.Normalize();
            Vector3 Left = new Vector3(-Forward.Z, 0.0f, Forward.X); Left.Normalize();
            Vector3 Up = Vector3.Cross(Left, Forward); Up.Normalize();
            
            Forward.Multiply(0.05f);
            Left.Multiply(0.05f);
            Up.Multiply(0.05f);
           
            Vector3 camera_vector = new Vector3();
            Vector3 camera_target = new Vector3();


            Vector3 x_vector = new Vector3(camera_targer_multiplier, 0, 0);
            Vector3 y_vector = new Vector3(0, camera_targer_multiplier, 0);
            Vector3 z_vector = new Vector3(0, 0, camera_targer_multiplier);
            foreach (var e in keyValues)
            {
                switch (e)
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
                    case Keys.Space:
                        {
                            camera_vector = Up;
                            camera_target = Up;
                            break;
                        }
                    case Keys.C:
                        {
                            camera_vector = -Up;
                            camera_target = -Up;
                            break;
                        }

                    case Keys.I:
                        {
                            camera_target = y_vector;
                            break;
                        }
                    case Keys.K:
                        {
                            camera_target = -y_vector;
                            break;
                        }
                    case Keys.J:
                        {
                            camera_target = -x_vector;
                            break;
                        }
                    case Keys.L:
                        {
                            camera_target = x_vector;
                            break;
                        }
                    case Keys.Y:
                        {
                            camera_target = z_vector;
                            break;
                        }
                    case Keys.H:
                        {
                            camera_target = -z_vector;
                            break;
                        }

                    default: { break; }
                }


                CameraPosition += camera_vector;
                CameraTarget += camera_target;
            }
            



        }

        public void MouseControl(float X, float Y)
        {
            //CameraTarget += new Vector3(X, Y, 0);
        }

       

    }

    internal class Subscription : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
