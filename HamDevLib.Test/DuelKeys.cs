using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamDevLib.Test
{
    public class DuelKeys
    {
        struct Point
        {
            public double X;
            public double Y;
        }
        static double CalculateXIntersection(double x1, double y1, double x2, double y2)
        {
            // Calculate the slope and y-intercept of the line passing through the points
            double slope = (y2 - y1) / (x2 - x1);
            double yIntercept = y1 - slope * x1;

            // Calculate the x-intercept (where y = 0)
            double xIntercept = -yIntercept / slope;

            return xIntercept;
        }

        static double CalculateYIntersection(double x1, double y1, double x2, double y2)
        {
            // Calculate the slope and y-intercept of the line passing through the points
            double slope = (y2 - y1) / (x2 - x1);
            double yIntercept = y1 - slope * x1;

            // Calculate the y-intercept (where x = 0)
            double yIntersect = yIntercept;

            return yIntersect;
        }
        static Point GenerateRandomPoint(Random rand)
        {
            Point point;

            // Generate a random value between 0 and 1
            double t = rand.NextDouble();

            // Calculate the point coordinates using linear interpolation
            point.X = t;
            point.Y = 1 - t;

            return point;
        }

        static bool IsPointOnLine(double givenX, double x1, double y1, double x2, double y2)
        {
            // Calculate the expected y value using linear interpolation
            double expectedY = InterpolateY(givenX, x1, y1, x2, y2);

            // Check if the provided y value matches the expected y value
            double tolerance = 0.001; // Adjust as needed
            return Math.Abs(expectedY - y1) < tolerance || Math.Abs(expectedY - y2) < tolerance;
        }

        static double InterpolateY(double x, double x1, double y1, double x2, double y2)
        {
            // Calculate the slope of the line
            double slope = (y2 - y1) / (x2 - x1);

            // Calculate the expected y value using linear interpolation
            double expectedY = y1 + slope * (x - x1);

            return expectedY;
        }
    }
}
