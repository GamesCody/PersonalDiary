using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Media;
using System.Reflection.Metadata.Ecma335;

namespace HelloGuiApp;

public partial class MainWindow : Window
{
    private List<Entry> entries = new();
    private string savePath = "entries.json";
    private void LoadEntries()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            entries = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
        }
        else
        {
            entries = new List<Entry>();
            SaveEntries();
        }
    }
    private void SaveEntries()
    {
        string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(savePath, json);
    }
    public void DisplayEntries()
    {


        History.Children.Clear();

        foreach (var entry in entries.OrderByDescending(e => e.Date))
        {
            var contentGrid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            },
                RowDefinitions =
            {
                new RowDefinition(GridLength.Auto)
            }
            };

            var dateText = new TextBlock
            {
                Text = $"Date and time of entry: \n{entry.Date:yyyy-MM-dd HH:mm}\n\n Title: {entry.Title}",
                FontWeight = Avalonia.Media.FontWeight.Bold
            };

            var viewEntry = new Button
            {
                Classes = { "button" },
                Content = "Read"
            };
            var editEntry = new Button
            {
                Classes = { "button" },
                Content = "Edit"
            };
            editEntry.Click += async (_, _) =>
            {
                var editWindow = new AddEntryWindow();
                editWindow.SetEditingMode();
                editWindow.FillWithEntry(entry);
                var updatedEntry = await editWindow.ShowDialog<Entry?>(this);
                if (updatedEntry != null)
                {
                    entry.Title = updatedEntry.Title;
                    entry.Content = updatedEntry.Content;

                    SaveEntries();
                    DisplayEntries();
                }
            };
            var deleteEntry = new Button
            {
                Classes = { "button" },
                Content = "Delete"
            };
            deleteEntry.Click += (_, _) =>
            {
                entries.Remove(entry);
                SaveEntries();
                DisplayEntries();
            };
            var buttonsStack = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 5
            };
            buttonsStack.Children.Add(viewEntry);
            buttonsStack.Children.Add(editEntry);
            buttonsStack.Children.Add(deleteEntry);

            Grid.SetColumn(buttonsStack, 1);
            Grid.SetColumn(dateText, 0);

            contentGrid.Children.Add(dateText);
            contentGrid.Children.Add(buttonsStack);




            var border = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(10, 50, 75)),
                BorderThickness = new Avalonia.Thickness(1),
                CornerRadius = new Avalonia.CornerRadius(4),
                Padding = new Avalonia.Thickness(10),
                Margin = new Avalonia.Thickness(0, 0, 0, 10),
                Child = contentGrid
            };

            History.Children.Add(border);
        }

    }
    private void ChangeTheme_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { 
        Console.WriteLine("Menu item clicked!");
    }
    private void ClearAll_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { 
        Console.WriteLine("Menu item clicked!");
    }
    private void Export_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { 
        Console.WriteLine("Menu item clicked!");
    }
    private void ChangePassword_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) { 
        Console.WriteLine("Menu item clicked!");
    }

    public MainWindow()
    {
        InitializeComponent();
        NewEntry.Click += async (_, _) =>
        {
            var addWindow = new AddEntryWindow();
            var result = await addWindow.ShowDialog<Entry?>(this);

            if (result != null)
            {
                entries.Add(result);
                SaveEntries();
                DisplayEntries();
            }
        };
        Help.Click += (_, _) =>
        {
            var addInfoWindow = new InfoWindow();
            addInfoWindow.Show();
        };


        LoadEntries();
        DisplayEntries();
    }


}
