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

    public class TestObserver : IObserver<Device>, IDisposable
    {

        public TestObserver(IObservable<Device> observable)
        {
           observable.Subscribe(this);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            Dispose();
        }

        public void OnError(Exception error)
        {
            Dispose();
        }

        public void OnNext(Device value)
        {
            throw new NotImplementedException();
        }
    }
    public delegate void InvalidateDelegate();
    public class DirectX9Facade : IObservable<Device>, IDisposable
    {
        private Device d3d;
        private Control control;

        private List<IObserver<Device>> observers;

        public event InvalidateDelegate On_Invalidate;

        public Vector3 CameraPosition { get; set; } = new Vector3();
        public Vector3 CameraTarget { get; set; } = new Vector3();

        public DirectX9Facade(Control render_object)
        {
            control = render_object;
        }
        public bool BuildUp()
        {
            try
            {
                // Устанавливаем режим отображения трехмерной графики
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
                control, // Окно для вывода графики
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

        public void RetrieveCamera()
        {
            CameraPosition = new Vector3();
        }

        public void RetrieveFocus()
        {
            CameraTarget = new Vector3();
        }

        /// <summary>
        /// Расположение камеры
        /// </summary>
        private void SetupCamera()
        {

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


      
        private class Subscription : IDisposable
        {
            public void Dispose()
            {
               
            }
        }

    }
}
