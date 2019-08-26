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

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {   


            if (turnContext.Activity.Text == "Hello")
            {
                
                await turnContext.SendActivityAsync(MessageFactory.Text($"From: {turnContext.Activity.From}"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Recipient.Name: {turnContext.Activity.Recipient.Name}"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Recipient.Id: {turnContext.Activity.Recipient.Id}"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Recipient.Role: {turnContext.Activity.Recipient.Role}"), cancellationToken);
                await turnContext.SendActivityAsync(MessageFactory.Text($"Recipient.Role: {turnContext.Activity.Type}"), cancellationToken);
            }
            
        }
    }
}
