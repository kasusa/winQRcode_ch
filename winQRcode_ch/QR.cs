using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Net.Codecrete.QrCodeGenerator;

namespace winQRcode_ch
{
    class QR
    {
        public static void RegenerateQr(int resolution,string text ,PictureBox QrPicture,ProgressBar progressBar1)
        {
            progressBar1.Value = 0;
            if (text.Equals("")) return;
            progressBar1.Value = 80;
            var qr = QrCode.EncodeText(text, QrCode.Ecc.Medium);
            var bitmap = qr.ToBitmap(scale: resolution, border: 1);
            progressBar1.Value = 100;
            //bitmap.Save("qr-code.png", ImageFormat.Png);
            QrPicture.Image = bitmap;
        }

    }
}
