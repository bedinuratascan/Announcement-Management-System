using AnnouncApp.Domain;
using AnnouncApp.Domain.Repositories;
using AnnouncApp.Domain.Services;
using AnnouncApp.Domain.UnitOfWork;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnnouncApp.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly IInteractionRepository InteractionRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpClientFactory ClientFactory;

        public InteractionService(IInteractionRepository interactionRepository,
            IUnitOfWork unitOfWork,
            IHttpClientFactory clientFactory)
        {
            this.InteractionRepository = interactionRepository;
            this.unitOfWork = unitOfWork;
            ClientFactory = clientFactory;
        }

        public async Task<Interaction> AddInteractionAsync(User user, Announcement announcement, Interaction.InteractionType interactionType)
        {
            if (announcement.User.Equals(user))
                return null;

            var existInteraction = await InteractionRepository.FindAsync(announcement, user, interactionType);
            if (existInteraction != null)
                return existInteraction;

            Interaction interaction = new Interaction
            {
                User = user,
                Announcement = announcement,
                Type = interactionType
            };

            
            await InteractionRepository.AddInteractionAsync(interaction);
            await unitOfWork.CompleteAsync();

            var base_url = "http://recommendation-service:7878";
            var request = new HttpRequestMessage(HttpMethod.Get, $"{base_url}/{user.Id}");
            var client = ClientFactory.CreateClient();
            _ = client.SendAsync(request);


            return interaction;
        }

        public async Task RemoveInteractionAsync(User user, Announcement announcement, Interaction.InteractionType interactionType)
        {
            Interaction interaction = await InteractionRepository.FindAsync(announcement, user, interactionType);

            if (interaction == null)
                return;

            InteractionRepository.RemoveUserInteraction(interaction);
        }
    }
}
