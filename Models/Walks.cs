using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walks
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = @"{0:hh\:mm\:ss}")]
        public TimeSpan Duration { get; set; }

        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public Walker Walker { get; set; }
        public Dog Dog { get; set; }
    }
}
