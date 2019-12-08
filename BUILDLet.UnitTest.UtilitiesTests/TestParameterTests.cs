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

using BUILDLet.UnitTest.Utilities.Extensions;

namespace BUILDLet.UnitTest.Utilities.Tests
{
    [TestClass()]
    public class TestParameterTests
    {
        public class MyTestParameter : TestParameter
        {
            public object Value;
            public override object Expected => this.Value;
        }


        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow("ABC", "ABC")]
        [DataRow(null, null, DisplayName = "Expected = null, Actual = null")]
        public void AssertTest(object expected, object actual)
        {
            MyTestParameter param = new MyTestParameter()
            {
                Keyword = nameof(AssertTest),
                Value = expected,
                Actual = actual
            };

            param.Assert();
        }


        [DataTestMethod]
        [DataRow(0, 1)]
        [DataRow("ABC", "XYZ")]
        [DataRow(null, 1, DisplayName = "Expected = null")]
        [DataRow(1, null, DisplayName = "Actual = null")]
        [TestCategory("Exception")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AssertFailedExceptionTest(object expected, object actual)
        {
            MyTestParameter param = new MyTestParameter()
            {
                Keyword = nameof(AssertFailedExceptionTest),
                Value = expected,
                Actual = actual
            };

            param.Assert();
        }
    }
}
