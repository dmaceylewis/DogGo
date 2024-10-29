using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add a Name...")]
        [DisplayName("Walker Name")]
        [MaxLength(35)]
        public string Name { get; set; }
        
        public string ImageUrl { get; set; }

        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
