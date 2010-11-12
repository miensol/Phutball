using System;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests
{
    [TestFixture]
    public class BasicExtensionsTester
    {
        public class TestObject
        {
            public TestObject Child { get; set; }
            public int Value { get; set; }
            public bool Boolean { get; set; }
        }

        [Test]
        public void FirstValue()
        {
            var objects = new TestObject[] {new TestObject(), new TestObject(), new TestObject()};
            objects.FirstValue(x => x.Child).ShouldBeNull();

            var theChild = new TestObject();
            objects[1].Child = theChild;
            objects[2].Child = new TestObject();

            objects.FirstValue(x => x.Child).ShouldBeTheSameAs(theChild);
        }

        public void UrlEncode_should_encode_string()
        {
            string test = "encode test";

            test.UrlEncoded().ShouldEqual("encode%20test");
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void If_should_also_return_an_ArgumentException()
        {
            var test = new TestObject
                           {
                               Boolean = true
                           };

            "test".If(() => test.Boolean && test.Boolean);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void If_should_return_an_ArgumentException()
        {
            var test = new TestObject
                           {
                               Value = 1
                           };

            "test".If(() => test.Value == 1);
        }

        [Test]
        public void If_should_return_empty_string_value()
        {
            var test = new TestObject
                           {
                               Boolean = false
                           };

            "test".If(() => test.Boolean).ShouldEqual("");
        }

        [Test]
        public void If_should_return_original_value()
        {
            var test = new TestObject
                           {
                               Boolean = true
                           };

            "test".If(() => test.Boolean).ShouldEqual("test");
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void IfNot_should_also_return_an_ArgumentException()
        {
            var test = new TestObject
                           {
                               Boolean = true
                           };

            "test".IfNot(() => test.Boolean && test.Boolean);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void IfNot_should_return_an_ArgumentException()
        {
            var test = new TestObject
                           {
                               Value = 1
                           };

            "test".IfNot(() => test.Value == 1);
        }

        [Test]
        public void IfNot_should_return_empty_string_value()
        {
            var test = new TestObject
                           {
                               Boolean = false
                           };

            "test".IfNot(() => test.Boolean).ShouldEqual("test");
        }

        [Test]
        public void IfNot_should_return_original_value()
        {
            var test = new TestObject
                           {
                               Boolean = true
                           };

            "test".IfNot(() => test.Boolean).ShouldEqual("");
        }



        
    }
}