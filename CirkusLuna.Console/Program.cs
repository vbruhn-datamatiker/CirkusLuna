using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using CirkusLuna.ClassLibrary.Service;

//Forbind repositories - ligger uden for while(true) så data bevares.
IArtistRepository artistRepository = new ArtistJSONRepository();
IShowRepository showRepository = new ShowJSONRepository();
IEmployeeRepository employeeRepository = new EmployeeRepository();
ICustomerRepository customerRepository = new CustomerJSONRepository();
IShowService showService = new ShowService(showRepository);
IReservationRepository reservationRepository = new ReservationJSONRepository();
IReservationService reservationService = new ReservationService(reservationRepository, showRepository);
INewsPostRepository newsPostRepository = new NewsPostJSONRepository();

//Console App Program
while (true)
{
    //Indledning
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.WriteLine("--------------------- Velkommen til Cirkus Luna ---------------------\n");
    Console.ResetColor();

    Console.WriteLine("------------------ Menu ------------------");

    Console.WriteLine("Se alle fremtidige forestillinger - Tast 1");
    Console.WriteLine("Søg efter den næste forestilling i en by - Tast 2");
    Console.WriteLine("Se de seneste nyheder! - tast 3");

    Console.WriteLine("\nLog ind som medarbejder - Tast 5\n");

    //Søg efter forestillinger
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        //Kald DisplayShows();
        DisplayShows();

        //Initier billetreservation
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.Write("Bestil billetter nu! - Tast 1");
        Console.ResetColor();
        Console.WriteLine(); //Også her, ellers fortsatte BackgroundColor...

        string createReservation = Console.ReadLine();

        if (createReservation == "1")
        {
            Console.WriteLine("Indtast SHOW NUMMER på det show du ønsker at bestille billetter til!");
            int showId = int.Parse(Console.ReadLine());
            Show chosenShow = showRepository.GetById(showId);

            //Tjekker at indtastet showId er valid
            if (chosenShow == null)
            {
                Console.Write("Show ikke fundet, prøv at indtast det rette nummer igen.");
            }

            //Opret kunde - kald CreateReservation(chosenShow)
            else
            {
                CreateReservation(chosenShow);
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
        DisplayNews();
    }

    //Medarbejder "LogIn"
    else if (choice == "5")
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
                Console.WriteLine("4 - Vis liste over reservationer");
                Console.WriteLine("5 - Vis liste over nyheder");
                Console.WriteLine("6 - Vis liste over medarbejdere");

                Console.WriteLine("\n0 - Log ud");

                string employeeChoice = Console.ReadLine();

                if (employeeChoice == "1")
                {
                    DisplayShows();
                    Console.WriteLine("1 - Vil du oprette et nyt show?");
                    Console.WriteLine("2 - Vil du ændre et eksisterende show?");
                    Console.WriteLine("3 - Vil du slette et eksisterende show?");

                    Console.WriteLine("\n0 - Gå tilbage til Menu");

                    string employeeEditShow = Console.ReadLine();

                    if (employeeEditShow == "1")
                    {
                        CreateShow();
                    } 
                    else if (employeeEditShow == "2")
                    {
                        UpdateShow();
                    }
                    else if (employeeEditShow == "3")
                    {
                        //Logik til DeleteShow
                    }


                }
                else if (employeeChoice == "2")
                {
                    DisplayCustomers();
                    Console.WriteLine("1 - Ønsker du at ændre oplysninger på en kunde?");
                    Console.WriteLine("0 - Gå tilbage til Menu");
                    string employeeEditCustomer = Console.ReadLine();
                    if (employeeEditCustomer == "1")
                    {
                        UpdateCustomer();
                    }
                    else if (employeeEditCustomer == "0")
                    {
                        //Returnerer til employeeActive
                    }
                }
                else if (employeeChoice == "3")
                {
                    DisplayArtists();
                    Console.WriteLine("1 - Ønsker du at ændre oplysninger på en Artist?");
                    Console.WriteLine("2 - Ønsker du at oprette en ny Artist?");

                    Console.WriteLine("\n0 - Gå tilbage til Menu");
                    string employeeEditArtist = Console.ReadLine();
                    if (employeeEditArtist == "1")
                    {
                        UpdateArtist();
                    }
                    else if (employeeEditArtist == "2")
                    {
                        CreateArtist();
                    }

                    else if (employeeEditArtist == "0")
                    {
                        //Returnerer til employeeActive
                    }
                }
                else if (employeeChoice == "4")
                {
                    DisplayReservation();
                    Console.WriteLine("1 - Vil du oprette en reservation til et bestemt show?");
                    Console.WriteLine("2 - Vil du ændre en eksisterende reservation?");
                    Console.WriteLine("3 - Vil du slette en eksisterende reservation?");

                    Console.WriteLine("\n0 - Gå tilbage til Menu");
                    string employeeEditReservation = Console.ReadLine();
                    if (employeeEditReservation == "1")
                    {
                        Console.WriteLine("Angiv ID på det show du vil oprette en reservation til: ");
                        int employeeCreateReservation = int.Parse(Console.ReadLine());
                        Show chosenShow = showRepository.GetById(employeeCreateReservation);
                        CreateReservation(chosenShow);
                    }
                    else if (employeeEditReservation == "2")
                    {
                        UpdateReservation();
                    }
                    else if (employeeChoice == "3")
                    {
                        DeleteReservation();
                    }

                }
                else if (employeeChoice == "5")
                {
                    DisplayNews();
                    Console.WriteLine("1 - Vil du oprette en ny Post?: ");
                    Console.WriteLine("2 - Vil du slette en Post?: ");
                    
                    string manageNewsPost = Console.ReadLine();
                    if (manageNewsPost == "1")
                    {
                        CreateNewsPost();
                    }
                    else if (manageNewsPost == "2")
                    {
                        DeleteNewsPost();
                    }


                }
                else if (employeeChoice == "6")
                {
                    DisplayEmployees();
                    Console.WriteLine("1 - Vil du oprette en ny medarbejder?");
                    Console.WriteLine("2 - Vil du ændre oplysninger på en eksisterende medarbejder?");
                    string employeeEditEmployee = Console.ReadLine();

                    if (employeeEditEmployee == "1") 
                    {
                        CreateEmployee();
                    }
                    else if (employeeEditEmployee == "2")
                    {
                        //UpdateEmployee();
                    }

                }

                else if (employeeChoice == "0")
                {
                    employeeActive = false;
                }
            }
        }
    }



