using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.AppCenter;

namespace Serilog
{
    public static class LoggerConfigurationAppCenterExtensions
    {
        public static LoggerConfiguration AppCenter(this LoggerSinkConfiguration loggerConfiguration,
            string appSecret,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Error,
            IFormatProvider formatProvider = null,
            IDictionary<string, string> properties = null)
        {
            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(loggerConfiguration));
            }

            if (string.IsNullOrWhiteSpace(appSecret))
            {
                throw new ArgumentNullException(nameof(appSecret));
            }

            return loggerConfiguration.Sink(new AppCenterSink(formatProvider,
                appSecret,
                properties,
                restrictedToMinimumLevel));
        }
    }
}
