﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using HtmlAgilityPack;
using System.Linq;
using XPlot.DotNet.Interactive.KernelExtensions;
using XPlot.Plotly;
using Xunit;

namespace MLS.Agent.Tests
{
    public partial class XplotKernelExtensionTests
    {
        public class GetChartHtmlTests
        {
            [Fact]
            public void Returns_the_html_with_div()
            {
                var extension = new XPlotKernelExtension();
                var html = extension.GetChartHtml(new PlotlyChart());
                var document = new HtmlDocument();
                document.LoadHtml(html);

                document.DocumentNode.SelectSingleNode("//div").InnerHtml.Should().NotBeNull();
                document.DocumentNode.SelectSingleNode("//div").Id.Should().NotBeNullOrEmpty();
            }

            [Fact]
            public void Returns_the_html_with_script_containing_require_config()
            {
                var extension = new XPlotKernelExtension();
                var html = extension.GetChartHtml(new PlotlyChart());
                var document = new HtmlDocument();
                document.LoadHtml(html);

                document.DocumentNode.SelectSingleNode("//script").InnerHtml.Should().Contain("require.config({paths:{plotly:\'https://cdn.plot.ly/plotly-latest.min\'}});");
            }

            [Fact]
            public void Returns_the_html_with_script_containing_require_plotly_and_call_to_new_plot_function()
            {
                var extension = new XPlotKernelExtension();
                var html = extension.GetChartHtml(new PlotlyChart());
                var document = new HtmlDocument();
                document.LoadHtml(html);

                var divId = document.DocumentNode.SelectSingleNode("//div").Id;
                document.DocumentNode
                    .SelectSingleNode("//script")
                    .InnerHtml.Split("\n")
                    .Select(item => item.Trim())
                    .Where(item => !string.IsNullOrWhiteSpace(item))
                    .Should()
                    .ContainInOrder(@"require(['plotly'], function(Plotly) {",
                                        "var data = null;",
                                         @"var layout = """";",
                                         $"Plotly.newPlot('{divId}', data, layout);");
            }
        }

    }
}