using System.Threading.Tasks;

namespace Aleph1.Skeletons.WebAPI.Captcha.Contracts
{
	/// <summary>Ensure humanity of user</summary>
	public interface ICaptcha
	{
		/// <summary>Validate the CAPTCHA token - throws if invalid</summary>
		/// <param name="captchaToken">a token to be validated with a BackEnd service</param>
		Task ValidateCaptcha(string captchaToken);
	}
}
