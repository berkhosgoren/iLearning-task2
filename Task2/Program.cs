using System;
using System.IO;
using System.Linq;

var folder = @"C:\Users\Berk\Desktop\task2";

if (!Directory.Exists(folder))
{
    Console.WriteLine("Folder not found: " + folder);
    return;
}

var files = Directory.GetFiles(folder).OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToArray();

Console.WriteLine($"Files found: {files.Length}");

if (files.Length > 0)
{
    Console.WriteLine("First few files:");
    foreach (var f in files.Take(5)) Console.WriteLine(" - " + Path.GetFileName(f));  
}

