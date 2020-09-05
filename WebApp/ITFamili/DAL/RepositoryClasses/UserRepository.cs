﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Models.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
        {

        }

        public User GetByToken(string token)
        {
            User user = Get(current => current.Token == token).FirstOrDefault();

            if (user != null)
                return user;
            else
                return new User();

        }
    }
}
