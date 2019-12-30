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

namespace BUILDLet.UnitTest.Utilities
{
    /// <summary>
    /// .NET Core プラットフォームで単体テストを行う場合に使用するテスト パラメーターを実装するための抽象クラスです。
    /// </summary>
    /// <typeparam name="T">
    /// テストの期待値 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) および
    /// 実際のテスト結果 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) の型を指定します。
    /// </typeparam>
    public abstract class TestParameter<T> : BUILDLet.Standard.UnitTest.TestParameter<T>
    {
        /// <summary>
        /// テストの期待値 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) と
        /// 実際のテスト結果 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}"/>) が等しいかどうかをテストします。
        /// また、コンソール (標準出力) に、それらの値を出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// コンソール (標準出力) への出力に対して、先頭に改行をしない場合に true を指定します。
        /// 既定は true です。
        /// </param>
        /// <param name="printKeyword">
        /// キーワード (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Keyword"/>) を出力しない場合に false を指定します。
        /// 既定は true です。
        /// </param>
        public void Assert(bool noBlankLine = true, bool printKeyword = true)
        {
            if (!this.IsMultipleParameter)
            {
                // Output:
                this.Print(noBlankLine, printKeyword);

                // Assertion:
                if (this.Expected != null || this.Actual != null)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(this.Expected, this.Actual);
                }
            }
            else
            {
                for (int i = 0; i < this.GetExpectedAsArray().Length; i++)
                {
                    // Output:
                    if (i == 0)
                    {
                        this.Print(noBlankLine, printKeyword);
                    }
                    else
                    {
                        this.Print(noBlankLine:false, printKeyword:false, i);
                    }

                    // Assertion:
                    if (this.GetExpectedAsArray()[i] != null || this.GetActualAsArray()[i] != null)
                    {
                        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(this.GetExpectedAsArray()[i], this.GetActualAsArray()[i]);
                    }
                }
            }
        }
    }
}
