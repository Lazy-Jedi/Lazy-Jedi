# Lazy Jedi

The Lazy Jedi is a light weight Unity Library of really cool Editor Tools and Runtime scripts that will spice up any project
really fast. <br>

Some of the features include:

+ A Project Setup Window
+ Multiple Extension Methods that extend .Net and Unity Classes
+ DataIO and SecureDataIO classes that allows you to easily save and load data to and from a file
+ A Unity Terminal that allows you to execute commands within Unity

Please use the Examples in the Project or the Documentation below to get started.

# Editor

## Project Setup

The Project Setup Window helps you to quickly setup any Unity Project with a few simple clicks.

To open the Project Settings Window, you can Press the following Shortcut "Shift+Alt+P" or
navigate the MenuBar<br>Lazy-Jedi/Setup/Project Setup<br>

![](~Documentation/Images/open-project-settings.png)

Following settings can be edited in the Project Setup Window:

+ Product Icon
+ Cursor Image
+ Cursor Hotspot
    + Only visible if you have a cursor image
+ Company Name
+ Product Name
+ Resources Folder
    + Your Local Resources Folder on your Computer,
    + You can leave it blank if you do not want to add it
+ Project Folders
    + Editable List and the Folders are only created if you click the "**Create Folders**" button

The Company name, Resources Folder and the Folders List is serialized to a .json file on your Machine.<br>
You can find that file at Application.persistentPath + /Uee/LazyJedi

The tool does come with an "Auto Save" feature that will automatically save your settings everytime you make a change to
the Folders List or Company name.
However, if you are not using Auto Save, please use the **"Save Settings"*+ Button.

![](~Documentation/Images/project-setup.png)

## Open

The "Open" menu in Lazy-Jedi allows you to open the Applications Persistent or Data Paths or open a Resources Folder on
your Computer.

![](~Documentation/Images/open.png)

### Resource and Temporary Folders

The Resources Folder is a local folder on your machine. This is the same folder that is linked when you use the Project
Setup window and Select a local Resources folder on your Computer.

The Temporary Folder is a local folder on your machine that is used to save temporary files that are created by
Serializing Scriptable Objects for example.<br>
You can use the Temporary Folder to save any temporary files that you may need to use in your project.

![](~Documentation/Images/open-folders.png)

### Application Paths

The Application Paths are local folders on your machine. These folders are the Persistent and Data Paths that are
relative to your Unity Project.

The "Open/Application Paths" allows you to easily open the Directories for these Paths so that you do not have to browse
your Computer to find them.

![](~Documentation/Images/open-application-paths.png)

### Process Utilities

The Process Utilities has static Methods that shorthand executing Processes via .Net Process method.

+ StartProcess(string filename, bool runAsAdmin = false)
+ StartAdvProcess(string filename, string argument, bool hideWindow = false, bool runAsAdmin = false)

Please note that you do not need to run Processes on another thread, the only reason why I am using async is to avoid
conflicts with the main Unity Thread.

# Runtime

## Data Persistence

LazyJedi comes with a DataIO and SecureDataIO class that allows you to easily save and load data to and from a file.

Note that I have included a Newtonsoft IO Package that uses the Newtonsoft Library to Serialize and Deserialize Data.

### DataIO

DataIO is a static class that allows you to easily save and load data to and from a file.
You can use this class to save and load Serializable Classes, Structs or ScriptableObjects.

The following method Parameters are optional:

+ slotIndex - Default Slot is 1
+ filename - Default filename is "typeof(T).Name"
+ pathType - Default pathType is PathType.DefaultFolder
+ prettyPrint - Default prettyPrint is false

The following strings can be changed at runtime to suit your needs.

```csharp
// Default PathType is PersistentDataPath
string defaultPath = DataIO.DefaultPath;

// Default Save Path is Application.persistentDataPath + "/Saves/"
string savePath = DataIO.SavePath;

// Default Settings Path is Application.persistentDataPath + "/Settings/"
string settingsPath = DataIO.SettingsPath;

// Default Slot Prefix is "Slot_"
string slotPrefix = DataIO.SlotPrefix;

// Default File Extension is "json"
string extension = DataIO.Extension;

```

