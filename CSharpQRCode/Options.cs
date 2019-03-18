using CommandLine;

namespace CSharpQRCode {
    internal class Options {
        [Value(0, MetaName ="encodeStr", Required = false, HelpText = "Assign a string as input.")]
        public string EncodeStr { get; set; }

        [Option('f', "file", Required = false, HelpText = "Assign strings in a file as input.")]
        public string File { get; set; }
        [Option('e', "encoding", Required = false, HelpText = "Specify encoding for txt files. Default value is UTF8.", Default = "UTF8")]
        public string Encoding { get; set; }

        [Option('d', "db-connect", Required = false, HelpText = "Assign strings in a database as input. check https://www.connectionstrings.com/ and https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring for info about connectString.")]
        public string DbConnect { get; set; }
        [Option('c', "db-command", Required = false, HelpText = "SQL command")]
        public string DbCommand { get; set; }
        [Option('r', "db-row", Required = false, HelpText = "Row to select")]
        public string DbRow { get; set; }

        [Option('o', "output", Required = false, HelpText = "Assign output dir for batch encoding.")]
        public string Output { get; set; }

        [Option('l', "logo", Required = false, HelpText = "Specify a path for logo on QRCode. Have no effect when printing QRCode to console.")]
        public string Logo { get; set; }
    }
}
