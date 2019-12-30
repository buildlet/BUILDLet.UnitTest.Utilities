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

namespace BUILDLet.Standard.UnitTest
{
    /// <summary>
    /// 単体テストで使用するテスト パラメーターを実装するための抽象クラスです。
    /// </summary>
    /// <typeparam name="T">
    /// テストの期待値 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) および
    /// 実際のテスト結果 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) の型を指定します。
    /// </typeparam>
    public abstract class TestParameter<T>
    {
        // Innver Value(s):
        private object[] expectedArray = null;
        private object[] actualArray = null;


        /// <summary>
        /// <see cref="TestParameter{T}.Expected"/> および <see cref="TestParameter{T}.Actual"/> が配列の場合に true が設定されます。
        /// </summary>
        public bool IsMultipleParameter
        {
            get
            {
                if (typeof(T).IsArray)
                {
                    if (this.Expected == null || this.Actual == null)
                    {
                        throw new InvalidOperationException();
                    }
                    
                    if ((this.Expected as Array).Length != (this.Actual as Array).Length)
                    {
                        throw new InvalidOperationException();
                    }


                    if (this.expectedArray is null)
                    {
                        this.expectedArray = new object[(this.Expected as Array).Length];
                        (this.Expected as Array).CopyTo(this.expectedArray, 0);
                    }

                    if (this.actualArray is null)
                    {
                        this.actualArray = new object[(this.Actual as Array).Length];
                        (this.Actual as Array).CopyTo(this.actualArray, 0);
                    }


                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 期待値を取得します。
        /// </summary>
        /// <remarks>
        /// 継承先のクラスで、期待値となるメンバー変数を返すように実装してください。
        /// </remarks>
        public abstract T Expected { get; }


        /// <summary>
        /// 実際のテスト結果を設定または取得します。
        /// </summary>
        public T Actual { get; set; }


        /// <summary>
        /// 当該テスト ケースを表現するのに適当なキーワードを指定または取得します。
        /// </summary>
        public string Keyword { get; set; } = null;


        /// <summary>
        /// <see cref="TestParameter{T}.Expected"/> が配列の場合
        /// (<see cref="TestParameter{T}.IsMultipleParameter"/> が true の場合) に、
        /// <see cref="TestParameter{T}.Expected"/> を <see cref="object"/> の配列として取得します。
        /// </summary>
        /// <returns>
        /// <see cref="TestParameter{T}.Expected"/> を <see cref="object"/> の配列として取得します。
        /// </returns>
        protected object[] GetExpectedAsArray() => this.expectedArray;


        /// <summary>
        /// <see cref="TestParameter{T}.Actual"/> が配列の場合
        /// (<see cref="TestParameter{T}.IsMultipleParameter"/> が true の場合) に、
        /// <see cref="TestParameter{T}.Actual"/> を <see cref="object"/> の配列として取得します。
        /// </summary>
        /// <returns>
        /// <see cref="TestParameter{T}.Actual"/> を <see cref="object"/> の配列として取得します。
        /// </returns>
        protected object[] GetActualAsArray() => this.actualArray;


        /// <summary>
        /// コンソール (標準出力) に、キーワード (<see cref="TestParameter{T}.Keyword"/>)、
        /// 期待値 (<see cref="TestParameter{T}.Expected"/>) および、実際のテスト結果 (<see cref="TestParameter{T}.Actual"/>) を出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// 出力前に改行しない場合に true を指定します。
        /// 既定は true です。
        /// </param>
        /// <param name="printKeyword">
        /// キーワード (<see cref="TestParameter{T}.Keyword"/>) を出力しない場合に false を指定します。
        /// 既定は true です。
        /// </param>
        /// <param name="count">
        /// 期待値 (<see cref="TestParameter{T}.Expected"/>) および、実際のテスト結果 (<see cref="TestParameter{T}.Actual"/>) が配列の場合
        /// (<see cref="TestParameter{T}.IsMultipleParameter"/> が true の場合) に、出力するの配列のインデックス番号を指定します。
        /// 配列でない場合は無視されます。
        /// </param>
        public void Print(bool noBlankLine = true, bool printKeyword = true, int count = 0)
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

            // for Multi-Parameter
            if (!this.IsMultipleParameter)
            {
                // Expected
                Console.WriteLine("Expected\t= " + (this.Expected == null ? "null" : $"\"{this.Expected}\""));

                // Actual
                Console.WriteLine("Actual\t= " + (this.Actual == null ? "null" : $"\"{this.Actual}\""));
            }
            else
            {
                // Expected
                Console.WriteLine($"Expected({count}) = " + (this.expectedArray[count] == null ? "null" : $"\"{expectedArray[count]}\""));

                // Actual
                Console.WriteLine($"Actual({count}) = " + (this.actualArray[count] == null ? "null" : $"\"{actualArray[count]}\""));
            }
        }
    }
}
