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
    /// 単体テストで使用するためのテスト パラメーターを実装するための抽象クラスです。
    /// </summary>
    public abstract class TestParameter
    {
        /// <summary>
        /// 期待値を取得します。
        /// </summary>
        /// <remarks>
        /// 継承先のクラスで、期待値となるメンバー変数を返すように実装してください。
        /// </remarks>
        public abstract object Expected { get; }


        /// <summary>
        /// 実際のテスト結果を設定または取得します。
        /// </summary>
        public object Actual { get; set; }


        /// <summary>
        /// 当該テスト ケースを表現するのに適当なキーワードを指定または取得します。
        /// </summary>
        public string Keyword { get; set; } = null;


        /// <summary>
        /// コンソール (標準出力) に、キーワード (<see cref="TestParameter.Keyword"/>)、
        /// 期待値 (<see cref="TestParameter.Expected"/>) および、実際のテスト結果 (<see cref="TestParameter.Actual"/>) を出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// 出力前に改行しない場合に true を指定します。
        /// 既定は false です。
        /// </param>
        /// <param name="printKeyword">
        /// キーワード (<see cref="TestParameter.Keyword"/>) を出力しない場合に false を指定します。
        /// 既定は true です。
        /// </param>
        public void Print(bool noBlankLine = false, bool printKeyword = true)
        {
            // Blank Line
            if (!noBlankLine)
            {
                Console.WriteLine();
            }

            // Keyword
            if (printKeyword && !string.IsNullOrWhiteSpace(this.Keyword))
            {
                Console.WriteLine($"[{this.Keyword}]");
            }

            // Expected
            Console.WriteLine("Expected\t= " + (this.Expected is null ? "null" : $"\"{this.Expected}\""));

            // Actual
            Console.WriteLine("Actual\t= " + (this.Actual is null ? "null" : $"\"{this.Actual}\""));
        }
    }
}
