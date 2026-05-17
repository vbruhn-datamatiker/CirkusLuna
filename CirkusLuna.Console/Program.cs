using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;



//Test til console app
IShowRepository repository = new ShowRepository();
IEmployeeRepository employeeRepository = new EmployeeRepository();

//Indledning
Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine("--------------------- Velkommen til Cirkus Luna ---------------------\n");
Console.ResetColor();

Console.WriteLine("------------------ Menu ------------------");

Console.WriteLine("Se alle fremtidige forestillinger - Tast 1");
Console.WriteLine("Søg efter den næste forestilling i en by - Tast 2");
Console.WriteLine("\nLog ind som medarbejder - Tast 3");

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
} else if (choice == "3")
    {
    Console.WriteLine("Angiv venligst dit medarbejder password:");
    string employeePassword = Console.ReadLine();

    //Medarbejder loggedIn. Default med ingen fundet medarbejder - eksisterer efter loop
    Employee loggedIn = null;
    
    //Gennemgår alle medarbejdere i listen
    foreach (Employee employee in employeeRepository.GetAll())
    {
        //Tjekker efter match med password
        if (employee.Password == employeePassword) 
            { 
                //Gemmer medarbejder i loggedIn
                loggedIn = employee;
                //Stop loop
                break;
            }
    }

    //Hvis loggedIn ikke er null (Medarbejder fundet)
    if (loggedIn != null)
        {
            //Sender medarbejder til "Profil"
            Console.WriteLine($"Velkommen til din profil {loggedIn.FullName}.");
            Console.WriteLine($"Din information:\n{loggedIn.Role}\n{loggedIn.Email}\n");

            Console.WriteLine("Hvad vil du foretage dig nu?");
        
    }
    }


