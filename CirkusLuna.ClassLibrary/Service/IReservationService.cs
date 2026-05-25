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



    }
}
