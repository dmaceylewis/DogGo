namespace DogGo.Models.ViewModels
{
    public class WalkerDetailsViewModel
    {
        public Walker Walker { get; set; }
        public List<Walks> Walks { get; set; }
        public List<Owner> Owner { get; set; }
    }
}