The following methods can be used to save and load data:

```csharp
// Save Data
DataIO.Save(data, filename: "filename", pathType: PathType.DefaultFolder, prettyPrint: true);

// Save Data to Slot
DataIO.SaveToSlot(data, slotIndex: 1, filename:"filename", pathType: PathType.DefaultFolder, prettyPrint: true);

// Load Data
DataIO.Load<T>(filename: "filename", pathType: PathType.DefaultFolder); // T is the Type of the Data you want to load

// Load Data from Slot
DataIO.LoadFromSlot<T>(slotIndex: 1, filename: "filename", pathType: PathType.DefaultFolder); // T is the Type of the Data you want to load

// Load and Overwrite Data
DataIO.LoadAndOverwrite(data, filename: "filename", pathType: PathType.DefaultFolder);

// Load and Overwrite Data from Slot
DataIO.LoadAndOverwriteFromSlot(slotIndex: 1, filename: "filename", pathType: PathType.DefaultFolder);

// Delete Data
DataIO.Delete<T>(filename: "filename", pathType: PathType.DefaultFolder); // T is the Type of the Data you want to delete

// Delete Data from Slot
DataIO.Delete<T>(slotIndex: 1, filename: "filename", pathType: PathType.DefaultFolder); // T is the Type of the Data you want to delete
```

## Extensions

For Practical examples please look at the Extension Examples in the Examples folder.
The examples will help you understand how to use the Various Extension methods that are available.

### Object Extensions

+ IsNull(),
+ IsNotNull(),
+ ToJson(),
+ Save(),
+ Load(),
+ Overwrite(),

```csharp
MyScriptableObject myScriptableObject;

myScriptableObject.IsNull(); // Returns true if the Object is null
myScriptableObject.IsNotNull(); // Returns true if the Object is not null

myScriptableObject.ToJson(); // Returns the ScriptableObject as a Json String

// Save ScriptableObject to a Json File
myScriptableObject.Save(); // Saves the ScriptableObject to a Json File

// Saves the ScriptableObject to a Json File with the name "MyScriptableObject" and pretty prints the Json
myScriptableObject.Save(filename:"MyScriptableObject", pathType:PathType.Default, prettyPrint: true);

// Saves the ScriptableObject to a Json File with the name "MyScriptableObject" to the first save slot and pretty prints the Json
myScriptableObject.Save(slotIndex:1, filename:"MyScriptableObject", pathType:PathType.Default, prettyPrint: true);

// Overwrite myScriptableObject
myScriptableObject.Overwrite(); // Loads the ScriptableObject data and Overwrites the myScriptableObject

// Overwrite myScriptableObject with the data from the Json File with the name "MyScriptableObject"
myScriptableObject.Overwrite(filename:"MyScriptableObject",pathType:PathType.Default);

// Overwrite myScriptableObject with the data from the first save slot
myScriptableObject.Overwrite(slotIndex:1,filename:"MyScriptableObject",pathType:PathType.Default);
```

### GameObject Extensions

+ Activate(),
+ Deactivate(),
+ Destroy(),
+ Clone(),
+ GetParent(),
+ SetParent(Transform parent),
+ etc

```csharp
// GameObject Extensions
YourObject.Deactivate();
YourObject.Activate();

// Set Parent
YourObject.SetParent(YourParent);

// Get Parent
Transform parent = YourObject.GetParent();
print($"Your Objects Parent - {parent.name}");

// Get Parent GameObject
GameObject goParent = YourObject.GetParentGo();
print($"Your Objects Parent GameObject - {goParent.name}");

// Clone GameObject
GameObject clone = YourObject.Clone();
print($"Your Objects Clone - {clone.name}");

// Destroy GameObject
YourObject.Destroy();
```

### Transform Extensions

+ Activate(),
+ Deactivate(),
+ Clone(),
+ Destroy(),
+ DestroyAllChildren(),
+ SetColliderInteractionLayers(string layerMaskName)

