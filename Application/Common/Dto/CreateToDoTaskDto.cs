using System.ComponentModel.DataAnnotations;

namespace ToDo.Application.Common.Dto
{
    public class CreateToDoTaskDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Task { get; set; } = string.Empty;
    }
}
