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
using System.Collections; // for IEnumerator

namespace BUILDLet.Standard.UnitTest
{
    /// <summary>
    /// 単体テストで使用するテスト パラメーターを実装するための抽象クラスです。
    /// </summary>
    /// <typeparam name="T">
    /// テストの期待値 (<see cref="Expected"/>) および 実際のテスト結果 (<see cref="Actual"/>) の型を指定します。
    /// </typeparam>
    /// <remarks>
    /// このクラスの使用方法の詳細は <see cref="Validate(bool, bool, bool)" autoUpgrade="true"/> メソッドを参照してください。
    /// </remarks>
    public abstract class TestParameter<T>
    {
        // ----------------------------------------------------------------------------------------------------
        // Private Field(s)
        // ----------------------------------------------------------------------------------------------------

        private T expected;
        private T actual;
        private bool expected_is_initialized = false;
        private bool actual_is_initialized = false;

        // Message(s) for AssertFailedException
        private static readonly string NumberOfActualLessThanExpectedExceptionMessage = $"Number of <{nameof(Actual)}> is less than <{nameof(Expected)}>.";
        private static readonly string NumberOfActualGreaterThanExpectedExceptionMessage = $"Number of <{nameof(Actual)}> is greater than <{nameof(Expected)}>.";


        // ----------------------------------------------------------------------------------------------------
        // Public, Protected Properties
        // ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// テスト ケースを表現するのに適当なキーワードを取得または設定します。
        /// </summary>
        public string Keyword { get; set; } = null;


        /// <summary>
        /// テストの期待値を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Expected"/> が初期化されていません。
        /// </exception>
        /// <remarks>
        /// <see cref="Arrange(out T)" autoUpgrade="true"/> メソッドで <c>expected</c> に格納された値が、このプロパティに設定されます。
        /// </remarks>
        public T Expected
        {
            get
            {
                // Validation:
                if (!this.expected_is_initialized) { throw new InvalidOperationException(); }

                // RETURN
                return this.expected;
            }
            protected set
            {
                // SET value
                this.expected = value;

                // SET Initialized Flag
                this.expected_is_initialized = true;
            }
        }


        /// <summary>
        /// 実際のテスト結果を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Actual"/> が初期化されていません。
        /// </exception>
        /// <remarks>
        /// <see cref="Act(out T)" autoUpgrade="true"/> メソッドで <c>actual</c> に格納された値が、このプロパティに設定されます。
        /// </remarks>
        public T Actual
        {
            get
            {
                // Validation:
                if (!this.actual_is_initialized) { throw new InvalidOperationException(); }

                // RETURN
                return this.actual;
            }
            protected set
            {
                // SET value
                this.actual = value;

                // SET Initialized Flag
                this.actual_is_initialized = true;
            }
        }


        // ----------------------------------------------------------------------------------------------------
        // Public, Protected Method(s)
        // ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// テストの事前準備 (Arrange) で実行される処理を実装します。
        /// </summary>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <remarks>
        /// <paramref name="expected"/> に格納した値は <see cref="Expected"/> から参照できます。
        /// <note type="implement">
        /// 継承先のクラスでこのメソッドをオーバーライドして、<paramref name="expected"/> にテストの期待値を格納してください。
        /// </note>
        /// </remarks>
        public abstract void Arrange(out T expected);


        /// <summary>
        /// テストの実行 (Act) 処理を実装します。
        /// </summary>
        /// <param name="actual">
        /// 実際のテスト結果。
        /// </param>
        /// <remarks>
        /// <paramref name="actual"/> に格納した値は <see cref="Actual"/> から参照できます。
        /// <para>
        /// <note type="implement">
        /// 継承先のクラスでこのメソッドをオーバーライドして、<paramref name="actual"/> にテスト結果を格納してください。
        /// </note>
        /// </para>
        /// </remarks>
        public abstract void Act(out T actual);


        /// <summary>
        /// テストの期待値 (<see cref="Expected"/>) に対して、実際のテスト結果 (<see cref="Actual"/>) を検証する処理 (Assert) を実装します。
        /// </summary>
        /// <typeparam name="TItem">
        /// 検証する値の型。
        /// 通常は <typeparamref name="T"/> と同じ型を指定します。
        /// </typeparam>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <param name="actual">
        /// 実際のテスト結果。
        /// </param>
        /// <remarks>
        /// <note type="implement">
        /// 継承先のクラスでこのメソッドをオーバーライドして、通常は、テストの期待値 (<see cref="Expected"/>) と
        /// 実際のテスト結果 (<see cref="Actual"/>) が等しいかどうかを検証する処理を実装してください。
        /// </note>
        /// <note type="implement">
        /// 検証に失敗したときは、テストが失敗したことを示す例外をスローしてください。
        /// </note>
        /// </remarks>
        public abstract void Assert<TItem>(TItem expected, TItem actual);


        /// <summary>
        /// テストに失敗したときにストーされる例外を取得します。
        /// </summary>
        /// <param name="message">
        /// エラーについて説明するメッセージ。
        /// </param>
        /// <returns>
        /// テストに失敗したときにストーされる例外。
        /// </returns>
        protected abstract Exception GetAssertFailedException(string message);


