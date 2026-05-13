using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

Console.WriteLine("Hello, World!");

//Test til console app
IShowRepository repository = new ShowRepository();
List<Show> shows = repository.GetAll();

foreach (Show show in shows)
{
    Console.WriteLine($"Forestillingen {show.ShowName} finder sted i {show.City.Name} d. {show.Date} !\n Kom og oplev aftenens stjerner:");
    foreach (Artist artist in show.Artists)
    {
        Console.WriteLine($"{artist.Act}, {artist.FullName}");
    }
}
