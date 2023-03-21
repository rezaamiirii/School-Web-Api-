
namespace School.Application.Interaces
{
    public interface ISmsService
    {
        Task SendVerificationCode(string mobile, string activeCode);
    }
}
