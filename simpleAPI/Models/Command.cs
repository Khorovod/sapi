using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(123)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        public string Platform { get; set; }
        
    }
}