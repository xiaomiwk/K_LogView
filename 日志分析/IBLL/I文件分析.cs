using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Win.Model;

namespace Win.IBLL
{
    public interface I文件分析
    {
        void 加载日志(List<FileInfo> 日志路径列表);

        List<M详细信息> 查询详细(int 启动次数);

        M概要信息 初步分析();
    }
}
