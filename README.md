# DLVbriControl

A lightweight Windows utility for adjusting the brightness of DisplayLink-based external monitors through virtual brightness control buttons.

## Overview

**DLVbriControl** is a small Windows desktop application designed for users who use DisplayLink-based external monitors and need a simple way to adjust display brightness.

Some DisplayLink monitors may not expose standard brightness controls through Windows display settings. This application provides virtual brightness buttons to make brightness adjustment more convenient.

## Features

- Adjust brightness for DisplayLink-based monitors
- Simple Windows desktop interface
- Lightweight and easy to use
- Built with C# and WPF
- Suitable for external monitors connected through DisplayLink adapters or docking stations

## Use Case

This tool is useful when:

- Your external monitor is connected through DisplayLink
- Windows built-in brightness controls are unavailable
- The monitor does not provide convenient physical brightness buttons
- You want a small desktop utility for quick brightness adjustment

## Tech Stack

- C#
- WPF
- Windows desktop application
- DisplayLink brightness control library

## Project Structure

```text
DLVbriControl/
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── DLVbriControl.csproj
├── DLVbriControl.sln
├── DLVbri.dll
├── App.config
├── LICENSE
└── README.md
```

## Getting Started

### Requirements
- Windows
- DisplayLink driver installed
- Visual Studio
- .NET desktop development workload

### Build from Source
1. Clone the repository:
```bash
git clone https://github.com/Markyan04/DLVbriControl.git
```
2. Open the solution file in Visual Studio:
```bash
DLVbriControl.sln
```
3. Build the project.
4. Run the application.

### Notes

This application is intended for DisplayLink-based monitor setups. It may not work with monitors that do not use DisplayLink or do not expose compatible brightness control interfaces.

### License

This project is licensed under the MIT License.
