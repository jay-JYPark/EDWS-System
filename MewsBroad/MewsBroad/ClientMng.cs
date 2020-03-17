using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Mews.Lib.Mewstcp;

namespace Mews.Svr.Broad
{
    public class ClientMng : TCPIndClient
    {
        public override void CreateSocket()
        {
            UserSocket userSoc = new UserSocket();
            socket = userSoc as ClientSocket;
        }
    };

    public class UserSocket : ClientSocket
    {
        protected override void ParsePacket(Socket socket, byte[] buf, int size)
        {
            ResetFlag();
            CallEventHandler(buf);
        }
    };
}