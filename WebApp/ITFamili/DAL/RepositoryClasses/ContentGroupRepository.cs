﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class ContentGroupRepository : Repository<Models.ContentGroup>, IContentGroupRepository
    {
        public ContentGroupRepository(Models.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
        {

        }

    }
}
