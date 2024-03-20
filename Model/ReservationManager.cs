using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppFlightReservation.Model
{
    public class ReservationManager
    {

        private static Random random = new Random();

        public static string GenerateRandomReservationCode()
        {
            StringBuilder reservationCodeBuilder = new StringBuilder();

            // Append a random letter (ASCII code 65 to 90 represents uppercase letters A-Z)
            char randomLetter = (char)random.Next(65, 91);
            reservationCodeBuilder.Append(randomLetter);

            // Append four random digits (ASCII code 48 to 57 represents digits 0-9)
            for (int i = 0; i < 4; i++)
            {
                char randomDigit = (char)random.Next(48, 58);
                reservationCodeBuilder.Append(randomDigit);
            }

            return reservationCodeBuilder.ToString();
        }
    }
}
