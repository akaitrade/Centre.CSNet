using System;
using System.Collections.Generic;
using NetCS;
using Base58Check;


namespace nugettester
{
    class Program
    {
        public static Connector connect_ = new Connector("194.163.152.177", 9091);
        static void Main(string[] args)
        {
            TestBalance();
        }

        public static string ToJson(object x)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(x);
        }
        public static void TestActualFeeGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.ActualFeeGet(0)));
        }

        public static void TestSmartContractGet()
        {
            var t = connect_.SmartContractGet(Base58Check.Base58CheckEncoding.DecodePlain("12DHXQ8rzYUawD6VSD6WuVaTQ4uen7fStWsTmZASnDv1"));
            t.SmartContract.Address = new byte[1];
            t.SmartContract.Deployer = new byte[1];
            t.SmartContract.SmartContractDeploy.ByteCodeObjects = new List<NodeApi.ByteCodeObject>();
            t.SmartContract.ObjectState = new byte[1];
            Console.WriteLine(ObjectDumper.Dump(t));
        }

        public static void TestTokenBalancesGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokenBalancesGet(Base58Check.Base58CheckEncoding.DecodePlain("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"))));
        }
        public static void TestTrustedget()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TrustedGet(0)));
        }
        public static void TestTokenHoldersGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokenHoldersGet(Base58Check.Base58CheckEncoding.DecodePlain("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"),NodeApi.TokenHoldersSortField.TH_Balance,true)));
        }

        public static void TestTokenListGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokensListGet(NodeApi.TokensListSortField.TL_HoldersCount,new NodeApi.TokenFilters(),true)));
        }

        public static void TestTokenTransfers()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokenTransfersGet(Base58Check.Base58CheckEncoding.DecodePlain("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"),0,10)));
        }

        public static void TestWalletsGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.WalletsGet(new sbyte(),true, 0, 10)));
        }

        public static void TestTokenInfoGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokenInfoGet(Base58Check.Base58CheckEncoding.DecodePlain("13T1JhxF614ZKT6L2Kh8wtpE5xoZLGdFxnXbFiWKFih1"))));
        }

        public static void TestTokenTransfersListGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokenTransfersListGet(0, 10)));
        }

        public static void TestTokenTransferGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TokenTransferGet(Base58Check.Base58CheckEncoding.DecodePlain("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"), new NodeApi.TransactionId())));
        }

        public static void TestCompileContract()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.SmartContractCompile("import com.credits.scapi.annotations.*; import com.credits.scapi.v0.*; public class MySmartContract extends SmartContract { public MySmartContract() {} public String hello2(String say) { return \"Hello\" + say; } }")));
        }


        public static void TestSmartContractGetList()
        {
            var r = connect_.SmartContractsListGet(Base58Check.Base58CheckEncoding.DecodePlain("EoPibFsGPE4xqXH2tYTBQUeJqSMMFvCZUdqAW9Bnh3nd"),0,2);
            foreach(var t in r.SmartContractsList)
            {
                t.Address = new byte[1];
                t.Deployer = new byte[1];
                t.SmartContractDeploy.ByteCodeObjects = new List<NodeApi.ByteCodeObject>();
                t.ObjectState = new byte[1];

            }
            
            Console.WriteLine(ObjectDumper.Dump(r));
        }

        public static void TestContractMethodGet()
        {
            var t = connect_.SmartContractGet(Base58Check.Base58CheckEncoding.DecodePlain("12DHXQ8rzYUawD6VSD6WuVaTQ4uen7fStWsTmZASnDv1"));
            t.SmartContract.Address = new byte[1];
            t.SmartContract.Deployer = new byte[1];
            t.SmartContract.ObjectState = new byte[1];
            Console.WriteLine(ObjectDumper.Dump(connect_.ContractAllMethodsGet(t.SmartContract.SmartContractDeploy.ByteCodeObjects)));
        }

        public static void TestWalletIdGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.WalletIdGet("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct")));
        }

        public static void TestSmartMethodGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.SmartMethodParamsGet(Base58Check.Base58CheckEncoding.DecodePlain("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"),1)));
        }

        public static void TestSmartDataGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.SmartContractDataGet(Base58Check.Base58CheckEncoding.DecodePlain("12DHXQ8rzYUawD6VSD6WuVaTQ4uen7fStWsTmZASnDv1"))));
        }

        public static void TestTransactionStateGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TransactionsStateGet(Base58CheckEncoding.DecodePlain("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"),new List<long>())));
        }

        public static void TestPoolListStable()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.PoolListGetStable(1451686, 1)));
        }

        public static void TestPoolList()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.PoolListGet(0, 1)));
        }

        public static void TestPoolInfo()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.PoolInfoGet(0, 1)));
        }
        public static void TestTransactionsListGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TransactionsListGet(0,3)));
        }
        public static void TestPoolTransactionsGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.PoolTransactionsGet(50,0, 3)));
        }
        public static void TestBalance()
        {
            //Console.WriteLine(connect_.WalletGetBalance("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"));
            NodeApi.WalletBalanceGetResult balance = connect_.WalletGetBalance("7CAbMzgimfoHTQy32bJhB3wCx1j3XfZp5TARGMcgp4Fe");
            //Console.WriteLine(ToJson(balance));
            Console.WriteLine(ObjectDumper.Dump(balance));
        }

        public void TestWalletGetBalance()
        {
            Connector connect_ = new Connector("95.111.224.219", 9091);
            NodeApi.WalletBalanceGetResult balance = connect_.WalletGetBalance("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct");
            Console.WriteLine(balance.Balance);
            Console.WriteLine(balance.Status.Code);
            Console.WriteLine(balance.Delegated);
        }     
 
        public static void TestTransactionsGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TransactionsGet("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct", 0, 10)));
        }
        public static void TestSendTransaction()
        {
            Console.WriteLine(connect_.SendTransaction(5000,0,1,"5B3YXqDTcWQFGAqEJQJP3Bg1ZK8FFtHtgCiFLT5VAxpe", "3rUevsW5xfob6qDxWMDFwwTQCq39SYhzstuyfUGSDvF2QHBRyPD8fSk49wFXaPk3GztfxtuU85QHfMV3ozfqa7rN", "EnrT42AkUpe32xw1tXE4e3siGMExpRe7uvsgNj36RzLc"));
        }

        public static void TestSendTransaction_Userdata()
        {
            Console.WriteLine(connect_.SendTransaction(0,0,1, "5B3YXqDTcWQFGAqEJQJP3Bg1ZK8FFtHtgCiFLT5VAxpe", "3rUevsW5xfob6qDxWMDFwwTQCq39SYhzstuyfUGSDvF2QHBRyPD8fSk49wFXaPk3GztfxtuU85QHfMV3ozfqa7rN", "4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct",Base58Check.Base58CheckEncoding.DecodePlain("Hi")));
        }

        public static void TestDeploySmartContract()
        {
            Console.WriteLine(connect_.DeploySmartContract(0, 0, 5, "5B3YXqDTcWQFGAqEJQJP3Bg1ZK8FFtHtgCiFLT5VAxpe", "3rUevsW5xfob6qDxWMDFwwTQCq39SYhzstuyfUGSDvF2QHBRyPD8fSk49wFXaPk3GztfxtuU85QHfMV3ozfqa7rN", "4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct"));
        }

        public static void TestSyncState()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.SyncState()));
        }

        public static void TestTransactionGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.TransactionGet(424078, 0)));
        }
        public static void TestWalletData()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.WalletDataGet("5B3YXqDTcWQFGAqEJQJP3Bg1ZK8FFtHtgCiFLT5VAxpe")));
        }
        public static void TestWalletTransactionscount()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.WalletTransactionsCountGet("4SFfA1S2xfA3BdgkTn2tK14yDhLuD11RVz78kqx35jct")));
        }
        public static void TestStatsGet()
        {
            Console.WriteLine(ObjectDumper.Dump(connect_.StatsGet()));
        }
    }
}
