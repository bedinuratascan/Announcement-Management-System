using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Responses
{
    public class LikeResponse : BaseResponse
    {
        public Like like { get; set; }
        private LikeResponse(bool success, string message, Like like) : base(success, message)
        {
            this.like = like;
        }

        //başarılı olursa
        public LikeResponse(Like like) : this(true, string.Empty, like) { }
        //başarısız olursa
        public LikeResponse(string message) : this(false, message, null) { }
    }
}