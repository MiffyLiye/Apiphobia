using System;
using System.Linq;
using Apiphobia.Controllers;
using Apiphobia.Models;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Apiphobia.Test.examples
{
    public class ExamplesControllerTest : IDisposable
    {
        private readonly ExamplesController _controller;

        public ExamplesControllerTest()
        {
            var examplesRepository = new ExamplesRepository();
            _controller = new ExamplesController(examplesRepository);
        }

        public void Dispose()
        {
            _controller.Dispose();
        }

        [Fact]
        public void should_get_all_values()
        {
            _controller.Post(new Example{Value = "value1"});
            _controller.Post(new Example{Value = "value2"});

            var result = _controller.Get().ToList();

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.Should().Contain(e => e.Value == "value1");
            result.Should().Contain(e => e.Value == "value2");
        }

        [Fact]
        public void should_get_not_found_when_get_with_invalid_parameter()
        {
            var result = _controller.Get("invalid");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}