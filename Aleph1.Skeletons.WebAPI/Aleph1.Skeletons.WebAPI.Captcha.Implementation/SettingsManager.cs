using System;
using System.Configuration;

namespace Aleph1.Skeletons.WebAPI.Captcha.Implementation
{
	/// <summary>Handle settings from web.config</summary>
	internal static class SettingsManager
	{
		private static Uri _captchaAPIUrl;
		public static Uri CaptchaAPIUrl
		{
			get
			{
				if (_captchaAPIUrl == default)
				{
					_captchaAPIUrl = new Uri(ConfigurationManager.AppSettings["CaptchaAPIUrl"]);
				}
				return _captchaAPIUrl;
			}
		}

		private static string _captchaSecret;
		public static string CaptchaSecret
		{
			get
			{
				if (_captchaSecret == default)
				{
					_captchaSecret = ConfigurationManager.AppSettings["CaptchaSecret"];
				}
				return _captchaSecret;
			}
		}
	}
}
