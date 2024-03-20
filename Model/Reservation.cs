using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppFlightReservation.Model
{
    public class Reservation
    {
        public string Code { get; set; }
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public double Cost { get; set; }
        public string Name { get; set; }
        public string Citizenship { get; set; }
        public string Status { get; set; }

        public Reservation(string code, string flightCode, string airline, double cost, string name, string citizenship, string status)
        {
            Code = code;
            FlightCode = flightCode;
            Airline = airline;
            Cost = cost;
            Name = name;
            Citizenship = citizenship;
            Status = status;
        }

        public override string ToString()
        {
            return $"{Code}, {FlightCode}, {Airline}, {Cost}, {Name}, {Citizenship}, {Status}";
        }
    }
}
