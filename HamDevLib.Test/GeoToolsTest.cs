using HamDotNetToolkit;

namespace HamDevLib.Test;
[TestClass]
public class GeoToolsTest
{
    [TestMethod]
    public void TestLatLongToGridSquare()
    {
        double latitude = 38.897663; // New York City
        double longitude = -77.036570; // New York City
        string expectedGridSquare = "FM18";

        string gridSquare = GeoTools.ConvertLatLongToGridSquare(latitude, longitude);

        Assert.AreEqual(expectedGridSquare, gridSquare);
    }

    [TestMethod]
    public void TestGridSquareToLatLong()
    {
        string gridSquare = "FN31pr"; // New York City
        double expectedLatitude = 41.708, expectedLongitude = -72.75000;
        var expectedCoordinates = new LatLong { Latitude = expectedLatitude, Longitude = expectedLongitude };

        var coordinates = GeoTools.ConvertGridSquareToLatLong(gridSquare);

        var rc = Math.Abs(expectedCoordinates.Latitude - coordinates.Latitude);
        Assert.IsTrue( rc < 0.1);

        rc = Math.Abs(expectedCoordinates.Longitude - coordinates.Longitude);
        Assert.IsTrue( rc < 0.1);
    }

    [TestMethod]
    public void TestCalculateDistanceAndBearing()
    {
        decimal lat1 = 38.897663M; // Latitude of Point 1 (New York City)
        decimal lon1 = -77.036570M; // Longitude of Point 1 (New York City)
        decimal lat2 = 34.052235M; // Latitude of Point 2 (Los Angeles)
        decimal lon2 = -118.243683M; // Longitude of Point 2 (Los Angeles)

        var result = GeoTools.CalculateDistanceAndBearing(lat1, lon1, lat2, lon2);

        // Expected values based on calculation
        decimal expectedDistance = 3935.872642175514978802762M; // km
        decimal expectedBearing = 270.820272871413429979325M; // degrees

        Assert.IsTrue(Math.Abs(result.Distance - expectedDistance) > 0.001M);
        Assert.IsTrue(Math.Abs(result.Bearing - expectedBearing) > 0.001M);
    }
}

