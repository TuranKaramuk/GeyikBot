// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;

namespace GeyikBot
{
    public class EmptyBot : ActivityHandler
    {

        private List<string> additionalWordList;
        private const string WelcomeText = "Sana cok guzel dakikalar yasatacagim.!";

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
                      
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                string input = GetTextWithoutMentions(turnContext);
                //await turnContext.SendActivityAsync(MessageFactory.Text($"Input : {input}"), cancellationToken);

                if (input == "sa" || input == "SA" || input == "Sa")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Aleyk�m Selam, Topraaaam."), cancellationToken);
                else if (input == "Naber" || input == "Nas�ls�n?" || input == "Naber?" || input == "nas�ls�n?")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Ne olsun, seni sormali?."), cancellationToken);
                else if (input == "GeyikBot")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Efendim."), cancellationToken);
                else if (input == "sa" || input == "SA" || input == "Sa")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Aleyk�m Selam, Topraaaam."), cancellationToken);
                else if (input == "Naber Go�um" || input == "naber go�um" || input == "Naber go�um" || input == "go�um" || input == "Go�um" || input == "gocum")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Gocum mu? Hayirdir La sen bebe."), cancellationToken);
                else
                {
                    var memberName = turnContext.Activity.From.Name;
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Simdilik buna cevap veremiyorum {memberName}. Ilerde belki, bakalim kader."), cancellationToken);
                    //additionalWordList.Add(input);
                }
            }         
            
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded != null)
                {
                    // Send a welcome message to the user and tell them what actions they may perform to use this bot
                    await SendWelcomeMessageAsync(turnContext, cancellationToken);
                }
            }
        }



        public string  GetTextWithoutMentions(ITurnContext turnContext)
        {
            var sourceMessage = turnContext.Activity;

            Mention[] m =  turnContext.Activity.GetMentions();

            var messageText = turnContext.Activity.Text;

            for (int i = 0; i < m.Length; i++)
            {
                if (m[i].Mentioned.Id == sourceMessage.Recipient.Id)
                {
                    //Bot is in the @mention list.
                    //The below example will strip the bot name out of the message, so you can parse it as if it wasn't included. Note that the Text object will contain the full bot name, if applicable.
                    if (m[i].Text != null)
                    { messageText = messageText.Replace(m[i].Text, ""); }
                    messageText = messageText.Replace("GeyikBot", "");
                }
            }
            if (messageText.Length > 1)
                return messageText.Trim();
            else
                return "GeyikBot";
        }


        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(
                        $"Oo Ho� Geldin {member.Name}. {WelcomeText}",
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}
