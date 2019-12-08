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

using System;

namespace BUILDLet.UnitTest.Utilities.Extensions
{
    /// <summary>
    /// 単体テストで使用するためのテスト パラメーターを表します。
    /// </summary>
    public static class TestParameterExtensions
    {
        /// <summary>
        /// 期待値 (<see cref="TestParameter.Expected"/>) と実際のテスト結果 (<see cref="TestParameter.Actual"/>) が等しいかどうかをテストします。
        /// また、コンソール (標準出力) に、それらの値を出力します。
        /// </summary>
        /// <param name="param">
        /// ターゲットの <see cref="TestParameter"/> クラス
        /// </param>
        /// <param name="noBlankLine">
        /// コンソール (標準出力) への出力に対して、先頭に改行をしない場合に true を指定します。
        /// 既定は false です。
        /// </param>
        /// <param name="printKeyword">
        /// キーワード (<see cref="TestParameter.Keyword"/>) を出力しない場合に false を指定します。
        /// 既定は true です。
        /// </param>
        public static void Assert(this TestParameter param, bool noBlankLine = false, bool printKeyword = true)
        {
            // Validation (Null Check):
            if (param is null) { throw new ArgumentNullException(nameof(param)); }

            // Output:
            param.Print(noBlankLine, printKeyword);

            // Assertion:
            if (param.Expected != null || param.Actual != null)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(param.Expected, param.Actual);
            }
        }
    }
}
