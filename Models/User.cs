using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChatAppXpress.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }

        [ValidateNever]
        public ICollection<User> Contacts { get; set; }

        [ValidateNever]
        public ICollection<Message> SentMessages { get; set; }
        [ValidateNever]
        public ICollection<Message> ReceiveMessages { get; set; }
    }
}
