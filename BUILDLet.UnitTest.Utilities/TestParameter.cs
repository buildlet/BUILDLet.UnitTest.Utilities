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
    /// .NET Core �v���b�g�t�H�[���ŒP�̃e�X�g���s���ꍇ�Ɏg�p����e�X�g �p�����[�^�[���������܂��B
    /// </summary>
    /// <typeparam name="T">
    /// �e�X�g�̊��Ғl (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) �����
    /// ���ۂ̃e�X�g���� (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) �̌^�B
    /// </typeparam>
    public class TestParameter<T> : BUILDLet.Standard.UnitTest.TestParameter<T>
    {
        /// <summary>
        /// �e�X�g�̊��Ғl (<paramref name="expected"/>) �Ǝ��ۂ̃e�X�g���� (<paramref name="actual"/>) �����������ǂ��������؂��܂��B
        /// </summary>
        /// <param name="expected">
        /// �e�X�g�̊��Ғl�B
        /// </param>
        /// <param name="actual">
        /// ���ۂ̃e�X�g���ʁB
        /// </param>
        /// <exception cref="AssertFailedException">
        /// �e�X�g�Ɏ��s���܂����B
        /// </exception>
        public override void Assert(T expected, T actual) => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual);


        /// <summary>
        /// �e�X�g�̊��Ғl (<paramref name="expected"/>) �Ǝ��ۂ̃e�X�g���� (<paramref name="actual"/>) �����������ǂ������e�X�g���܂��B
        /// </summary>
        /// <typeparam name="TItem">
        /// <typeparamref name="T"/> ���R���N�V�����^�̏ꍇ�̊e�A�C�e���̌^���w�肵�܂��B
        /// </typeparam>
        /// <param name="expected">
        /// �e�X�g�̊��Ғl�B
        /// </param>
        /// <param name="actual">
        /// ���ۂ̃e�X�g���ʁB
        /// </param>
        /// <exception cref="AssertFailedException">
        /// �e�X�g�Ɏ��s���܂����B
        /// </exception>
        public override void AssertForEach<TItem>(TItem expected, TItem actual) => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, actual);


        /// <summary>
        /// �e�X�g�Ɏ��s�����Ƃ��ɃX�g�[������O (<see cref="AssertFailedException"/>) ���擾���܂��B
        /// </summary>
        /// <param name="message">
        /// �G���[�ɂ��Đ������郁�b�Z�[�W�B
        /// </param>
        /// <returns>
        /// �e�X�g�Ɏ��s�����Ƃ��ɃX�g�[������O�B
        /// </returns>
        protected sealed override Exception GetAssertFailedException(string message) => new AssertFailedException(message);
    }
}
