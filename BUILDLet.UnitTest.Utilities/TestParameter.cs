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
        /// ���ۂ̃e�X�g���� (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}"/>) �����������ǂ������e�X�g���܂��B
        /// �܂��A�R���\�[�� (�W���o��) �ɁA�����̒l���o�͂��܂��B
        /// </summary>
        /// <param name="noBlankLine">
        /// �R���\�[�� (�W���o��) �ւ̏o�͂ɑ΂��āA�擪�ɉ��s�����Ȃ��ꍇ�� true ���w�肵�܂��B
        /// ����� true �ł��B
        /// </param>
        /// <param name="printKeyword">
        /// �L�[���[�h (<see cref="BUILDLet.Standard.UnitTest.TestParameter{T}.Keyword"/>) ���o�͂��Ȃ��ꍇ�� false ���w�肵�܂��B
        /// ����� true �ł��B
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
