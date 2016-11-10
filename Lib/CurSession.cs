using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.LIb
{
    public static class CurSession
    {
        //public static UserBL userBl = new UserBL();
        //public static AirportBL airportBl = new AirportBL();
        //public static FlightBL flightBl = new FlightBL();
        //public static PhoneBL phoneBl = new PhoneBL();
        //public static AddressBL addressBl = new AddressBL();
        //public static PersonBL personBl = new PersonBL();
        //public static PaymentBL paymentBl = new PaymentBL();
        //public static AircraftBL aircraftBl = new AircraftBL();
        //public static AircraftPictureBL aircraftPictureBl = new AircraftPictureBL();
   
        //public static User LoggedInUser
        //{
        //    get
        //    {
        //        var user = HttpContext.Current.Session[Constants.User];
        //        return user != null ? (User)user : new BO.User();
        //        //return user != null ? (User)user : userBl.Get("a@b.com", "asdf");
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Constants.User] = value;
        //    }
        //}

        //public static bool IsPilot
        //{
        //    get
        //    {
        //        var isPilot = !AppBase.LoggedInUser.IsEmpty() && aircraftBl.GetByPersonId(AppBase.LoggedInUser.Person.Id).Count > 0;
        //        HttpContext.Current.Session[Constants.IsPilot] = isPilot;
        //        return isPilot;
        //    }
        //}
    }
}