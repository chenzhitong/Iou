using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using System;
using System.ComponentModel;
using System.Numerics;

namespace iou
{
    [ManifestExtra("Author", "chenzhitong")]
    [ManifestExtra("Email", "chenzhitong@ngd.neo.org")]
    [ManifestExtra("Description", "IOU（欠条币）：一种支持负数的NEP-17（非严格意义上的）资产，合约无存储区，账户由区块链浏览器统计")]
    public class Contract1 : SmartContract
    {
        [DisplayName("Transfer")]
        public static event Action<UInt160, UInt160, BigInteger>  OnTransfer;

        public static byte Decimals() => 1;

        public static string Symbol() => "IOU";

        /// <summary>
        /// 信用额度，1亿（且不会减少），为了兼容钱包转账时的余额验证
        /// </summary>
        public static BigInteger BalanceOf() => 100000000;

        public static bool Transfer(UInt160 from, UInt160 to, BigInteger amount, object data)
        {
            if (from is null || !from.IsValid) throw new Exception("The argument \"from\" is invalid.");
            if (to is null || !to.IsValid) throw new Exception("The argument \"to\" is invalid.");
            if (amount < 0) throw new Exception("The amount must be a positive number.");
            if (!Runtime.CheckWitness(from)) return false;
            OnTransfer(from, to, amount);
            return true;
        }
    }
}
