
using School.Application.Interaces;

namespace School.Application.Services
{

    public class SmsService : ISmsService
    {
        public string apiKey = "7745374B644C4644524A2B75594A4C42456347477248716938326F64707466543672375750495A6C4734303D";
        #region properties
        public async Task SendVerificationCode(string mobile, string activeCode)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);

            await api.VerifyLookup(mobile, activeCode, "HfMobinSchool");
        }
        #endregion

    }
}
