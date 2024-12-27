namespace OldPhonePad.Core.Constants;

public static class OldPhonePadConstants
{
    public const char SendKey   = '#';
    public const char BackSpace = '*';
    public const char Pause     = ' ';
    public const char SpaceKey  = '0';

    public static readonly Dictionary<char, string> PhoneKeyToCharactersMapping = new()
    {
        {SpaceKey,  " " },
        {'1',       "&'(" },
        {'2',       "ABC"},
        {'3',       "DEF"},
        {'4',       "GHI"},
        {'5',       "JKL"},
        {'6',       "MNO"},
        {'7',       "PQRS"},
        {'8',       "TUV"},
        {'9',       "WXYZ"},
    };
}
