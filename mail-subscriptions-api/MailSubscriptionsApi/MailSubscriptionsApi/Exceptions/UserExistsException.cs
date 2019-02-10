using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Exceptions
{
    public class UserExistsException: Exception
    {
        public UserExistsException(string message): base(message)
        {}
    }
}
