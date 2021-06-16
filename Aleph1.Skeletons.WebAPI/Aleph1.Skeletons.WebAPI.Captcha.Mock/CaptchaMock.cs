
using System.Threading.Tasks;

using Aleph1.Skeletons.WebAPI.Captcha.Contracts;

namespace Aleph1.Skeletons.WebAPI.Captcha.Mock
{
	internal class CaptchaMock : ICaptcha
	{
		public Task ValidateCaptcha(string captchaToken) => Task.CompletedTask;
	}
}