//Funktioner 
// -------------------- Display() funktioner ----------------------------

void DisplayShows()
{
    //Vis alle fremtidige shows og artister
    List<Show> shows = showRepository.GetAll();
    foreach (Show show in shows)
    {
        //Tilføjet d. 20-5
        //Udregner til booked seats for det valgte show
        //(show.TotalSeats) udskiftes med RemainingSeats, så det der oprettes i ReservationRepo også vises.
        int bookedStandardSeats = 0;
        int bookedVipSeats = 0;
            foreach (Reservation r in reservationRepository.GetByShow(show.Id))
            {
                if (r.TicketType == TicketType.VIP)
                {
                    bookedVipSeats += r.TotalSeats;
                } 
                else
                {
                    bookedStandardSeats += r.TotalSeats;
                }
            }

            int remainingStandard = show.Seats - bookedStandardSeats;
            int remainingVip = show.VipSeats - bookedVipSeats;
        
        //Display til konsol
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write($"SHOW NUMMER [{show.Id}]");
        Console.ResetColor();
        Console.WriteLine(); //Plads til ResetColor();

        Console.WriteLine($"{show.ShowName} i {show.City.Name} d. {show.Date}" +
            $"\nDer er {remainingStandard} standard pladser og {remainingVip} VIP pladser tilbage! Skynd dig at bestille inden de er udsolgt! " +
            $"\nKom og oplev aftenens stjerner:\n");

            foreach (Artist artist in show.Artists)
            {
                Console.WriteLine($"{artist.Act}, {artist.FullName}");
            }
            Console.WriteLine("------------------------------------");

        }
}

