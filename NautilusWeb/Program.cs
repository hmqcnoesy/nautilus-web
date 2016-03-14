using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Thermo.Informatics.Web.Nautilus.COMAdapter;

namespace NautilusWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = File.ReadAllText("samplelogin.xml");
            var sampleName = LoginSampleUsingWebService("", "", "", xml);
        }


        public static string LoginSampleUsingWebService(string username, string password, string wsdlUrl, string sampleLoginXml)
        {
            NauHelper helper = new NauHelper();
            helper.IMessageHelper_SetUserDetails(username, password, string.Empty);
            helper.IMessageHelper_TargetService = wsdlUrl;
            helper.initialise();

            string xmlResponse = helper.ProcessXML(sampleLoginXml);
            helper.Logout();

            //parse the sample_id from the response xml
            var doc = XDocument.Parse(xmlResponse);
            var sample = doc.XPathSelectElement("//SAMPLE");

            return sample.Element("NAME").Value;
        }
    }
}
