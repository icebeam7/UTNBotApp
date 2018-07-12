using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Bot.Connector.DirectLine;
using UTNBotApp.Modelos;
using UTNBotApp.Helpers;
using System;
using System.IO;

namespace UTNBotApp.Servicios
{
    public class ServicioBotDirectLine
    {
        public DirectLineClient ClienteDL;
        public Conversation Conversacion;
        public ChannelAccount Cuenta;

        public ServicioBotDirectLine(string nombre)
        {
            var tokenResponse = new DirectLineClient(Constantes.DirectLineSecret).Tokens.GenerateTokenForNewConversation();

            ClienteDL = new DirectLineClient(tokenResponse.Token);
            Conversacion = ClienteDL.Conversations.StartConversation();
            Cuenta = new ChannelAccount { Id = nombre, Name = nombre };
        }

        public void EnviarImagen(Stream imagen)
        {
            try
            {
                ClienteDL.Conversations.Upload(Conversacion.ConversationId, imagen, Constantes.BotUser, "image/jpeg");
            }
            catch (Exception)
            {
            }
        }

        public void EnviarMensaje(string mensaje)
        {
            try
            {
                var activity = new Activity
                {
                    From = Cuenta,
                    Text = mensaje,
                    Type = ActivityTypes.Message
                };

                ClienteDL.Conversations.PostActivity(Conversacion.ConversationId, activity);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task ObtenerMensajes(ObservableCollection<Mensaje> collection)
        {
            string watermark = null;

            while (true)
            {
                var set = await ClienteDL.Conversations.GetActivitiesAsync(Conversacion.ConversationId, watermark);
                watermark = set?.Watermark;

                foreach (var actividad in set.Activities)
                {
                    if (actividad.From.Id == Constantes.BotID)
                        collection.Add(new Mensaje(actividad.Text, actividad.From.Name));
                }

                await Task.Delay(3000);
            }
        }
    }
}
