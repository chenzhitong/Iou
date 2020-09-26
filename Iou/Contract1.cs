using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Iou
{
    /// <summary>
    /// IOU（欠条币）：一种支持负数的NEP-5资产
    /// 合约无存储区，账户由区块链浏览器统计
    /// </summary>
    public class Contract1 : SmartContract
    {
        public static event Action<byte[], byte[], BigInteger> transfer;

        public static object Main(string method, object[] args)
        {
            if (method == "decimals") return 1;

            if (method == "name" || method == "symbol") return "IOU";

            if (method == "balanceOf") return "I don't know";

            if (method == "totalSupply") return 0;

            if (method == "transfer")
            {
                var from = (byte[])args[0];
                var to = (byte[])args[1];
                var amount = (BigInteger)args[2];

                if (from.Length != 20 || to.Length != 20 || amount <= 0 || !Runtime.CheckWitness(from)) throw new InvalidOperationException("😂");
                transfer(from, to, amount);
                return true;
            }

            return false;
        }
    }
}