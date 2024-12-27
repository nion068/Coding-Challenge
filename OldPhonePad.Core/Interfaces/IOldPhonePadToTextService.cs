namespace OldPhonePad.Core.Interfaces;

public interface IOldPhonePadToTextService
{
    public string ConvertOldPhonePadInputToText(string input);
    (bool IsValid, string ErrorMessage) ValidateInput(string input);
}
