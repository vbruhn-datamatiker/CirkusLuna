using CirkusLuna.ClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        List<Reservation> GetByCustomer(int id);
        List<Reservation> GetByShow(int id);
        Reservation GetById(int id);
        void Delete(int id);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
    }
}