```csharp
// Transform Extensions
// Deactivate
YourParent.Deactivate();

// Activate
YourParent.Activate();

// Clone Parent
Transform clonedParentWithChildren = ParentWithChildren.Clone();
print("Cloned Parent With Children");

// Change Layer Masks of Children that have Colliders
clonedParentWithChildren.SetColliderInteractionLayers(LayerMask.LayerToName(LayerMaskA));
print("Set Collider Interaction Layer");

// Destroy All Children
ParentWithChildren.DestroyAllChildren();
print("Delete Original Parent With Children");

```

### LayerMask Extensions

+ InLayerMask(LayerMask layerMask)

```csharp
// Check if Layer Mask A is in Layer Mask B
print($"Is Layer Mask A, in Layer Mask B - {LayerMaskB.InLayerMask(LayerMaskA)}");
```

### String Extensions

+ IsNullOrEmpty(), IsNullOrWhiteSpace()
+ FromJson(), FromJsonOverwrite()
+ ToBase64(), FromBase64(),
+ ToBytes(), FromBytes()
+ ToShort(), ToInt(), ToFloat(), ToDouble(), ToLong(), ToBool()
+ IsValidEmail_Regex(), IsValidEmail_StrictRegex(), IsValidEmail_MailAddress()
+ IsValidUrl_Regex(), IsValidUrl_Uri()
+ IsValidPhoneNumber(), IsAlphanumeric(), InnerTrim()
+ HasSpecialChars(), RemoveAllSpecialChars(), RemoveSpecialChars_ExclSpaces(), RemoveSpecialChars_ExclPunctuation()

```csharp
// String Conversions to Base64 and back and to Bytes and back
string word = "Hello, World!";

string word64 = word.ToBase64();
string from64 = word64.FromBase64();

byte[] wordBytes = word.ToBytes();
string fromBytes = wordBytes.FromBytes();

print($"Base64 - {word64}");
print($"From Base 64 - {from64}");

print($"Word Bytes Length - {wordBytes.Length}");
print($"From Bytes - {fromBytes}");

if (word.IsNull())
{
    print("Word is Null");
}

if (word.IsNotNull())
{
    print("Word is Not Null");
}

// Parsing a String Int, Float, Double, etc to its Primitive Type
string aStringInt = "1";
int anInt = aStringInt.ToInt();

string aStringFloat = "3.14";
float aFloat = aStringFloat.ToFloat();

string aStringDouble = "3.145";
double aDouble = aStringDouble.ToDouble();

string emailAddress = "your.email.address-123@domain1.com";
string specialCharactersSequence = "A!@#$%^&*()_+=-`~[]B{}\\|;:\'\",<.>/?C";
string wordsWithSpecialChars = "Hello, %^#$World!";
string sentence = "Hello, World!\nLet's go!\nTime for a \"Programming\" Lesson!\nEmail me @ youremail@gmail.com for*any#questions (what language to use).";
string alphaNumeric = "123ABC";
string url = "https://www.google.com";
string mobileNumber = "+27821234567";

print($"Is Valid Email {emailAddress} : {emailAddress.IsValidEmail_MailAddress()}");
print($"Is Valid Email {emailAddress} : {emailAddress.IsValidEmail_Regex()}");
print($"Is Valid Email {emailAddress} : {emailAddress.IsValidEmail_StrictRegex()}");

print($"Is AlphaNumeric {alphaNumeric} - {alphaNumeric.IsAlphanumeric()}");

print($"Has Special Characters {specialCharactersSequence} - {specialCharactersSequence.HasSpecialChars()}");
print($"Remove All Special Characters {specialCharactersSequence} - {specialCharactersSequence.RemoveAllSpecialChars()}");
print($"Remove Special Chars Excl Spaces {wordsWithSpecialChars} - {wordsWithSpecialChars.RemoveSpecialChars_ExclSpaces()}");
print($"Remove Special Chars Excl Punctuation {sentence} - {sentence.RemoveSpecialChars_ExclPunctuation(" ")}");

print($"Is Valid Number {mobileNumber} - {mobileNumber.IsValidPhoneNumber()}");
print($"Is Valid URL {url} - {url.IsValidUrl_Regex()}");
print($"Is Valid URL {url} - {url.IsValidUrl_Uri()}");

