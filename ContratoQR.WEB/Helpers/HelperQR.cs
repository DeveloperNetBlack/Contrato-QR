using QRCoder;

namespace ContratoQR.WEB.Helpers
{
    public class HelperQR
    {
        public string GenerateQRCode(string? texto)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(texto!, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode bitmapByteQRCode = new BitmapByteQRCode(qrData);
            var bitMap = bitmapByteQRCode.GetGraphic(20);

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bitMap);

                byte[] byteImg = ms.ToArray();
                string base64String = Convert.ToBase64String(byteImg);

                return base64String;
            }
        }

    }
}
