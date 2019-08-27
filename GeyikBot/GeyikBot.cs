// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace GeyikBot
{
    public class EmptyBot : ActivityHandler
    {
       

        private const string WelcomeText = "Sana �ok g�zel dakikalar ya�ataca��m.!";
   
        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
          
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                string input = turnContext.Activity.Text;
                if (input == "@GeyikBot !Naber")
                await turnContext.SendActivityAsync(MessageFactory.Text($"�yi senden naber c�n�ms?"), cancellationToken);
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
