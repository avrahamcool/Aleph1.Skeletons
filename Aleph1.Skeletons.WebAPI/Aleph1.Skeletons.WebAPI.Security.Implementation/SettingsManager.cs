using System;
using System.Configuration;
using System.Globalization;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
	/// <summary>Manage settings from Web.config</summary>
	internal static class SettingsManager
	{
		// Randomly generated GUID; You can change this to whatever you want
		public static string AppPrefix => "{5BEE28FC-635A-4BB3-A82F-611BB51900F9}";

		private static int? _claimsInactivityMaxAgeValue;
		private static TimeSpan? _claimsInactivityMaxAge;
		public static TimeSpan? ClaimsInactivityMaxAge
		{
			get
			{
				if (_claimsInactivityMaxAgeValue == default)
				{
					_claimsInactivityMaxAgeValue = int.Parse(ConfigurationManager.AppSettings["ClaimsInactivityMaxAge"], CultureInfo.InvariantCulture);
					if (_claimsInactivityMaxAgeValue.Value != 0)
					{
						_claimsInactivityMaxAge = TimeSpan.FromMinutes(_claimsInactivityMaxAgeValue.Value);
					}
				}
				return _claimsInactivityMaxAge;
			}
		}

		private static int? _claimsRefreshMaxAgeValue;
		private static TimeSpan _claimsRefreshMaxAge;
		public static TimeSpan ClaimsRefreshMaxAge
		{
			get
			{
				if (_claimsRefreshMaxAgeValue == default)
				{
					_claimsRefreshMaxAgeValue = int.Parse(ConfigurationManager.AppSettings["ClaimsRefreshMaxAge"], CultureInfo.InvariantCulture);
					if (_claimsRefreshMaxAgeValue.Value != 0)
					{
						_claimsRefreshMaxAge = TimeSpan.FromMinutes(_claimsRefreshMaxAgeValue.Value);
					}
				}
				return _claimsRefreshMaxAge;
			}
		}

		private static int? _claimsExpirationsMaxAgeValue;
		private static TimeSpan _claimsExpirationMaxAge;
		public static TimeSpan ClaimsExpirationMaxAge
		{
			get
			{
				if (_claimsExpirationsMaxAgeValue == default)
				{
					_claimsExpirationsMaxAgeValue = int.Parse(ConfigurationManager.AppSettings["ClaimsExpirationMaxAge"], CultureInfo.InvariantCulture);
					if (_claimsExpirationsMaxAgeValue.Value != 0)
					{
						_claimsExpirationMaxAge = TimeSpan.FromMinutes(_claimsExpirationsMaxAgeValue.Value);
					}
				}
				return _claimsExpirationMaxAge;
			}
		}

	}
}
