using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
    public class BaseViewModelHelper
    {
        private DatabaseContext db = new DatabaseContext();
        public List<ContentGroup> GetMenuContentGroup()
        {
            return db.ContentGroups.Where(c => c.IsDeleted == false && c.IsActive).OrderBy(x=>x.CreationDate).ToList();
        }
    }
}