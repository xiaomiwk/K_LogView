using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Win.Model;

namespace Win.IBLL
{

    public class B文件分析_log : I文件分析
    {
        private readonly List<M详细信息> _缓存 = new List<M详细信息>();

        private readonly List<int> _启动索引 = new List<int>();

        private const string _字段分隔符 = "||";

        public void 加载日志(List<FileInfo> __路径列表)
        {
            __路径列表.Sort((m, n) =>
            {
                if (m.Name.Split('.').Length == 2)
                {
                    return 1;
                }
                if (n.Name.Split('.').Length == 2)
                {
                    return -1;
                }
                try
                {
                    var __m后缀 = m.Name.Substring(m.Name.LastIndexOf('.') + 1);
                    var __n后缀 = n.Name.Substring(n.Name.LastIndexOf('.') + 1);
                    return int.Parse(__n后缀).CompareTo(int.Parse(__m后缀));
                }
                catch (Exception)
                {
                    return m.Name.CompareTo(n.Name);
                }
            });
            var __日志路径列表 = __路径列表.Select(q => q.FullName).ToList();
            _缓存.Clear();
            _启动索引.Clear();
            var __所有行 = new List<string>();
            __日志路径列表.ForEach(q =>
            {
                var __备份路径 = q + DateTime.Now.ToString("mmssfff");
                File.Copy(q, __备份路径, true);
                __所有行.AddRange(File.ReadAllLines(__备份路径, Encoding.Default));
                File.Delete(__备份路径);
            });
            foreach (var __单行 in __所有行)
            {
                var __M调试信息 = 解析调试信息(__单行);
                if (__M调试信息 == null)
                {
                    continue;
                }

                if (__M调试信息.跟踪标记 > 0)
                {
                    //调整内容
                    int __分割位置1 = __M调试信息.内容.IndexOf('[');
                    int __分割位置2 = __M调试信息.内容.IndexOf(']');
                    __M调试信息.标题 = __M调试信息.内容.Substring(__分割位置1 + 1, __分割位置2 - __分割位置1 - 1);
                    //__M调试信息.内容 = __M调试信息.内容.Substring(0, __分割位置1);
                    __M调试信息.内容 = __M调试信息.内容.Remove(__分割位置1, __分割位置2 - __分割位置1 + 1);
                }
                if (__M调试信息.跟踪标记 < 0)
                {
                    //调整内容
                    int __分割位置1 = __M调试信息.内容.LastIndexOf(']');
                    int __分割位置2 = __M调试信息.内容.LastIndexOf('[');
                    float __耗时;
                    int __耗时位置 = __分割位置2 + 5;
                    float.TryParse(__M调试信息.内容.Substring(__耗时位置, __分割位置1 - __耗时位置 - 1), out __耗时);
                    __M调试信息.耗时 = __耗时;
                    __M调试信息.标题 = string.Empty;
                    //__M调试信息.内容 = __M调试信息.内容.Substring(__分割位置2);
                    __M调试信息.跟踪周期 = E跟踪周期.无;
                }

                __M调试信息.Id = _缓存.Count;
                _缓存.Add(__M调试信息);

            }
            //_缓存.Reverse();
        }

        public M概要信息 初步分析()
        {
            if (_缓存.Count == 0)
            {
                throw new ArgumentException("该文件中没有日志");
            }
            var __M概要信息 = new M概要信息();
            var __当前索引 = 0;
            //file rolling
            _缓存[0].跟踪周期 = E跟踪周期.应用程序;
            _缓存[0].跟踪标记 = 1;
            _缓存.ForEach(q =>
            {
                if (q.跟踪周期 == E跟踪周期.应用程序 && q.跟踪标记 > 0)
                {
                    __M概要信息.启动次数++;
                    __M概要信息.启动列表.Add(q);
                    _启动索引.Add(__当前索引);
                }
                if (q.等级 <= TraceEventType.Warning)
                {
                    __M概要信息.警告列表.Add(q);
                }
                __当前索引++;
            });
            __M概要信息.启动次数 = _启动索引.Count;
            __M概要信息.起始时间 = _缓存.First().时间;
            __M概要信息.结束时间 = _缓存.Last().时间;
            return __M概要信息;
        }

        public List<M详细信息> 查询详细(int __启动次数)
        {
            __启动次数--;
            var __起始位置 = _启动索引[__启动次数];
            if (__启动次数 == 0)
            {
                __起始位置 = 0;
            }
            int __结束位置;
            if (__启动次数 == _启动索引.Count - 1)
            {
                __结束位置 = _缓存.Count;
            }
            else
            {
                __结束位置 = _启动索引[__启动次数 + 1];
            }
            return _缓存.GetRange(__起始位置, __结束位置 - __起始位置);
        }


        private static readonly string _内容和扩展分割字符 = " ... " + Environment.NewLine;

        public static M详细信息 解析调试信息(string line)
        {
            line = line.Replace("<br/>", Environment.NewLine);
            var spans = line.Split(new[] { _字段分隔符 }, StringSplitOptions.None);
            if (spans.Length < 7)
            {
                return null;
            }
            var __时间描述 = spans[0].Trim();
            var __等级描述 = spans[1].Trim();
            var __事件描述 = spans[2].Trim();
            var __线程 = spans[3].Trim();
            var __标题 = spans[4].Trim();
            var __内容 = spans[5].Trim();
            var __辅助信息 = spans[6].Trim();
            var __行号 = string.Empty;
            var __方法 = string.Empty;
            var __类型 = string.Empty;
            var __文件 = string.Empty;
            if (spans.Length > 7)
            {
                __行号 = spans[7].Trim();
            }
            if (spans.Length > 8)
            {
                __方法 = spans[8].Trim();
            }
            if (spans.Length > 9)
            {
                __类型 = spans[9].Trim();
            }
            if (spans.Length > 10)
            {
                __文件 = spans[10].Trim();
            }

            DateTime __时间;
            TraceEventType __等级;
            int __跟踪标记;
            E跟踪周期 __跟踪周期;
            if (!DateTime.TryParse(__时间描述, out __时间))
            {
                return null;
            }
            if (!Enum.TryParse(__等级描述, out __等级))
            {
                return null;
            }
            if (!int.TryParse(__事件描述, out __跟踪标记))
            {
                return null;
            }
            if (__跟踪标记 == 0)
            {
                __跟踪周期 = E跟踪周期.无;
            }
            else
            {
                if (!Enum.TryParse(__标题, out __跟踪周期))
                {
                    return null;
                }
            }
            var __M调试信息 = new M详细信息
            {
                时间 = __时间,
                等级 = __等级,
                线程 = __线程,
                标题 = __标题,
                内容 = __内容,
                辅助信息 = __辅助信息,
                跟踪周期 = __跟踪周期,
                跟踪标记 = __跟踪标记,
                方法 = __方法,
                类型 = __类型,
                文件 = __文件,
                行号 = __行号,
                耗时 = 0,
                来源 = "文本",
            };
            if (!string.IsNullOrEmpty(__辅助信息.Trim()))
            {
                __M调试信息.内容 = string.Format("{0}{2}{1}", __内容, __辅助信息, _内容和扩展分割字符);
            }
            return __M调试信息;
        }

    }
}
