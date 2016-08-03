using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win.Model
{
    public class M概要信息
    {
        public int 启动次数 { get; set; }

        public List<M详细信息> 警告列表 { get; set; }

        public List<M详细信息> 启动列表 { get; set; }

        public DateTime 起始时间 { get; set; }

        public DateTime 结束时间 { get; set; }

        public M概要信息()
        {
            警告列表 = new List<M详细信息>();
            启动列表 = new List<M详细信息>();
        }
    }
}
