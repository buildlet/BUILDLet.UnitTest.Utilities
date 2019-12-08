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

namespace BUILDLet.UnitTest.Utilities.Extensions
{
    /// <summary>
    /// �P�̃e�X�g�Ŏg�p���邽�߂̃e�X�g �p�����[�^�[��\���܂��B
    /// </summary>
    public static class TestParameterExtensions
    {
        /// <summary>
        /// ���Ғl (<see cref="TestParameter.Expected"/>) �Ǝ��ۂ̃e�X�g���� (<see cref="TestParameter.Actual"/>) �����������ǂ������e�X�g���܂��B
        /// �܂��A�R���\�[�� (�W���o��) �ɁA�����̒l���o�͂��܂��B
        /// </summary>
        /// <param name="param">
        /// �^�[�Q�b�g�� <see cref="TestParameter"/> �N���X
        /// </param>
        /// <param name="noBlankLine">
        /// �R���\�[�� (�W���o��) �ւ̏o�͂ɑ΂��āA�擪�ɉ��s�����Ȃ��ꍇ�� true ���w�肵�܂��B
        /// ����� false �ł��B
        /// </param>
        /// <param name="printKeyword">
        /// �L�[���[�h (<see cref="TestParameter.Keyword"/>) ���o�͂��Ȃ��ꍇ�� false ���w�肵�܂��B
        /// ����� true �ł��B
        /// </param>
        public static void Assert(this TestParameter param, bool noBlankLine = false, bool printKeyword = true)
        {
            // Validation (Null Check):
            if (param is null) { throw new ArgumentNullException(nameof(param)); }

            // Output:
            param.Print(noBlankLine, printKeyword);

            // Assertion:
            if (param.Expected != null || param.Actual != null)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(param.Expected, param.Actual);
            }
        }
    }
}
