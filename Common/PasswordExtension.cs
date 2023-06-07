namespace hazped.sharedkernel.Common;

/// <summary>
/// Class to generate a password and salt for password
/// </summary>
public class PasswordExtension
{
    /// <summary>
    /// Generate a salt with a specific number of digit
    /// </summary>
    /// <param name="SaltDigitNumber">number of salt digit</param>
    /// <returns><see cref="string"/></returns>
    public static string GenerateSalt(int SaltDigitNumber)
    {
        //Créer une variable qui contient tous les caractères spéciaux et les chiffres
        string codes = "&é'(-è_çà)=§/.?µ<>£^¨ù|~#{[|`^@]}*0123456789";

        //Créer un variable qui contient tous les caractères de l'alphabet
        string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        //récupérer la longueur des code
        int lengthCodes = codes.Length - 1;

        //récupérer la longueur des lettres
        int lengthLetters = letters.Length - 1;

        //générer une chaine de caractère vide
        string otp = string.Empty;

        string finalDigit;
        int getIndex;

        //tant que la longueur de la chaine de caractère est inférieur au nombre de chiffre demandé
        for (int i = 0; i < SaltDigitNumber; i++)
        {
            do
            {
                //générer un chiffre aléatoire
                int y = new Random().Next(1, 7);
                //si le chiffre est un multiple du nombre aléatoire
                if (i % y == 0)
                {
                    getIndex = new Random().Next(0, lengthLetters);
                    finalDigit = letters.ToCharArray()[getIndex].ToString();
                }
                else
                {
                    getIndex = new Random().Next(0, lengthCodes);
                    finalDigit = codes.ToCharArray()[getIndex].ToString();
                }
            } while (otp.IndexOf(finalDigit) != -1);

            otp += finalDigit;
        }
        return otp;
    }

    /// <summary>
    /// Generate a password with a specific input password and salt
    /// </summary>
    /// <param name="password">input password</param>
    /// <param name="salt">salt</param>
    /// <returns><see cref="string"/></returns>
    public static string GeneratePassword(string password, string salt) => CustomHashGenerate(password, salt);
    static string CustomHashGenerate(string password, string salt)
    {
        var fullPassword = password + salt;

        return BitConverter.ToString(MD5.HashData(Encoding.ASCII.GetBytes(fullPassword))).Replace("-", "");
    }
}
