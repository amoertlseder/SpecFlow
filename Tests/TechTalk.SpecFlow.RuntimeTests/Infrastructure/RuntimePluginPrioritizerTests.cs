﻿using System.Runtime.InteropServices;
using FluentAssertions;
using TechTalk.SpecFlow.Plugins;
using Xunit;

namespace TechTalk.SpecFlow.RuntimeTests.Infrastructure
{
    public class RuntimePluginPrioritizerTests
    {
        [Fact]
        public void Prioritize_EmptyList_EmptyList()
        {
            //ARRANGE
            var runtimePluginPrioritizer = new RuntimePluginPrioritizer();


            //ACT
            var result = runtimePluginPrioritizer.Prioritize(new string[] { });


            //ASSERT
            result.Should().HaveCount(0);
        }

        [SkippableFact]
        public void Prioritize_SingleEntry_ThisIsReturned_Windows()
        {
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            //ARRANGE
            var runtimePluginPrioritizer = new RuntimePluginPrioritizer();


            //ACT
            var result = runtimePluginPrioritizer.Prioritize(new[]{ "C:\\temp\\Plugin.SpecFlowPlugin.dll" } );


            //ASSERT
            result.Should().HaveCount(1);
            result[0].Should().Be("C:\\temp\\Plugin.SpecFlowPlugin.dll");
        }

        [SkippableFact]
        public void Prioritize_SingleEntry_ThisIsReturned_Unix()
        {
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||RuntimeInformation.IsOSPlatform(OSPlatform.OSX));

            //ARRANGE
            var runtimePluginPrioritizer = new RuntimePluginPrioritizer();


            //ACT
            var result = runtimePluginPrioritizer.Prioritize(new[] { "/temp/Plugin.SpecFlowPlugin.dll" });


            //ASSERT
            result.Should().HaveCount(1);
            result[0].Should().Be("/temp/Plugin.SpecFlowPlugin.dll");
        }


        [SkippableFact]
        public void Prioritize_MultiplePath_ReturnedByPath_Windows()
        {
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            //ARRANGE
            var runtimePluginPrioritizer = new RuntimePluginPrioritizer();


            //ACT
            var result = runtimePluginPrioritizer.Prioritize(new[] { "C:\\temp\\Plugin.SpecFlowPlugin.dll", "C:\\temp\\AnotherPlugin.SpecFlowPlugin.dll" });


            //ASSERT
            result.Should().HaveCount(2);
            result[0].Should().Be("C:\\temp\\AnotherPlugin.SpecFlowPlugin.dll");
            result[1].Should().Be("C:\\temp\\Plugin.SpecFlowPlugin.dll");
        }


        [SkippableFact]
        public void Prioritize_MultiplePath_ReturnedByPath__Unix()
        {
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX));

            //ARRANGE
            var runtimePluginPrioritizer = new RuntimePluginPrioritizer();


            //ACT
            var result = runtimePluginPrioritizer.Prioritize(new[] { "/temp/Plugin.SpecFlowPlugin.dll", "/temp/AnotherPlugin.SpecFlowPlugin.dll" });


            //ASSERT
            result.Should().HaveCount(2);
            result[0].Should().Be("/temp/AnotherPlugin.SpecFlowPlugin.dll");
            result[1].Should().Be("/temp/Plugin.SpecFlowPlugin.dll");
        }
    }
}