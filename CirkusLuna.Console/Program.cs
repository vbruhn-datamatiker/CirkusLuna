

Console.WriteLine("Hello, World!");

//Test til console app
IShowRepository repository = new ShowRepository();
List<Show> shows = repository.GetAll();

foreach (Show show in shows)
{
    Console.WriteLine($"Forestillingen {show.ShowName} finder sted i {show.City.Name} d. {show.Date} ! ");
}
