using MauiAppFlightReservation.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace MauiAppFlightReservation.Components.Pages
{
    public partial class ReservationComponent : ComponentBase
    {
        List<Reservation> Reservations = new List<Reservation>();
        string ResCode { get; set; }
        string ResFlightCode { get; set; }
        string ResAirline { get; set; }
        double ResCost { get; set; }
        string ResName { get; set; }
        string ResCitizenship { get; set; }
        string ResStatus { get; set; }


        protected override void OnInitialized()
        {
            LoadReservation();
        }
        
        public override string ToString()
        {
            return $"{ReservationManager.GenerateRandomReservationCode()},{ResFlightCode}, {ResAirline}, {ResCost}, {ResName}, {ResCitizenship}, Active\n";
        }
        void LoadReservation()
        {
            string resDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/Data");

            // Specify the filename you want to read
            string filename = "reservations.txt";

            // Combine the directory and file name to get the full path
            string filepath = Path.Combine(resDirectory, filename);

            try
            {
                // Read the contents of the file
                string[] lines = File.ReadAllLines(filepath);
                foreach (string line in lines)
                {
                    string[] words = line.Trim().Split(",");
                    Reservation reservation = new Reservation(words[0], words[1],
                                                words[2],
                                                double.Parse(words[3]),
                                                words[4],
                                                words[5],
                                                (words[6]));
                    Reservations.Add(reservation);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                Console.WriteLine($" Error reading the file: {ex.Message}");
            }
        }

        string ReservationDetails { get; set; }
        void FindReservations()
        {
            var filteredReservation = Reservations.Where(f => f.Code == ResCode && f.Airline == ResAirline && f.Name == ResName);
            ReservationDetails = string.Join("\n", filteredReservation.Select(f => $"{f.Code}, {f.FlightCode}, {f.Airline}, {f.Cost}, {f.Name}, {f.Citizenship}, {f.Status}"));

            var selectedReservation = filteredReservation.FirstOrDefault();
            if (selectedReservation != null)
            {
                ResCode = selectedReservation.Code;
                ResFlightCode = selectedReservation.FlightCode;
                ResAirline = selectedReservation.Airline;
                ResCost = selectedReservation.Cost;
                ResName = selectedReservation.Name;
                ResCitizenship = selectedReservation.Citizenship;
                ResStatus = selectedReservation.Status;
            }
        }


        void ModifyReservation()
        {
            // Find the reservation to modify
            var reservationToModify = Reservations.FirstOrDefault(r => r.Code == ResCode);

            if (reservationToModify != null)
            {
                // Update the reservation's properties
                reservationToModify.FlightCode = ResFlightCode;
                reservationToModify.Airline = ResAirline;
                reservationToModify.Cost = ResCost;
                reservationToModify.Name = ResName;
                reservationToModify.Citizenship = ResCitizenship;
                reservationToModify.Status = ResStatus;

                // Update ReservationDetails string
                ReservationDetails = string.Join("\n", Reservations.Select(r => $"{r.Code}, {r.FlightCode}, {r.Airline}, {r.Cost}, {r.Name}, {r.Citizenship}, {r.Status}"));

                // Write the modified reservations back to the file
                string resDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/Data");
                string filename = "reservations.txt";
                string filepath = Path.Combine(resDirectory, filename);

                try
                {
                    // Write the updated reservation details to the file
                    File.WriteAllLines(filepath, ReservationDetails.Split('\n'));
                    Console.WriteLine("Reservation modified successfully and saved to file.");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during file write
                    Console.WriteLine($"Error writing to file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }
    }
}
