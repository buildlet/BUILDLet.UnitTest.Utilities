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
    /// このクラスの使用方法の詳細は <see cref="Validate(bool, bool, bool, bool, bool)" autoUpgrade="true"/> メソッドを参照してください。
    /// </remarks>
    public abstract class TestParameter<T>
    {
        // Innver Value(s):
        private T expected;
        private T actual;
        private bool expected_is_initialized = false;
        private bool actual_is_initialized = false;

        // Message(s) for AssertFailedException
        private static string NumberOfActualLessThanExpectedExceptionMessage = $"Number of <{nameof(Actual)}> is less than <{nameof(Expected)}>.";
        private static string NumberOfActualGreaterThanExpectedExceptionMessage = $"Number of <{nameof(Actual)}> is greater than <{nameof(Expected)}>.";


        /// <summary>
        /// テスト ケースを表現するのに適当なキーワードを取得または設定します。
        /// </summary>
        public string Keyword { get; set; } = null;


        /// <summary>
        /// テストの期待値を取得または設定します。
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
            set
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
            set
            {
                // SET value
                this.actual = value;

                // SET Initialized Flag
                this.actual_is_initialized = true;
            }
        }


        /// <summary>
        /// テストの事前準備 (Arrange) で実行される処理を実装します。
        /// </summary>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <exception cref="NotImplementedException">
        /// このメソッドは、既定で <see cref="NotImplementedException"/> をスローします。
        /// </exception>
        /// <remarks>
        /// <para>
        /// <see cref="Validate(bool, bool, bool, bool, bool)" autoUpgrade="true"/> メソッドのパラメーター <c>noArrange</c> に <c>false</c> を指定して実行すると、
        /// <see cref="Validate(bool, bool, bool, bool, bool)" autoUpgrade="true"/> メソッドの実行中に <see cref="Arrange(out T)" autoUpgrade="true"/> メソッドが実行されます。
        /// </para>
        /// <note type="implement">
        /// 継承先のクラスでこのメソッドをオーバーライドして、<paramref name="expected"/> にテストの期待値を格納してください。<br/>
        /// その際、基底クラス (<see cref="TestParameter{T}" qualifyHint="true"/>) の <see cref="Arrange(out T)" autoUpgrade="true"/> メソッドはコールしないでください。
        /// (<see cref="NotImplementedException"/> がスローされます。)
        /// </note>
        /// <note type="note">
        /// <paramref name="expected"/> に格納した値は <see cref="Expected"/> から参照できます。
        /// </note>
        /// </remarks>
        public virtual void Arrange(out T expected) { throw new NotImplementedException(); }


        /// <summary>
        /// テストの実行 (Act) 処理を実装します。
        /// </summary>
        /// <param name="actual">
        /// 実際のテスト結果。
        /// </param>
        /// <exception cref="NotImplementedException">
        /// このメソッドは、既定で <see cref="NotImplementedException"/> をスローします。
        /// </exception>
        /// <remarks>
        /// <para>
        /// <see cref="Validate(bool, bool, bool, bool, bool)" autoUpgrade="true"/> メソッドのパラメーター <c>noAct</c> に <c>false</c> を指定して実行すると、
        /// <see cref="Validate(bool, bool, bool, bool, bool)" autoUpgrade="true"/> メソッドの実行中に <see cref="Act(out T)" autoUpgrade="true"/> メソッドが実行されます。
        /// </para>
        /// <note type="implement">
        /// 継承先のクラスでこのメソッドをオーバーライドして、<paramref name="actual"/> にテスト結果を格納してください。<br/>
        /// その際、基底クラス (<see cref="TestParameter{T}" qualifyHint="true"/>) の <see cref="Act(out T)" autoUpgrade="true"/> メソッドはコールしないでください。
        /// (<see cref="NotImplementedException"/> がスローされます。)
        /// </note>
        /// <note type="note">
        /// <paramref name="actual"/> に格納した値は <see cref="Actual"/> から参照できます。
        /// </note>
        /// </remarks>
        public virtual void Act(out T actual) { throw new NotImplementedException(); }


        /// <summary>
        /// テストの期待値 (<paramref name="expected"/>) に対する実際のテスト結果 (<paramref name="actual"/>) を検証する処理 (Assert) を実装します。
        /// </summary>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <param name="actual">
        /// 実際のテスト結果。
        /// </param>
        /// <remarks>
        /// <note type="implement">
        /// <para>
        /// 継承先のクラスでこのメソッドをオーバーライドして、通常は、テストの期待値 (<paramref name="expected"/>) と
        /// 実際のテスト結果 (<paramref name="actual"/>) が等しいかどうかを検証する処理を実装してください。
        /// </para>
        /// <para>
        /// 検証に失敗したときは、テストが失敗したことを示す例外をスローしてください。
        /// </para>
        /// </note>
        /// </remarks>
        public abstract void Assert(T expected, T actual);


        /// <summary>
        /// テストの期待値 (<paramref name="expected"/>) に対する実際のテスト結果 (<paramref name="actual"/>) を検証する処理 (Assert) を実装します。
        /// </summary>
        /// <typeparam name="TItem">
        /// <typeparamref name="T"/> がコレクション型の場合の各アイテムの型を指定します。
        /// </typeparam>
        /// <param name="expected">
        /// テストの期待値。(<see cref="Expected"/> がコレクション型の場合の各アイテム。)
        /// </param>
        /// <param name="actual">
        /// 実際のテスト結果。(<see cref="Actual"/> がコレクション型の場合の各アイテム。)
        /// </param>
        /// <remarks>
        /// <para>
        /// <typeparamref name="T"/> がコレクション型である場合に、
        /// <see cref="Expected"/> および <see cref="Actual"/> の各アイテムに対して行う検証処理を実装します。
        /// </para>
        /// <note type="implement">
        /// <para>
        /// 継承先のクラスでこのメソッドをオーバーライドして、通常は、テストの期待値 (<paramref name="expected"/>) と
        /// 実際のテスト結果 (<paramref name="actual"/>) が等しいかどうかを検証する処理を実装してください。<br/>
        /// </para>
        /// <para>
        /// 検証に失敗したときは、テストが失敗したことを示す例外をスローしてください。
        /// </para>
        /// </note>
        /// </remarks>
        public abstract void AssertForEach<TItem>(TItem expected, TItem actual);


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
        /// このテスト パラメーターに設定されたテストを実行して、テスト結果を検証します。<br/>
        /// また、それらをコンソール (標準出力) に出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// コンソール (標準出力) への出力に対して、先頭に改行をしない場合に <c>true</c> を指定します。
        /// </param>
        /// <param name="printKeyword">
        /// <para>
        /// キーワード (<see cref="Keyword"/>) を出力しない場合に <c>false</c> を指定します。<br/>
        /// </para>
        /// <note type="note">
        /// キーワード (<see cref="Keyword"/>) が、空文字 (<see cref="String.Empty"/>) または <c>null</c> の場合、<see cref="Keyword"/> は出力されません。
        /// </note>
        /// </param>
        /// <param name="noArrange">
        /// <para>
        /// テストの事前準備 (Arrange) として <see cref="Arrange(out T)" autoUpgrade="true"/> メソッドを実行しない場合に <c>true</c> を指定します。
        /// </para>
        /// <note type="note">
        /// 通常は、このパラメーターに <c>false</c> (既定) を指定して、
        /// <see cref="Arrange(out T)" autoUpgrade="true"/> メソッドを実行することによって <see cref="Expected"/> を設定してください。
        /// </note>
        /// </param>
        /// <param name="noAct">
        /// <para>
        /// テストの実行 (Act) 処理として <see cref="Act(out T)" autoUpgrade="true"/> メソッドを実行しない場合に <c>true</c> を指定します。
        /// </para>
        /// <note type="note">
        /// 通常は、このパラメーターに <c>false</c> (既定) を指定して、
        /// <see cref="Act(out T)" autoUpgrade="true"/> メソッドを実行することによって <see cref="Actual"/> を設定してください。
        /// </note>
        /// </param>
        /// <param name="notEnumerable">
        /// <para>
        /// <typeparamref name="T"/> がコレクション型である場合に <c>false</c> (既定) を指定すると、
        /// <see cref="Assert(T, T)" autoUpgrade="true"/> メソッドによる <see cref="Expected"/> と <see cref="Actual"/> に対する検証が実行されるのではなく、
        /// <see cref="AssertForEach{TItem}(TItem, TItem)" autoUpgrade="true"/> メソッドによる <see cref="Expected"/> および <see cref="Actual"/> それぞれの各アイテムに対する検証が実行されます。
        /// </para>
        /// <para>
        /// <c>true</c> が指定されると、コレクション型の <see cref="Expected"/> および <see cref="Actual"/> それぞれの各アイテムに対する検証は実行されないので、
        /// 独自に <see cref="Assert(T, T)" autoUpgrade="true"/> メソッドをオーバーライドしていない場合は、通常、<c>false</c> (既定) を指定してください。
        /// </para>
        /// </param>
        /// <remarks>
        /// <para>
        /// テストの期待値 (<see cref="Expected"/>) に対する実際のテスト結果 (<see cref="Actual"/>) を検証して、それらをコンソール (標準出力) に出力します。<br/>
        /// 通常、これらの値 (<see cref="Expected"/> および <see cref="Actual"/>) が等しいかどうかをテストします。
        /// </para>
        /// <para>
        /// <see cref="Validate(bool, bool, bool, bool, bool)" autoUpgrade="true"/> メソッドを実行すると、
        /// <see cref="Act(out T)" autoUpgrade="true"/> メソッド、<see cref="Arrange(out T)" autoUpgrade="true"/> メソッド、および
        /// <see cref="Assert(T, T)" autoUpgrade="true"/> メソッド (あるいは <see cref="AssertForEach{TItem}(TItem, TItem)" autoUpgrade="true"/> メソッド) が、
        /// これらの順番で実行されます。
        /// </para>
        /// <para>
        /// 簡易的に表現すると、以下のようなコードが実行されます。
        /// <code language="cs" numberLines="true">
        /// if (!noArrange)
        /// {
        ///     // ARRANGE
        ///     Arrange(out var expected);
        /// 
        ///     Expected = expected;
        /// }
        /// 
        /// if (!noAct)
        /// {
        ///     // ACT
        ///     Act(out var actual);
        /// 
        ///     Actual = actual;
        /// }
        /// 
        /// if (notEnumerable)
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
        ///         // ASSERT
        ///         AssertForEach(ee.Current, ae.Current);
        ///     }
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        public void Validate(bool noBlankLine = true, bool printKeyword = true, bool noArrange = false, bool noAct = false, bool notEnumerable = false)
        {
            try
            {
                // Update Single or Multiple Operation Flag
                notEnumerable = (notEnumerable || typeof(T) == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(typeof(T)));


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


                // GET & SET Expected
                if (!noArrange)
                {
                    // ARRANGE
                    this.Arrange(out var expected);

                    // SET Expected
                    this.Expected = expected;
                }

                // Print Expected
                if (notEnumerable)
                {
                    TestParameter<T>.PrintItem("Expected", this.Expected);
                }


                // GET & SET Actual
                if (!noAct)
                {
                    // ACT
                    this.Act(out var actual);

                    // SET Actual
                    this.Actual = actual;
                }

                // Print Actual
                if (notEnumerable)
                {
                    TestParameter<T>.PrintItem("Actual", this.Actual);
                }


                // ASSERT
                if (notEnumerable)
                {
                    if (this.Expected != null || this.Actual != null)
                    {
                        this.Assert((T)this.Expected, (T)this.Actual);
                    }
                }
                else
                {
                    this.AssertForEachItems(this.Expected as IEnumerable, this.Actual as IEnumerable);
                }
            }
            catch (Exception) { throw; }
        }


        private void AssertForEachItems(IEnumerable expected, IEnumerable actual)
        {
            try
            {
                var eExpected = expected.GetEnumerator();
                var eActual = actual.GetEnumerator();
                var index = 0;

                while (eExpected.MoveNext())
                {
                    // Print Expected
                    TestParameter<T>.PrintItem("Expected", eExpected.Current, index);

                    // Validation:
                    if (!eActual.MoveNext())
                    {
                        throw this.GetAssertFailedException(TestParameter<T>.NumberOfActualLessThanExpectedExceptionMessage);
                    }

                    // Print Actual
                    TestParameter<T>.PrintItem("Actual", eActual.Current, index);

                    // ASSERT
                    if (eExpected.Current != null || eActual.Current != null)
                    {
                        this.AssertForEach(eExpected.Current as object, eActual.Current as object);
                    }

                    // Increment Counter
                    index++;
                }

                // Validation:
                if (!eExpected.MoveNext() && eActual.MoveNext())
                {
                    // Print Actual
                    TestParameter<T>.PrintItem("Actual", eActual.Current, index);

                    throw this.GetAssertFailedException(TestParameter<T>.NumberOfActualGreaterThanExpectedExceptionMessage);
                }
            }
            catch (Exception) { throw; }
        }

        private static void PrintItem(string name, object value, int index = -1)
        {
            Console.WriteLine($"{name}" + (index < 0 ? "" : $" [{index}]") + "\t= " + (value == null ? "null" : (value is string ? $"\"{value}\"" : value.ToString())));
        }
    }
}
