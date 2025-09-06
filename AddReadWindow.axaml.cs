using Avalonia.Controls;
using System;

namespace HelloGuiApp;

public partial class AddReadWindow : Window
{
    public AddReadWindow()
    {
        InitializeComponent();
    }

    public void FillWithEntry(Entry entry)
    {
        EntryTitle.Text = entry.Title;
        EntryText.Text = entry.Content;
    }

}
