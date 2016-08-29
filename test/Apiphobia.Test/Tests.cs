using System;
using Xunit;
using FluentAssertions;

namespace Apiphobia.Test
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            true.Should().BeTrue();
        }
    }
}
