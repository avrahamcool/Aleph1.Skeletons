using System.Threading.Tasks;

namespace Aleph1.Skeletons.WebAPI.Captcha.Contracts
{
	/// <summary>Completely automated public Turing test to tell computers and humans apart</summary>
	public interface ICaptcha
	{
		/// <summary>Validate a CAPTCHA token; Throws if invalid</summary>
		/// <param name="captcha">CAPTCHA token to be validated</param>
		Task ValidateCaptcha(string captcha);
	}
}
