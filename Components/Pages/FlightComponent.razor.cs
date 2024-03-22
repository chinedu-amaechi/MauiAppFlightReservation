using MauiAppFlightReservation.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


namespace MauiAppFlightReservation.Components.Pages
{
    public partial class FlightComponent : ComponentBase
    {
        List<Flights> Flights = new List<Flights>();
        string SelectedFrom { get; set; }
        string SelectedTo { get; set; }
        string SelectedWeekday { get; set; }
        string FlightDetails { get; set; }
        string SelectedFlightCode { get; set; }
        string SelectedAirline { get; set; }
        string SelectedFlightWeekday { get; set; }
        string SelectedTime { get; set; }
        double SelectedCostPerSeat { get; set; }

        string Name { get; set; }

        string Citizenship { get; set; }

        protected override void OnInitialized()
        {
            LoadFlights();
        }

        void LoadFlights()
        {
            string resDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/Data");

            // Specify the filename you want to read
            string filename = "flights.csv";

            // Combine the directory and file name to get the full path
            string filepath = Path.Combine(resDirectory, filename);

            try
            {
                // Read the contents of the file
                string[] lines = File.ReadAllLines(filepath);
                foreach (string line in lines)
                {
                    string[] words = line.Trim().Split(",");
                    Flights flight = new Flights(words[0], words[1],
                                                words[2],
                                                words[3],
                                                words[4],
                                                words[5],
                                                int.Parse(words[6]),
                                                double.Parse(words[7]));
                    Flights.Add(flight);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                Console.WriteLine($" Error reading the file: {ex.Message}");
            }
        }
        void FindFlights()
        {
            var filteredFlights = Flights.Where(f => f.From == SelectedFrom && f.To == SelectedTo && f.Weekday == SelectedWeekday);
            FlightDetails = string.Join("\n", filteredFlights.Select(f => $"{f.Code}, {f.Airline}, {f.From}, {f.To}, {f.Weekday}, {f.Time}, {f.CostPerSeat}"));

            var selectedFlight = filteredFlights.FirstOrDefault();
            if (selectedFlight != null)
            {
                SelectedFlightCode = selectedFlight.Code;
                SelectedAirline = selectedFlight.Airline;
                SelectedFlightWeekday = selectedFlight.Weekday;
                SelectedTime = selectedFlight.Time;
                SelectedCostPerSeat = selectedFlight.CostPerSeat;
            }
        }

        public string DisplayResCode { get; set; }


        List<Reservation> Reservations = new List<Reservation>();
        public void MakeReservation()
        {
            DisplayResCode = ReservationManager.GenerateRandomReservationCode();
            Reservation reservation = new Reservation(DisplayResCode, SelectedFlightCode, SelectedAirline, SelectedCostPerSeat, Name, Citizenship, "Active");
            Reservations.Add(reservation);

            string strToAdd = reservation.ToString();

            try
            {
                string resDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/Data");

                // Specify the filename you want to read
                string filename = "reservations.txt";

                // Combine the directory and file name to get the full path
                string filepath = Path.Combine(resDirectory, filename);

                File.AppendAllText(filepath, strToAdd + "\n");
            }

            catch (Exception ex)
            {
                // Handle the exception, you can log it or display an error message
                Console.WriteLine("An error occurred while appending to the file:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
