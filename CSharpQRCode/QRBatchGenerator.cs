using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Brushes = System.Drawing.Brushes;

namespace CSharpQRCode {
    public class QRBatchGenerator {
        private static readonly Regex fileNameRegex = new Regex("[\\/\\\\\\:\\*\\?\\<\\>\\|\\\"]");
        private static string NormalizeFileName(string name) {
            return fileNameRegex.Replace(name, "-");
        }
        private static readonly QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
        private static readonly WriteableBitmapRenderer render = new WriteableBitmapRenderer(new FixedModuleSize(10, QuietZoneModules.Two), Colors.Black, Colors.White);
        private static readonly GraphicsRenderer gRender = new GraphicsRenderer(new FixedModuleSize(10, QuietZoneModules.Two), Brushes.Black, Brushes.White);
        public static void Generate(string dir, IEnumerable<string> source, string logoPath = null) {
            Image logo = logoPath != null ? Image.FromFile(logoPath) : null;
            var count = 1;
            foreach (var data in source) {
                if (data == "") continue;
                try {
                    var qrCode = qrEncoder.Encode(data);
                    var picPath = Path.Combine(dir, $"{count.ToString().PadLeft(3, '0')}_{NormalizeFileName(data.Substring(0, Math.Min(4, data.Length)))}.png");
                    if (logoPath != null) {
                        var dSize = gRender.SizeCalculator.GetSize(qrCode.Matrix.Width);
                        var map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
                        var graphics = Graphics.FromImage(map);
                        gRender.Draw(graphics, qrCode.Matrix);
                        var imgPoint = new Point((map.Width - logo.Width) / 2, (map.Height - logo.Height) / 2);
                        graphics.DrawImage(logo, imgPoint.X, imgPoint.Y, logo.Width, logo.Height);
                        map.Save(picPath);
                    } else {
                        using (var fileStream = new FileStream(picPath, FileMode.OpenOrCreate)) {
                            render.WriteToStream(qrCode.Matrix, ImageFormatEnum.PNG, fileStream);
                        }
                    }
                } catch (Exception e) {
                    Console.WriteLine("错误: {0}", e.Message);
                }
                count++;
            }
        }
    }
}
