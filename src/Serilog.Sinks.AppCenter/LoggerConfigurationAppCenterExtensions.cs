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
        /// <summary>
        /// Extension method that provides a <see cref="LoggerConfiguration"/>.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="appSecret">The app secret.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log level.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>The logger configuration with an AppCenterSink.</returns>
        public static LoggerConfiguration AppCenter(
            this LoggerSinkConfiguration loggerConfiguration,
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

            return loggerConfiguration.Sink(new AppCenterSink(
                formatProvider,
                appSecret,
                properties,
                restrictedToMinimumLevel));
        }
    }
}
