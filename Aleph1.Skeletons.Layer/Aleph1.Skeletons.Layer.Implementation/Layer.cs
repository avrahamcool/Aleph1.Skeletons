using Aleph1.Logging;
using Aleph1.Skeletons.Layer.Contracts;

using System;

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