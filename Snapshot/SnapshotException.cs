using System;
using Xunit.Sdk;

namespace Snapshot
{
    public class SnapshotException : XunitException
    {
        public SnapshotException(Exception equalException, string message) : base(message, equalException)
        {
        }

        public SnapshotException(string message) : base(message)
        {
        }
    }
}