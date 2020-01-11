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
    /// テストの期待値 (<see cref="TestParameter{T}.Expected"/>) および
    /// 実際のテスト結果 (<see cref="TestParameter{T}.Actual"/>) の型を指定します。
    /// </typeparam>
    public abstract class TestParameter<T>
    {
        // Innver Value(s):
        private T expected;
        private T actual;
        private bool expected_is_initialized = false;
        private bool actual_is_initialized = false;


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
        /// <see cref="Arrange(out T)" autoUpgrade="true"/> メソッドを実行すると、このプロパティに値が設定されます。
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
        /// <see cref="Act(out T)" autoUpgrade="true"/> メソッドを実行すると、このプロパティに値が設定されます。
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
        /// <paramref name="expected"/> にテストの期待値が格納されるように、継承先のクラスでこのメソッドをオーバーライドしてください。<br/>
        /// その際、基底クラスの <see cref="Arrange(out T)"/> メソッドはコールしないでください。
        /// (<see cref="NotImplementedException"/> がスローされます。)
        /// </para>
        /// <para>
        /// <paramref name="expected"/> に格納した値は <see cref="TestParameter{T}.Expected"/> から参照できます。
        /// </para>
        /// </remarks>
        public virtual void Arrange(out T expected) { throw new NotImplementedException(); }

        /// <summary>
        /// 繰り返しテストのためのテストの事前準備 (Arrange) で実行される処理を実装します。
        /// </summary>
        /// <param name="expected">
        /// テストの期待値。
        /// </param>
        /// <param name="index">
        /// 繰り返し処理のためのインデックス。
        /// </param>
        /// <exception cref="NotImplementedException">
        /// このメソッドは、既定で <see cref="NotImplementedException"/> をスローします。
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="expected"/> にテストの期待値が格納されるように、継承先のクラスでこのメソッドをオーバーライドしてください。<br/>
        /// その際、基底クラスの <see cref="Arrange(out T)"/> メソッドはコールしないでください。
        /// (<see cref="NotImplementedException"/> がスローされます。)
        /// </para>
        /// <para>
        /// <paramref name="expected"/> に格納した値は <see cref="TestParameter{T}.Expected"/> から参照できます。
        /// </para>
        /// <para>
        /// 繰り返してテストを実行する場合は、<see cref="Arrange(out T)"/> ではなく、このメソッドが実行されます。
        /// </para>
        /// </remarks>
        public virtual void Arrange(out T expected, int index) { throw new NotImplementedException(); }


        /// <summary>
        /// テストの実行 (Act) で実行される処理を実装します。
        /// </summary>
        /// <param name="actual">
        /// テスト結果。
        /// </param>
        /// <exception cref="NotImplementedException">
        /// このメソッドは、既定で <see cref="NotImplementedException"/> をスローします。
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="actual"/> にテストの期待値が格納されるように、継承先のクラスでこのメソッドをオーバーライドしてください。<br/>
        /// その際、基底クラスの <see cref="Arrange(out T)"/> メソッドはコールしないでください。
        /// (<see cref="NotImplementedException"/> がスローされます。)
        /// </para>
        /// <para>
        /// <paramref name="actual"/> に格納した値は <see cref="TestParameter{T}.Actual"/> から参照できます。
        /// </para>
        /// </remarks>
        public virtual void Act(out T actual) { throw new NotImplementedException(); }

        /// <summary>
        /// 繰り返しテストのためのテストの実行 (Act) で実行される処理を実装します。
        /// </summary>
        /// <param name="actual">
        /// テスト結果。
        /// </param>
        /// <param name="index">
        /// 繰り返し処理のためのインデックス。
        /// </param>
        /// <exception cref="NotImplementedException">
        /// このメソッドは、既定で <see cref="NotImplementedException"/> をスローします。
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="actual"/> にテストの期待値が格納されるように、継承先のクラスでこのメソッドをオーバーライドしてください。<br/>
        /// その際、基底クラスの <see cref="Arrange(out T)"/> メソッドはコールしないでください。
        /// (<see cref="NotImplementedException"/> がスローされます。)
        /// </para>
        /// <para>
        /// <paramref name="actual"/> に格納した値は <see cref="TestParameter{T}.Actual"/> から参照できます。
        /// </para>
        /// <para>
        /// 繰り返してテストを実行する場合は、<see cref="Act(out T)"/> ではなく、このメソッドが実行されます。
        /// </para>
        /// </remarks>
        public virtual void Act(out T actual, int index) { throw new NotImplementedException(); }


        /// <summary>
        /// コンソール (標準出力) に、キーワード (<see cref="TestParameter{T}.Keyword"/>) を出力します。
        /// </summary>
        /// <param name="noBlankLine">
        /// 出力前に改行しない場合に <c>true</c> を指定します。
        /// </param>
        /// <remarks>
        /// キーワード (<see cref="TestParameter{T}.Keyword"/>) が、空文字または <c>null</c> の場合は出力されません。
        /// </remarks>
        protected void PrintKeyword(bool noBlankLine = true)
        {
            // Blank Line
            if (!noBlankLine)
            {
                Console.WriteLine();
            }

            // Keyword
            if (!string.IsNullOrWhiteSpace(this.Keyword))
            {
                Console.WriteLine($"[{this.Keyword}]");
            }
        }


        /// <summary>
        /// コンソール (標準出力) に、アイテムの名前と値を出力します。
        /// </summary>
        /// <param name="name">
        /// アイテムの名前。
        /// </param>
        /// <param name="value">
        /// アイテムの値。
        /// </param>
        /// <param name="index">
        /// 複数回実行する場合のインデックス。<br/>
        /// <c>0</c> より小さい値が指定されると、インデックスは出力されません。
        /// </param>
        /// <remarks>
        /// 出力の書式は
        /// <c><paramref name="name"/> = <paramref name="value"/></c>
        /// または
        /// <c><paramref name="name"/> (<paramref name="index"/>) = <paramref name="value"/></c>
        /// です。
        /// </remarks>
        protected void PrintItem(string name, T value, int index = -1)
        {
            Console.WriteLine($"{name}" + (index < 0 ? "" : $" ({index})") + "\t= "
                + (value == null ? "null" : (value is string ? $"\"{value}\"" : value.ToString())));
        }
    }
}
