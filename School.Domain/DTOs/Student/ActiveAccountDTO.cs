
namespace School.Domain.DTOs.Student
{
    public class ActiveAccountDTO
    {
       
        public string PhoneNumber { get; set; }
        
        public string ActiveCode { get; set; }

    }
    public enum ActiveAccountResult
    {
        Error,
        Success,
        NotFound
    }
}
