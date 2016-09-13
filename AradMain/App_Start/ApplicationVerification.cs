using System;
using System.Web.Configuration;

namespace AradMain
{
    public class ApplicationVerification
    {
        public static void Check()
        {
            if (WebConfigurationManager.AppSettings["RecaptchaPublicKey"].ToUpper() == "CHANGEME") { throw new Exception("Web Config is missing a Recaptcha Public Key"); }
            if (WebConfigurationManager.AppSettings["RecaptchaPrivateKey"].ToUpper() == "CHANGEME") { throw new Exception("Web Config is missing a Recaptcha Private Key"); }
        }
    }
}