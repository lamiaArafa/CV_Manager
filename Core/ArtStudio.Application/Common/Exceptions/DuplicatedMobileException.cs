namespace CV_Manager.Application.Common.Exceptions
{
    public class DuplicatedMobileException : BadRequestException
    {
        public DuplicatedMobileException() : base("Mobile already Exist before") { }
    }
}
