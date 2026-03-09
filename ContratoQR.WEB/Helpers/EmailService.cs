using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace ContratoQR.WEB.Helpers
{
    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "codigoqrsalud@gmail.com";
        private readonly string _appPassword = "jcrwltsxciadccrv"; // App Password de 16 caracteres

        /// <summary>
        /// Envía un correo electrónico mediante Gmail con archivo adjunto opcional.
        /// </summary>
        /// <param name="destinatario">Correo del destinatario</param>
        /// <param name="asunto">Asunto del mensaje</param>
        /// <param name="cuerpo">Cuerpo del mensaje (admite HTML)</param>
        /// <param name="rutaArchivo">Ruta del archivo adjunto (null si no hay adjunto)</param>
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo, string rutaArchivo = null!)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Depto. Salud Quilicura", _senderEmail));
            mensaje.To.Add(new MailboxAddress("", destinatario));
            mensaje.Subject = asunto;

            var builder = new BodyBuilder();
            builder.HtmlBody = cuerpo; // Cambia a TextBody si no usas HTML

            // Adjuntar archivo si se especifica
            if (!string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo))
            {
                builder.Attachments.Add(rutaArchivo);
            }

            mensaje.Body = builder.ToMessageBody();

            using (var cliente = new SmtpClient())
            {
                cliente.Connect(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
                cliente.Authenticate(_senderEmail, _appPassword);
                cliente.Send(mensaje);
                cliente.Disconnect(true);
            }
        }

        // Versión asíncrona (recomendada para apps web o de escritorio)
        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo, string rutaArchivo = null!)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Depto. Salud Quilicura", _senderEmail));
            mensaje.To.Add(new MailboxAddress("", destinatario));
            mensaje.Subject = asunto;

            var builder = new BodyBuilder();
            builder.HtmlBody = cuerpo;

            if (!string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo))
            {
                builder.Attachments.Add(rutaArchivo);
            }

            mensaje.Body = builder.ToMessageBody();

            using (var cliente = new SmtpClient())
            {
                await cliente.ConnectAsync(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
                await cliente.AuthenticateAsync(_senderEmail, _appPassword);
                await cliente.SendAsync(mensaje);
                await cliente.DisconnectAsync(true);
            }
        }
    }
}
