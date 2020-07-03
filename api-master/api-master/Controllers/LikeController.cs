using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AnnouncApp.Domain;
using AnnouncApp.Domain.Responses;
using AnnouncApp.Domain.Services;
using AnnouncApp.Resources;
using AnnouncApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncApp.Controllers
{
    [Authorize]
    [Route("api/announcement/{id:int}/like")]
    [ApiController]
    public class LikeController : Controller
    {
        public IAnnouncementService AnnouncementService { get; }
        public IMapper Mapper { get; }
        public User CurrentUser { get; }
        public ILikeService LikeService { get; }
        public IUserService UserService { get; }

        public LikeController(IUserService userService,
            IAnnouncementService announcementService,
            ILikeService likeService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.UserService = userService;
            this.AnnouncementService = announcementService;
            this.CurrentUser = userService.CurrentUser(httpContextAccessor.HttpContext.User);
            this.LikeService = likeService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(int id)
        {
            Announcement announcement = await AnnouncementService.FindByIdAsync(id);

            if (announcement == null)
                return NotFound();

            var control = announcement.Likes.Exists(l => l.User.Equals(CurrentUser));

            if (control)
                return BadRequest(new { message = "Zaten bu duyuruyu beğendiniz."});

            if (announcement.User.Equals(CurrentUser))
                return BadRequest(new { message = "Kendi duyurunuzu beğenemezsiniz." });

            await LikeService.AddLikeAsync(CurrentUser, announcement);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Destroy(int id)
        {
            Announcement announcement = await AnnouncementService.FindByIdAsync(id);

            if (announcement == null)
                return NotFound();

            var control = announcement.Likes.Exists(l => l.User.Equals(CurrentUser));

            if (!control)
                return BadRequest(new { message = "Zaten bu duyuruyu beğenmediniz." });


            await LikeService.RemoveLikeAsync(CurrentUser, announcement);

            return Ok();
        }
    }
}