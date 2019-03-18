using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharpQRCode {
    class SQLReader : ContentReader {
        private readonly string connectString;
        private readonly string command;
        private readonly string row;
        // check https://www.connectionstrings.com/ for info about connectString
        public SQLReader(string connectString, string command, string row) {
            this.connectString = connectString;
            this.command = command;
            this.row = row;
        }
        public override IEnumerator<string> GetEnumerator() {
            using (var conn = new SqlConnection(connectString)) {
                conn.Open();
                using (var cmd = new SqlCommand(command)) {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        yield return reader.GetString(reader.GetOrdinal(row));
                    }
                    reader.Close();
                }
                conn.Close();
            }
        }
    }
}
