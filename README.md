# DI.WeatherApp
[SOLID] Dependency Inversion. What? How? 

![.net build workflow](https://github.com/rostech/DI.WeatherApp/actions/workflows/dotnet.yml/badge.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

### 🧑‍🎓 Disclaimer:
This summary on DI is based on my understanding of the DI principle (after research) and is for learning purposes. It's open for discussion and improvements. You can demo source code below.

# Dependency Inversion
## 🧠 Definition
#### High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should not depend on abstractions.

## The 🎯 of dependency inversion principle
Is to avoid this highly coupled distribution with the mediation of an abstract layer and to increase the re-usability of higher/policy layers. It promotes loosely coupled architecture, flexibility, pluggabiliy within our code.

## 🥇 Golden rule
High-level modules should own the abstraction otherwise the dependency is not inverted!

## 🚀 Summary 
- **Dependency Inversion** **Principle**- Higher-level component owns the interface. Lower-level implements.
- **Dependency Injection** **Pattern** - Frequently, developers confuse IoC with DI. As mentioned previously, IoC deals with object creation being inverted, whereas DI is a pattern for supplying the dependencies since the control is inverted.
- **Inversion of Control** -  is the **technique** for inverting the control of object creation and **not the actual pattern for making it happen.**
- **Dependency Inversion** promotes loosely coupled architecture, flexibility, pluggability within our code
- Without **Dependency injection**, there is no Dependency inversion
