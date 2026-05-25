using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface IReservationService
    {
        List<Reservation> GetAll();
        List<Reservation> GetByCustomer(int id);
        List<Reservation> GetByShow(int id);
        bool CreateReservation(Reservation reservation);

        Reservation GetById(int id);
        bool UpdateReservationTickets(int reservationId, int newTicketAmount);
        void UpdateCustomerInfo(int reservationId, string email, string phoneNumber);
        void DeleteReservation(int id);

    }
}
