using System;
using FluentAssertions;
using Serilog.Context;
using Xunit;

namespace Serilog.Sinks.AppCenter.Crash.Tests
{
    public class FormattingTests : AppCenterTest
    {
        [Fact]
        public void Properties_Should_Contain_Level()
        {
            Logger.Error("test");

            Sink.Properties.ContainsKey("level").Should().BeTrue();
        }

        [Fact]
        public void Properties_Should_Contain_Message()
        {
            Logger.Error("test");

            Sink.Properties.ContainsKey("message").Should().BeTrue();
        }

        [Fact]
        public void Properties_Should_Contain_Log_Context()
        {
            using (LogContext.PushProperty("hello", "world"))
            {
                Logger.Error("hello");

                Sink.Properties.TryGetValue("hello", out string result).Should().BeTrue();
                result.Should().Be("\"world\"");
            }
        }

        [Fact]
        public void Properties_Should_Include_Log_Context()
        {
            using (LogContext.PushProperty("custom1", "value1"))
            {
                Logger.Error("test context");

                Sink.Properties.TryGetValue("custom1", out string result);
                result.Should().Be("\"value1\"");
            }
        }

        [Fact]
        public void Json_Parameter_Should_Be_Compact()
        {
            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;
            var str = "value";
            var numbers = new int[] { 1, 2, 3, 4 };

            Logger.Error("Processed {@Position} in {Elapsed:000} ms., str {str}, numbers: {numbers}", position, elapsedMs, str, numbers);

            Sink.Properties["Elapsed"].Should().Be("34");
            Sink.Properties["Position"].Should().Be("{ Latitude: 25, Longitude: 134 }");
            Sink.Properties["str"].Should().Be("\"value\"");
            Sink.Properties["numbers"].Should().Be("[1, 2, 3, 4]");
        }

        [Fact]
        public void Exception_Should_Format()
        {
            var exception = new Exception("This is only an exception.");

            Logger.Error(exception, $"message template: {exception}");

            Sink.Properties["message"].Should().Be("message template: System.Exception: This is only an exception.");
        }
    }
}