void DisplayReservation()
{
    foreach (Reservation reservation in reservationRepository.GetAll())
    {
        Console.WriteLine($"Reservation [{reservation.ReservationId}] - Kunde: {reservation.Customer.FullName}, mail: {reservation.Customer.Email}, Antal billetter: {reservation.TotalSeats}");
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
    foreach (Artist artist in artistRepository.GetAll())
    {
        Console.WriteLine($"[{artist.Id}] {artist.Act}, {artist.FullName}");
    }
}

void DisplayNews()
{
    foreach (NewsPost post in newsPostRepository.GetAll())
    {
        Console.WriteLine($"[{post.NewsPostId}] | {post.Title} | {post.Content} - Udgivet d. {post.PublishedDateTime}");
    }
}

void DisplayEmployees()
    {
        foreach (Employee employee in employeeRepository.GetAll())
        {
            Console.WriteLine($"Medarbejder nr: [{employee.Id}] Navn: {employee.FullName} | Stilling: {employee.Role} | Kontakt: {employee.Email}");
        }
    }

// -------------------- Create() funktioner ----------------------------

//Funktion til at oprette reservation ud fra chosenShow
void CreateReservation(Show chosenShow)
{
    Console.WriteLine("Indtast navn: ");
    string firstName = Console.ReadLine();

    Console.WriteLine("Indtast efternavn: ");
    string lastName = Console.ReadLine();

    Console.WriteLine("Indtast email: ");
    string email = Console.ReadLine();

    Console.WriteLine("Indtast telefonnummer: ");
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
    Console.WriteLine("Hvor mange billetter ønsker du?:  ");
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

void CreateArtist()
    {
        Console.WriteLine("Indtast navn på ny artist: ");
        string firstName = Console.ReadLine();
        Console.WriteLine("Indtast efternavn på ny artist: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Indtast email på ny artist: ");
        string email = Console.ReadLine();
        Console.WriteLine("Indtast Act på ny artist: ");
        string act = Console.ReadLine();

        int artistId = artistRepository.GetAll().Count + 1;
       //Opret og tilføj ny artist til artistRepo
        Artist newArtist = new Artist(artistId, firstName, lastName, email, act);
        artistRepository.Add(newArtist);
        Console.WriteLine($"Artist nr: [{artistId}] - {newArtist.FirstName} {newArtist.LastName}, {newArtist.Act} er nu oprettet i systemet! ");
    }

void CreateEmployee()
    {
        Console.WriteLine("Angiv fornavn på ny medarbejder: ");
        string firstName = Console.ReadLine();
        Console.WriteLine("Angiv efternavn på ny medarbejder: ");
        string lastName = Console.ReadLine();
        Console.WriteLine("Angiv email på ny medarbejder: ");
        string email = Console.ReadLine();
        Console.WriteLine($"Angiv hvilken stilling {firstName} skal have?: ");
        string role = Console.ReadLine();
        string password = lastName;

        int newEmployeeId = employeeRepository.GetAll().Count + 1;
        //Opret og tilføj ny medarbejder
        Employee newEmployee = new Employee(newEmployeeId, firstName, lastName, email, role, password);
        employeeRepository.Add(newEmployee);

        Console.WriteLine($"Velkommen til ny {newEmployee.Role} - {newEmployee.FullName}, {newEmployee.Email}.\nMedarbejder password: {newEmployee.Password}");
    }

void CreateNewsPost()
    {
        Console.WriteLine("Angiv titel på post: ");
        string newsTitle = Console.ReadLine();
        Console.WriteLine("Skriv postens indhold: ");
        string newsContent = Console.ReadLine();

        //Sæt automatisk dato og tid til "nu"
        DateTime publishedDateTime = DateTime.Now;

        int newsPostId = newsPostRepository.GetAll().Count + 1;
        //Opret og tilføj ny post til NewsPostRepo
        NewsPost newNewsPost = new NewsPost(newsPostId, newsTitle, newsContent, publishedDateTime);
        newsPostRepository.Add(newNewsPost);

        Console.WriteLine($"Din post [{newsPostId}] er oprettet d. {publishedDateTime}\n" +
            $"{newsTitle} - {newsContent}");

    }

void CreateShow()
    {
        Console.WriteLine("Indtast titel på nyt show: ");
        string showName = Console.ReadLine();
        Console.WriteLine("Angiv dato for show i YYYY-MM-DD format:");
        string showDateInput = Console.ReadLine();
        DateOnly newDate = DateOnly.Parse(showDateInput);
        Console.WriteLine("Angiv hvor mange standard pladser showet skal have: ");
        int showStandardTickets = int.Parse(Console.ReadLine());
        Console.WriteLine("Angiv hvor mange VIP pladser showet skal have: ");
        int showVipTickets = int.Parse(Console.ReadLine());
        Console.WriteLine($"Angiv by hvor showet skal finde sted: ");
        string showCity = Console.ReadLine();
        //Opret ny city objekt
        int newCityId = showRepository.GetAll().Count + 1;
        City newCity = new City(newCityId, showCity );
        
        //Opret nyt show objekt
        int newShowId = showRepository.GetAll().Count + 1;
        Show newShow = new Show(newShowId, showName, newDate, showStandardTickets, showVipTickets, newCity);
        
 
        showRepository.Add(newShow);
        Console.WriteLine($"{newShow.ShowName} er oprettet og afholdes d. {newShow.Date}. Der er {showStandardTickets} standard pladser og {showVipTickets} VIP pladser." +
            $"\n Forestillingen finder sted i {newShow.City.Name}");
    }

// -------------------- Update() funktioner ----------------------------

//UpdateCustomer
void UpdateCustomer()
{
    //Finder eksisterende kunde på ID
    Console.WriteLine("Skriv ID på kunde der skal ændres: ");
    int existingCustomerId = int.Parse(Console.ReadLine());
    Customer customer = customerRepository.GetById(existingCustomerId);

    //Holder update() kørende
    bool updateCustomer = true;
    while (updateCustomer == true) 
    {   
        //Oversigt over mulige handlinger
        Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
        Console.WriteLine("1 - Ændre Fornavn + Efternavn");
        Console.WriteLine("2 - Ændre Email");
        Console.WriteLine("3 - Ændre telefon nr.");
        //Console.WriteLine("4 - Ændre kundestatus: Standard / VIP\n"); - Lige udkommenteret, den skal ikke være tilgængelig for customer

        Console.WriteLine("0 - Afslut ændringer");

        int updateCustomerChoice = int.Parse(Console.ReadLine());

        if (updateCustomerChoice == 1)
        {
            Console.WriteLine("Indtast nyt fornavn:");
            customer.FirstName = Console.ReadLine();
            Console.WriteLine("Indtast nyt efternavn:");
            customer.LastName = Console.ReadLine();
            //Opdater ændringer
            customerRepository.Update(customer);

        }
        else if (updateCustomerChoice == 2)
        {
            Console.WriteLine("Indtast ny email:");
            customer.Email = Console.ReadLine();
            customerRepository.Update(customer);
        }
        else if (updateCustomerChoice == 3)
        {
            Console.WriteLine("Indtast nyt telefon nr.:");
            customer.PhoneNumber = Console.ReadLine();
            customerRepository.Update(customer);
        }
        else if (updateCustomerChoice == 0)
        {
            updateCustomer = false;
        }

    }
}

void UpdateArtist()
{
    //Finder eksisterende Artist på ID
    Console.WriteLine("Skriv ID på artist der skal ændres: ");
    int existingArtistId = int.Parse(Console.ReadLine());
    Artist artist = artistRepository.GetById(existingArtistId);

    //Holder update() kørende
    bool updateArtist = true;
    while (updateArtist == true)
    {
        //Oversigt over mulige handlinger
        Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
        Console.WriteLine("1 - Ændre Fornavn + Efternavn");
        Console.WriteLine("2 - Ændre Email");
        Console.WriteLine("3 - Ændre artist Act\n");
        
        Console.WriteLine("0 - Afslut ændringer");

        int updateArtistChoice = int.Parse(Console.ReadLine());

        if (updateArtistChoice == 1)
        {
            Console.WriteLine("Indtast nyt fornavn:");
            artist.FirstName = Console.ReadLine();
            Console.WriteLine("Indtast nyt efternavn:");
            artist.LastName = Console.ReadLine();
            //Opdater ændringer
            artistRepository.Update(artist);

        }
        else if (updateArtistChoice == 2)
        {
            Console.WriteLine("Indtast ny email:");
            artist.Email = Console.ReadLine();
            artistRepository.Update(artist);
        }
        else if (updateArtistChoice == 3)
        {
            Console.WriteLine("Indtast ny Act:");
            artist.Act = Console.ReadLine();
            artistRepository.Update(artist);
        }
        else if (updateArtistChoice == 0)
        {
            updateArtist = false;
        }

    }
}

void UpdateReservation()
{
    //Finder eksisterende Artist på ID
    Console.WriteLine("Skriv ID på den reservation der skal ændres: ");
    int existingReservationId = int.Parse(Console.ReadLine());
    Reservation reservation = reservationRepository.GetById(existingReservationId);

    //Holder update() kørende
    bool updateReservation = true;
    while (updateReservation == true)
    {
        //Oversigt over mulige handlinger
        Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
        Console.WriteLine("1 - Ændre kundeoplysninger (Email og tlf.)");
        Console.WriteLine("2 - Ændre antal billetter: ");
        //Console.WriteLine("3 - Ændre billettype (Standard/VIP): "); - ikke skrevet
        //Console.WriteLine("4 - Ændre til et andet show"); - Gemmer den her, hvis vi ønsker at implementere den

        Console.WriteLine("0 - Afslut ændringer");

        int updateReservationChoice = int.Parse(Console.ReadLine());

        if (updateReservationChoice == 1)
        {
            Console.WriteLine("Indtast ny email: ");
            reservation.Customer.Email = Console.ReadLine();
            Console.WriteLine("Indtast nyt tlf nummer: ");
            reservation.Customer.PhoneNumber = Console.ReadLine();
            customerRepository.Update(reservation.Customer);
            reservationRepository.Update(reservation);
        }
        else if (updateReservationChoice == 2)
        {
            
            Console.WriteLine("Indtast ønsket antal billetter: ");
            int newTicketAmount = int.Parse(Console.ReadLine());

            //Logik til at tjekke tilgængelighed af ønsket antal sæder gennem service
            //Tæl eksisterende bookede billetter/sæder for det specifikke show - ekskluderer denne reservation
            int bookedSeats = 0;
            foreach (Reservation r in reservationRepository.GetByShow(reservation.Show.Id))
            {
                if (r.ReservationId != reservation.ReservationId)
                {
                    bookedSeats += r.TotalSeats;
                }
            }
            //Tjekker om reservation er VIP eller standard og vælger den korrekte type.
            int availableSeats = 0;
            if (reservation.TicketType == TicketType.VIP)
            {
                availableSeats = reservation.Show.VipSeats;
            } 
            else
            {
                availableSeats = reservation.Show.Seats;
            }

            if (bookedSeats + newTicketAmount <= availableSeats)
            {
                reservation.TotalSeats = newTicketAmount;
                reservationRepository.Update(reservation);
                Console.WriteLine($"Billetter opdateret. Du har nu {reservation.TotalSeats} billetter.");
            }
            else
            {
                Console.WriteLine("Der er ikke nok ledige billetter.");
            }


        }

        else if (updateReservationChoice == 0)
        {
            updateReservation = false;
        }

    }
}

void UpdateShow()
    {
        Console.WriteLine("Angiv ID på det show der skal ændres: ");
        int existingShowId = int.Parse(Console.ReadLine());
        Show show = showRepository.GetById(existingShowId);

        //Holder update() kørende
        bool updateShow = true;
        while (updateShow == true)
        {
            //Oversigt over mulige handlinger
            Console.WriteLine("\n----- Update Menu -----\n Vælg handling ved at angive nr. \n");
            Console.WriteLine("1 - Ændre navn på show: ");
            Console.WriteLine("2 - Ændre dato for show: ");
            Console.WriteLine("3 - Ændre antal ledige standard pladser: ");
            Console.WriteLine("4 - Ændre antal ledige VIP pladser: ");
            Console.WriteLine("5 - Ændre by for show: ");

            Console.WriteLine("\n0 - Gå tilbage til Menu. ");

            string updateShowChoice = Console.ReadLine();

            if (updateShowChoice == "1")
            {
                Console.WriteLine("Angiv nyt navn til show: ");
                show.ShowName = Console.ReadLine();
                showRepository.Update(show);
                Console.WriteLine($"Du har ændret navnet til {show.ShowName}");
            }
            else if (updateShowChoice == "2")
            {
                Console.WriteLine("Angiv ny dato for show i YYYY-MM-DD format:");
                string newDateInput = Console.ReadLine();
                DateOnly newDate = DateOnly.Parse(newDateInput);

                //Ændre show.Date til newDate
                show.Date = newDate;
                showRepository.Update(show);
                Console.WriteLine($"Dato ændret til {show.Date}");

            }
            else if (updateShowChoice == "3")
            {
                Console.WriteLine("Angiv hvor mange ledige standard pladser der skal være: ");
                int UpdatedStandardSeat = int.Parse(Console.ReadLine());
                show.Seats = UpdatedStandardSeat;
                showRepository.Update(show);
                Console.WriteLine($"Du har oprettet {show.Seats} standard pladser. ");
            }
            else if (updateShowChoice == "4")
            {
                Console.WriteLine("Angiv hvor mange ledige VIP pladser der skal være: ");
                int UpdatedVipSeat = int.Parse(Console.ReadLine());
                show.Seats = UpdatedVipSeat;
                showRepository.Update(show);
                Console.WriteLine($"Du har oprettet {show.VipSeats} VIP pladser. ");
            }
            else if (updateShowChoice == "5")
            {
                Console.WriteLine($"Hvor skal showet finde sted istedet for {show.City.Name}?: ");
                string UpdatedShowCity = Console.ReadLine();
                //Opret ny city objekt
                City newCity = new City(show.City.Id, UpdatedShowCity);
                show.City = newCity;
                showRepository.Update(show);
                Console.WriteLine($"{show.ShowName} er flyttet til {show.City.Name}");
            }
            //Afslut updateShow
            else if (updateShowChoice == "0")
            {
                updateShow = false;
            }


        }

    }


    // -------------------- Delete() funktioner ----------------------------

void DeleteReservation()
    {
        Console.WriteLine("Angiv ID på den reservation du ønsker at slette: ");
        int reservationId = int.Parse(Console.ReadLine());
        //Find reservation i repository
        Reservation reservation = reservationRepository.GetById(reservationId);
        //Slet
        reservationRepository.Delete(reservationId);
        Console.WriteLine("Reservationen er slettet.");
    }

void DeleteNewsPost()
    {
        Console.WriteLine("Angiv ID på den news post der skal slettes: ");
        int newsPostId = int.Parse(Console.ReadLine());
        //Find post i repository
        NewsPost newsPost = newsPostRepository.GetById(newsPostId);
        //Slet
        newsPostRepository.Delete(newsPostId);
        Console.WriteLine("Post slettet.");
    }



} //Ende af While (true)