        /// <summary>
        /// このテスト パラメーターに設定されたテストを実行して、テストの検証結果をコンソール (標準出力) に出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// コンソール (標準出力) への出力に対して、先頭に改行をしない場合に <c>true</c> を指定します。
        /// </param>
        /// <param name="printKeyword">
        /// キーワード (<see cref="Keyword"/>) を出力しない場合に <c>false</c> を指定します。<br/>
        /// <note type="note">
        /// キーワード (<see cref="Keyword"/>) が、空文字 (<see cref="String.Empty"/>) または <c>null</c> の場合、<see cref="Keyword"/> は出力されません。
        /// </note>
        /// </param>
        /// <param name="autoEnumerable">
        /// <typeparamref name="T"/> がコレクション型である場合に <c>true</c> (既定) を指定すると、<see cref="Expected"/> と <see cref="Actual"/> それ自体ではなく、
        /// <see cref="Expected"/> と <see cref="Actual"/> それぞれのコレクションの各アイテムに対して <see cref="Assert{TItem}(TItem, TItem)"/> メソッドによる検証が行われます。
        /// </param>
        /// <remarks>
        /// <para>
        /// テストの期待値 (<see cref="Expected"/>) に対する実際のテスト結果 (<see cref="Actual"/>) を検証して、それらをコンソール (標準出力) に出力します。<br/>
        /// 通常、これらの値 (<see cref="Expected"/> および <see cref="Actual"/>) が等しいかどうかをテストします。
        /// </para>
        /// <para>
        /// <see cref="Validate(bool, bool, bool)" autoUpgrade="true"/> メソッドを実行すると、
        /// <see cref="Act(out T)" autoUpgrade="true"/> メソッド、<see cref="Arrange(out T)" autoUpgrade="true"/> メソッド、および
        /// <see cref="Assert{TItem}(TItem, TItem)" autoUpgrade="true"/> メソッドが、これらの順番で実行されます。
        /// </para>
        /// <para>
        /// 簡易的に表現すると、以下のようなコードが実行されます。
        /// <code language="cs" numberLines="true">
        /// // ARRANGE
        /// Arrange(out var expected);
        /// Expected = expected;
        /// 
        /// // ACT
        /// Act(out var actual);
        /// Actual = actual;
        /// 
        /// if (!autoEnumerable)
        /// {
        ///     // ASSERT
        ///     Assert(Expected, Actual);
        /// }
        /// else
        /// {
        ///     while (eExpected.MoveNext())
        ///     {
        ///         var ee = expected.GetEnumerator();
        ///         var ae = actual.GetEnumerator();
        ///     
        ///         // ASSERT for each item
        ///         assertForEach(ee.Current, ae.Current);
        ///     }
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        public void Validate(bool noBlankLine = true, bool printKeyword = true, bool autoEnumerable = true)
        {
            // Update Single or Multiple Operation Flag
            var tartget_is_enum = (autoEnumerable && typeof(T) != typeof(string) && typeof(IEnumerable).IsAssignableFrom(typeof(T)));


            // Blank Line
            if (!noBlankLine)
            {
                Console.WriteLine();
            }

            // Print Keyword
            if (printKeyword && !string.IsNullOrWhiteSpace(this.Keyword))
            {
                Console.WriteLine($"[{this.Keyword}]");
            }


            // ARRANGE: GET Expected
            this.Arrange(out var expected);

            // SET Expected
            this.Expected = expected;

            // Print Expected
            if (!tartget_is_enum)
            {
                TestParameter<T>.PrintItem("Expected", this.Expected);
            }


            // ACT: GET Actual
            this.Act(out var actual);

            // SET Actual
            this.Actual = actual;

            // Print Actual
            if (!tartget_is_enum)
            {
                TestParameter<T>.PrintItem("Actual", this.Actual);
            }


            // ASSERT
            if (!tartget_is_enum)
            {
                if (this.Expected != null || this.Actual != null)
                {
                    this.Assert(this.Expected, this.Actual);
                }
            }
            else
            {
                this.AssertForEachItem(this.Expected as IEnumerable, this.Actual as IEnumerable);
            }
        }


        // ----------------------------------------------------------------------------------------------------
        // Private Method(s)
        // ----------------------------------------------------------------------------------------------------

        private void AssertForEachItem(IEnumerable expected, IEnumerable actual)
        {
            var eExpected = expected.GetEnumerator();
            var eActual = actual.GetEnumerator();
            var nextExpected = true;
            var nextActual = true;
            var index = 0;

            while (nextExpected && nextActual)
            {
                // Move Next
                nextExpected = eExpected.MoveNext();
                nextActual = eActual.MoveNext();

                // Validation:
                if (nextExpected & !nextActual)
                {
                    TestParameter<T>.PrintItem("Expected", eExpected.Current, index);
                    Console.WriteLine($"Actual [{index}]\t= (None)");

                    throw this.GetAssertFailedException(TestParameter<T>.NumberOfActualLessThanExpectedExceptionMessage);
                }
                else if (!nextExpected & nextActual)
                {
                    Console.WriteLine($"Expected [{index}]\t= (None)");
                    TestParameter<T>.PrintItem("Actual", eActual.Current, index);

                    throw this.GetAssertFailedException(TestParameter<T>.NumberOfActualGreaterThanExpectedExceptionMessage);
                }
                else if (nextExpected & nextActual)
                {
                    // Print Expected & Actual
                    TestParameter<T>.PrintItem("Expected", eExpected.Current, index);
                    TestParameter<T>.PrintItem("Actual", eActual.Current, index);

                    // ASSERT
                    if (eExpected.Current != null || eActual.Current != null)
                    {
                        this.Assert(eExpected.Current, eActual.Current);
                    }

                    // Increment Counter
                    index++;
                }
            }
        }

        private static void PrintItem(string name, object value, int index = -1)
        {
            Console.WriteLine($"{name}" + (index < 0 ? "" : $" [{index}]") + "\t= " + (value == null ? "(null)" : (value is string ? $"\"{value}\"" : value.ToString())));
        }
    }
}
