namespace CirkusLuna.ClassLibrary.Model
{
    public class Show
    {
        //Show Properties
        public int Id { get; set; }
        public string ShowName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public int TotalSeats { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();
        
        //Store city object - By hvor forestillingen finder sted
        public City City { get; set; }

        //Constructor
        public Show(int id, string showName, DateOnly date, int totalSeats, City city)
        {
            Id = id;
            ShowName = showName;
            Date = date;
            TotalSeats = totalSeats;
            City = city;
        }
        

    }
}
