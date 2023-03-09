using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Blaze_2._0 {

    class TelegramBot {



        private static int idResposta = 0;

        private static TelegramBotClient client = new TelegramBotClient("6117182932:AAEpaORpaDAfMh2DfDhWmjsK9uarcNkJ3ok");
        private static List<int> ListaApagar = new List<int>();

            public async void EnviarMensagem(string mensagem, bool responder, EnumMensagem tipoMensagem, Color corWin,Message msgAtual = null) {
            try {

                if(corWin == Color.Red) {
                    mensagem = mensagem.Replace("{CORES}", "🔴 + 🟤");
                }
                else {
                    mensagem = mensagem.Replace("{CORES}", "🔵 + 🟤");
                }

                Emoji.ReplaceEmojis(ref mensagem);

                if(responder) {
                    msgAtual = await client.SendTextMessageAsync(chatId: -1001822411577, text: mensagem, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, disableWebPagePreview: true, replyToMessageId: idResposta);
                }
                else {
                    msgAtual = await client.SendTextMessageAsync(chatId: -1001822411577, text: mensagem, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, disableWebPagePreview: true);
                }
                idResposta = msgAtual.MessageId;

                ConsomeListaApagar(tipoMensagem, msgAtual.MessageId);
                ApagarMensagem(tipoMensagem);
                ZeraListaApagar(tipoMensagem);



            } catch {
            }



        }




        private void ConsomeListaApagar(EnumMensagem tipoMensagem, int apagar) {
            if(tipoMensagem == EnumMensagem.Aguarde1Notificacao ||
                tipoMensagem == EnumMensagem.Aguarde2Notificacoes ||
                tipoMensagem == EnumMensagem.QuebrouNo1Crash ||
                tipoMensagem == EnumMensagem.QuebrouNo2Crash) {
                ListaApagar.Add(apagar);
            }
        }

        private void ZeraListaApagar(EnumMensagem tipoMensagem) {
            if(tipoMensagem == EnumMensagem.QuebrouNo1Crash ||
               tipoMensagem == EnumMensagem.QuebrouNo2Crash ||
               tipoMensagem == EnumMensagem.EntradaConfirmada) {
                ListaApagar.Clear();
            }
        }

        public void ApagarMensagem(EnumMensagem tipoMensagem) {
            if(tipoMensagem == EnumMensagem.QuebrouNo1Crash ||
               tipoMensagem == EnumMensagem.QuebrouNo2Crash) {
                foreach(int numero in ListaApagar) {
                    try {
                        new DataBase().Insert($"INSERT INTO ApagarMensagem VALUES (Null,\"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\",{numero});");
                    } catch {

                    }
                }
            }

        }
    }

}
