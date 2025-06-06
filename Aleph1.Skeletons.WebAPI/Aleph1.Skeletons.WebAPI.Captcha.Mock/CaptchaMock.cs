
using System.Threading.Tasks;

using Aleph1.Skeletons.WebAPI.Captcha.Contracts;

namespace Aleph1.Skeletons.WebAPI.Captcha.Mock
{
	internal sealed class CaptchaMock : ICaptcha
	{
		public Task ValidateCaptcha(string captchaToken)
		{
			return Task.CompletedTask;
		}
	}
}
