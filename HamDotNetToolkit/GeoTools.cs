using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamDotNetToolkit
{
    public class GeoTools
    {

        public static string ConvertLatLongToGridSquare(double latitude, double longitude)
        {
            char[] chars = new char[4];

            longitude += 180;
            latitude += 90;

            chars[0] = (char)('A' + (int)(longitude / 20));
            chars[1] = (char)('A' + (int)(latitude / 10));
            chars[2] = (char)('0' + (int)((longitude % 20) / 2));
            chars[3] = (char)('0' + (int)(latitude % 10));

            return new string(chars);
        }
        public static LatLong ConvertGridSquareToLatLong(string gridSquare)
        {
            int lonIdx = gridSquare[0] - 'A';
            int latIdx = gridSquare[1] - 'A';

            double longitude = lonIdx * 20.0 - 180.0;
            double latitude = latIdx * 10.0 - 90.0;

            if (gridSquare.Length >= 4)
            {
                longitude += (gridSquare[2] - '0') * 2.0;
                latitude += gridSquare[3] - '0';
            }

            if (gridSquare.Length >= 6)
            {
                longitude += (gridSquare[4] - 'a') * (5.0 / 60.0);
                latitude += (gridSquare[5] - 'a') * (2.5 / 60.0);

                longitude = Math.Round(longitude,2);
                latitude = Math.Round(latitude, 2);
            }

            return new LatLong { Latitude = latitude, Longitude = longitude };
        }
        public struct DistanceAndBearing
        {
            public decimal Distance;
            public decimal Bearing;
        }
        public static DistanceAndBearing CalculateDistanceAndBearing(decimal lat1, decimal lon1, decimal lat2, decimal lon2)

        {
            decimal radius = 6371.0M; // Earth's radius in kilometers

            // Convert latitude and longitude to radians
            lat1 = (decimal)Math.PI * lat1 / 180.0M;
            lon1 = (decimal)Math.PI * lon1 / 180.0M;
            lat2 = (decimal)Math.PI * lat2 / 180.0M;
            lon2 = (decimal)Math.PI * lon2 / 180.0M;

            // Calculate Haversine distance
            decimal dlat = lat2 - lat1;
            decimal dlon = lon2 - lon1;
            decimal a = (decimal)Math.Sin((double)(dlat / 2)) * (decimal)Math.Sin((double)(dlat / 2)) +
                       (decimal)Math.Cos((double)lat1) * (decimal)Math.Cos((double)lat2) *
                       (decimal)Math.Sin((double)(dlon / 2)) * (decimal)Math.Sin((double)(dlon / 2));
            decimal c = 2 * (decimal)Math.Atan2((double)Math.Sqrt((double)a), (double)Math.Sqrt((double)(1 - a)));
            decimal distance = radius * c;

            // Calculate initial bearing
            decimal y = (decimal)Math.Sin((double)dlon) * (decimal)Math.Cos((double)lat2);
            decimal x = (decimal)Math.Cos((double)lat1) * (decimal)Math.Sin((double)lat2) -
                       (decimal)Math.Sin((double)lat1) * (decimal)Math.Cos((double)lat2) * (decimal)Math.Cos((double)dlon);
            decimal initialBearing = (decimal)Math.Atan2((double)y, (double)x);

            // Convert initial bearing to degrees
            initialBearing = (initialBearing * 180.0M) / (decimal)Math.PI;
            if (initialBearing < 0)
            {
                initialBearing += 360.0M;
            }

            return new DistanceAndBearing { Distance = distance, Bearing = initialBearing };
        }
    }
}
