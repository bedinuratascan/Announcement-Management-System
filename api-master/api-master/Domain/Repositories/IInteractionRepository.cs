using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{
    public interface IInteractionRepository
    {
        Task AddInteractionAsync(Interaction interaction);
        Task<Interaction> FindAsync(Announcement announcement, User user, Interaction.InteractionType interactionType);
        void RemoveUserInteraction(Interaction interaction);
    }
}
