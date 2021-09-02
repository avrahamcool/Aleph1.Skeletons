using System;
using System.Configuration;

namespace Aleph1.Skeletons.WebAPI.Captcha.Implementation
{
	/// <summary>Handle settings from web.config</summary>
	internal static class SettingsManager
	{
		private static Uri _captchaApiUrl;
		public static Uri CaptchaApiUrl
		{
			get
			{
				if (_captchaApiUrl == default)
				{
					_captchaApiUrl = new Uri(ConfigurationManager.AppSettings["CaptchaApiUrl"]);
				}
				return _captchaApiUrl;
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
