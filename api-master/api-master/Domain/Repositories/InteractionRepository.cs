using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{

    public class InteractionRepository : BaseRepository, IInteractionRepository
    {
        public InteractionRepository(AnnouncAppDBContext context) : base(context) { }

        public async Task AddInteractionAsync(Interaction interaction)
        {
            await context.Interaction.AddAsync(interaction);
        }

        public async Task<Interaction> FindAsync(Announcement announcement, User user, Interaction.InteractionType interactionType)
        {
            return await context.Interaction.SingleOrDefaultAsync(ui => ui.Announcement.Equals(announcement) && ui.User.Equals(user) && ui.Type.Equals(interactionType));
        }

        public void RemoveUserInteraction(Interaction interaction)
        {
            context.Interaction.Remove(interaction);
        }
    }
}
