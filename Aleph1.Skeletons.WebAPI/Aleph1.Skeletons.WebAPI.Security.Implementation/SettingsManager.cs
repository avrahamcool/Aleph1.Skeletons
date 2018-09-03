using System;
using System.Configuration;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    /// <summary>Handle settings from config</summary>
    internal static class SettingsManager
    {
        private static string _appPrefix;
        public static string AppPrefix
        {
            get
            {
                if (_appPrefix == default(string))
                {
                    _appPrefix = ConfigurationManager.AppSettings["AppPrefix"];
                }
                return _appPrefix;
            }
        }

        private static int? _ticketDurationMin;
        private static TimeSpan? _ticketDurationTimeSpan;
        public static TimeSpan? TicketDurationTimeSpan
        {
            get
            {
                if (_ticketDurationMin == default(int?))
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
