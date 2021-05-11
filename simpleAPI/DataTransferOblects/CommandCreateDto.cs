
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.DataTransferObjects
{
    public class CommandCreateDto
    {
        //бд автоматически сделает
        //public int Id { get; set; }
        
        //с аннотациями ошибка будет 400 а не 500
        [Required]
        [MaxLength(123)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        public string Platform { get; set; }

    }
}