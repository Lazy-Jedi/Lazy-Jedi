# Lazy Jedi

The Lazy Jedi is a lite Unity Library of really cool Editor Tools and Runtime scripts that will spice up any project really fast.

# Editor

## Project Setup

The Project Setup Window helps you to quickly setup any Unity Project with a few simple clicks.

To open the Project Settings Window, you can Press the following Shortcut "Shift+Alt+P" or
navigate the MenuBar<br>Lazy-Jedi/Setup/Project Setup<br>
![](~Documentation/Images/open-project-settings.png)

Following settings can be edited in the Project Setup Window:

* Product Icon
* Cursor Image
* Cursor Hotspot (only visible if you have a cursor image)
* Company Name
* Product Name
* Resources Folder (Your Local Resources Folder on your Computer)
* Project Folders (Editable List and the Folders are only created if you click the "**Create Folders**" button)

The Company name, Resources Folder and the Folders List is serialized to a .json file on your Machine.<br>
You can find that file at Application.persistentPath + /Uee/LazyJedi

The tool does come with an "Auto Save" feature that will automatically save your settings everytime you make a change to the Folders List or Company name.
However, if you are not using Auto Save, please use the **"Save Settings"** Button.
![](~Documentation/Images/project-setup.png)

## Open

The "Open" menu in Lazy-Jedi allows you to open the Applications Persistent or Data Paths or open a Resources Folder on your Computer.

![](~Documentation/Images/open.png)

### Resources Folder

The Resources Folder is a local folder on your machine. This is the same folder that is linked when you use the Project Setup<br>
window and Select a local Resources folder on your Computer.

![](~Documentation/Images/open-resources.png)

### Application Paths

The Application Paths are local folders on your machine. These folders are the Persistent and Data Paths that are relative to your Unity Project.

The "Open/Application Paths" allows you to easily open the Directories for these Paths so that you do not have to browse your Computer to find them.

![](~Documentation/Images/open-application-paths.png)

## Create

### Serializable Dictionary Creator

## Unity Terminal

### Command Prompt and PowerShell

### Custom Processes

# Runtime

## Extensions

For Practical examples please look at the Extension Examples in the Examples folder. 
The examples will help you understand how to use the Various Extension methods that are available.

* GameObject - Activate, Deactivate, Destroy, Clone, GetParent, Parent, etc

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

* Transform - Activate, Deactivate, Clone, Destroy, DesroyAllChildren, SetColliderInteractionLayers

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

* LayerMask - InLayerMask
```csharp
// Check if Layer Mask A is in Layer Mask B
print($"Is Layer Mask A, in Layer Mask B - {LayerMaskB.InLayerMask(LayerMaskA)}");
```

* String - ToBase64, FromBase64, ToBytes, FromBytes
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
```

* Array and List - Shuffle
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

// Shuffle Elements in an Array or List
WordsArray.Shuffle();
WordsList.Shuffle();
```

## Utilities

* MathUtility - GetValueFromPercentage(), GetPercentageFromValue()

## Serializable Dictionary

# Packages

## Rotary Heart - Serializable Dictionary Lite

## MackySoft - Serializable References

# Plugins

## Adoconnection - Seven Zip Extractor

Adoconnection's Seven Zip Extractor is used to Extract Archives within Unity. It support multiple archive formats such as .zip, .rar, .7z and many others.

To use the Archive tools, you just need to right-click on an Archive, Choose Archive Selection and then choose to Extract the Archive however you wish.

* Extract Files - Extract to a folder of your choosing
* Extract Here - Extract in the Current Directory
* Extract to Folder - Extract to a Folder in the Current Directory (the folder will have the same name as the archive)
  ![](~Documentation/Images/archive-usecases.png)

# Credits

## Assets

1. Kenney - [Fonts](https://www.kenney.nl/assets)

## Packages

1. Rotary Heart - [Serializable Dictionary Lite](https://assetstore.unity.com/publishers/28547)
2. MackySoft - [Serializable Reference Extensions](https://github.com/mackysoft/Unity-SerializeReferenceExtensions)

## Plugins

1. Adoconnection - [Seven Zip Extractor](https://github.com/adoconnection/SevenZipExtractor)

## Icons

1. FlatIcon - [Star Wars](https://www.flaticon.com/free-icons/star-wars)
