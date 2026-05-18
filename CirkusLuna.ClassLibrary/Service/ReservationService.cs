using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Service
{
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;
        private IShowRepository _showRepository;

        public ReservationService(IReservationRepository reservationRepository, IShowRepository showRepository)
        {
            _reservationRepository = reservationRepository;
            _showRepository = showRepository;
        }

        public bool CreateReservation(Reservation reservation)
        {
            //is the show in the future
            if (reservation.Show.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                return false; //show is in the past
            }


            //count booked seats
            List<Reservation> currentReservations = _reservationRepository.GetByShow(reservation.Show.Id);
            int bookedSeats = 0;
            int bookedVipSeats = 0;

            for (int i = 0; i < currentReservations.Count; i++)
            {
                if (currentReservations[i].TicketType == TicketType.VIP)
                {
                    bookedVipSeats += currentReservations[i].TotalSeats;
                }
                else
                {
                    bookedSeats += currentReservations[i].TotalSeats;
                }

            }

            //check available seats
            if (reservation.TicketType == TicketType.VIP)
            {
                if (bookedVipSeats + reservation.TotalSeats > reservation.Show.VipSeats)
                {
                    return false; //Not enough VIP seats
                }

            }
            else
            {
                if (bookedSeats + reservation.TotalSeats > reservation.Show.Seats)
                {
                    return false; //not enough standard seats
                }

            }
            _reservationRepository.Add(reservation);
            return true;

        }

        public List<Reservation> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public List<Reservation> GetByCustomer(int id)
        {
            return _reservationRepository.GetByCustomer(id);
        }

        public List<Reservation> GetByShow(int id)
        {
            return _reservationRepository.GetByShow(id);
        }
    }
}
