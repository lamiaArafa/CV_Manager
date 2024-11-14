using System.Text.RegularExpressions;

namespace CV_Manager.Application.DTOs.Validations
{
    public static class ValidationNumberExtension
    {
        private const string numberRegExp = "^[0-9]";

        public static bool IsValidNumber(this string? number)
        {
            if (!string.IsNullOrEmpty(number))
                return new Regex(numberRegExp).IsMatch(number);

            return false;
        }
    }
}
