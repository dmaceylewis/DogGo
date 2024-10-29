using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Dog Name")]
        public string? Name { get; set; }


        [DisplayName("Dog Breed")]
        public string? Breed { get; set; }

        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }

        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
