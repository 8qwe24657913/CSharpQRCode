using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpQRCode {
    internal abstract class ContentReader : IEnumerable<string> {
        public abstract IEnumerator<string> GetEnumerator();
        // System.Collections.Generic.IEnumerator<string> can't override System.Collections.IEnumerator...
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        public static IEnumerable<string> ReadFile(string path) {
            if (!File.Exists(path)) {
                throw new Exception("文件不存在");
            }
            switch (Path.GetExtension(path).ToLower()) {
                case ".txt":
                    return new PlainTextReader(path);
                case ".xls":
                    return new XLSReader(path, false);
                case ".xlsx":
                    return new XLSReader(path, true);
                default:
                    throw new Exception("无法识别的文件类型");
            }
        }
    }
}
