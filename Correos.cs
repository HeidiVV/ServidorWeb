using System.Net.Mail;
public class Correos{
    public  string Destinatario{get;set;}="";
    public string Asunto {get;set;}="";
    public string Mensaje {get;set;}="";
    public void Enviar(){
        MailMessage correo = new MailMessage();
    correo.From = new MailAddress("crusaderscompanyentertainment@gmail.com", null, System.Text.Encoding.UTF8);//Correo de salida
    correo.To.Add(this.Destinatario); //Correo destino?
    correo.Subject = this.Asunto;
    correo.Body = this.Mensaje; //Mensaje del correo
    correo.IsBodyHtml = true;
    correo.Priority = MailPriority.Normal;
    SmtpClient smtp = new SmtpClient();
    smtp.UseDefaultCredentials = false;
    smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
    smtp.Port = 25; //Puerto de salida
    smtp.EnableSsl = true;
    smtp.Credentials = new System.Net.NetworkCredential("crusaderscompanyentertainment@gmail.com", "dupd snin kspi hfqp");//Cuenta de correo
    smtp.Send(correo);
    }
}