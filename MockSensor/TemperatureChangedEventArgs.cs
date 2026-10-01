using System;

namespace MockSensorLibrary
{
    /// <summary>
    /// Contains the data for a temperature reading event.
    /// </summary>
    public class TemperatureChangedEventArgs : EventArgs
    {
        public double TemperatureCelsius { get; }
        public DateTime Timestamp { get; }

        public TemperatureChangedEventArgs(double temperatureCelsius, DateTime timestamp)
        {
            TemperatureCelsius = Math.Round(temperatureCelsius, 2);
            Timestamp = timestamp;
        }
    }
}