using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;



//Test til console app
IShowRepository repository = new ShowRepository();

//Indledning
Console.WriteLine("Velkommen til Cirkus Luna");
Console.WriteLine("Se alle nærmeste forestillinger - Tast 1");
Console.WriteLine("Søg efter den næste forestilling i en by - tast 2");

//Søg efter forestillinger
string choice = Console.ReadLine();

if (choice == "1")
{
    List<Show> shows = repository.GetAll();

    foreach (Show show in shows)
    {
        Console.WriteLine($"Forestillingen {show.ShowName} finder sted i {show.City.Name} d. {show.Date} !\n Kom og oplev aftenens stjerner:");
        foreach (Artist artist in show.Artists)
        {
            Console.WriteLine($"{artist.Act}, {artist.FullName}");
        }
    }
} 

else if (choice == "2")
{
    Console.WriteLine("Indtast bynavn");
    string cityInput = Console.ReadLine();
    List<Show> shows = repository.GetByCity(cityInput);

    if (shows.Count == 0)
    {
        Console.WriteLine($"Ingen forestillinger fundet i {cityInput}.");
    }
    else
    {
        foreach (Show show in shows)
        {
            Console.WriteLine($"\n{show.ShowName} kommer til {show.City.Name} d. {show.Date}. \n Følgende stjerner optræder:");
            foreach (Artist artist in show.Artists)
            {
                Console.WriteLine($"{artist.Act}, {artist.FullName}");
            }
            Console.WriteLine($"Der er {show.Seats} antal ledige pladser og {show.VipSeats} VIP pladser. Book nu mens der stadig er ledige biletter!");
        }
    }
}


