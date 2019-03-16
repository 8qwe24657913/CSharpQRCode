using Gma.QrCodeNet.Encoding;
using System;

namespace CSharpQRCode {
    public class QRPrinter {
        private static QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.M);
        public readonly static int maxLength = 100;
        private readonly static char whiteSpace = '　';//' ';
        private readonly static char blackSpace = '█';
        public static void Print(string content, int padding = 2) {
            if (content.Length > maxLength) throw new Exception("内容过长");
            var bg = Console.BackgroundColor;
            var fr = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            var qrCode = encoder.Encode(content);
            var line = new string(whiteSpace, qrCode.Matrix.Width + padding * padding);
            for (var i = 0; i < padding; i++) {
                Console.WriteLine(line);
            }
            var paddingAside = new string(whiteSpace, padding);
            for (var i = 0; i < qrCode.Matrix.Height; i++) {
                Console.Write(paddingAside);
                for (var j = 0; j < qrCode.Matrix.Width; j++) {
                    Console.Write(qrCode.Matrix[j, i] ? blackSpace : whiteSpace);
                }
                Console.WriteLine(paddingAside);
            }
            for (var i = 0; i < padding; i++) {
                Console.WriteLine(line);
            }
            Console.BackgroundColor = bg;
            Console.ForegroundColor = fr;
        }
    }
}
