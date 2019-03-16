using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpQRCode {
    internal class Program {
        static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
                try {
                    string dir = opts.Output ?? Environment.CurrentDirectory;
                    IEnumerable<string> source = null;
                    if (opts.EncodeStr != null) {
                        if (opts.Output == null) {
                            // encode a string and print QRCode to console
                            QRPrinter.Print(opts.EncodeStr);
                            return;
                        } else {
                            // encode a string and write QRCode to output dir
                            source = new string[] { opts.EncodeStr };
                        }
                    } else if (opts.File != null) {
                        // encode string(s) in a file and write QRCode(s) to output dir
                        dir = opts.Output ?? Path.Combine(Path.GetDirectoryName(opts.File), Path.GetFileNameWithoutExtension(opts.File));
                        source = ContentReader.ReadFile(opts.File);
                    } else if (opts.DbConnect != null && opts.DbCommand != null && opts.DbRow != null) {
                        // encode string(s) in a database and write QRCode(s) to output dir
                        source = new SQLReader(opts.DbConnect, opts.DbCommand, opts.DbRow);
                    } else {
                        Console.WriteLine("Please input command line arguments.");
                        return;
                    }
                    Directory.CreateDirectory(dir);
                    QRBatchGenerator.Generate(dir, source, opts.Logo);
                } catch (Exception e) {
                    Console.WriteLine("Error: {0} {1}", e.Message, e.StackTrace);
                }
            }).WithNotParsed(errs => {
                Console.WriteLine("Can't parse command line arguments:");
                foreach (var e in errs) {
                    Console.WriteLine(e.ToString());
                }
            });
            Console.Read();
        }
    }
}
