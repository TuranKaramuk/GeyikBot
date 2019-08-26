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
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello world!"), cancellationToken);
                }
            }
        }

       
        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            Mention[] m = turnContext.Activity.GetMentions();
  
            var messageText = turnContext.Activity.Text;

            for (int i = 0; i < m.Length; i++)
            {
                if (m[i].Mentioned.Id == turnContext.Activity.Recipient.Id)
                {
                    //Bot is in the @mention list.
                    //The below example will strip the bot name out of the message, so you can parse it as if it wasn't included. Note that the Text object will contain the full bot name, if applicable.
                    if (m[i].Text != null)
                        messageText = messageText.Replace(m[i].Text, "");
                }
                
            }
            
                await turnContext.SendActivityAsync(MessageFactory.Text($"{m.Length}"), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text($"{turnContext.Activity.Recipient.Id}"), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text($"{messageText}"), cancellationToken);


            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                string input = turnContext.Activity.Text;
                if (input == "!Naber")
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
