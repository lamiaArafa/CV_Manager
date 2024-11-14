using System.Text.RegularExpressions;

namespace CV_Manager.Application.DTOs.Validations
{
    public static class ValidationEmailExtension
    {
        private const string emailRegExp = "^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$";

        public static bool IsValidEmail(this string? email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Regex validateEmailRegex = new Regex(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$");
                return validateEmailRegex.IsMatch(email);
            }
            return false;
        }
    }
}
