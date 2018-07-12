using Xamarin.Forms;
using UTNBotApp.Helpers;

namespace UTNBotApp.Modelos
{
    public class Mensaje
    {
        public string Texto { get; set; }
        public string Emisor { get; set; }

        public TextAlignment LblMensajeTextAlignment { get; set; }
        public TextAlignment LblEmisorTextAlignment { get; set; }

        public Mensaje(string texto, string emisor)
        {
            Texto = texto;
            Emisor = emisor;

            if (emisor.ToUpper() == Constantes.BotUser)
            {
                LblMensajeTextAlignment = TextAlignment.End;
                LblEmisorTextAlignment = TextAlignment.End;
            }
            else
            {
                LblMensajeTextAlignment = TextAlignment.Start;
                LblEmisorTextAlignment = TextAlignment.Start;
            }
        }
    }
}
