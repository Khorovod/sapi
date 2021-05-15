
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.DataTransferObjects
{
    //полностью дублирует модель для создания, как исправить?
    public class CommandUpdateDto
    {
        [Required]
        [MaxLength(123)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        public string Platform { get; set; }

    }
}