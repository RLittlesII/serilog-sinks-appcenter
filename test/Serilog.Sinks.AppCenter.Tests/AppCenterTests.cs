using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Serilog.Sinks.AppCenter.Tests
{
    public class AppCenterTests
    {
        protected ILogger Logger { get; private set; }

        public AppCenterTests()
        {
            Logger = new LoggerConfiguration()
                .WriteTo.AppCenterCrashes()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        [Fact]
        public void Should()
        {
            Logger.Information("test");
        }
    }
    public abstract class AppCenterTest
    {
//        private readonly UnitTestTelemetryChannel _channel;
//
//        protected AppCenterTest()
//        {
//            var tc = new TelemetryConfiguration("", _channel = new UnitTestTelemetryChannel());
//
//            Logger = new LoggerConfiguration()
//                .WriteTo.ApplicationInsights(tc, converter ?? TelemetryConverter.Traces)
//                .MinimumLevel.Debug()
//                .Enrich.FromLogContext()
//                .CreateLogger();
//        }
//
//        protected ILogger Logger { get; private set; }
//
//        protected List<ITelemetry> SubmittedTelemetry => _channel.SubmittedTelemetry;
//
//        protected ITelemetry LastSubmittedTelemetry => _channel.SubmittedTelemetry.LastOrDefault();
//
//        protected TraceTelemetry LastSubmittedTraceTelemetry =>
//            _channel.SubmittedTelemetry
//                .Where(t => t is TraceTelemetry)
//                .Select(t => (TraceTelemetry)t)
//                .LastOrDefault();

    }
}
