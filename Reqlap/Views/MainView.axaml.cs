using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Reqlap.ViewModels;
using System;

namespace Reqlap.Views;

public partial class MainView : UserControl
{
    public string BackgroundImagePath { get; }

    public MainView()
    {
        InitializeComponent();
/*
        if (ActualThemeVariantChanged  != null)

        var mainViewModel = (DataContext as MainViewModel)!;
        var themeName = mainViewModel.ActualThemeVariant.Key.ToString()!.ToLower();
        var backgroundBitmap = new Bitmap(
            AssetLoader.Open(new Uri($"avares://Reqlap/Assets/bg720{themeName}.jpg")));
        Background = new ImageBrush(backgroundBitmap);*/
    }

}
