using Plugin.FirebasePushNotification;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFPushNotifications
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            Trace.WriteLine("Hola soy el programa y ahora mismo estoy antes de la linea de token");
            Trace.WriteLine("Token: " + CrossFirebasePushNotification.Current.Token);

            //Se ejecuta en el caso de que el token se refresque ( comunmente al ejecutar en debug )
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"Token: {p.Token}");
            };

            //Se ejecuta al recibir una notificacion ( y por lo que he estado comprobando, cuando se recibe la notificación mientras se usa la app ).
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                Trace.WriteLine("Received");
                System.Diagnostics.Debug.WriteLine("Received");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    Trace.WriteLine($"{data.Key} : {data.Value}");
                }
            };

            //Se ejecuta cuandose abre la notificación mostrada en la bandeja del sistema
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach ( var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                }
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
