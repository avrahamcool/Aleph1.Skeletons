using System.Collections.Generic;

using Newtonsoft.Json;

namespace Aleph1.Skeletons.WebAPI.Captcha.Implementation
{
	/// <summary>See: https://developers.google.com/recaptcha/docs/verify#api-response</summary>
	internal class GoogleRecaptchaResponse
	{
		/// <summary>Success flag</summary>
		public bool Success { get; set; }

		/// <summary>Error codes</summary>
		[JsonProperty("error-codes")]
		public IEnumerable<string> ErrorCodes { get; set; }
	}
}
