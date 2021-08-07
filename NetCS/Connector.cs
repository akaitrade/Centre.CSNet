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
        ///Retrieve a certain Transaction with pool and index number
        ///</summary>
        public NodeApi.TransactionGetResult TransactionGet(long PoolSeq, int Index)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TransactionGet(new NodeApi.TransactionId { Index = Index, PoolSeq = PoolSeq });
                
            }
            catch (Exception e)
            {
                return new NodeApi.TransactionGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }
        ///<summary>
        ///Retrieve the balance of a default CS symbol
        ///</summary>
        public NodeApi.WalletBalanceGetResult WalletGetBalance(string PubKey)
        {
            try
            {
                Client client_ = new Client(ip, port, PubKey, "", PubKey);
                return client_.WalletGetBalance();
            }
            catch (Exception e)
            {
                return new NodeApi.WalletBalanceGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } } ;
            }
        }
        ///<summary>
        ///Retrieve Transaction Count of the give Wallet
        ///</summary>
        public NodeApi.WalletTransactionsCountGetResult WalletTransactionsCountGet(string PubKey)
        {
            try
            {
                Client client_ = new Client(ip, port, PubKey, "", PubKey);
                return client_.WalletTransactionsCountGet();
            }
            catch (Exception e)
            {
                return new NodeApi.WalletTransactionsCountGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }
        ///<summary>
        ///WalletDataGet Information unknown
        ///</summary>
        public NodeApi.WalletDataGetResult WalletDataGet(string PubKey)
        {
            try
            {
                Client client_ = new Client(ip, port, PubKey, "", PubKey);
                return client_.WalletDataGet();
            }
            catch (Exception e)
            {
                return new NodeApi.WalletDataGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }
        ///<summary>
        ///StatsGet
        ///</summary>
        public NodeApi.StatsGetResult StatsGet()
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.StatsGet();
            }
            catch (Exception e)
            {
                return new NodeApi.StatsGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }
        ///<summary>
        ///Retrieve history for a given publickey
        ///Optional Values Offset/Limit
        ///Default retrieves 10 Latest Transactions
        ///</summary>
        public NodeApi.TransactionsGetResult TransactionsGet(string PubKey,long OffSet = 0,long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, PubKey, "", PubKey);
                return client_.TransactionsGet(OffSet, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.TransactionsGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };

            }
        }
        ///<summary>
        ///Send a Transaction with the default CS Symbol to the Credits Blockchain
        ///Optional Values : UserData/LastTransactionID
        ///</summary>
        public NodeApi.TransactionFlowResult SendTransaction(int Integeral, long fraction, double fee ,string PublicKey, string PrivateKey, string Target, byte[] UserData = null, long TxsID = 0)
        {
            try
            {
                Client client_ = new Client(ip, port, PublicKey, PrivateKey, Target);
                if (fraction.ToString().Length < 18)
                {
                    var tempvar = fraction.ToString();
                    var extracount = 18 - tempvar.Length;
                    foreach (int value in Enumerable.Range(1, extracount)) { tempvar = tempvar + "0"; }
                    fraction = Convert.ToInt64(tempvar);
                }
                if(TxsID == 0) { TxsID = client_.WalletTransactionsCountGet().LastTransactionInnerId + 1; };
                return client_.TransferCoins(Integeral, fraction, fee, UserData, TxsID);
            }
            catch (Exception e)
            {
                return new NodeApi.TransactionFlowResult { Status = new NodeApi.APIResponse { Code = 1 , Message = e.ToString() } };

            }
        }
        ///<summary>
        ///Checks if the node is synced with the blockchain.
        ///This Function also returns Current Round and Last Block information
        ///</summary>
        public NodeApi.SyncStateResult SyncState()
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.SyncStateGet();
            }
            catch (Exception e)
            {
                return new NodeApi.SyncStateResult { Status = new NodeApi.APIResponse { Code = 1 , Message = e.ToString() } };

            }
        }
        ///<summary>
        ///Deploys Smart Contract
        ///Without Smart Contract code it will deploy default contract
        ///</summary>
        public NodeApi.TransactionFlowResult DeploySmartContract (int Integeral, long fraction, double fee, string PublicKey, string PrivateKey, string Target, string smcode = "", long TxsID = 0)
        {
            try
            {
                Client client_ = new Client(ip, port, PublicKey, PrivateKey, Target);
                if (TxsID == 0) { TxsID = client_.WalletTransactionsCountGet().LastTransactionInnerId + 1; };
                return client_.DeploySmartContract(smcode,fee,TxsID);
            }
            catch (Exception e)
            {
                return new NodeApi.TransactionFlowResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }

        }
    }
}
