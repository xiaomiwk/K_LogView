using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Win.Model;

namespace Win.IBLL
{

    public class B文件分析 : I文件分析
    {
        private I文件分析 _当前分析;

        public void 加载日志(List<FileInfo> __日志路径列表)
        {
            var __日志路径 = __日志路径列表[0];
            选择版本(__日志路径);
            _当前分析.加载日志(__日志路径列表);
        }

        public M概要信息 初步分析()
        {
            return _当前分析.初步分析();
        }

        public List<M详细信息> 查询详细(int __启动次数)
        {
            return _当前分析.查询详细(__启动次数);
        }

        private void 选择版本(FileInfo __日志路径)
        {
            if (__日志路径.FullName.EndsWith(".log1") || __日志路径.FullName.Contains(".log1."))
            {
                _当前分析 = new B文件分析_log1();
                return;
            }
            if (__日志路径.FullName.Contains(".log"))
            {
                _当前分析 = new B文件分析_log();
                return;

            }
            throw new NotImplementedException("不支持该文件格式");
        }
    }
}
