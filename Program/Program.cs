
// MAIN PROGRAM THAT WILL EXECUTE ALL FUNCTIONALITIES.

using System;





namespace MartinApps
{
    class Program
    {
        public static void Main(string[] args)
        {

            string[] availableFishSpecies = { "Makrell", "Torsk", "Laks", "Megladon", "Nessie", "Kraken", "Moby Dick" };

            const string weightMeasureUnit = "kg";

            const string lengthMeasureUnit = "cm";

            SensorSimulator simulator = new SensorSimulator(availableFishSpecies, weightMeasureUnit, lengthMeasureUnit);






            // SIMULATE FISHING ACTIVITY.
            int amountOfFishRegistered = 0;

            string[] registeredCatches = Array.Empty<string>();

            string[,] activePosition = simulator.PositionLogger();


            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("Fishing Activity Has Started.");
            Console.WriteLine("-----------------------------------------------\n");
            Console.WriteLine($"Position Tracker: Current coordinates are {activePosition}.");


            const int iterations = 5;

            for (int i = 0; i < iterations; i++)
            {

                var random = new Random();

                int sizeOfCatch = random.Next(0, 15);

                
                int fishCount = simulator.CameraSensorBoat(sizeOfCatch); // Track Number of Caught Fish.

                string[,] simMLRawData = simulator.CameraSensorTable(fishCount); // Take Photograph of Fish ( Will use Machine Learning to identify Art, Length and Weight. )

                string controlWeighFish = simulator.WeightSensorTable(fishCount); // Control weighing measurements of Fish.


                amountOfFishRegistered += fishCount;


                Console.WriteLine("\n\n´´CAPTAIN AHAB THROWS HIS FISHING NET INTO THE SEA.´\n");
                Console.WriteLine($"Camera Sensor - Outside Boat: Registered {fishCount} fish.");
                Console.WriteLine($"Camera Sensor - Table: Registered fish as art: {simMLRawData[0,1]}. With an estimated length of: {simMLRawData[1,1]} and weight of {simMLRawData[2,1]}.");
                Console.WriteLine($"Weight Sensor - Table: Registered actual weight of fish as {controlWeighFish}.");

            }

            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("Fishing Activity Has Ended. ");
            Console.WriteLine("-----------------------------------------------\n");
            Console.WriteLine($"Total Catch: {amountOfFishRegistered} fish. \n");

        }
    }
}



// NEXT STEP: ORDNE SLIK AT: "WEIGHT SENSOR - TABLE" OG "CAMERA SENSOR - TABLE", ER I STAND TIL Å ANALYSERE FLERE FISK ISTEDENFOR KUN 1.