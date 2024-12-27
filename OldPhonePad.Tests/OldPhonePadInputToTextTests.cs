using OldPhonePad.Core.Services;

namespace OldPhonePad.Tests;

public class OldPhonePadInputToTextTests
{
    [Fact]
    public void ConvertOldPhonePadInputToText_ShouldReturnEmptyOutputForEmptyInput()
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText("#");

        Assert.Empty(output);
    }

    [Fact]
    public void ConvertOldPhonePadInputToText_ShouldThrowErrorForInvalidKeyInInput()
    {
        var service = new OldPhonePadToTextService();

        Assert.Throws<ArgumentException>(() =>
            service.ConvertOldPhonePadInputToText("1234a#"));
    }

    [Fact]
    public void ConvertOldPhonePadInputToText_ShouldThrowErrorIfInputContainsMultipleSendKey()
    {
        var service = new OldPhonePadToTextService();

        Assert.Throws<ArgumentException>(() =>
            service.ConvertOldPhonePadInputToText("1234##"));
    }

    [Fact]
    public void ConvertOldPhonePadInputToText_ShouldThrowErrorIfInputDoesNotEndWithSendKey()
    {
        var service = new OldPhonePadToTextService();

        Assert.Throws<ArgumentException>(() =>
            service.ConvertOldPhonePadInputToText("1234#1"));
    }

    [Fact]
    public void ConvertOldPhonePadInputToText_ShouldReturnEmptyOutputIfBackspacesClearAllTheCharacters()
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText("7**#");

        Assert.Empty(output);
    }
    
    [Fact]
    public void ConvertOldPhonePadInputToText_PressingKeyMultipleTimesShouldCycleThroughCharacters()
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText("2222#");

        Assert.Equal("A", output);
    }

    [Fact]
    public void ConvertOldPhonePadInputToText_MustPauseForASecondToGetTwoCharactersFromSameButton()
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText("222 2#");

        Assert.Equal("CA", output);
    }

    [Fact]
    public void ConvertOldPhonePadInputToText_SampleTests()
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText("227*#");

        Assert.Equal("B", output);


        output = service.ConvertOldPhonePadInputToText("4433555 555666#");

        Assert.Equal("HELLO", output);

        output = service.ConvertOldPhonePadInputToText("8 88777444666*664#");

        Assert.Equal("TURING", output);
    }
}
