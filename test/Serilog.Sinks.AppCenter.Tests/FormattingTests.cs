using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Serilog.Events;
using Xunit;

namespace Serilog.Sinks.AppCenter.Tests
{
    public sealed class FormattingTests
    {
        private static readonly ScalarValue ScalarComplex = new ScalarValue(new { A = 1, B = 2 });

        public class TheSimplifyMethod
        {
            [Fact]
            public void Should_Return_Null()
            {
                // Given, When, Then
                AppCenterPropertyFormatter.Simplify(null).Should().BeNull();
            }

            [Fact]
            public void Should_Return_String_When_Complex_Type_Passed_As_Scalar()
            {
                // Given, When
                var simplified = AppCenterPropertyFormatter.Simplify(ScalarComplex);

                // Then
                simplified.Should().BeOfType<string>();
            }

            [Fact]
            public void Should_Return_Array()
            {
                // Given, When
                var simplified = AppCenterPropertyFormatter.Simplify(new SequenceValue(new[] { ScalarComplex }));

                // Then
                simplified.Should().BeOfType<object[]>();
                ((object[])simplified).First().Should().BeOfType<string>();
            }

            [Fact]
            public void Should_Return_Structure_As_Dictionary()
            {
                // Given, When
                var simplified = AppCenterPropertyFormatter.Simplify(new StructureValue(new[] { new LogEventProperty("C", ScalarComplex) }));

                // Then
                simplified.Should().BeOfType<Dictionary<string, object>>();
                ((Dictionary<string, object>)simplified).Where(x => x.Key == "C").Select(x => x.Value).First().Should().BeOfType<string>();
            }
        }
    }
}
