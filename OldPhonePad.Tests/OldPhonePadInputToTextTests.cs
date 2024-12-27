using OldPhonePad.Core.Constants;
using OldPhonePad.Core.Services;
using System.Text;

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
    public void ConvertOldPhonePadInputToText_ShouldThrowErrorNullInInput()
    {
        var service = new OldPhonePadToTextService();

        Assert.Throws<ArgumentException>(() =>
            service.ConvertOldPhonePadInputToText(null));
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
    public void ConvertOldPhonePadInputToText_ConsecutiveSpaceKeyShouldReturnMultipleSpaces()
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText("0002#");

        Assert.Equal("   A", output);
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

    [Theory]
    [InlineData("227*#", "B")]
    [InlineData("4433555 555666#", "HELLO")]
    [InlineData("8 88777444666*664#", "TURING")]
    public void ConvertOldPhonePadInputToText_ReturnsExpectedOutput(string input, string expectedOutput)
    {
        var service = new OldPhonePadToTextService();

        var output = service.ConvertOldPhonePadInputToText(input);

        Assert.Equal(expectedOutput, output);
    }


    [Fact]
    public void ConvertOldPhonePadInputToText_RandomValidInputsShouldReturnCorrectOutputs()
    {
        var service = new OldPhonePadToTextService();

        var randomTestRunningCount = Random.Shared.Next(1, 10);

        for (int test = 0; test < randomTestRunningCount; test++)
        {
            var randomKeyCountForTestCase = Random.Shared.Next(1, 30);

            var (randomInput, expectedOutput) =
                GetRandomInputAndExpectedOutput(randomKeyCountForTestCase);

            var actualOutput = service.ConvertOldPhonePadInputToText(randomInput);

            Assert.Equal(expectedOutput, actualOutput);
        }
    }

    private (string RandomInput, string ExpectedOutput) GetRandomInputAndExpectedOutput(int inputLength)
    {
        var output = new StringBuilder();
        var input = new StringBuilder();

        var phoneKeysWithCharacters = OldPhonePadConstants.PhoneKeyToCharactersMapping;
        var keys = phoneKeysWithCharacters.Keys.ToList();
        char? previousKey = null;

        for (int i = 0; i < inputLength; i++)
        {
            var randomKey = keys[Random.Shared.Next(keys.Count)];

            if (previousKey is not null && previousKey.Value == randomKey)
            {
                input.Append(OldPhonePadConstants.Pause);
            }

            previousKey = randomKey;

            var keyPressCount = Random.Shared.Next(1, 6);

            input.Append(
                string.Join("", Enumerable.Repeat(randomKey, keyPressCount)));

            if (randomKey == OldPhonePadConstants.SpaceKey)
            {
                output.Append(
                    string.Join("", Enumerable.Repeat(phoneKeysWithCharacters[randomKey], keyPressCount).ToList()));
            }
            else
            {
                output.Append(phoneKeysWithCharacters[randomKey][(keyPressCount - 1) % phoneKeysWithCharacters[randomKey].Length]);
            }

            if (Random.Shared.NextDouble() <= 0.2 && Random.Shared.NextDouble() > 0.1)
            {
                input.Append(OldPhonePadConstants.BackSpace);

                if (output.Length > 0)
                {
                    output.Remove(output.Length - 1, 1);
                }
            }
        }

        input.Append(OldPhonePadConstants.SendKey);

        return (input.ToString(), output.ToString());
    }
}