using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMT.Domain.Abstractions;

namespace BMT.Domain.Users.UserErrors
{
    public static class ManagerErrors
    {
        public static Error ManagerDoesNotExist =
            new("User.Existence", "Manager does not exist.");        
    }
}
