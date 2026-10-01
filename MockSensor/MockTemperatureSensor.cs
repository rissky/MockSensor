using System;
using System.Timers;

namespace MockSensorLibrary
{
    public class MockTemperatureSensor : IDisposable
    {
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        private readonly System.Timers.Timer _timer;
        private readonly Random _random;

        private double _currentTemperature;
        private double _targetTemperature;

        public MockTemperatureSensor()
        {
            _random = new Random();

            // Baseline starting temperatures for a refrigerated van
            _currentTemperature = 5.0;
            _targetTemperature = 5.0;

            // Fire every 10 seconds
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
        }

        public void Connect()
        {
            _timer.Start();
            Console.WriteLine("[Sensor Hardware] Connected and monitoring...");
        }

        public void Disconnect()
        {
            _timer.Stop();
            Console.WriteLine("[Sensor Hardware] Disconnected.");
        }

        // --- SIMULATION METHODS FOR TESTING ---

        /// <summary>
        /// Simulates the van doors opening, causing the temperature to rise quickly toward ambient outside temp.
        /// </summary>
        public void SimulateDoorsOpen()
        {
            _targetTemperature = 15.0; // Warm outside air
            Console.WriteLine("\n[Simulation] ⚠️ Van doors opened! Target temp is now 15.0°C");
        }

        /// <summary>
        /// Simulates the van doors closing, allowing the chiller to bring the temperature back down.
        /// </summary>
        public void SimulateDoorsClosed()
        {
            _targetTemperature = 5.0; // Chiller working normally
            Console.WriteLine("\n[Simulation] ❄️ Van doors closed! Target temp is now 5.0°C");
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Move the current temperature towards the target temperature
            if (_currentTemperature < _targetTemperature)
            {
                // Temperature rises quickly when doors are open
                _currentTemperature += _random.NextDouble() * 1.5;
            }
            else if (_currentTemperature > _targetTemperature)
            {
                // Temperature drops steadily when doors are closed
                _currentTemperature -= _random.NextDouble() * 1.0;
            }

            // Add a tiny bit of random sensor noise (-0.2 to +0.2)
            _currentTemperature += (_random.NextDouble() - 0.5) * 0.4;

            var eventArgs = new TemperatureChangedEventArgs(_currentTemperature, DateTime.Now);
            TemperatureChanged?.Invoke(this, eventArgs);
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}