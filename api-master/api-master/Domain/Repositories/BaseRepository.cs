using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.Repositories
{
    public class BaseRepository
    {

        protected readonly AnnouncAppDBContext context;
        public BaseRepository(AnnouncAppDBContext context)
        {
            this.context = context;
        }
    }
}
