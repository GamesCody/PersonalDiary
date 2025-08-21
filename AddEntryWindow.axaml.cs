using Avalonia.Controls;
using System;

namespace HelloGuiApp;

public partial class AddEntryWindow : Window
{
    public AddEntryWindow()
    {
        InitializeComponent();
        CountChars();
    }

    private void OnSaveClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var title = TitleBox.Text?.Trim();
        var content = EntryTextBox.Text?.Trim();

        if (string.IsNullOrWhiteSpace(title))
        {
            return;
        }

        var newEntry = new Entry
        {
            Title = TitleBox.Text ?? "",
            Content = EntryTextBox.Text ?? "",
            Date = DateTime.Now
        };

        Close(newEntry);
    }
    private void CountChars()
    {
        int count = EntryTextBox.Text?.Length ?? 0;
        CharCounter.Text = $"{count}/2048";
    }
    private void OnTextChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("Text changed!");
        CountChars();
    }

    public void FillWithEntry(Entry entry)
    {
        TitleBox.Text = entry.Title;
        EntryTextBox.Text = entry.Content;
    }
    public void SetEditingMode()
    {
        this.Title = "Edit entry";
        SaveButton.Content = "Save Changes"; 
    }

}
