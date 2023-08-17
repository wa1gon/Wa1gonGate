namespace HamDotNetToolkit;



public class Validations
{

    /// <summary>
    /// This is a callsign validation, but needs work as there is not real standard for non-US callsigns.
    /// Some examples are:
    ///     9A209A
    ///     AX2000
    ///     TP2000CE
    /// Are valid callsigns 
    ///
    /// </summary>
    /// <param name="callSign"></param>
    /// <returns></returns>
    static public bool ValidateCallSign(string callSign)
    {
        int length = callSign.Length;

        if (length >= 3 && length <= 6)
        {
            if (char.IsLetter(callSign[0]) && char.IsLetter(callSign[length - 1]))
            {
                if (length == 3)
                {
                    // 1x1 call sign (one letter, one digit, one letter)
                    if (char.IsDigit(callSign[1]))
                    {
                        return true;
                    }
                }
                else if (length == 4)
                {
                    if (char.IsDigit(callSign[1]) && char.IsDigit(callSign[2]))
                    {
                        // 1x2 call sign (one letter, two digits, one letter)
                        return true;
                    }
                    else if (char.IsDigit(callSign[1]) && char.IsLetter(callSign[2]))
                    {
                        // 2x1 call sign (two letters followed by one digit and one letter)
                        return true;
                    }
                }
                else if (length == 5 && char.IsLetter(callSign[2]) && char.IsDigit(callSign[3]))
                {
                    // 2x3 call sign (two letters followed by a digit and a letter)
                    return true;
                }
                else if (length == 6)
                {
                    // Special event call sign (two letters followed by two digits)
                    return true;
                }
            }
        }

        return false;

        return false;
    }
}
