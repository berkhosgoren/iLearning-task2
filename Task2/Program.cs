using System;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

static string Sha3_256Hex(byte[] data)
{
    var d = new Sha3Digest(256);
    d.BlockUpdate(data, 0, data.Length);
    var hash = new byte[32];
    d.DoFinal(hash, 0);
    return Convert.ToHexString(hash).ToLowerInvariant();
}

var empty = Array.Empty<byte>();
Console.WriteLine(Sha3_256Hex(empty)); 