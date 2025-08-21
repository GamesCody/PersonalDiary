using Avalonia.Controls;
using System;

namespace HelloGuiApp;

public partial class InfoWindow : Window
{
    public InfoWindow()
    {
        InitializeComponent();
    }
    private void OnCloseClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }



}