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
        

        private const string WelcomeText = "Sana çok güzel dakikalar yaþatacaðým.!";

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
                      
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                string input = GetTextWithoutMentions(turnContext);
                //await turnContext.SendActivityAsync(MessageFactory.Text($"Input : {input}"), cancellationToken);
                
                if (input == "GeyikBot sa" || input == "GeyikBot SA" || input == "GeyikBot Sa")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Aleyküm Selam, Topraaaam."), cancellationToken);
                if (input == "GeyikBot Naber" || input == "GeyikBot Nasýlsýn?" || input == "GeyikBot Naber?" || input == "GeyikBot nasýlsýn?")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Ne olsun, seni sormali?."), cancellationToken);
                if (input == "GeyikBot sa" || input == "GeyikBot SA" || input == "GeyikBot Sa")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Aleyküm Selam, Topraaaam."), cancellationToken);
                if (input == "GeyikBot Naber Goçum" || input == "GeyikBot naber goçum" || input == "GeyikBot Naber goçum" || input == "GeyikBot goçum" || input == "GeyikBot Goçum")
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Gocum mu? Hayýrdýr La sen bebe."), cancellationToken);
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
                        messageText = messageText.Replace(m[i].Text, "");
                }
            }

            return messageText;
        }


        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(
                        $"Oo Hoþ Geldin {member.Name}. {WelcomeText}",
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}
