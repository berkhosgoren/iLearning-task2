using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Crypto.Digests;

var folder = @"C:\Users\Berk\Desktop\task2";

if (!Directory.Exists(folder))
{
    Console.WriteLine("Folder not found: " + folder);
        return;
}

var files = Directory.GetFiles(folder).OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToArray();

Console.WriteLine($"Files found: {files.Length}");
if(files.Length != 256)
{
    Console.WriteLine("Unexpected file count. Expected is 256.");
    return;
}

static string Sha3_256Hex(byte[] data)
{
    var d = new Sha3Digest(256);
    d.BlockUpdate(data, 0, data.Length);
    var hash = new byte[32];
    d.DoFinal(hash, 0);
    return Convert.ToHexString(hash).ToLowerInvariant();
}

static int HexVal(char c)
{
    if (c >= '0' && c <= '9') return c - '0';
    if (c >= 'a' && c <= 'f') return c - 'a' +10;
    if (c >= 'A' && c <= 'F') return c - 'A' +10;
    throw new ArgumentException("Bad hex char: " + c);
}

static System.Numerics.BigInteger SortKey(string hex64)
{
    var k = System.Numerics.BigInteger.One;
    foreach (var ch in hex64)
        k *= (HexVal(ch) + 1);
    return k;
}

var items = new List<(string Hash, System.Numerics.BigInteger Key)>(files.Length);

foreach (var path in files)
{
    var bytes = File.ReadAllBytes(path);
    var h = Sha3_256Hex(bytes);
    var k = SortKey(h);
    items.Add((h, k));
}

Console.WriteLine($"Hashes computed: {items.Count}");

var sorted = items.OrderBy(x => x.Key).Select(x => x.Hash).ToList();

Console.WriteLine("First 3 hashes after sort:");
foreach (var h in sorted.Take(3))
    Console.WriteLine(h);

Console.WriteLine("Ready for the final concatenation step.");