using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

//Console App Program

//Opret repositories
IShowRepository showRepository = new ShowRepository();
IEmployeeRepository employeeRepository = new EmployeeRepository();
IReservationRepository reservationRepository = new ReservationRepository();
ICustomerRepository customerRepository = new CustomerRepository();


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
    //Vis alle fremtidige shows og artister
    List<Show> shows = showRepository.GetAll();

    foreach (Show show in shows)
    {
        Console.WriteLine($"SHOW NUMMER {show.Id}\nForestillingen {show.ShowName} finder sted i {show.City.Name} d. {show.Date} !\n Kom og oplev aftenens stjerner:");
        foreach (Artist artist in show.Artists)
        {
            Console.WriteLine($"{artist.Act}, {artist.FullName}\n");
        }
    }
    Console.WriteLine("Bestil biletter nu! - Tast 1");
    string createReservation = Console.ReadLine();
    
    if (createReservation == "1")
    {
        Console.WriteLine("Indtast SHOW NUMMER på det show du ønsker at bestille biletter til!");
        int showId = int.Parse(Console.ReadLine());
        Show chosenShow = showRepository.GetById(showId);

        //Tjekker at indtastet showId er valid
        if (chosenShow == null)
        {
            Console.Write("Show ikke fundet, prøv at indtast det rette nummer igen.");
        } 
        
        //Opret kunde
        else
        {
            Console.WriteLine("Indtast dit navn: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Indtast dit efternavn: ");
            string lastName = Console.ReadLine();

            Console.WriteLine("Indtast din email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Indtast dit telefonnummer: ");
            string phoneNumber = Console.ReadLine();

            //Generer nyt CustomerID
            int customerId = customerRepository.GetAll().Count + 1;
            Customer newCustomer = new Customer(customerId, firstName, lastName, email, phoneNumber, false);

            customerRepository.Add(newCustomer);

            //Vælg billettype
            Console.WriteLine("Vælg billettype - Standard (1) eller VIP (2)");
            string ticketChoice = Console.ReadLine();
            TicketType ticketType;
            
            //Simpel if statement til at afgøre billettype
            if (ticketChoice == "2")
            {
                ticketType = TicketType.VIP;
            } 
            
            else
            {
                ticketType = TicketType.Standard;
            }
            
            //Vælg antal biletter
            Console.WriteLine("Hvor mange biletter ønsker du?:  ");
            int ticketAmount = int.Parse(Console.ReadLine());

            //Opret Reservation
            int reservationId = reservationRepository.GetAll().Count + 1;
            Reservation newReservation = new Reservation(
                reservationId,
                new DateTime(chosenShow.Date.Year, chosenShow.Date.Month, chosenShow.Date.Day),
                ticketType,
                ticketAmount,
                newCustomer,
                chosenShow
                );
            //Tilføj reservation til liste
            reservationRepository.Add(newReservation);
            Console.WriteLine($"\nTak {newCustomer.FirstName}! Din reservation er oprettet. Her er din kvittering: ");
            Console.WriteLine($"Show: {chosenShow.ShowName} i {chosenShow.City.Name} d. {chosenShow.Date}");
            Console.WriteLine($"Billettype: {ticketType}, antal {ticketAmount}.");
        }

    }
    

} 

else if (choice == "2")
{
    //Søg efter show i en bestemt by
    Console.WriteLine("Indtast bynavn");
    string cityInput = Console.ReadLine();
    List<Show> shows = showRepository.GetByCity(cityInput);

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


