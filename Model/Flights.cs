using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppFlightReservation.Model
{
    public class Flights
    {
        public string Code { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Weekday { get; set; }
        public string Time { get; set; }
        public int Seats { get; set; }
        public double CostPerSeat { get; set; }

        public Flights(string code, string airline, string from, string to, string weekday, string time, int seats, double costPerSeat)
        {
            Code = code;
            Airline = airline;
            From = from;
            To = to;
            Weekday = weekday;
            Time = time;
            Seats = seats;
            CostPerSeat = costPerSeat;
        }
    }
}
