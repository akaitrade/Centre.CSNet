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
        ///StatsGet Retrieve total counts of Blockchain
        ///</summary>
        [Obsolete("StatsGet not yet implemented")]
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
        public NodeApi.TransactionFlowResult SendTransaction(int Integeral, long fraction, double fee ,string PublicKey, string PrivateKey, string Target, byte[] UserData = null, long TxsID = 0, NodeApi.Transaction txs = null)
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
                return client_.TransferCoins(Integeral, fraction, fee, UserData, TxsID,txs);
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
        ///Check how much the fee actually cost in the blockchain
        ///</summary>
        public NodeApi.ActualFeeGetResult ActualFeeGet(int TransactionSize)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.ActualFeeGet(TransactionSize);
            }
            catch (Exception e)
            {
                return new NodeApi.ActualFeeGetResult();
            }
        }

        ///<summary>
        ///Retrieve Wallet Id
        ///</summary>
        public NodeApi.WalletIdGetResult WalletIdGet(string Address)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.WalletIdGet(Base58Check.Base58CheckEncoding.DecodePlain(Address));
            }
            catch (Exception e)
            {
                return new NodeApi.WalletIdGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve TransactionsList of The blockchain
        ///Optional Values Offset/Limit
        ///Default retrieves 10 Latest Transactions
        ///</summary>
        public NodeApi.TransactionsGetResult TransactionsListGet(long OffSet = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TransactionsListGet(OffSet,Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.TransactionsGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Pool List Stable
        ///Optional Values Sequence/Limit
        ///Default retrieves 10 Latest Pool
        ///</summary>
        public NodeApi.PoolListGetResult PoolListGetStable(long Sequence = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.PoolListGetStable(Sequence, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.PoolListGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Pool List
        ///Optional Values Offset/Limit
        ///Default retrieves 10 Latest Pool
        ///</summary>
        public NodeApi.PoolListGetResult PoolListGet(long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.PoolListGet(Offset, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.PoolListGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Pool Info 
        ///Optional Values Offset/Limit
        ///Default retrieves 10 Latest Pool
        ///</summary>
        public NodeApi.PoolInfoGetResult PoolInfoGet(long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.PoolInfoGet(Offset, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.PoolInfoGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Pool Transactions
        ///Optional Values Offset/Limit/Sequence
        ///Default retrieves 10 Latest Pool Transactions
        ///</summary>
        public NodeApi.PoolTransactionsGetResult PoolTransactionsGet(long Sequence= 0, long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.PoolTransactionsGet(Sequence, Offset, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.PoolTransactionsGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Single SmartContract
        ///</summary>
        public NodeApi.SmartContractGetResult SmartContractGet(byte[] Address)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.SmartContractGet(Address);
            }
            catch (Exception e)
            {
                return new NodeApi.SmartContractGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Smart Contracts based on Deployer Address
        ///Optional Values Offset/Limit
        ///Default retrieves 10 Latest Pool Transactions
        ///</summary>
        public NodeApi.SmartContractsListGetResult SmartContractsListGet(byte[] Deployer, long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.SmartContractsListGet(Deployer, Offset, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.SmartContractsListGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Transactions State based on id
        ///</summary>
        public NodeApi.TransactionsStateGetResult TransactionsStateGet(byte[] Address, List<long> Id)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TransactionsStateGet(Address, Id);
            }
            catch (Exception e)
            {
                return new NodeApi.TransactionsStateGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve All Methods of Contract
        ///</summary>
        public NodeApi.ContractAllMethodsGetResult ContractAllMethodsGet(List<NodeApi.ByteCodeObject> byteCodeObjects)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.ContractAllMethodsGet(byteCodeObjects);
            }
            catch (Exception e)
            {
                return new NodeApi.ContractAllMethodsGetResult { Code = 1, Message = e.ToString() };
            }
        }

        ///<summary>
        ///Retrieve Method Parameters
        ///</summary>
        public NodeApi.SmartMethodParamsGetResult SmartMethodParamsGet(byte[] Address, long Id)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.SmartMethodParamsGet(Address,Id);
            }
            catch (Exception e)
            {
                return new NodeApi.SmartMethodParamsGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Smart Contract Data
        ///</summary>
        public NodeApi.SmartContractDataResult SmartContractDataGet(byte[] Address)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.SmartContractDataGet(Address);
            }
            catch (Exception e)
            {
                return new NodeApi.SmartContractDataResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Compile Smart Contract
        ///</summary>
        public NodeApi.SmartContractCompileResult SmartContractCompile(string SCcode)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.SmartContractCompile(SCcode);
            }
            catch (Exception e)
            {
                return new NodeApi.SmartContractCompileResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve All Token Balances
        ///</summary>
        public NodeApi.TokenBalancesResult TokenBalancesGet(byte[] Address)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenBalancesGet(Address);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenBalancesResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Token Transfers
        ///Optional Values Offset/Limit
        ///Default retrieves 10 Latest Token Transfers
        ///</summary>
        public NodeApi.TokenTransfersResult TokenTransfersGet(byte[] Token, long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenTransfersGet(Token,Offset,Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenTransfersResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Retrieve Single Token Transfer
        ///</summary>
        public NodeApi.TokenTransfersResult TokenTransferGet(byte[] Token, NodeApi.TransactionId TxsID)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenTransferGet(Token, TxsID);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenTransfersResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Token Transfers List
        ///Optional Values Offset/Limit
        ///Default Offset = 0 Limit = 10
        ///</summary>
        public NodeApi.TokenTransfersResult TokenTransfersListGet(long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenTransfersListGet(Offset,Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenTransfersResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Specific Token Transfers Get
        ///Optional Values Offset/Limit
        ///Default Offset = 0 Limit = 10
        ///</summary>
        public NodeApi.TokenTransfersResult TokenWalletTransfersGet(byte[] Token, byte[] Address ,long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenWalletTransfersGet(Token,Address,Offset, Limit);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenTransfersResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Get Token Info 
        ///</summary>
        public NodeApi.TokenInfoResult TokenInfoGet(byte[] Token)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenInfoGet(Token);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenInfoResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Get Token Holders for specific Token
        ///Optional Values Offset/Limit
        ///Default Offset = 0 Limit = 10
        ///</summary>
        public NodeApi.TokenHoldersResult TokenHoldersGet(byte[] Token, NodeApi.TokenHoldersSortField Order, bool Desc, long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokenHoldersGet(Token,Offset,Limit,Order,Desc);
            }
            catch (Exception e)
            {
                return new NodeApi.TokenHoldersResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Get List of Tokens
        ///Optional Values Offset/Limit
        ///Default Offset = 0 Limit = 10
        ///</summary>
        public NodeApi.TokensListResult TokensListGet(NodeApi.TokensListSortField Order, NodeApi.TokenFilters Filters, bool Desc, long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TokensListGet(Offset,Limit,Order,Desc,Filters);
            }
            catch (Exception e)
            {
                return new NodeApi.TokensListResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Get List of Wallets
        ///Optional Values Offset/Limit
        ///Default Offset = 0 Limit = 10
        ///</summary>
        public NodeApi.WalletsGetResult WalletsGet(sbyte OrdCol,  bool Desc, long Offset = 0, long Limit = 10)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.WalletsGet(Offset, Limit, OrdCol, Desc);
            }
            catch (Exception e)
            {
                return new NodeApi.WalletsGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
            }
        }

        ///<summary>
        ///Get List of Trusted
        ///</summary>
        public NodeApi.TrustedGetResult TrustedGet(int Page)
        {
            try
            {
                Client client_ = new Client(ip, port, "", "", "");
                return client_.TrustedGet(Page);
            }
            catch (Exception e)
            {
                return new NodeApi.TrustedGetResult { Status = new NodeApi.APIResponse { Code = 1, Message = e.ToString() } };
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
