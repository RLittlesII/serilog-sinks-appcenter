using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.AppCenter.Crash;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.AppCenter() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationAppCenterExtensions
    {
        /// <summary>
        /// Extension method that provides a <see cref="LoggerConfiguration"/>.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log level.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>The logger configuration with an AppCenterSink.</returns>
        public static LoggerConfiguration AppCenterCrashes(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Error,
            ITextFormatter formatProvider = null,
            IDictionary<string, string> properties = null)
        {
            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(loggerConfiguration));
            }

            return loggerConfiguration.Sink(
                new CrashSink(
                    formatProvider,
                    properties,
                    restrictedToMinimumLevel));
        }

        /// <summary>
        /// Extension method that provides a <see cref="LoggerConfiguration" /> and starts <see cref="AppCenter"/>.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="appCenterSecret">The application center secret.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log level.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="types">The <see cref="AppCenter"/> types.</param>
        /// <returns>
        /// The logger configuration with an AppCenterSink.
        /// </returns>
        public static LoggerConfiguration AppCenterCrashes(
            this LoggerSinkConfiguration loggerConfiguration,
            string appCenterSecret,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Error,
            ITextFormatter formatProvider = null,
            IDictionary<string, string> properties = null,
            params Type[] types)
        {
            if (!(string.IsNullOrEmpty(appCenterSecret) || AppCenter.IsEnabledAsync().GetAwaiter().GetResult()))
            {
                AppCenter.Start(appCenterSecret, types);
            }

            return loggerConfiguration.AppCenterCrashes(restrictedToMinimumLevel, formatProvider, properties);
        }
    }
}
