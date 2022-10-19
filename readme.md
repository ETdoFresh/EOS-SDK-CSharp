# EOS-SDK-Unity
A minimal wrapper for Epic Online Services (EOS-SDK) for Unity.

Thank [ETdoFresh](https://github.com/sponsors/etdofresh) with your support.


## Installation

### Unity Package (git url)

1. Open Unity Package Manager. **Unity > Window > Package Manager**
2. Add a package from git URL. **Package Manager > + > Add package from git URL...**
3. Enter this url: `https://github.com/ETdoFresh/EOS-SDK-Unity.git`

### Direct Download (Assets Folder)

Download this repository into an **EOS-SDK-Unity** folder in your **Assets** folder.

### Direct Download (Packages Folder)

Download this repository into an **EOS-SDK-Unity** folder in your **Packages** folder.


## Setting Up

Before you can use EOS-SDK-Unity, you must first set up and acccount and project on https://dev.epicgames.com. 
You can find the instructions for this [here](https://dev.epicgames.com/docs/game-services/services-quick-start#setting-up-an-account).

After downloading this package and setting up your account, you can enter your information in Unity:
1. Open Edit > Project Settings > Epic Online Services for Unity
2. Enter the following information found on your EOS Project Settings Page
   1. Product Name
   2. Product Version
   3. Product Id
   4. Sandbox Id
   5. Deployment Id
   6. Client Id
   7. Client Secret
   8. Encryption Key [random 16 byte, 32 character hex string]
      1. I used this online [GUID Generator](https://www.guidgenerator.com/online-guid-generator.aspx) (no hyphens).


## Usage

### EOS PlatformInterface
All interactions with EOS goes through the EOS-SDK provided **PlatformInterface** object. 

```cs
EOS.GetPlatformInterface();
```

Behind the scenes, this creates an **EOS** GameObject that gets (and will properly disposes of) the PlatformInterface object.
Additionally, this wrapper handles locating and loading DLLs by platform [in editor or build].

### EOS Logging
The only other custom interaction within this wrapper is logging. To set the logging level...

```csharp
EOSLogging.SetLogLevel(LogLevel.Error); // Off, Fatal, Error, Warning, Info, Verbose, VeryVerbose
```

You can however, also simply use the built in EOS LoggingInterface. 
But most Unity users I believe will want at least error logging to appear by default in the console. 
That being said, the other custom logging access is whether to log to the console or not...

```csharp
EOSLogging.LogToUnityDebug = true; // Set true by default
```

### Supported Platforms
Currently, this package has been tested on Windows and Mac. It should work on Linux, but has not been tested.

However, with a little modification, it should be able to support all systems listed [here](https://dev.epicgames.com/docs/game-services/platforms#platform-specific-documentation).