﻿using Chaos.NaCl;
using NodeApi;
using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;
using Thrift.Transport;
using System.Threading;
using System.Threading.Tasks;
using Blake2Fast;
namespace NetCS
{
    class Client : IDisposable
    {
        TSocket transport;
        TBinaryProtocol protocol;
        API.Client api;
        public Keys keys;

        public Client(string ip, int port, string publicKey, string privateKey, string targetKey)
        {
            transport = new TSocket(ip, port);
            protocol = new TBinaryProtocol(transport);
            api = new API.Client(protocol);
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

        public void TransactionGet(long poolSeq, int index)
        {
            var tr = new TransactionId();
            tr.PoolSeq = poolSeq;
            tr.Index = index;

            TransactionGetResult trgr = api.TransactionGet(tr);

        }

        public async Task Transferasync(int integral, long fraction, double fee)
        {
            api.TransactionFlow(CreateTransaction(integral, fraction, fee));
        }

        public WalletBalanceGetResult WalletGetBalance()
        {
            return api.WalletBalanceGet(keys.PublicKeyBytes);
        }

        public WalletTransactionsCountGetResult WalletTransactionsCountGet()
        {
            return api.WalletTransactionsCountGet(keys.PublicKeyBytes);
        }

        public WalletDataGetResult WalletDataGet( )
        {
            return api.WalletDataGet(keys.PublicKeyBytes);
        }

        public StatsGetResult StatsGet()
        {
            return api.StatsGet();
        }

        public TransactionGetResult TransactionGet(TransactionId transactionId)
        {
            return api.TransactionGet(transactionId);
        }

        public SyncStateResult SyncStateGet()
        {
            return api.SyncStateGet();
        }
       

        public TransactionsGetResult TransactionsGet(long offset, long limit)
        {
            return api.TransactionsGet(keys.PublicKeyBytes,offset, limit);
        }

        public TransactionFlowResult TransferCoins(int integral, long fraction, double fee)
        {
            return api.TransactionFlow(CreateTransaction(integral, fraction, fee));
        }

        public TransactionFlowResult SendMessage(int integral, long fraction, double fee, byte[] message)
        {
            return api.TransactionFlow(CreateMessage(integral, fraction, fee,message));
        }

        public TransactionFlowResult SendMessage_Incremental(int integral, long fraction, double fee, byte[] message,long txsid)
        {
            return api.TransactionFlow(CreateMessagewithincremental(integral, fraction, fee, message,txsid));
        }

        public TransactionFlowResult DeploySmartContract(string smCode)
        {
            return api.TransactionFlow(CreateTransactionWithSmartContract(smCode));
        }

        private Transaction CreateTransaction(int integral, long fraction, double fee)
        {
            var transaction = new Transaction();
            transaction.Id = api.WalletTransactionsCountGet(keys.PublicKeyBytes).LastTransactionInnerId + 1;
            transaction.Source = keys.PublicKeyBytes;
            transaction.Target = keys.TargetKeyBytes;
            transaction.Amount = new Amount(integral, fraction);
            transaction.Fee = new AmountCommission(Fee(fee));
            transaction.Currency = 1;

            var bytes = new byte[86];
            Array.Copy(BitConverter.GetBytes(transaction.Id), 0, bytes, 0, 6);
            Array.Copy(transaction.Source, 0, bytes, 6, 32);
            Array.Copy(transaction.Target, 0, bytes, 38, 32);
            Array.Copy(BitConverter.GetBytes(transaction.Amount.Integral), 0, bytes, 70, 4);
            Array.Copy(BitConverter.GetBytes(transaction.Amount.Fraction), 0, bytes, 74, 8);
            Array.Copy(BitConverter.GetBytes(transaction.Fee.Commission), 0, bytes, 82, 2);
            bytes[84] = 1;
            bytes[85] = 0;

            var signature = Ed25519.Sign(bytes, keys.PrivateKeyBytes);
            var verifyResult = Ed25519.Verify(signature, bytes, keys.PublicKeyBytes);
            if (!verifyResult) throw new Exception("Signature could not be verified");

            transaction.Signature = signature;
            return transaction;
        }

        private Transaction CreateMessage(int integral, long fraction, double fee, byte[] message)
        {
            var transaction = new Transaction();
            transaction.Id = api.WalletTransactionsCountGet(keys.PublicKeyBytes).LastTransactionInnerId + 1;
            transaction.Source = keys.PublicKeyBytes;
            transaction.Target = keys.TargetKeyBytes;
            transaction.Amount = new Amount(integral, fraction);
            transaction.Fee = new AmountCommission(Fee(fee));
            transaction.Currency = 1;
            transaction.UserFields = message;
            var lenght = message.Length;
            var bytes = new byte[90];
            Array.Copy(BitConverter.GetBytes(transaction.Id), 0, bytes, 0, 6);
            Array.Copy(transaction.Source, 0, bytes, 6, 32);
            Array.Copy(transaction.Target, 0, bytes, 38, 32);
            Array.Copy(BitConverter.GetBytes(transaction.Amount.Integral), 0, bytes, 70, 4);
            Array.Copy(BitConverter.GetBytes(transaction.Amount.Fraction), 0, bytes, 74, 8);
            Array.Copy(BitConverter.GetBytes(transaction.Fee.Commission), 0, bytes, 82, 2);
            bytes[84] = 1;
            bytes[85] = 1;
            Array.Copy(BitConverter.GetBytes(lenght),0,bytes,86,4);

            byte[] combined = new byte[bytes.Length + transaction.UserFields.Length];

            Array.Copy(bytes, 0, combined, 0, bytes.Length);
            Array.Copy(transaction.UserFields, 0, combined, bytes.Length, transaction.UserFields.Length);

            var signature = Ed25519.Sign(combined, keys.PrivateKeyBytes);
            var verifyResult = Ed25519.Verify(signature, combined, keys.PublicKeyBytes);
            if (!verifyResult) throw new Exception("Signature could not be verified");

            transaction.Signature = signature;
            return transaction;
        }

        private Transaction CreateMessagewithincremental(int integral, long fraction, double fee, byte[] message,long txsid)
        {
            var transaction = new Transaction();
            //transaction.Id = api.WalletTransactionsCountGet(keys.PublicKeyBytes).LastTransactionInnerId + 1;
            transaction.Id = txsid;
            transaction.Source = keys.PublicKeyBytes;
            transaction.Target = keys.TargetKeyBytes;
            transaction.Amount = new Amount(integral, fraction);
            transaction.Fee = new AmountCommission(Fee(fee));
            transaction.Currency = 1;
            transaction.UserFields = message;
            var lenght = message.Length;
            var bytes = new byte[90];
            Array.Copy(BitConverter.GetBytes(transaction.Id), 0, bytes, 0, 6);
            Array.Copy(transaction.Source, 0, bytes, 6, 32);
            Array.Copy(transaction.Target, 0, bytes, 38, 32);
            Array.Copy(BitConverter.GetBytes(transaction.Amount.Integral), 0, bytes, 70, 4);
            Array.Copy(BitConverter.GetBytes(transaction.Amount.Fraction), 0, bytes, 74, 8);
            Array.Copy(BitConverter.GetBytes(transaction.Fee.Commission), 0, bytes, 82, 2);
            bytes[84] = 1;
            bytes[85] = 1;
            Array.Copy(BitConverter.GetBytes(lenght), 0, bytes, 86, 4);

            byte[] combined = new byte[bytes.Length + transaction.UserFields.Length];

            Array.Copy(bytes, 0, combined, 0, bytes.Length);
            Array.Copy(transaction.UserFields, 0, combined, bytes.Length, transaction.UserFields.Length);

            var signature = Ed25519.Sign(combined, keys.PrivateKeyBytes);
            var verifyResult = Ed25519.Verify(signature, combined, keys.PublicKeyBytes);
            if (!verifyResult) throw new Exception("Signature could not be verified");

            transaction.Signature = signature;
            return transaction;
        }

        private byte[] Reverse(byte[] arr)
        {
            Array.Reverse(arr, 0, arr.Length);
            return arr;
        }

        private Transaction CreateTransactionWithSmartContract(string smCode)
        {
            if (smCode == "")
                smCode =
                "import com.credits.scapi.annotations.*; import com.credits.scapi.v0.*; public class MySmartContract extends SmartContract { public MySmartContract() {} public String hello2(String say) { return \"Hello\" + say; } }";

            var transaction = new Transaction();
            transaction.Id = api.WalletTransactionsCountGet(keys.PublicKeyBytes).LastTransactionInnerId + 1;
            transaction.Source = keys.PublicKeyBytes;
            //transaction.Target = keys.PublicKeyBytes;
            transaction.Amount = new Amount(0, 0);
            transaction.Fee = new AmountCommission(Fee(1.0));
            transaction.Currency = 1;

            var tarr = new byte[6];
            List<byte> target = new List<byte>(transaction.Source);
            Array.Copy(BitConverter.GetBytes(transaction.Id), 0, tarr, 0, 6);
            target.AddRange(tarr);
            var byteCode = api.SmartContractCompile(smCode);
            if (byteCode.Status.Code == 0)
            {
                for (int i = 0; i < byteCode.ByteCodeObjects.Count; i++)
                {
                    target.AddRange(byteCode.ByteCodeObjects[i].ByteCode);
                }
            }
            else
            {
                Console.WriteLine(byteCode.Status.Message);
                return null;
            }

            transaction.SmartContract = new SmartContractInvocation();
            transaction.SmartContract.SmartContractDeploy = new SmartContractDeploy()
            {
                SourceCode = smCode,
            };
            transaction.SmartContract.ForgetNewState = false;
            transaction.Target = Blake2s.ComputeHash(target.ToArray());

            var bytes = new List<byte>();

            Array.Copy(BitConverter.GetBytes(transaction.Id), 0, tarr, 0, 6);
            bytes.AddRange(tarr);
            bytes.AddRange(transaction.Source);
            bytes.AddRange(transaction.Target);
            bytes.AddRange(BitConverter.GetBytes(transaction.Amount.Integral));
            bytes.AddRange(BitConverter.GetBytes(transaction.Amount.Fraction));
            bytes.AddRange(BitConverter.GetBytes(transaction.Fee.Commission));
            bytes.Add(1);
            bytes.Add(1);

            var uf = new List<byte>();
            uf.AddRange(new byte[] { 11, 0, 1, 0, 0, 0, 0, 15, 0, 2, 12, 0, 0, 0, 0, 15, 0, 3, 11, 0, 0, 0, 0, 2, 0, 4, 0, 12, 0, 5, 11, 0, 1 });

            uf.AddRange(Reverse(BitConverter.GetBytes(smCode.Length))); //reverse ???

            uf.AddRange(Encoding.Default.GetBytes(smCode));
            uf.AddRange(new byte[] { 15, 0, 2, 12 });
            uf.AddRange(Reverse(BitConverter.GetBytes(byteCode.ByteCodeObjects.Count))); //reverse ???

            foreach (var bco in byteCode.ByteCodeObjects)
            {

                uf.AddRange(new byte[] { 11, 0, 1 });
                uf.AddRange(Reverse(BitConverter.GetBytes(bco.Name.Length))); //reverse ???
                uf.AddRange(Encoding.Default.GetBytes(bco.Name));
                uf.AddRange(new byte[] { 11, 0, 2 });
                uf.AddRange(Reverse(BitConverter.GetBytes(bco.ByteCode.Length))); //reverse ???
                uf.AddRange(bco.ByteCode);
                transaction.SmartContract.SmartContractDeploy.ByteCodeObjects = new List<ByteCodeObject>()
                {
                    new ByteCodeObject()
                    {
                        Name = bco.Name,
                        ByteCode = bco.ByteCode
                    }
                };

                uf.Add(0);
            }

            uf.AddRange(new byte[] { 11, 0, 3, 0, 0, 0, 0, 8, 0, 4, 0, 0, 0, 0, 0 });
            uf.Add(0);

            bytes.AddRange(BitConverter.GetBytes(uf.Count)); //reverse ???
            bytes.AddRange(uf.ToArray());

            var signature = Ed25519.Sign(bytes.ToArray(), keys.PrivateKeyBytes);
            var verifyResult = Ed25519.Verify(signature, bytes.ToArray(), keys.PublicKeyBytes);
            if (!verifyResult) throw new Exception("Signature could not be verified");

            foreach (var i in signature)
            {
                Console.Write(i);
                Console.Write(" ");
            }

            transaction.Signature = signature;
            return transaction;
        }

        private short Fee(Double value)
        {
            byte sign = (byte)(value < 0.0 ? 1 : 0); // sign
            int exp;   // exponent
            long frac; // mantissa
            value = Math.Abs(value);
            double expf = value == 0.0 ? 0.0 : Math.Log10(value);
            int expi = Convert.ToInt32(expf >= 0 ? expf + 0.5 : expf - 0.5);
            value /= Math.Pow(10, expi);
            if (value >= 1.0)
            {
                value *= 0.1;
                ++expi;
            }
            exp = expi + 18;
            if (exp < 0 || exp > 28)
            {
                throw new Exception($"exponent value {exp} out of range [0, 28]");
            }
            frac = (long)Math.Round(value * 1024);
            return (short)(sign * 32768 + exp * 1024 + frac);
        }
    }
}