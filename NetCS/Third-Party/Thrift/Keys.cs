using System;
using System.Collections.Generic;
using System.Text;


namespace NetCS
{
    public class Keys
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string TargetKey { get; set; }
        public byte[] PublicKeyBytes
        {
            get
            {
                return PublicKey != null ? Base58Check.Base58CheckEncoding.DecodePlain(PublicKey) : null;
            }
        }
        public byte[] PrivateKeyBytes
        {
            get
            {
                return PrivateKey != null ? Base58Check.Base58CheckEncoding.DecodePlain(PrivateKey) : null;
            }
        }
        public byte[] TargetKeyBytes
        {
            get
            {
                return TargetKey != null ? Base58Check.Base58CheckEncoding.DecodePlain(TargetKey) : null;
            }
        }
    }
}