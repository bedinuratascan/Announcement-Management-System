using System.Collections.Generic;
using System.Threading.Tasks;
using AnnouncApp.Domain.Services;
using AnnouncApp.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnnouncApp.Extensions;
using AnnouncApp.Domain;
using Microsoft.AspNetCore.Authorization;

namespace AnnouncApp.Controllers
{
    [Authorize]
    [Route("api/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {

        private IAnnouncementService AnnouncementService { get; }
        private IMapper Mapper { get; }
        private User CurrentUser { get; }
        private IInteractionService InteractionService { get; }

        public AnnouncementController(IAnnouncementService announcementService,
            IMapper mapper,
            IUserService userService,
            IInteractionService interactionService,
            IHttpContextAccessor httpContextAccessor)
        {
            AnnouncementService = announcementService;
            Mapper = mapper;
            CurrentUser = userService.CurrentUser(httpContextAccessor.HttpContext.User);
            InteractionService = interactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "offset")] int offset = 0,
            [FromQuery(Name = "length")] int length = 15)
        {
            var announcements = await AnnouncementService.ListAsync(offset, length);
            var total = await AnnouncementService.CountAsync();
            var result = new Serializer.AnnouncementSerializer.IndexSerializer(offset, length, total, announcements, CurrentUser);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            Announcement announcement = await AnnouncementService.FindByIdAsync(id);

            if (announcement == null)
                return NotFound();

            await InteractionService.AddInteractionAsync(CurrentUser, announcement, Interaction.InteractionType.VIEW);

            var response = new Serializer.AnnouncementSerializer.ShowSerializer(announcement, CurrentUser);

            return Ok(response.Response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementResource announcementResource)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState.GetErrorMessages());

            Announcement announcement = Mapper.Map<AnnouncementResource, Announcement>(announcementResource);
            announcement = await AnnouncementService.AddAnnouncement(announcement, CurrentUser);

            if ( announcement == null )
                return UnprocessableEntity(new { message = "Duyuru eklenemedi" });

            var response = new Serializer.AnnouncementSerializer.CreateSerializer(announcement, CurrentUser);
            return Created($"/api/announcement/${announcement.Id}", response.Response);
        }

        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery(Name = "text")] string text,
            [FromQuery(Name = "offset")] int offset = 0,
            [FromQuery(Name = "length")] int length = 15)
        {
            var announcements = await AnnouncementService.SearchAsync(text, offset, length);
            var total = await AnnouncementService.SearchCountAsync(text);
            var result = new Serializer.AnnouncementSerializer.IndexSerializer(offset, length, total, announcements, CurrentUser);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(AnnouncementResource announcementResource, int id)
        {
            Announcement announcement = await AnnouncementService.FindByIdAsync(id);

            if (announcement == null)
                return NotFound();

            if (!announcement.User.Equals(CurrentUser))
                return Unauthorized();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState.GetErrorMessages());
            
            announcement = Mapper.Map<AnnouncementResource, Announcement>(announcementResource);
            announcement = await AnnouncementService.UpdateAnnouncement(announcement, id);

            if (announcement == null)
                return UnprocessableEntity(new { message = "Duyuru güncellenemedi" });

            var response = new Serializer.AnnouncementSerializer.ShowSerializer(announcement, CurrentUser);
            return Ok(response.Response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            Announcement announcement = await AnnouncementService.FindByIdAsync(id);

            if (announcement == null)
                return NotFound();

            if (!announcement.User.Equals(CurrentUser))
                return Unauthorized();

            announcement = await AnnouncementService.RemoveAnnouncement(id);

            if (announcement == null)
                return UnprocessableEntity(new { message = "Duyuru silinemedi" });

            return Ok();
        }
    }
}