using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookMessengerWebhook.Controllers
{
    [Produces("application/json")]
    [Route("api/Webhook")]
    public class WebhookController : Controller
    {
        /// <summary>
        /// Endpoint for getting message
        /// </summary>
        /// <param name="request">Request sent when a user initiate a message</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]WebhookMessageRequest request)
        {
            if (request.Object == "page")
            {
                foreach (var entry in request.Entries)
                {
                    string pageID = entry.Id;

                    foreach (var mEvent in entry.Messagings)
                    {
                        if (mEvent.Message != null)
                        {
                            Console.WriteLine("Message received: " + mEvent.Message.Text);
                        }
                    }
                }
                return Ok("EVENT_RECEIVED");
            }
            return NotFound();
        }

        /// <summary>
        /// Endpoint for verifying webhook
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "hub.mode")] string mode, [FromQuery(Name = "hub.challenge")] string Challenge, [FromQuery(Name = "hub.verify_token")] string VerifyToken)
        {
            string VERIFY_TOKEN = "<YOUR_VERIFY_TOKEN>";
            if (mode == "subscribe" && VerifyToken == VERIFY_TOKEN)
            {
                Console.WriteLine("WEBHOOK_VERIFIED");
                return Ok(Challenge);
            }
            else
            {
                return Forbid();
            }
        }
    }
}