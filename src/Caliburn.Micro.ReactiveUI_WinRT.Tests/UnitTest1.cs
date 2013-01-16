using System;
using System.Linq;
using Caliburn.Micro.ReactiveUI;
using Xunit;

namespace Caliburn.Micro.ReactiveUI_WinRT.Tests
{
    public class TypeExtensionsTests
    {
        Type type;

        public TypeExtensionsTests()
        {
            type = typeof (SimpleClass);
        }

        [Fact]
        public void GetProperties_ForSimpleClass_ReturnsPublicMembers()
        {
            // "Returns all the public properties of the current Type."
            Assert.Equal(2, type.GetProperties().Count());
        }

        [Fact]
        public void GetProperty_ForSimpleClass_ReturnsCorrectMembers()
        {
            Assert.NotNull(type.GetProperty("Foo"));
            Assert.NotNull(type.GetProperty("Bar"));

            Assert.Null(type.GetProperty("foo")); // incorrect casing
            Assert.Null(type.GetProperty("Private")); // private property
            Assert.Null(type.GetProperty("Steve")); // does not exist
        }

        [Fact]
        public void GetMethod_ForSimpleClass_ReturnsCorrectMembers()
        {
            Assert.NotNull(type.GetMethod("A"));
            
            Assert.Null(type.GetMethod("a")); // incorrect casing
            Assert.Null(type.GetMethod("B")); // private property
            Assert.Null(type.GetMethod("C")); // does not exist
        }

        class SimpleClass
        {
            public string Foo { get; set; }
            public string Bar { get; set; }
            string Private { get; set; }

            public void A() { }
            void B() { }
        }
    }
}
