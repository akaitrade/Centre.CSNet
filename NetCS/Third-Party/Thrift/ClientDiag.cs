using Chaos.NaCl;
using NodeApiDiag;
using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;
using Thrift.Transport;
using Blake2Fast;
namespace NetCS
{
    class ClientDiag : IDisposable
    {
        TSocket transport;
        TBinaryProtocol protocol;
        API_DIAG.Client api;
        public Keys keys;

        public ClientDiag(string ip, int port, string publicKey, string privateKey, string targetKey)
        {
            transport = new TSocket(ip, port);
            protocol = new TBinaryProtocol(transport);
            api = new API_DIAG.Client(protocol);
            transport.Open();

            keys = new Keys();
            keys.PublicKey = publicKey;
            keys.PrivateKey = privateKey;
            keys.TargetKey = targetKey;
        }

        public void Dispose()
        {
            transport.Close();
        }

        public NodeInfoRespone GetNodeInfo()
        {
            var request = new NodeApiDiag.NodeInfoRequest()
            {
                BlackListContent = true,
                GrayListContent = true,
                Session = true,
                State = true
            };
            return api.GetNodeInfo(request);
        }

        public ActiveNodesResult GetActiveNodes()
        {
            return api.GetActiveNodes();
        }

    }
    
}
