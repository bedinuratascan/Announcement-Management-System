using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncApp.Domain.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AnnouncAppDBContext context;
        public UnitOfWork(AnnouncAppDBContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
