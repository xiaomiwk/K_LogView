using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Win.Model;

namespace Win.IBLL
{
    public interface IUDP分析
    {
        void 启动(int udpPort);

        event Action<M详细信息> 收到信息;

        void 关闭();
    }
}
