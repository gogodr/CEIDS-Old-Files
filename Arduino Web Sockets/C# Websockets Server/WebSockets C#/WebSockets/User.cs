using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alchemy.Classes;

namespace WebSockets
{
    class User
    {
        public int id;
        public UserContext context;

        public User(int id, UserContext context) {
            this.id = id;
            this.context = context;        
        }
    }
}
