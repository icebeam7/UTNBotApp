using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UTNBotApp.Helpers;
using UTNBotApp.Modelos;
using UTNBotApp.Servicios;

namespace UTNBotApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaBot : ContentPage
    {
        ServicioBotDirectLine bot;
        ObservableCollection<Mensaje> listaMensajes;

        public PaginaBot ()
        {
            InitializeComponent ();
            bot = new ServicioBotDirectLine(Constantes.BotUser);
            listaMensajes = new ObservableCollection<Mensaje>();

            lsvMensajes.ItemsSource = listaMensajes;
            bot.ObtenerMensajes(listaMensajes);
        }

        public async void btnImagen_Clicked(object sender, EventArgs e)
        {
            var imagen = await ServicioImagen.TomarFoto();
            if (imagen != null)
                bot.EnviarImagen(imagen.GetStream());
        }

        public void btnEnviar_Clicked(object sender, EventArgs e)
        {
            var texto = txtMensaje.Text;

            if (texto.Length > 0)
            {
                txtMensaje.Text = "";

                var mensaje = new Mensaje(texto, bot.Cuenta.Name);
                listaMensajes.Add(mensaje);
                bot.EnviarMensaje(texto);
            }
        }
    }
}