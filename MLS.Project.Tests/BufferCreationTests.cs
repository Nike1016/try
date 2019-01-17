﻿using System.Linq;
using FluentAssertions;
using MLS.Project.Generators;
using MLS.Protocol.Execution;
using MLS.TestSupport;
using Xunit;

namespace MLS.Project.Tests
{
    public class BufferCreationTests
    {
        [Fact]
        public void can_create_buffers_from_file_with_regions()
        {
            var file = FileGenerator.Create("Program.cs", TestSupport.SourceCodeProvider.ConsoleProgramMultipleRegions);

            var buffers = BufferGenerator.CreateFromFile(file).ToList();

            buffers.Should().NotBeNullOrEmpty();
            buffers.Count.Should().Be(2);
            buffers.Should().Contain(b => b.Id == "Program.cs@alpha");
            buffers.Should().Contain(b => b.Id == "Program.cs@beta");
        }

        [Fact]
        public void can_create_buffers_from_file_without_regions()
        {
            var file = FileGenerator.Create("Program.cs", TestSupport.SourceCodeProvider.ConsoleProgramNoRegion);

            var buffers = BufferGenerator.CreateFromFile(file).ToList();

            buffers.Should().NotBeNullOrEmpty();
            buffers.Count.Should().Be(1);
            buffers.Should().Contain(b => b.Id == "Program.cs");
        }

        [Fact]
        public void can_create_buffer_from_code_and_bufferId()
        {
            var buffer = BufferGenerator.CreateBuffer("Console.WriteLine(12);", "program.cs");
            buffer.Should().NotBeNull();
            buffer.Id.Should().Be(new BufferId("program.cs"));
            buffer.Content.Should().Be("Console.WriteLine(12);");
            buffer.AbsolutePosition.Should().Be(0);
        }

        [Fact]
        public void can_create_buffer_with_bufferId_and_region()
        {
            var buffer = BufferGenerator.CreateBuffer("Console.WriteLine(12);", "program.cs@region1");
            buffer.Should().NotBeNull();
            buffer.Id.Should().Be(new BufferId("program.cs", "region1"));
            buffer.Content.Should().Be("Console.WriteLine(12);");
            buffer.AbsolutePosition.Should().Be(0);
        }

        [Fact]
        public void can_create_buffer_with_markup()
        {
            var buffer = BufferGenerator.CreateBuffer("Console.WriteLine($$);", "program.cs@region1");
            buffer.Should().NotBeNull();
            buffer.Id.Should().Be(new BufferId("program.cs", "region1"));
            buffer.Content.Should().Be("Console.WriteLine();");
            buffer.AbsolutePosition.Should().Be(18);
        }
    }
}