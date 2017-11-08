using System;

namespace CarRental.Helpers
{
    public static class GenerateOTP
    {
        public static string OtpNumber(int count, OtpType otpType)
        {
            string _otp = "";

            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;

            if (OtpType.Alphanumeric == otpType)
            {
                characters += alphabets + small_alphabets + numbers;
            }
            
            for (int i = 0; i < count; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                }
                while (_otp.IndexOf(character) != -1);

                _otp += character;
            }

            return _otp;
        }

        public enum OtpType
        {
            Alphanumeric = 1,
            Numeric = 2
        }
    }
}