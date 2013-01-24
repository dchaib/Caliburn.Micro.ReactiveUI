# Caliburn.Micro.ReactiveUI

## What is it?

The goal of this project is to allow better integration of 2 existing MVVM libraries: [Caliburn.Micro](http://caliburnmicro.codeplex.com/) and [ReactiveUI](http://www.reactiveui.net/).

The idea is to be able to easily use Caliburn.Micro's conventions and  screen management with ReactiveUI's asynchronous features.

## What is in it?

The project contains the default screens and conductors provided by Caliburn.Micro that have been rewritten to inherit from `ReactiveObject`. The name of these classes matches their Caliburn.Micro counterpart,  prefixed with Reactive.

## What does it support?

For now, this only supports the latest versions of Caliburn.Micro (1.4) and ReactiveUI (4.3.0), for .Net 4.5.

## Where can it be found?

On NuGet, of course! https://nuget.org/packages/caliburn.micro.reactiveui
