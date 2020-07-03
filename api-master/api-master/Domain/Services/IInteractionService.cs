using AnnouncApp.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Services
{
    public interface IInteractionService
    {
        Task<Interaction> AddInteractionAsync(User user, Announcement announcement, Interaction.InteractionType interactionType);
        Task RemoveInteractionAsync(User user, Announcement announcement, Interaction.InteractionType interactionType);
    }
}