```

### IO Extensions

The IO Extensions provide quick and easy access to the File System using the File.Write, File.Read and Stream Writer and Reader classes.

+ WriteText(), WriteBytes(), WriteLines()
+ WriteTextAsync(), WriteBytesAsync(), WriteLinesAsync()
+ ReadText(), ReadBytes(), ReadLines()
+ ReadTextAsync(), ReadBytesAsync(), ReadLinesAsync()
+ StreamWriter(), StreamWriterAsync()
+ StreamReader(), StreamReaderAsync()

**Important** <br>
These methods only check if the file path is not null or empty and if the file has an extension. <br>
Other checks like if the file exists, if the file is read only, etc are not done.

```csharp

// Valid File Path String
string filePath = $"{Application.persistentDataPath}//Test.txt";

// Sync File Write
filePath.WriteText("Hello, World!");
filePath.WriteBytes(new byte[] { 1, 2, 3, 4, 5 });
filePath.WriteLines(new string[] { "Hello", "World!" });

// Async File Write
await filePath.WriteTextAsync("Hello, World!");
await filePath.WriteBytesAsync(new byte[] { 1, 2, 3, 4, 5 });
await filePath.WriteLinesAsync(new string[] { "Hello", "World!" });

// Sync File Read
string text = filePath.ReadText();
byte[] bytes = filePath.ReadBytes();
string[] lines = filePath.ReadLines();

// Async File Read
string textAsync = await filePath.ReadTextAsync();
byte[] bytesAsync = await filePath.ReadBytesAsync();
string[] linesAsync = await filePath.ReadLinesAsync();

// Sync Stream Writer
filePath.StreamWriter("Hello, World!");
filePath.StreamWriter(new byte[] { 1, 2, 3, 4, 5 });

// Async Stream Writer
await filePath.StreamWriterAsync("Hello, World!");
await filePath.StreamWriterAsync(new byte[] { 1, 2, 3, 4, 5 });

// Sync Stream Reader
string streamReaderText = filePath.StreamReader();
byte[] streamReaderBytes = filePath.StreamReaderBytes();

// Async Stream Reader
string streamReaderTextAsync = await filePath.StreamReaderAsync();
```

### Float Extensions

Convert a float to a Time String either a mm:ss (minutes and seconds) or hh:mm:ss (hours, minutes and seconds)

+ ToTimeMS(),
+ ToTimeHMS(),

```csharp

float time = 1800.14f;

// This will print out the time equivalent of the float in Minutes and Seconds
print(time.ToTimeMS());

// This will print out the time equivalent of the float in Hours,Minutes and Seconds
print(time.ToTimeHMS());

```

### Array and List Extensions

+ Shuffle(),
+ GetRandomItem(),
+ Swap()

```csharp
public List<string> WordsList = new List<string>()
{
    "Hello",
    "World!",
    "Simple",
    "Shuffle"
};

public string[] WordsArray = new[]
{
    "Hello",
    "World!",
    "Simple",
    "Shuffle"
};

private void Start()
{
    // Shuffle Elements in an Array or List
    WordsArray.Shuffle();
    WordsList.Shuffle();
    
    WordsArray.Swap(i, j);
    WordsList.Swap(i, j);
    
    string item1 = WordsArray.GetRandomItem();
    string item2 = WordsList.GetRandomItem();

}
```

### Texture2D Extensions

+ ToBase64(),
+ ToTexture2D(),

```csharp

public Texture2D YourTexture2D;

public void Start()
{

    string base64 = YourTexture2D.ToBase64();
    print(base64);
    
    Texture2D newTexture2D = base64.ToTexure2D();
    print(newTexture2D == null);

}

```

### UI Extensions

+ AddListener(UnityAction action),
+ ToBase64(UnityAction action),
+ RemoveAllListeners(),
+ SetPlaceHolderText(string text),

```csharp
public Button YourButton;
public TMP_InputField YourInput;

public void Start()
{
    YourButton.AddListener(ButtonClickEvent);
    YourButton.RemoveListener(ButtonClickEvent);
    YourButton.RemoveAllListeners();
    
    YourInput.placeholder.SetPlaceHolderText("Hi");
}

