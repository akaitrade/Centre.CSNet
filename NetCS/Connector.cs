using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NetCS
{
    public class Connector
    {
        static string ip;
        static int port;
        public Connector(string ip_,int port_)
        {
            ip = ip_;
            port = port_;
        }
        ///<summary>
        ///Retrieve the balance of a default CS symbol
        ///</summary>
        public string balance(string PubKey)
        {
            try
            {
                var client_ = new Client(ip, port, PubKey, "", PubKey);
                var data = client_.WalletGetBalance();
                if (data.Status.Code == 0)
                {
                    return data.Balance.Integral.ToString() + "." + data.Balance.Integral.ToString();
                }
                else
                {
                    return "ERROR";
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "ERROR";

            }
        }
        ///<summary>
        ///Retrieve history for a given publickey
        ///</summary>
        public List<NodeApi.SealedTransaction> FetchHistory(string PubKey)
        {
            try
            {
                var client_ = new Client(ip, port, PubKey, "", PubKey);
                var data = client_.TransactionsGet(0, 10);

                if (data.Status.Code == 0){return data.Transactions;}
                else{return new List<NodeApi.SealedTransaction>();}
            }
            catch (Exception e)
            {
                return new List<NodeApi.SealedTransaction>();

            }
        }
        ///<summary>
        ///Send a Transaction with the default CS Symbol to the Credits Blockchain
        ///</summary>
        public NodeApi.TransactionFlowResult SendTransaction(int Integeral, long fraction, string PublicKey, string PrivateKey, string Target)
        {
            try
            {
                var client_ = new Client(ip, port, PublicKey, PrivateKey, Target);
                if (fraction.ToString().Length < 18)
                {
                    var tempvar = fraction.ToString();
                    var extracount = 18 - tempvar.Length;
                    foreach (int value in Enumerable.Range(1, extracount)) { tempvar = tempvar + "0"; }
                    fraction = Convert.ToInt64(tempvar);
                }
                return client_.TransferCoins(Integeral, fraction, 1.0);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return new NodeApi.TransactionFlowResult { Status = new NodeApi.APIResponse { Code = 1 } };

            }
        }
    }
}
