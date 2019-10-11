using System;
using System.Collections.Generic;

namespace Serilog.Sinks.AppCenter.Crash.Tests
{
    public class CrashSinkMock : CrashSink
    {
        protected override void TrackError(Exception exception, IDictionary<string, string> properties)
        {
            Exception = exception;
            Properties = properties;
        }

        public Exception Exception { get; set; }

        public new IDictionary<string, string> Properties { get; set; }
    }
}