public void ButtonClickEvent()
{
    print("Something");
}
```

## Utilities

### MathUtility - Created by BluMalice

+ GetValueFromPercentage(float percentage, float min, float max)
+ GetPercentageFromValue(float value, float min, float max)

```csharp
public class ProgressBar : MonoBehaviour
{
    public float Min;
    public float Max;
    
    public void UpdateProgress(float value)
    {
        Fill.amount = MathUtility.GetPercentageFromValue(value, Min, Max);
    }
    
    public float GetCurrentProgress()
    {
        return MathUtility.GetValueFromPercentage(Fill.amount, Min, Max);
    }
}
```

### WebRequestUtility

The WebRequestUtility is a static class that allows you to easily make Web Requests to a Web Server or RestAPI. The WebRequestUtility uses the UnityWebRequest
class to make Web Requests which is compatible with coroutines.

The WebRequestUtility has the following HTTP methods:

```csharp

// HTTP GET METHODS
HttpGet(string url, Action<Response<string>> response, Action<float> progress = null, Dictionary<string, string> headers = null);
HTTPGet(string url, Action<Response<byte[]>> response, Action<float> progress = null, Dictionary<string, string> headers = null);

// HTTP POST METHOD
HttpPost(string url, string body, Action<Response<byte[]>> response, Action<float> progress = null, string contentType = "application/json", Dictionary<string, string> headers = null);

// HTTP PUT METHOD
HttpPut(string url, string body, Action<Response<byte[]>> response, Action<float> progress = null, string contentType = "application/json", Dictionary<string, string> headers = null);

// HTTP DELETE METHOD
HttpDelete(string url, Action<Response<string>> response, Dictionary<string, string> headers = null);

// HTTP HEAD METHOD
HttpHead(string url, Action<Response<Dictionary<string, string>>> response);

```

The WebRequestUtility has the following Downloader and Uploader Methods:

```csharp
// DOWNLOAD METHODS
DownloadAudio(string url, AudioType audioType, Action<AudioResponse> response, Action<float> progress = null, Dictionary<string, string> headers = null);
DownloadTexture(string url, bool isTextureReadable, TextureType textureType, Action<Texture2DResponse> response, Action<float> progress = null, Dictionary<string, string> headers = null);
DownloadAssetBundle(string url, uint checksum, Action<Response<AssetBundle>> response, Action<float> progress = null, Dictionary<string, string> headers = null);
DownloadFileBuffer(string url, Action<Response<byte[]>> response, Action<float> progress = null, Dictionary<string, string> headers = null);
DownloadFile(string url, string path, Action<Response<byte[]>> response, Action<float> progress = null, Dictionary<string, string> headers = null);

// UPLOAD METHODS
UploadFile(string url, string path, HttpMethodType httpMethod, Action<Response<byte[]>> response, Action<float> progress = null, Dictionary<string, string> headers = null);
UploadRawFile(string url, byte[] rawData, HttpMethodType httpMethod, Action<Response<byte[]>> response, Action<float> progress = null, Dictionary<string, string> headers = null);
```

<br>

#### Example

For more examples please look at the Examples->Scripts->Runtime->WebRequest folder.

```csharp
public void Start()
{
    StartCoroutine(WebRequestUtility.DownloadAudio(
                    AudioURL,
                    AudioType,
                    response => { File.WriteAllBytes(Path.Combine("Assets", Path.GetFileName(AudioURL)), response.Data); },
                    progress => { Debug.unityLogger.Log($"Audio Progress - {progress}"); }
                )
            );
    
    StartCoroutine(WebRequestUtility.DownloadTexture(
                ImageURL,
                IsTextureReadable,
                TextureType,
                response =>
                {
                    if (IsTextureReadable)
                    {
                        File.WriteAllBytes(Path.Combine("Assets", Path.GetFileName(ImageURL)), response.Data);
                    }
                },
                progress => { Debug.unityLogger.Log($"Texture Progress - {progress}"); }
            ));
    
    string filePath = Path.Combine("Assets", Path.GetFileName(ZipURL));
            StartCoroutine(WebRequestUtility.DownloadFileBuffer(
                ZipURL,
                response => { File.WriteAllBytes(filePath, response.Data); },
                progress => { Debug.unityLogger.Log($"File Progress - {progress}"); }
            ));
}

