using System.ComponentModel.DataAnnotations;

namespace MailSubscriptionsApi.Models
{
    public class SubscribeRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserId is required")]
        [EmailAddress(ErrorMessage = "UserId should be an email")]
        public string UserId { get; set; }

        [MinLength(1, ErrorMessage = "At least one topic should be selected")]
        public int[] Topics { get; set; }
    }
}
