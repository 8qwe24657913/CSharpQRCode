using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpQRCode {
    internal class PlainTextReader: ContentReader {
        public string Path { get; private set; }
        public Encoding Encoding { get; set; }
        public PlainTextReader(string path, string encoding = "UTF8") {
            Path = path;
            Encoding = encoding != null ? Encoding.GetEncoding(encoding) : Encoding.UTF8;
        }
        public override IEnumerator<string> GetEnumerator() {
            using (var reader = new StreamReader(Path, Encoding)) {
                while (true) {
                    var line = reader.ReadLine();
                    if (line == null) break;
                    yield return line;
                }
            }
        }
    }
}
