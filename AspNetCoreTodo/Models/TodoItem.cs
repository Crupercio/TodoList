using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        
        public bool IsDone { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string? Title { get; set; }

         [Required]
        public DateTimeOffset DateDue { get; set; } = DateTimeOffset.Now.AddDays(3); // Default value
    }
}