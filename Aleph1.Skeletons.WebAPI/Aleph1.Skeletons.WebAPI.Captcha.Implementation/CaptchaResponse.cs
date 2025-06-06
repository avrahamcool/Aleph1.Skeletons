using System.Collections.Generic;

using Newtonsoft.Json;

namespace Aleph1.Skeletons.WebAPI.Captcha.Implementation
{
	/// <summary>https://developers.google.com/recaptcha/docs/verify#api-response</summary>
	internal sealed class CaptchaResponse
	{
		/// <summary></summary>
		public bool Success { get; set; }

		/// <summary></summary>
		[JsonProperty("error-codes")]
		public IEnumerable<string> ErrorCodes { get; set; }
	}
}
