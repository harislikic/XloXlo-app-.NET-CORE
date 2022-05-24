using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vonage;
using Vonage.Request;


namespace haris_edin_rs1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {

        public class Sms
        {
            public string Brojtelefona { get; set; }
            public string Poruka { get; set; }

        }

        [HttpPost]
        public IActionResult SMS ([FromBody] Sms x)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
         "13f9c53c",
         "EPdD3PQwQfMT8t9D"
        );
            var VonageClient = new VonageClient(credentials);

            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = x.Brojtelefona,    //38762547231  npr
                From = "Xloxlo.app",
                Text = x.Poruka,
            });

            return Ok(response);
        }
        
    }
}
