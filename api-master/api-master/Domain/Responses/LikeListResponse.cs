using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Responses
{
    public class LikeListResponse : BaseResponse
    {
        public IEnumerable<Like> likeList { get; set; }
        private LikeListResponse(bool success, string message, IEnumerable<Like> likeList) : base(success, message)
        {
            this.likeList = likeList;
        }
        public LikeListResponse(IEnumerable<Like> likeList) : this(true, string.Empty, likeList)
        {

        }

        public LikeListResponse(string message) : this(false, message, null)
        {

        }

    }
}