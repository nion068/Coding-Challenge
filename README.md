# C# Coding Challenge

## **Problem Statement**

We need to implement an old phone keypad with alphabetical letters, a backspace key, and a send button.

Each button has a number to identify it and pressing a button
multiple times will
cycle through the letters on it allowing each button to represent more than one letter.

For example, pressing 2 once will return ‘A’ but pressing twice
in succession will return
‘B’.
You must pause for a second in order to type two characters from the same
button
after each other: “222 2 22” -> “CAB”.

### Examples

| Input                   | Output   |
|-------------------------|----------|
| 33#                    | E        |
| 227*#                  | B        |
| 4433555 555666#        | HELLO    |
| 8 88777444666*664#     | TURING    |

## Assumptions for Implementation

- A send key (#) will always be included at the end of every input:

- For the key 0, consecutive key presses will be considered as multiple key presses and won't require any 1-second pause:

- Backspace (*) would be ignored if the output is already empty.


## **How to Run the Project**

### **Prerequisites**

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- An IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (Optional)

### **Steps to Run**

1. **Clone the Repository**:

   ```bash
   git clone <repository-url>
   cd Coding-Challenge
   ```

2. **Open the Solution**:

   - If you want to use an IDE, open the `OldPhonePad.sln` file in Visual Studio or your preferred IDE.

3. **Restore Dependencies**:

   ```bash
   dotnet restore
   ```

4. **Run the Console Application**:

   ```bash
   dotnet run --project OldPhonePad.ConsoleApp
   ```

5. **Interact with the Application**:

   Follow on-screen prompts to input old phone pad key sequences and see the converted text.

    ```
    *** Instructions ***
    1. Enter valid old keypad input to get the text output.
    2. Type "exit" to end the program.

    Enter old keypad input: 222 2#
    Output: CA

    Enter old keypad input: exit
    Exiting Program...
    ```

---

## **How to Test the Project**

1. **Navigate to the Test Project Directory**:

   ```bash
   cd OldPhonePad.Tests
   ```

2. **Run Unit Tests**:

   ```bash
   dotnet test
   ```

3. **View Test Results**:

   - The test output will indicate the pass/fail status of all test cases.

---

## **Documentation**

## Interface: `IOldPhonePadToTextService`

### Namespace
`OldPhonePad.Core.Interfaces`

### Assembly
`OldPhonePad.Core.dll`

### Syntax
```csharp
public interface IOldPhonePadToTextService
```

### Methods

#### `ConvertOldPhonePadInputToText(string input)`
- **Declaration**: 
  ```csharp
  string ConvertOldPhonePadInputToText(string input)
  ```
- **Parameters**: 
  - `input` *(string)*: The old phone pad input sequence to be converted.
- **Returns**: A `string` representing the converted text from the input sequence.
- **Description**: Processes an input sequence of key presses from an old phone keypad and converts it into the corresponding text.
- **Exceptions**:
  - Throws `ArgumentException` if the input is invalid (e.g., does not end with `#` or contains unsupported characters).

---

## Class: `OldPhonePadConstants`

### Namespace
`OldPhonePad.Core.Constants`

### Assembly
`OldPhonePad.Core.dll`

### Description
Defines constants used throughout the OldPhonePad application to represent key mappings and special keys for processing input sequences.

### Syntax
```csharp
public static class OldPhonePadConstants
```

### Fields

#### `PhoneKeyToCharactersMapping`
- **Declaration**: 
  ```csharp
  public static readonly Dictionary<char, string> PhoneKeyToCharactersMapping;
  ```
- **Description**: Maps phone keypad keys to their corresponding character sequences.

#### `BackSpace`
- **Declaration**: 
  ```csharp
  public const char BackSpace = '*';
  ```
- **Description**: Represents the backspace key on the old phone keypad.

#### `Pause`
- **Declaration**: 
  ```csharp
  public const char Pause = ' ';
  ```
- **Description**: Represents a pause in the input sequence.

#### `SendKey`
- **Declaration**: 
  ```csharp
  public const char SendKey = '#';
  ```
- **Description**: Represents the send key, which terminates the input sequence.

---

## Class: `OldPhonePadToTextService`

### Implements
IOldPhonePadToTextService

### Namespace
`OldPhonePad.Core.Services`

### Assembly
`OldPhonePad.Core.dll`

### Description
Provides the core logic for converting old phone pad input sequences into text.

### Syntax
```csharp
public class OldPhonePadToTextService : IOldPhonePadToTextService
```

### Methods

#### `ConvertOldPhonePadInputToText(string input)`
- **Declaration**: 
  ```csharp
  public string ConvertOldPhonePadInputToText(string input)
  ```
- **Parameters**: 
  - `input` *(string)*: The old phone pad input sequence to be converted.
- **Returns**: A `string` representing the converted text from the input sequence.
- **Description**: Implements the conversion of input sequences into corresponding text, including handling special keys like backspace and pause.
- **Exceptions**:
  - Throws `ArgumentException` if the input is invalid (e.g., does not end with `#` or contains unsupported characters).

---

## Class: `Startup`

### Namespace
`OldPhonePad.ConsoleApp`

### Assembly
`OldPhonePad.ConsoleApp.dll`

### Description
Configures and initializes the console application for the OldPhonePad project.

### Syntax
```csharp
public class Startup
```

### Methods

#### `ConfigureServices(IServiceCollection services)`
- **Declaration**: 
  ```csharp
  public void ConfigureServices(IServiceCollection services)
  ```
- **Parameters**: 
  - `services` *(IServiceCollection)*: The service collection to configure.
- **Description**: Registers the necessary services and dependencies for the console application.

---

## Class: `Program`

### Namespace
`OldPhonePad.ConsoleApp`

### Assembly
`OldPhonePad.ConsoleApp.dll`

### Description
The entry point of the console application.

### Syntax
```csharp
public class Program
```

### Methods

#### `Main(string[] args)`
- **Declaration**: 
  ```csharp
  public static void Main(string[] args)
  ```
- **Parameters**: 
  - `args` *(string[])*: Command-line arguments for the application.
- **Description**: Initializes the application and runs the main console interaction loop for processing input sequences.

