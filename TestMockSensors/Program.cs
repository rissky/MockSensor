using MockSensorLibrary;

namespace TestMockSensors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Sensor System...");

            // 1. Create the sensor
            using (var sensor = new MockTemperatureSensor())
            {
                // 2. Subscribe to the event
                sensor.TemperatureChanged += Sensor_TemperatureChanged;

                // 3. Start the sensor
                sensor.Connect();

                Console.WriteLine("Press ENTER to exit the application at any time.\n");
                Console.ReadLine(); // Keep the app running so the timer can work

                // 4. Cleanly disconnect (optional, but good practice)
                sensor.Disconnect();
            }
        }

        // The Event Handler method
        private static void Sensor_TemperatureChanged(object sender, TemperatureChangedEventArgs e)
        {
            Console.WriteLine($"[New Reading] Time: {e.Timestamp:T} | Temp: {e.TemperatureCelsius}°C");
        }
    }
}