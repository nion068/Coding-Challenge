using OldPhonePad.Core.Constants;
using OldPhonePad.Core.Interfaces;
using System.Text;

namespace OldPhonePad.Core.Services;

public class OldPhonePadToTextService : IOldPhonePadToTextService
{
    public string ConvertOldPhonePadInputToText(string input)
    {
        var validationResult = ValidateInput(input);

        if (validationResult.IsValid is false)
        {
            throw new ArgumentException(validationResult.ErrorMessage);
        }

        var output = new StringBuilder();

        var length        = input.Length;
        var keyPressCount = 0;

        for (var i = 0; i < length; i++)
        {
            var currentKey = input[i];

            switch (currentKey)
            {
                case OldPhonePadConstants.SendKey:
                    return output.ToString();

                case OldPhonePadConstants.BackSpace:
                    if (output.Length > 0)
                    {
                        output.Remove(output.Length - 1, 1);
                    }

                    break;

                case OldPhonePadConstants.Pause:
                    break;

                default:
                    if (i + 1 < length && currentKey == input[i+1])
                    {
                        keyPressCount++;
                        break;
                    }

                    output.Append(
                        GetCharacterForOldPhoneKeyAndKeyPressCount(
                            currentKey, keyPressCount));

                    keyPressCount = 0;

                    break;
            }
        }

        return output.ToString();
    }

    public (bool IsValid, string ErrorMessage) ValidateInput(string input)
    {
        if (input is null)
        {
            return (false, "Input cannot be null");
        }
        if (input.Any(ch =>
            OldPhonePadConstants.PhoneKeyToCharactersMapping.ContainsKey(ch) is false
            && ch != OldPhonePadConstants.SendKey
            && ch != OldPhonePadConstants.Pause
            && ch != OldPhonePadConstants.BackSpace))
        {
            return (false, "Input contains invalid key for old phone");
        }

        if (input.EndsWith(OldPhonePadConstants.SendKey) is false)
        {
            return(false, "Input must end with a send key #");
        }

        if (input.Where(ch => ch == OldPhonePadConstants.SendKey).Count() > 1)
        {
            return(false, "Input contains multiple # key");
        }

        return (true, "");
    }

    private char GetCharacterForOldPhoneKeyAndKeyPressCount(char phoneKey, int keyPressCount)
    {
        if (OldPhonePadConstants.PhoneKeyToCharactersMapping.ContainsKey(phoneKey) is false)
        {
            throw new ArgumentException($"Invalid phone key: {phoneKey}");
        }

        var charactersForKey = OldPhonePadConstants.PhoneKeyToCharactersMapping[phoneKey];

        return charactersForKey[keyPressCount % charactersForKey.Length];
    }
}
