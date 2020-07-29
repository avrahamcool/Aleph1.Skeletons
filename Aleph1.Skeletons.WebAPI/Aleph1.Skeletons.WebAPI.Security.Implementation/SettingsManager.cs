using System;
using System.Configuration;
using System.Globalization;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    /// <summary>Handle settings from config</summary>
    internal static class SettingsManager
    {
        // randomly generated GUID for each project - you can change this to whatever you want
        public static string AppPrefix => "{5BEE28FC-635A-4BB3-A82F-611BB51900F9}";

        private static int? _ticketDurationMin;
        private static TimeSpan? _ticketDurationTimeSpan;
        public static TimeSpan? TicketDurationTimeSpan
        {
            get
            {
                if (_ticketDurationMin == default)
                {
                    _ticketDurationMin = int.Parse(ConfigurationManager.AppSettings["TicketDurationMin"], CultureInfo.InvariantCulture);
                    if (_ticketDurationMin.Value != 0)
                    {
                        _ticketDurationTimeSpan = TimeSpan.FromMinutes(_ticketDurationMin.Value);
                    }
                }
                return _ticketDurationTimeSpan;
            }
        }
    }
}
