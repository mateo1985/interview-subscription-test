using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Models
{
    public class UnsubscribeRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserId is required")]
        public string UserId { get; set; }
        
        public Guid UnsubscribeGuid { get; set; }
    }
}
