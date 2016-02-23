using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAzure.ActiveDirectory.Authentication;

namespace WpfCalculos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthenticationResult _authenticationResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        private AuthenticationResult Autenticar()
        {
            if (_authenticationResult != null)
                return _authenticationResult;
            AuthenticationContext contexto = new AuthenticationContext("https://login.windows.net/dlrivadaAD.onmicrosoft.com");
            return contexto.AcquireToken("https://dlrivadaAD.onmicrosoft.com/WebApplicationAuth", "6a98d922-45be-4c8d-b9c4-21717437ec86", "http://meloinvento.com");
        }

        private async Task<string> Operar(AuthenticationResult result, string operacion, Operacion model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                HttpResponseMessage response = await client.PostAsync("https://localhost:44300/api/" + operacion, new StringContent(model.ToString(), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                return string.Empty;
            }
        }

        private async void button_Click(object sender, RoutedEventArgs e) => labelResult.Content = await Operar(Autenticar(), "suma", new Operacion(Convert.ToInt32(textBoxOp1.Text), Convert.ToInt32(textBoxOp2.Text)));

        private async void buttonMenos_Click(object sender, RoutedEventArgs e) => labelResult.Content = await Operar(Autenticar(), "resta", new Operacion(Convert.ToInt32(textBoxOp1.Text), Convert.ToInt32(textBoxOp2.Text)));

        private async void buttonPor_Click(object sender, RoutedEventArgs e) => labelResult.Content = await Operar(Autenticar(), "producto", new Operacion(Convert.ToInt32(textBoxOp1.Text), Convert.ToInt32(textBoxOp2.Text)));

        private async void buttonEntre_Click(object sender, RoutedEventArgs e) => labelResult.Content = await Operar(Autenticar(), "division", new Operacion(Convert.ToInt32(textBoxOp1.Text), Convert.ToInt32(textBoxOp2.Text)));
    }
}
