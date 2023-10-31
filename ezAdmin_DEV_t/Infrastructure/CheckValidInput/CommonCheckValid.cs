using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Infrastructure.CheckValidInput
{
	public class CommonCheckValid
	{
        // Kiểm tra valid dữ liệu cho Name. 
        public static bool IsValidName(string name)
        {
            // Kiểm tra tồn tại ký tự đặc biệt.
            Regex regex1 = new Regex(@"^[a-zA-Z0-9 ]+$");
            if (!regex1.IsMatch(name))
            {
                return false;
            }
            //// Kiểm tra chuỗi có phải là chuỗi số. 
            //Regex regex2 = new Regex(@"^-?\d*\.?\d*$");
            //if (regex2.IsMatch(name))
            //{
            //    return false;
            //}
            // Kiểm tra độ dài ký tự. 
            if (name.Length == 0 || name.Length > 300)
            {
                return false;
            }
            return true;
        }
        // Kiểm tra valid dữ liệu cho Email. 
        public static bool IsValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        // Kiểm tra valid dữ liệu cho Phone. 
        public static bool IsValidPhone(string phone)
        {
            string regex = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
            return Regex.IsMatch(phone, regex, RegexOptions.IgnoreCase);
        }
    }
}

