using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Security.Policy;
using System.Text;
using School.Domain.DTOs;

namespace BootCampCoreProject.Web.Controllers
{
    public class PaymentController : Controller
    {
        string merchant = "cfa83c81-89b0-4993-9445-2c3fcd323455";
        string amount = "1100";
        string authority;
        string description = "خرید تستی ";
        string callbackurl = "https://localhost:44391/Payment/VerifyByHttpClient";



        public async Task<IActionResult> PaymenBytHttpClient()
        {

            try
            {

                using (var client = new HttpClient())
                {
                    RequestParameters parameters = new RequestParameters(merchant, amount, description, callbackurl, "", "");

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.requestUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jo = JObject.Parse(responseBody);
                    string errorscode = jo["errors"].ToString();

                    JObject jodata = JObject.Parse(responseBody);
                    string dataauth = jodata["data"].ToString();
                    if (dataauth != "[]")
                    {
                        authority = jodata["data"]["authority"].ToString();

                        string gatewayUrl = URLs.gateWayUrl + authority;

                        return Redirect(gatewayUrl);

                    }
                    else
                    {

                        return BadRequest("error " + errorscode);


                    }

                }


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }
            return NotFound();
        }

        public async Task<IActionResult> VerifyByHttpClient()
        {
            try
            {

                VerifyParameters parameters = new VerifyParameters();


                if (HttpContext.Request.Query["Authority"] != "")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }

                parameters.authority = authority;

                parameters.amount = amount;

                parameters.merchant_id = merchant;


                using (HttpClient client = new HttpClient())
                {

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.verifyUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jodata = JObject.Parse(responseBody);

                    string data = jodata["data"].ToString();

                    JObject jo = JObject.Parse(responseBody);

                    string errors = jo["errors"].ToString();

                    if (data != "[]")
                    {
                        string refid = jodata["data"]["ref_id"].ToString();


                        return Ok(refid);
                    }
                    else if (errors != "[]")
                    {

                        string errorscode = jo["errors"]["code"].ToString();

                        return BadRequest($"error code {errorscode}");

                    }
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            return NotFound();
        }
    }
}
