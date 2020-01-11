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
    /// .NET Core �v���b�g�t�H�[���ŒP�̃e�X�g���s���ꍇ�Ɏg�p����e�X�g �p�����[�^�[���������邽�߂̒��ۃN���X�ł��B
    /// </summary>
    /// <typeparam name="T">
    /// �e�X�g�̊��Ғl (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) �����
    /// ���ۂ̃e�X�g���� (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) �̌^���w�肵�܂��B
    /// </typeparam>
    public abstract class TestParameter<T> : BUILDLet.Standard.UnitTest.TestParameter<T>
    {
        /// <summary>
        /// �e�X�g�̊��Ғl (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Expected"/>) ��
        /// ���ۂ̃e�X�g���� (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Actual"/>) �����������ǂ������e�X�g���܂��B<br/>
        /// �܂��A�������R���\�[�� (�W���o��) �ɏo�͂��܂��B
        /// </summary>
        /// <param name="noBlankLine">
        /// �R���\�[�� (�W���o��) �ւ̏o�͂ɑ΂��āA�擪�ɉ��s�����Ȃ��ꍇ�� <c>true</c> ���w�肵�܂��B
        /// </param>
        /// <param name="printKeyword">
        /// �L�[���[�h (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Keyword"/>) ���o�͂��Ȃ��ꍇ�� <c>false</c> ���w�肵�܂��B
        /// </param>
        /// <param name="count">
        /// 1 ��̃p�����[�^�[�̏����ɑ΂��āA�J��Ԃ��ăe�X�g�����s����ꍇ�ɁA<c>1</c> �ȏ�̌J��Ԃ��񐔂��w�肵�܂��B
        /// </param>
        /// <param name="startIndex">
        /// 1 ��̃p�����[�^�[�̏����ɑ΂��āA�J��Ԃ��ăe�X�g�����s����ꍇ�ɁA�C���f�b�N�X�̊J�n�ԍ����w�肵�܂��B
        /// </param>
        /// <exception cref="NotImplementedException">
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Arrange(out T)" autoUpgrade="true"/> ���\�b�h�A���邢��
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Act(out T)" autoUpgrade="true"/> ���\�b�h���I�[�o�[���C�h����Ă��Ȃ��ꍇ�ɃX���[����܂��B
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="count"/> �� <c>0</c> �ȉ����w�肳�ꂽ�ꍇ��
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Act(out T)"/> ����� <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Arrange(out T)"/>
        /// �����s����܂��B<br/>
        /// <paramref name="count"/> �� <c>1</c> �ȏオ�w�肳�ꂽ�ꍇ��
        /// <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Act(out T, int)"/> ����� <see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Arrange(out T, int)"/>
        /// �����s����܂��B
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
