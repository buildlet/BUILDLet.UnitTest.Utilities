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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BUILDLet.UnitTest.Utilities
{
    /// <summary>
    /// .NET Core プラットフォームで単体テストを行う場合に使用するテスト パラメーターを実装します。
    /// </summary>
    /// <typeparam name="T">
    /// テストの期待値 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) および
    /// 実際のテスト結果 (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) の型。
    /// </typeparam>
    public class TestParameter<T> : BUILDLet.Standard.UnitTest.TestParameter<T>
    {
        /// <summary>
        /// テストの期待値 (<paramref name="expected"/>) と実際のテスト結果 (<paramref name="actual"/>) が等しいかどうかを検証します。
        /// </summary>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <param name="actual">
        /// 実際のテスト結果。
        /// </param>
        /// <exception cref="AssertFailedException">
        /// テストに失敗しました。
        /// </exception>
        public override void Assert(T expected, T actual) => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual);


        /// <summary>
        /// テストの期待値 (<paramref name="expected"/>) と実際のテスト結果 (<paramref name="actual"/>) が等しいかどうかをテストします。
        /// </summary>
        /// <typeparam name="TItem">
        /// <typeparamref name="T"/> がコレクション型の場合の各アイテムの型を指定します。
        /// </typeparam>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <param name="actual">
        /// 実際のテスト結果。
        /// </param>
        /// <exception cref="AssertFailedException">
        /// テストに失敗しました。
        /// </exception>
        public override void AssertForEach<TItem>(TItem expected, TItem actual) => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual);


        /// <summary>
        /// テストに失敗したときにストーされる例外 (<see cref="AssertFailedException"/>) を取得します。
        /// </summary>
        /// <param name="message">
        /// エラーについて説明するメッセージ。
        /// </param>
        /// <returns>
        /// テストに失敗したときにストーされる例外。
        /// </returns>
        protected sealed override Exception GetAssertFailedException(string message) => new AssertFailedException(message);
    }
}
