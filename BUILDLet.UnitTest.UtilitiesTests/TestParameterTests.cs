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
        public class IntTestParameter : TestParameter<int>
        {
            public int Value;
            public override int Expected => this.Value;
        }

        public class StringTestParameter : TestParameter<string>
        {
            public string Value;
            public override string Expected => this.Value;
        }

        public class IntArrayTestParameter : TestParameter<int[]>
        {
            public int[] Value;
            public override int[] Expected => this.Value;
        }

        public class StringArrayTestParameter : TestParameter<string[]>
        {
            public string[] Value;
            public override string[] Expected => this.Value;
        }



        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(null, null, DisplayName = "Expected = null, Actual = null")]
        public void IntAssertTest(int expected, int actual)
        {
            IntTestParameter param = new IntTestParameter
            {
                Keyword = nameof(IntAssertTest),
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow("ABC", "ABC")]
        [DataRow("", "", DisplayName = "Expected = Actual = String.Empty")]
        [DataRow(null, null, DisplayName = "Expected = Actual = null")]
        public void StringAssertTest(string expected, string actual)
        {
            StringTestParameter param = new StringTestParameter
            {
                Keyword = nameof(StringAssertTest),
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow(0, 1)]
        [DataRow(null, 1, DisplayName = "Expected = null")]
        [DataRow(1, null, DisplayName = "Actual = null")]
        [TestCategory("Exception Test")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IntAssertFailedExceptionTest(int expected, int actual)
        {
            IntTestParameter param = new IntTestParameter
            {
                Keyword = nameof(IntAssertFailedExceptionTest),
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow("ABC", "XYZ")]
        [DataRow(null, "XYZ", DisplayName = "Expected = null")]
        [DataRow("ABC", null, DisplayName = "Actual = null")]
        [DataRow("", "XYZ", DisplayName = "Expected = String.Empty")]
        [DataRow("ABC", "", DisplayName = "Actual = String.Empty")]
        [TestCategory("Exception Test")]
        [ExpectedException(typeof(AssertFailedException))]
        public void StringAssertFailedExceptionTest(string expected, string actual)
        {
            StringTestParameter param = new StringTestParameter
            {
                Keyword = nameof(StringAssertFailedExceptionTest),
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 }, "Print Keyword")]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 }, null)]
        public void IntArrayAssertTest(int[] expected, int[] actual, string keyword)
        {
            IntArrayTestParameter param = new IntArrayTestParameter
            {
                Keyword = keyword,
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XYZ" }, "Print Keyword")]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XYZ" }, null)]
        public void StringArrayAssertTest(string[] expected, string[] actual, string keyword)
        {
            StringArrayTestParameter param = new StringArrayTestParameter
            {
                Keyword = keyword,
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow(new int[] { 0, 1, 2 }, new int[] { 0, 1, 3 }, null)]
        [TestCategory("Exception Test")]
        [ExpectedException(typeof(AssertFailedException))]
        public void IntArrayAssertFailedExceptionTest(int[] expected, int[] actual, string keyword)
        {
            IntArrayTestParameter param = new IntArrayTestParameter
            {
                Keyword = keyword,
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }


        [DataTestMethod()]
        [DataRow(new string[] { "ABC", "XYZ" }, new string[] { "ABC", "XY" }, null)]
        [TestCategory("Exception Test")]
        [ExpectedException(typeof(AssertFailedException))]
        public void StringArrayAssertFailedExceptionTest(string[] expected, string[] actual, string keyword)
        {
            StringArrayTestParameter param = new StringArrayTestParameter
            {
                Keyword = keyword,
                Value = expected,
                Actual = actual,
            };

            param.Assert();
        }
    }
}
