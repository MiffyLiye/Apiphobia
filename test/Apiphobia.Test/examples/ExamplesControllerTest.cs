using System;
using System.Linq;
using System.Net.Http;
using Apiphobia.Controllers;
using Xunit;
using FluentAssertions;

namespace Apiphobia.Test.examples
{
    public class ExamplesControllerTest : IDisposable
    {
        private readonly ExamplesController _controller;

        public ExamplesControllerTest()
        {
            _controller = new ExamplesController();
        }

        public void Dispose()
        {
            _controller.Dispose();
        }

        [Fact]
        public void should_get_default_values()
        {
            var result = _controller.Get().ToList();

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.ElementAt(0).Should().Be("value1");
            result.ElementAt(1).Should().Be("value2");
        }

        [Fact]
        public void should_throw_when_get_with_invalid_parameter()
        {
            _controller.Invoking(c => c.Get("invalid"))
                .ShouldThrow<HttpRequestException>();
        }
    }
}