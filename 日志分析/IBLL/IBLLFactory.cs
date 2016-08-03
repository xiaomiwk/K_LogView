using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win.IBLL
{
    public class IBLLFactory
    {
        public static I文件分析 获取文本分析()
        {
            return new B文件分析();
        }
    }
}
