/***************************************************************************************************
The MIT License (MIT)

Copyright 2019 Daiki Sakamoto

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, 
sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or 
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
***************************************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using BUILDLet.UnitTest.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BUILDLet.UnitTest.Utilities.Tests
{
    [TestClass()]
    public class TestParameterTests
    {
        public class ActNotImplementationTestParameter : TestParameter<string>
        {
            public override void Arrange(out string expected) { expected = null; }
        };

        public class ActsNotImplementationTestParameter : TestParameter<string>
        {
            public override void Arrange(out string expected, int index) { expected = null; }
        };

        public class ArrangeNotImplementationTestParameter : TestParameter<string>
        {
            public override void Act(out string actual) { actual = null; }
        };

        public class ArrangesNotImplementationTestParameter : TestParameter<string>
        {
            public override void Act(out string actual, int index){ actual = null; }
        };


        public abstract class TestParameterBase<T> : TestParameter<T>
        {
            public T Value1;
            public T Value2;

            public override void Arrange(out T expected)
            {
                // SET Expected
                expected = this.Value1;
            }

            public override void Act(out T actual)
            {
                // GET Actual
                actual = this.Value2;
            }
        }

        public class IntTestParameter : TestParameterBase<int> { }
        public class StringTestParameter : TestParameterBase<string> { }


        public abstract class ArrayTestParameterBase<T> : TestParameter<T>
        {
            public T[] Value1;
            public T[] Value2;

            public override void Arrange(out T expected, int index)
            {
                // SET Expected
                expected = this.Value1[index];
            }

            public override void Act(out T actual, int index)
            {
                // GET Actual
                actual = this.Value2[index];
            }
        }

        public class IntArrayTestParameter : ArrayTestParameterBase<int> { }
        public class StringArrayTestParameter : ArrayTestParameterBase<string> { }



        [TestMethod()]
        [TestCategory("Exception")]
        [ExpectedException(typeof(NotImplementedException))]
        public void ArrangeNotImplementationExceptionTest()
        {
            // SET Parameter
            ArrangeNotImplementationTestParameter param = new ArrangeNotImplementationTestParameter
            {
                Keyword = nameof(ArrangeNotImplementationExceptionTest),
            };

            // ASSERT
            param.Assert();
        }

        [TestMethod()]
        [TestCategory("Exception")]
        [ExpectedException(typeof(NotImplementedException))]
        public void ArrangesNotImplementationExceptionTest()
        {
            // SET Parameter
            ArrangesNotImplementationTestParameter param = new ArrangesNotImplementationTestParameter
            {
                Keyword = nameof(ArrangesNotImplementationExceptionTest),
            };

            // ASSERT
            param.Assert(count: 1);
        }

        [TestMethod()]
        [TestCategory("Exception")]
        [ExpectedException(typeof(NotImplementedException))]
        public void ActNotImplementationExceptionTest()
        {
            // SET Parameter
            ActNotImplementationTestParameter param = new ActNotImplementationTestParameter
            {
                Keyword = nameof(ActNotImplementationExceptionTest),
            };

            // ASSERT
            param.Assert();
        }

        [TestMethod()]
        [TestCategory("Exception")]
        [ExpectedException(typeof(NotImplementedException))]
        public void ActsNotImplementationExceptionTest()
        {
            // SET Parameter
            ActsNotImplementationTestParameter param = new ActsNotImplementationTestParameter
            {
                Keyword = nameof(ActsNotImplementationExceptionTest),
            };

            // ASSERT
            param.Assert(count: 1);
        }



        [TestMethod()]
        [TestCategory("Exception")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ExpectedInvalidOperationExceptionTest()
        {
            // SET Parameter
            IntTestParameter param = new IntTestParameter { };

            // ASSERT
            Console.WriteLine(param.Expected);
        }

        [TestMethod()]
        [TestCategory("Exception")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ActualInvalidOperationExceptionTest()
        {
            // SET Parameter
            IntTestParameter param = new IntTestParameter { };

            // ASSERT
            Console.WriteLine(param.Actual);
        }



        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(null, null, DisplayName = "Expected = null, Actual = null")]
        public void IntAssertTest(int expected, int actual)
        {
            // SET Parameter
            IntTestParameter param = new IntTestParameter
            {
                Keyword = nameof(IntAssertTest),
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert();
        }

        [DataTestMethod()]
        [DataRow(0, 1)]
        [DataRow(null, 1, DisplayName = "Expected = null")]
        [DataRow(1, null, DisplayName = "Actual = null")]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IntAssertFailedExceptionTest(int expected, int actual)
        {
            // SET Parameter
            IntTestParameter param = new IntTestParameter
            {
                Keyword = nameof(IntAssertFailedExceptionTest),
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert();
        }



        [DataTestMethod()]
        [DataRow("ABC", "ABC")]
        [DataRow("", "", DisplayName = "Expected = Actual = String.Empty")]
        [DataRow(null, null, DisplayName = "Expected = Actual = null")]
        public void StringAssertTest(string expected, string actual)
        {
            // SET Parameter
            StringTestParameter param = new StringTestParameter
            {
                Keyword = nameof(StringAssertTest),
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert();
        }

        [DataTestMethod()]
        [DataRow("ABC", "XYZ")]
        [DataRow(null, "XYZ", DisplayName = "Expected = null")]
        [DataRow("ABC", null, DisplayName = "Actual = null")]
        [DataRow("", "XYZ", DisplayName = "Expected = String.Empty")]
        [DataRow("ABC", "", DisplayName = "Actual = String.Empty")]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void StringAssertFailedExceptionTest(string expected, string actual)
        {
            // SET Parameter
            StringTestParameter param = new StringTestParameter
            {
                Keyword = nameof(StringAssertFailedExceptionTest),
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert();
        }



        [DataTestMethod()]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 }, "Print Keyword")]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 }, null)]
        public void IntArrayAssertTest(int[] expected, int[] actual, string keyword)
        {
            // SET Parameter
            IntArrayTestParameter param = new IntArrayTestParameter
            {
                Keyword = keyword,
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert(count: param.Value1.Length);
        }

        [DataTestMethod()]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 3 }, null)]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IntArrayAssertFailedExceptionTest(int[] expected, int[] actual, string keyword)
        {
            // SET Parameter
            IntArrayTestParameter param = new IntArrayTestParameter
            {
                Keyword = keyword,
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert(count: param.Value1.Length);
        }


        [DataTestMethod()]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XYZ" }, "Print Keyword")]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XYZ" }, null)]
        public void StringArrayAssertTest(string[] expected, string[] actual, string keyword)
        {
            // SET Parameter
            StringArrayTestParameter param = new StringArrayTestParameter
            {
                Keyword = keyword,
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert(count: param.Value1.Length);
        }

        [DataTestMethod()]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XY" }, null)]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void StringArrayAssertFailedExceptionTest(string[] expected, string[] actual, string keyword)
        {
            // SET Parameter
            StringArrayTestParameter param = new StringArrayTestParameter
            {
                Keyword = keyword,
                Value1 = expected,
                Value2 = actual,
            };

            // ASSERT
            param.Assert(count: param.Value1.Length);
        }
    }
}
