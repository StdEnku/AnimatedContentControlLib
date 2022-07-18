# ![Header](./Img/Header.png)

[![Build&Test](https://github.com/StdEnku/AnimatedContentControlLib/actions/workflows/Build&Test.yml/badge.svg)](https://github.com/StdEnku/AnimatedContentControlLib/actions/workflows/Build&Test.yml) <img src="https://img.shields.io/badge/6-Dot%20net-5C2D91.svg?logo=dot-net&style=popout"> <img src="https://img.shields.io/badge/-MIT%20License-666666.svg?logo=&style=popout-square">

## 概要

本ライブラリはContentプロパティ変更時に自作したアニメーションや 組み込まれた標準のアニメーションを実行可能な WPFで使用可能なAnimatedContentControlコントロールを提供するライブラリである。

本ライブラリは二つのアセンブリから構成されている

|                            Nuget                             |           assembly names           |                           remarks                            |
| :----------------------------------------------------------: | :--------------------------------: | :----------------------------------------------------------: |
| ![Nuget](https://img.shields.io/nuget/dt/AnimatedContentControlLib.Core?logo=nuget&style=social) | AnimatedContentControlLib.AnimKeys | WPFに依存しないViewModelで使用可能な<br/>組み込みアニメーション名の定数<br/>を提供するアセンブリ |
| ![Nuget](https://img.shields.io/nuget/dt/AnimatedContentControlLib.Wpf?logo=nuget&style=social) |   AnimatedContentControlLib.Wpf    |        AnimatedContentControl本体を提供するアセンブリ        |

### どちらのアセンブリをインストールすれば良いのか?

ViewとViewModelを同一のプロジェクトで管理する場合AnimatedContentControlLib.Wpfのみの インストールでよい。 AnimatedContentControlLib.WpfはAnimatedContentControlLib.AnimKeysに依存しているので AnimatedContentControlLib.WpfのインストールのみでAnimatedContentControlLib.Coreも付いてくる。

しかし、ViewとViewModelが別プロジェクトの場合 View側のプロジェクトにAnimatedContentControlLib.Wpfをインストールし、 ViewModel側のプロジェクトにAnimatedContentControlLib.AnimKeysをインストールすれば良い。
