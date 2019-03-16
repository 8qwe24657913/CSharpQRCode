using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpQRCode {
    internal class PlainTextReader: ContentReader {
        public string Path { get; private set; }
        public Encoding Encoding { get; set; }
        public PlainTextReader(string path, Encoding encoding = null) {
            Path = path;
            Encoding = encoding ?? Encoding.Default;
        }
        public override IEnumerator<string> GetEnumerator() {
            using (var reader = new StreamReader(Path, Encoding.Default)) {
                while (true) {
                    var line = reader.ReadLine();
                    if (line == null) break;
                    yield return line;
                }
            }
        }
    }
}
