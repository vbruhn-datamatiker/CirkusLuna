using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private List<Reservation> _reservations = new List<Reservation>();

        public ReservationRepository()
        {

            //data creation testing

            City copenhagen = new City(1, "København");

            Show show1 = new Show(1, "Cirkus Luna", new DateOnly(2026, 7, 12), 150, 10, copenhagen);

            Customer customer1 = new Customer(1, "Gunner", "Gunnersen", "gun@mail.com", "56345678", false);


            //------//


            //Reservation reservation1 =
            //    //DateTime( year  month day hour min sec
            //    new Reservation(1, new DateTime(2026, 7, 12, 30, 0),
            //    TicketType.Regular, 2, 5, customer1, show1);


            //_reservations.Add(reservation1);

        }
        public List<Reservation> GetAll()
        {
            return _reservations;
        }
        public Reservation GetById(int id)
        {

            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].ReservationId == id)
                {
                    return _reservations[i];
                }
            }
            return null;
        }

        public void Add(Reservation reservation)
        {
            _reservations.Add(reservation);

        }
        public void Update(Reservation reservation)
        {
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].ReservationId == reservation.ReservationId)
                {
                    _reservations[i].ReservationTime = reservation.ReservationTime;
                    _reservations[i].TicketType = reservation.TicketType;
                    _reservations[i].TotalSeats = reservation.TotalSeats;
                   // _reservations[i].SeatNumber = reservation.SeatNumber;
                    _reservations[i].Customer = reservation.Customer;
                    _reservations[i].Show = reservation.Show;
                    break; //no point continuing the loop
                }
            }

        }
        public void Delete(int id)
        {
            _reservations.Remove(GetById(id));

        }

        public List<Reservation> GetByCustomer(int id)
        {
            List<Reservation> result = new List<Reservation>();
            for (int i = 0; i < _reservations.Count; i++)
            {
                if (_reservations[i].Customer.Id == id) //check customers id
                {
                    result.Add(_reservations[i]);
                }
            }
            return result;
        }

        public List<Reservation> GetByShow(int id)
        {
            List<Reservation> result = new List<Reservation>();
            for (int i = 0; i < _reservations.Count; i++)
            {

                if (_reservations[i].Show.Id == id) //check show id
                {
                    result.Add(_reservations[i]);
                }
            }
            return result;


        }

    }
}
