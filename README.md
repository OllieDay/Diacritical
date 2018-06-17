# Diacritical

Library for converting text to pinyin with diacritics.

## Getting started

Install the NuGet package into your application.

### Package Manager

```shell
Install-Package Diacritical
```

### .NET CLI

```shell
dotnet add package Diacritical
```

## Usage

```fsharp
Diacritical.convert "san1ren2xing2bi4you3wo3shi1"
// "sānrénxíngbìyǒuwǒshī"
```

ü can be typed by using v.

```fsharp
Diacritical.convert "lv4"
// "lǜ"
```

Works with uppercase characters too.

```fsharp
Diacritical.convert "WO3 HEN3 XI3HUAN DIACRITICAL!"
// "WǑ HĚN XǏHUAN DIACRITICAL!"
```
