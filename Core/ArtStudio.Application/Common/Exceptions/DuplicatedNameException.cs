namespace CV_Manager.Application.Common.Exceptions
{
    public class DuplicatedNameException : BadRequestException
    {
        public DuplicatedNameException() : base("Name already Exist before") { }
    }
}
