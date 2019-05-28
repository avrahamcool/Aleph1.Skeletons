using System;
using System.Configuration;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    /// <summary>Handle settings from config</summary>
    internal static class SettingsManager
    {
        public static string AppPrefix
        {
            // randomly generated guid for each project - you can change this to whatever you want
            get => "{5BEE28FC-635A-4BB3-A82F-611BB51900F9}";
        }

        private static int? _ticketDurationMin;
        private static TimeSpan? _ticketDurationTimeSpan;
        public static TimeSpan? TicketDurationTimeSpan
        {
            get
            {
                if (_ticketDurationMin == default)
                {
                    _ticketDurationMin = int.Parse(ConfigurationManager.AppSettings["TicketDurationMin"]);
                    if (_ticketDurationMin.Value != 0)
                        _ticketDurationTimeSpan = TimeSpan.FromMinutes(_ticketDurationMin.Value);
                }
                return _ticketDurationTimeSpan;
            }
        }
    }
}
