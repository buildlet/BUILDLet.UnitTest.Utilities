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
        /// 実際のテスト結果 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) が等しいかどうかをテストします。<br/>
        /// また、それらをコンソール (標準出力) に出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// コンソール (標準出力) への出力に対して、先頭に改行をしない場合に <c>true</c> を指定します。
        /// </param>
        /// <param name="printKeyword">
        /// キーワード (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Keyword"/>) を出力しない場合に <c>false</c> を指定します。
        /// </param>
        /// <param name="count">
        /// 1 回のパラメーターの処理に対して、繰り返してテストを実行する場合に、<c>1</c> 以上の繰り返し回数を指定します。
        /// </param>
        /// <param name="startIndex">
        /// 1 回のパラメーターの処理に対して、繰り返してテストを実行する場合に、インデックスの開始番号を指定します。
        /// </param>
        /// <exception cref="NotImplementedException">
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Arrange(out T)" autoUpgrade="true"/> メソッド、あるいは
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Act(out T)" autoUpgrade="true"/> メソッドがオーバーライドされていない場合にスローされます。
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="count"/> に <c>0</c> 以下が指定された場合は
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Act(out T)"/> および <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Arrange(out T)"/>
        /// が実行されます。<br/>
        /// <paramref name="count"/> に <c>1</c> 以上が指定された場合は
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Act(out T, int)"/> および <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Arrange(out T, int)"/>
        /// が実行されます。
        /// </para>
        /// </remarks>
        public void Assert(bool noBlankLine = true, bool printKeyword = true, int count = 0, int startIndex = 0)
        {
            // Print Keyword
            if (printKeyword) { this.PrintKeyword(noBlankLine); }

            var index = startIndex;
            do
            {
                if (count < 1)
                {
                    // ARRANGE
                    this.Arrange(out var expected);

                    // SET Expected
                    this.Expected = expected;

                    // Print Expected
                    this.PrintItem("Expected", this.Expected);


                    // ACT
                    this.Act(out var actual);

                    // SET Actual
                    this.Actual = actual;

                    // Print Actual
                    this.PrintItem("Actual", this.Actual);
                }
                else
                {
                    // ARRANGE
                    this.Arrange(out var expected, index);

                    // SET Expected
                    this.Expected = expected;

                    // Print Expected
                    this.PrintItem("Expected", this.Expected, index);


                    // ACT
                    this.Act(out var actual, index);

                    // SET Actual
                    this.Actual = actual;

                    // Print Actual
                    this.PrintItem("Actual", this.Actual, index);
                }


                // ASSERT
                if (this.Expected != null || this.Actual != null)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(this.Expected, this.Actual);
                }


                // Increment counter
                index++;

            } while ((index > 0) && (index < count));
        }
    }
}
