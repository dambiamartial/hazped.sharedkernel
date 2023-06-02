namespace hazped.sharedkernel.Common;

public static class OTPGenerator
{
    public static string GenerateOTP(int otpdigitNumber)
    {
        string num = "0123456789";
        int len = num.Length - 1;
        string otp = string.Empty;
        string finalDigit;
        int getIndex;
        for (int i = 0; i < otpdigitNumber; i++)
        {
            do
            {
                getIndex = new Random().Next(0, len);
                finalDigit = num.ToCharArray()[getIndex].ToString();
            } while (otp.IndexOf(finalDigit) != -1);
            otp += finalDigit;
        }
        return otp;
    }
}
