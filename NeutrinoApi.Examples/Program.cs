using System;

namespace NeutrinoApi.Examples
{
    public class Program
    {
        public static void Main(string[] args = null)
        {
            var example = "IpInfo";
            if (args != null && args.Length > 0)
            {
                example = args[0];
            }
            
            switch (example)
            {
                case "BadWordFilter":
                    BadWordFilter.ExecRequest();
                    break;
                case "BinListDownload":
                    BinListDownload.ExecRequest();
                    break;
                case "BinLookup":
                    BinLookup.ExecRequest();
                    break;
                case "BrowserBot":
                    BrowserBot.ExecRequest();
                    break;
                case "Convert":
                    Convert.ExecRequest();
                    break;
                case "EmailValidate":
                    EmailValidate.ExecRequest();
                    break;
                case "EmailVerify":
                    EmailVerify.ExecRequest();
                    break;
                case "GeocodeAddress":
                    GeocodeAddress.ExecRequest();
                    break;
                case "GeocodeReverse":
                    GeocodeReverse.ExecRequest();
                    break;
                case "HlrLookup":
                    HlrLookup.ExecRequest();
                    break;
                case "HostReputation":
                    HostReputation.ExecRequest();
                    break;
                case "HtmlClean":
                    HtmlClean.ExecRequest();
                    break;
                case "HtmlRender":
                    HtmlRender.ExecRequest();
                    break;
                case "ImageResize":
                    ImageResize.ExecRequest();
                    break;
                case "ImageWatermark":
                    ImageWatermark.ExecRequest();
                    break;
                case "IpBlocklist":
                    IpBlocklist.ExecRequest();
                    break;
                case "IpBlocklistDownload":
                    IpBlocklistDownload.ExecRequest();
                    break;
                case "IpInfo":
                    IpInfo.ExecRequest();
                    break;
                case "IpProbe":
                    IpProbe.ExecRequest();
                    break;
                case "PhonePlayback":
                    PhonePlayback.ExecRequest();
                    break;
                case "PhoneValidate":
                    PhoneValidate.ExecRequest();
                    break;
                case "PhoneVerify":
                    PhoneVerify.ExecRequest();
                    break;
                case "QrCode":
                    QrCode.ExecRequest();
                    break;
                case "SmsMessage":
                    SmsMessage.ExecRequest();
                    break;
                case "SmsVerify":
                    SmsVerify.ExecRequest();
                    break;
                case "UaLookup":
                    UaLookup.ExecRequest();
                    break;
                case "UrlInfo":
                    UrlInfo.ExecRequest();
                    break;
                case "VerifySecurityCode":
                    VerifySecurityCode.ExecRequest();
                    break;
                default:
                    Console.WriteLine("Unknown example");
                    break;
            }
        }
    }
}
