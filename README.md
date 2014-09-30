# Caliburn.Micro.ReactiveUI

## What is it?

The goal of this project is to allow better integration of 2 existing MVVM libraries: [Caliburn.Micro](http://caliburnmicro.codeplex.com/) and [ReactiveUI](http://www.reactiveui.net/).

The idea is to be able to easily use Caliburn.Micro's conventions and  screen management with ReactiveUI's asynchronous features.

## What is in it?

The project contains the default screens and conductors provided by Caliburn.Micro that have been rewritten to inherit from `ReactiveObject`. The name of these classes matches their Caliburn.Micro counterpart, prefixed with Reactive.

## What does it support?

It supports the latest versions of Caliburn.Micro (starting with v1.4) and ReactiveUI (starting with v4.3.0), for .Net 4.5, WinRT, Windows Phone and Silverlight 5.

Since Caliburn.Micro is moving to a Portable Class Library with version 2.0, Caliburn.Micro.ReactiveUI will also be bundled as a PCL. The next version will support the framework supported by both Caliburn.Micro and ReactiveUI, namely **.Net 4.5, Windows store apps (for Windows 8 and higher) and Windows Phone 8 and 8.1**. In other words, support for Silverlight 5 will stop at version 1.2.2.

## Where can it be found?

On NuGet, of course! https://nuget.org/packages/caliburn.micro.reactiveui
