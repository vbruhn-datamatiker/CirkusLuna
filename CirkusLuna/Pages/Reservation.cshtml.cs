using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using CirkusLuna.ClassLibrary.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Pages
{
    public class ReservationModel : PageModel
    {
        private IReservationService _reservationService;
        private IShowRepository _showRepository;
        private ICustomerRepository _customerRepository;

        //List of shows for drop-down
        public List<Show> Shows { get; set; } = new List<Show>();

        //Form fields
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;
        [BindProperty]
        public string LastName { get; set; } = string.Empty;
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string PhoneNumber { get; set; } = string.Empty;
        [BindProperty]
        public int ShowId { get; set; }
        [BindProperty]
        public int TotalSeats { get; set; }
        [BindProperty]
        public TicketType TicketType { get; set; }
        public bool FormSubmitted { get; set; } = false;

        //feedback message
        public string Message { get; set; } = string.Empty;

        public ReservationModel(IReservationService reservationService, IShowRepository showRepository, ICustomerRepository customerRepository)
        {
            _reservationService = reservationService;
            _showRepository = showRepository;
            _customerRepository = customerRepository;
        }


        public void OnGet()
        {
            //Load shows for drop-down
            Shows = _showRepository.GetAll();


        }

        public void OnPost()
        {
            FormSubmitted = true;
            Shows = _showRepository.GetAll();

            //Get selected show
            Show selectedShow = _showRepository.GetById(ShowId);

            if (selectedShow == null)
            {
                Message = "Forestilling ikke fundet...";
                return;
            }
            //create new customer
            int customerId = _customerRepository.GetAll().Count + 1;

            Customer newCustomer = new Customer(customerId, FirstName, LastName, Email, PhoneNumber, false);
            _customerRepository.Add(newCustomer);

            //calculate next seat number
            List<Reservation> existingReservation = _reservationService.GetByShow(selectedShow.Id);
            int nextSeatNumber = 1;
            foreach (Reservation reservation in existingReservation)
            {
                nextSeatNumber += reservation.TotalSeats;

            }
            //create reservation
            int reservationId = _reservationService.GetAll().Count + 1;

            Reservation newReservation = new Reservation(
                reservationId,
                DateTime.Now,
                TicketType,
                TotalSeats,
                nextSeatNumber,
                newCustomer,
                selectedShow
                );

            //use service to validate and save
            bool success = _reservationService.CreateReservation(newReservation);
            if (success)
            {
                Message = $"Tak {FirstName}! Din reservation er oprettet for {selectedShow.ShowName} i {selectedShow.City.Name} d. {selectedShow.Date}";
            }
            else
            {
                Message = "Reservation kunne ikke oprettes - ingen ledige pladser eller forestillingen er allerede afholdt ";
            }
        }
    }
}