```

### EaseUtility

The EaseUtility is a static class that allows you to easily create Easing Functions that can be used to animate UI Elements or GameObjects.
You can also use the Easing Functions to change the value of a variable over time.

The EaseUtility has the following EaseType's Functions:
+ Linear
+ InQuad, OutQuad, InOutQuad
+ InCubic, OutCubic, InOutCubic
+ InQuart, OutQuart, InOutQuart
+ InQuint, OutQuint, InOutQuint
+ InSine, OutSine, InOutSine
+ InExpo, OutExpo, InOutExpo
+ InCirc, OutCirc, InOutCirc
+ InElastic, OutElastic, InOutElastic
+ InBack, OutBack, InOutBack
+ InBounce, OutBounce, InOutBounce

```csharp

// Notes:
// from - Starting Value or Current Value
// to - Ending or Target Value
// time - Fraction between [0, 1], calculated as time / duration

// Example's using Ease Functions
EaseUtility.Float(EaseType, from, to, time);
EaseUtility.Vector2(EaseType, from, to, time);
EaseUtility.Vector3(EaseType, from, to, time);
EaseUtility.Color(EaseType, from, to, time);

// Example's using Animation Curves
EaseUtility.Float(from, to, time, animationCurve);
EaseUtility.Vector2(from, to, time, animationCurve);
EaseUtility.Vector3(from, to, time, animationCurve);
EaseUtility.Color(from, to, time, animationCurve);

// You can also use the EaseUtility to access the Ease Methods
EaseUtility.Linear(time);
EaseUtility.InQuad(time);
EaseUtility.InElastic(time);

// You can also use the Evaluate Method to apply the Ease Function to a value

public EaseType EaseType = EaseType.InOutQuad;

public IEnumerator TestEase()
{
    float startValue = 0f;
    float endValue = 4f;
    float time = 0f;
    float duration = 1f;
    Vector3 position = transform.position;
    
    while(time < duration)
    {
        position.y = start + (endValue - startValue) * EaseUtility.Evaluate(EaseType, time / duration);
        // position.y = EaseUtility.Float(EaseType, startValue, endValue, time / duration); This is the Preferred Way.
        trasform.position = position;
        yield return null;
        time += Time.deltaTime;
    }
    
    position.y = endValue;
    transform.position = position;
}

```

# Lazy Jedi Addons

+ [Lazy Seven Zip](https://github.com/Lazy-Jedi/lazy-seven-zip)
+ [Lazy Palette Swapper](https://github.com/Lazy-Jedi/lazy-palette-swapper)
+ [Lazy Sprite Extractor](https://github.com/Lazy-Jedi/lazy-sprite-extractor)

# Credits

## Creators

1. Mentor - [BluMalice](https://github.com/BLUDRAG)
2. Cristian Alexandru Geambasu - [Daemon3000](https://gist.github.com/daemon3000)

## Assets

1. Kenney - [Fonts](https://www.kenney.nl/assets)
2. Omnibus-Type - MuseoModerno - [Font](https://fonts.google.com/specimen/MuseoModerno?query=Museo)
3. Andrew Paglinawan - Quicksand - [Font](https://fonts.google.com/specimen/Quicksand?query=quicksand)
4. Hubert and Fischer - Rubik - [Font](https://fonts.google.com/specimen/Rubik?query=rubik)

## Packages

1. Kryzarel - [Easing Functions](https://gist.github.com/Kryzarel/bba64622057f21a1d6d44879f9cd7bd4)
2. UniTask - [UniTask](https://github.com/Cysharp/UniTask)
3. NuGetForUnity - [NuGet](https://github.com/GlitchEnzo/NuGetForUnity)

## Plugins

1. SevenZipSharp - [SquidBox](https://github.com/squid-box/SevenZipSharp)
2. 7Zip - [7Zip](https://www.7-zip.org/)

## Icons

1. FlatIcon - [Star Wars](https://www.flaticon.com/free-icons/star-wars)
