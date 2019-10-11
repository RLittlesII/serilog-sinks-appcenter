using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.AppCenter.Crash.Tests
{
    public abstract class AppCenterTest
    {
        protected AppCenterTest()
        {
            Sink = new CrashSinkMock();

            Logger = new LoggerConfiguration()
                .WriteTo
                .Sink(Sink)
                .MinimumLevel
                .Error()
                .Enrich
                .FromLogContext()
                .CreateLogger();

        }

        public CrashSinkMock Sink { get; set; }

        public Exception LastException { get; set; }

        public IDictionary<string,string> LastProperties { get; set; }

        protected ILogger Logger { get; private set; }
    }
}
