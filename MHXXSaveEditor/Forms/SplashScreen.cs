using System;
using System.Windows.Forms;
using System.Threading;

namespace MHXXSaveEditor.Forms
{
    public partial class SplashScreen : Form
    {
        private delegate void CloseDelegate();

        private static SplashScreen splashForm;

        static public void ShowSplashScreen()
        {
            if (splashForm != null && !splashForm.IsDisposed)
                return;
            splashForm = null;
            Thread thread = new Thread(new ThreadStart(ShowForm))
            {
                IsBackground = true
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            var form = new SplashScreen();
            splashForm = form;
            Application.Run(form);
        }

        static public void CloseForm()
        {
            var form = splashForm;
            if (form == null)
                return;

            try
            {
                // The splash thread may not have created the window handle yet.
                for (int i = 0; i < 50 && form != null && !form.IsDisposed && !form.IsHandleCreated; i++)
                    Thread.Sleep(10);

                if (form.IsDisposed)
                {
                    splashForm = null;
                    return;
                }

                if (form.IsHandleCreated)
                    form.BeginInvoke(new CloseDelegate(CloseFormInternal));
                else
                    splashForm = null;
            }
            catch (ObjectDisposedException)
            {
                splashForm = null;
            }
            catch (InvalidOperationException)
            {
                splashForm = null;
            }
        }

        static private void CloseFormInternal()
        {
            var form = splashForm;
            splashForm = null;
            if (form != null && !form.IsDisposed)
                form.Close();
        }

        public SplashScreen()
        {
            InitializeComponent();
        }
    }
}
