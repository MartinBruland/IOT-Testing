// Will be used to simulate sensors that generate raw data.

using System;
using System.Drawing;
using System.Reflection;

namespace MartinApps
{

    public class SensorSimulator
    {

        public string[] availableFishSpecies;

        public string weightMeasureUnit;

        public string lengthMeasureUnit;



        public static void Main(string[] args)
        { }


        public SensorSimulator(string[] fishSpecies, string weightUnit, string lengthUnit)
        {
            availableFishSpecies = fishSpecies;
            weightMeasureUnit = weightUnit;
            lengthMeasureUnit = lengthUnit;
        }


        // POSITION LOGGER: Track position of boat.
        public string[,] PositionLogger()
        {

            string[,] randomCoordinates = { { "Lon", "12" }, {"Lat", "45" } };

            return randomCoordinates;

        }


        // CAMERA SENSOR OUTSIDE BOAT: Fish Counter.
        public int CameraSensorBoat(int amountRegistered)
        {
            return amountRegistered;
        }


        // CAMERA SENSOR: Should Generate Length, Weight and Art. 
        public string[,] CameraSensorTable(int amountRegistered)
        {

            var random = new Random();
            int randomNumber = random.Next(0, availableFishSpecies.Length);

            string randomTypeOfFish = availableFishSpecies[randomNumber];

            string lengthOfFish = "120" + lengthMeasureUnit;

            string weightOfFish = "12" + weightMeasureUnit;


            string[,] rawData = { {"art", randomTypeOfFish }, {"length", lengthOfFish}, {"weight", weightOfFish} };

            return rawData;

        }


        // TABLE WEIGHT: Should Generate Weight. 
        public string WeightSensorTable(int amountRegistered)
        {

            string weightOfFish = "12" + weightMeasureUnit;

            return weightOfFish;

        }

    }

}