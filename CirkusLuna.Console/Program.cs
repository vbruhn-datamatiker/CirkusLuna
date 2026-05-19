using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using CirkusLuna.ClassLibrary.Service;

//Console App Program

//Forbind repositories
IShowRepository showRepository = new ShowRepository();
IEmployeeRepository employeeRepository = new EmployeeRepository();
IReservationRepository reservationRepository = new ReservationRepository();
ICustomerRepository customerRepository = new CustomerRepository();
IShowService showService = new ShowService(showRepository);
IReservationService reservationService = new ReservationService(reservationRepository, showRepository);
INewsPostRepository newsPostRepository = new NewsPostRepository();


//Indledning
Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine("--------------------- Velkommen til Cirkus Luna ---------------------\n");
Console.ResetColor();

Console.WriteLine("------------------ Menu ------------------");

Console.WriteLine("Se alle fremtidige forestillinger - Tast 1");
Console.WriteLine("Søg efter den næste forestilling i en by - Tast 2");
Console.WriteLine("\nLog ind som medarbejder - Tast 3\n");

//Søg efter forestillinger
string choice = Console.ReadLine();

if (choice == "1")
{
    //Kald DisplayShows();
    DisplayShows();
    
    //Initier billetreservation
    Console.BackgroundColor = ConsoleColor.DarkGreen;
    Console.Write("Bestil biletter nu! - Tast 1");
    Console.ResetColor();
    Console.WriteLine(); //Også her, ellers fortsatte BackgroundColor...

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

            //Auto-tildeler kunde et sæde, baseret på nuværende bookings for samme show
            List<Reservation> extistingReservations =
            reservationService.GetByShow(chosenShow.Id);
            int nextSeatNumber = 1;
            foreach (Reservation r in extistingReservations)
            {
                nextSeatNumber += r.TotalSeats;
            }

            //Opret Reservation
            int reservationId = reservationRepository.GetAll().Count + 1;
            Reservation newReservation = new Reservation(
                reservationId,
                new DateTime(chosenShow.Date.Year, chosenShow.Date.Month, chosenShow.Date.Day),
                ticketType,
                ticketAmount,
                nextSeatNumber,
                newCustomer,
                chosenShow
                );

            //Tjek fra servicelag at reservationen kan foretages
            bool success = reservationService.CreateReservation(newReservation);

            if (success)
            {
                Console.WriteLine($"\nTak {newCustomer.FirstName}! Din reservation er oprettet. Her er din kvittering: ");
                Console.WriteLine($"Show: {chosenShow.ShowName} i {chosenShow.City.Name} d. {chosenShow.Date}");
                Console.WriteLine($"Billettype: {ticketType}, antal {ticketAmount}.");
                Console.WriteLine($"Du har følgende sæder nr: {nextSeatNumber} - {nextSeatNumber + ticketAmount + 1}");

            }
            else
            {
                Console.WriteLine("Reservationen kunne ikke oprettes - ingen ledige pladser eller showet er i fortiden");
            }

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
}
else if (choice == "3")
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

        //While loop for at holde konsol kørende
        bool employeeActive = true;
        while (employeeActive)
        {
            Console.WriteLine("\n----- Menu -----\n Vælg handling ved at angive nr. \n");
            Console.WriteLine("1 - Vis liste over forestillinger");
            Console.WriteLine("2 - Vis liste over kunder");
            Console.WriteLine("3 - Vis liste over artister");
            Console.WriteLine("4 - Vis liste over nyheder");

            Console.WriteLine("0 - Log ud");

            string employeeChoice = Console.ReadLine();

            if (employeeChoice == "1")
            {
                DisplayShows();
            }
            else if (employeeChoice == "2")
            {
                DisplayCustomers();
            }
            else if (employeeChoice == "3") 
            {
                DisplayArtists();
            }
        }
    }
}

//Funktioner 

void DisplayShows()
{
    //Vis alle fremtidige shows og artister
    List<Show> shows = showRepository.GetAll();
    foreach (Show show in shows)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write($"SHOW NUMMER [{show.Id}]");
        Console.ResetColor();
        Console.WriteLine(); //Plads til ResetColor();

        Console.WriteLine($"{show.ShowName} i {show.City.Name} d. {show.Date}" +
            $"\nDer er {show.TotalSeats} pladser tilbage | {show.Seats} standard pladser og {show.VipSeats} VIP pladser." +
            $"\nKom og oplev aftenens stjerner:\n");

            foreach (Artist artist in show.Artists)
            {
                Console.WriteLine($"{artist.Act}, {artist.FullName}");
            }
            Console.WriteLine("------------------------------------");

        }
}

void DisplayCustomers()
{
    foreach (Customer customer in customerRepository.GetAll())
    {
        Console.WriteLine($"Kunde nr: [{customer.Id}] - {customer.FullName} - {customer.Email} - {customer.PhoneNumber}");
    }
}

void DisplayArtists()
{
    foreach (Artist artist in showRepository.GetAll())
    {
            Console.WriteLine($"{artist.Act}, {artist.FullName}");
    }
}