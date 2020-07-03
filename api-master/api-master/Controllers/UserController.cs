using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AnnouncApp.Domain;
using AnnouncApp.Domain.Responses;
using AnnouncApp.Domain.Services;
using AnnouncApp.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private IAnnouncementService AnnouncementService { get;  }
        private ILikeService LikeService { get;  }
        private IRecommendationService RecommendationService { get;  }

        public UserController(IUserService userService,
            IMapper mapper,
            IAnnouncementService announcementService,
            ILikeService likeService,
            IRecommendationService recommendationService)
        {
            this.userService = userService;
            this.mapper = mapper;
            AnnouncementService = announcementService;
            LikeService = likeService;
            RecommendationService = recommendationService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;
            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            UserResponse userResponse = userService.FindById(int.Parse(userId));
            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = mapper.Map<UserResource, User>(userResource);

            var existEmail = userService.FindByEmail(user.Email);

            if (existEmail != null)
                return BadRequest(new { message = "Email adresi kullanımda" });


            UserResponse userResponse = userService.AddUser(user);
            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }
            else
            {
                return BadRequest(new { message = userResponse.Message });
            }

        }

        [Authorize]
        [Route("announcements")]
        [HttpGet]
        public async Task<IActionResult> AnnouncementsAsync([FromQuery(Name = "offset")] int offset = 0,
            [FromQuery(Name = "length")] int length = 15)
        {
            var currentUser = userService.CurrentUser(User);
            var announcements = await AnnouncementService.GetUserAnnouncementsAsync(currentUser, offset, length);
            var total = await AnnouncementService.UserAnnouncementsCountAsync(currentUser);
            var response = new Serializer.UserSerializer.AnnouncementsSerializer(offset, length, total, announcements);

            return Ok(response);
        }


        [Authorize]
        [Route("likes")]
        [HttpGet]
        public async Task<IActionResult> LikesAsync([FromQuery(Name = "offset")] int offset = 0,
            [FromQuery(Name = "length")] int length = 15)
        {
            var currentUser = userService.CurrentUser(User);
            var likes = await LikeService.GetUserLikesAsync(currentUser, offset, length);
            var total = await LikeService.UserLikesCountAsync(currentUser);
            var response = new Serializer.UserSerializer.LikesSerializer(offset, length, total, likes);

            return Ok(response);
        }

        [Authorize]
        [Route("recommendations")]
        [HttpGet]
        public async Task<IActionResult> RecommendationsAsync([FromQuery(Name = "offset")] int offset = 0,
            [FromQuery(Name = "length")] int length = 15)
        {
            var currentUser = userService.CurrentUser(User);
            var recommendations = await RecommendationService.GetUserRecommendationsAsync(currentUser, offset, length);
            var total = await RecommendationService.UserRecommendationsCountAsync(currentUser);
            var response = new Serializer.UserSerializer.RecommendationsSerializer(offset, length, total, recommendations);

            return Ok(response);
        }
    }
}