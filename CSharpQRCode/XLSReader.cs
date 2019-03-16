using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;

namespace CSharpQRCode {
    internal class XLSReader : ContentReader {
        public string Path { get; private set; }
        public bool Xlsx { get; private set; }
        public XLSReader(string path, bool xlsx = true) {
            Path = path;
            Xlsx = xlsx;
        }
        public override IEnumerator<string> GetEnumerator() {
            using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read)) {
                var workbook = Xlsx ? new XSSFWorkbook(fs) as IWorkbook : new HSSFWorkbook(fs) as IWorkbook;
                var sheet = workbook.GetSheetAt(0);
                for (int i = 0; i < sheet.LastRowNum; i++) {
                    var row = sheet.GetRow(i);
                    if (row != null && row.LastCellNum != 0) {
                        yield return row.GetCell(0).ToString();
                    }
                }
                workbook.Close();
            }
        }
    }
}
