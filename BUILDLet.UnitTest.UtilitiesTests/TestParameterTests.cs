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
using System;
using System.Collections.Generic;
using System.Text;

namespace BUILDLet.UnitTest.Utilities.Tests
{
    [TestClass]
    public class TestParameterTests
    {
        // TestParameter for NotInitializedTest
        public class NotInitializedTestParameter : TestParameter<object>
        {
            public override void Act(out object actual) => throw new NotImplementedException();
            public override void Arrange(out object expected) => throw new NotImplementedException();
        }

        [TestMethod]
        [TestCategory("Exception")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ExpectedNotInitializedExceptionTest()
        {
            // SET Parameter
            NotInitializedTestParameter param = new NotInitializedTestParameter();

            // TEST
            Console.WriteLine(param.Expected);
        }

        [TestMethod]
        [TestCategory("Exception")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ActualNotInitializedExceptionTest()
        {
            // SET Parameter
            NotInitializedTestParameter param = new NotInitializedTestParameter();

            // TEST
            Console.WriteLine(param.Actual);
        }


        // TestParameter for Validate Method
        public class ValidateMethodTestParameter<T> : TestParameter<T>
        {
            public T ExpectedValue;
            public T ActualValue;

            // SET Expected
            public override void Arrange(out T expected) => expected = this.ExpectedValue;

            // GET Actual
            public override void Act(out T actual) => actual = this.ActualValue;
        }


        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(null, null, DisplayName = "Expected, Actual = null")]
        public void ValidateIntPassTest(int expected, int actual) => ValidateIntTest(expected, actual, nameof(ValidateIntPassTest));

        [DataTestMethod]
        [DataRow(0, 1)]
        [DataRow(null, 1, DisplayName = "Expected = null")]
        [DataRow(1, null, DisplayName = "Actual = null")]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void ValidateIntFailTest(int expected, int actual) => ValidateIntTest(expected, actual, nameof(ValidateIntFailTest));

        public void ValidateIntTest(int expected, int actual, string keyword)
        {
            // SET Parameter
            ValidateMethodTestParameter<int> param = new ValidateMethodTestParameter<int>
            {
                Keyword = keyword,
                ExpectedValue = expected,
                ActualValue = actual,
            };

            // TEST
            param.Validate();
        }


        [DataTestMethod]
        [DataRow("ABC", "ABC")]
        [DataRow("", "", DisplayName = "Expected, Actual = String.Empty")]
        [DataRow(null, null, DisplayName = "Expected, Actual = null")]
        public void ValidateStringPassTest(string expected, string actual) => ValidateStringTest(expected, actual, nameof(ValidateStringPassTest));

        [DataTestMethod]
        [DataRow("ABC", "XYZ")]
        [DataRow(null, "XYZ", DisplayName = "Expected = null")]
        [DataRow("ABC", null, DisplayName = "Actual = null")]
        [DataRow("", "XYZ", DisplayName = "Expected = String.Empty")]
        [DataRow("ABC", "", DisplayName = "Actual = String.Empty")]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void ValidateStringFailTest(string expected, string actual) => ValidateStringTest(expected, actual, nameof(ValidateStringFailTest));

        public void ValidateStringTest(string expected, string actual, string keyword)
        {
            // SET Parameter
            ValidateMethodTestParameter<string> param = new ValidateMethodTestParameter<string>
            {
                Keyword = keyword,
                ExpectedValue = expected,
                ActualValue = actual,
            };

            // TEST
            param.Validate();
        }


        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 })]
        public void ValidateIntArrayPassTest(int[] expected, int[] actual) => ValidateIntArrayTest(expected, actual, nameof(ValidateIntArrayPassTest));

        [DataTestMethod]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 3 })]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void ValidateIntArrayFailTest(int[] expected, int[] actual) => ValidateIntArrayTest(expected, actual, nameof(ValidateIntArrayFailTest));

        public void ValidateIntArrayTest(int[] expected, int[] actual, string keyword)
        {
            // SET Parameter
            ValidateMethodTestParameter<int[]> param = new ValidateMethodTestParameter<int[]>
            {
                Keyword = keyword,
                ExpectedValue = expected,
                ActualValue = actual,
            };

            // TEST
            param.Validate();
        }


        [DataTestMethod]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XYZ" }, null)]
        [DataRow(new string[] { "ABC", null }, new string[] { "ABC", null }, null)]
        public void ValidateStringArrayPassTest(string[] expected, string[] actual, string _) => ValidateStringArrayTest(expected, actual, nameof(ValidateStringArrayPassTest));

        [DataTestMethod]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XY" }, null)]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", null }, null)]
        [DataRow(new string[] { "ABC", null }, new string[] { "ABC", "XYZ" }, null)]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void ValidateStringArrayFailTest(string[] expected, string[] actual, string _) => ValidateStringArrayTest(expected, actual, nameof(ValidateStringArrayFailTest));

        public void ValidateStringArrayTest(string[] expected, string[] actual, string keyword)
        {
            // SET Parameter
            ValidateMethodTestParameter<string[]> param = new ValidateMethodTestParameter<string[]>
            {
                Keyword = keyword,
                ExpectedValue = expected,
                ActualValue = actual,
            };

            // TEST
            param.Validate();
        }


        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4 }, "Number of <Actual> is greater than <Expected>.", null)]
        [DataRow(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3 }, "Number of <Actual> is less than <Expected>.", null)]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void ValidateArrayNumberMismatchTest(int[] expected, int[] actual, string message, string _)
        {
            // SET Parameter
            ValidateMethodTestParameter<int[]> param = new ValidateMethodTestParameter<int[]>
            {
                Keyword = nameof(ValidateArrayNumberMismatchTest),
                ExpectedValue = expected,
                ActualValue = actual,
            };

            try
            {
                // TEST
                param.Validate();
            }
            catch (AssertFailedException e)
            {
                // Print Message
                Console.WriteLine($"Message = \"{e.Message}\"");

                try
                {
                    // ASSERT Message
                    Assert.AreEqual(message, e.Message);
                }
                catch (AssertFailedException)
                {
                    throw new InternalTestFailureException();
                }
                catch (Exception)
                {
                    throw;
                }

                // Throw AssertFailedException
                throw e;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
