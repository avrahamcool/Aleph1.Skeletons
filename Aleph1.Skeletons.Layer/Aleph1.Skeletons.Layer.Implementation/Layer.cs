using System;

using Aleph1.Logging;
using Aleph1.Skeletons.Layer.Contracts;

namespace Aleph1.Skeletons.Layer.Implementation
{
	internal class Layer : ILayer
	{
		[Logged]
		public void DoSomething()
		{
			throw new NotImplementedException();
		}
	}
}