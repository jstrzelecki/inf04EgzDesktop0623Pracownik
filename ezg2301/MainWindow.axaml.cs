using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ezg2301;

public partial class MainWindow : Window
{
    // Hasło oraz zestawy znaków są przechowywane w zmiennych typu napisowego
    
    // Poszczególne znaki hasła są wybierane losowo z zestawu małych liter
    //Za zestaw liter przyjmuje się wszystkie litery z klawiatury małe i wielkie alfabetu łacińskiego
    private readonly string lowerLetters = "abcdefghijklmnoprstuvwxyz";
    private readonly string upperLetters = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
    
    // Za zestaw cyfr przyjmuje się kolejne cyfry od 0 do 9
    private readonly string numbers = "0123456789";
    
    // Za zestaw znaków specjalnych przyjmuje się znaki !@#$%^&*()_+-=
    private readonly string specChars = "!#$%^&()_+-=";
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnSubmitButton(object? sender, RoutedEventArgs e)
    {
        var name = NameTextBox.Text;
        var surName = SurnameTextBox.Text;
        var position = (PositionComboBox.SelectionBoxItem as ComboBoxItem)?.Content as string;
        var password = string.Empty;
        if (int.TryParse(CharNumberTextBox.Text, out int length))
        {
            password = GeneratePassword(length); 
        }
        
        var content = $"Dane pracownika: {name} {surName} {position} Hasło: {password}";
        
        
        var infoWindow = new InfoWindow(content);
        infoWindow.ShowDialog(this);
    }

    private void OnPassWordButton(object? sender, RoutedEventArgs e)
    {
        var stringPasswordCharNumber = CharNumberTextBox.Text;
        string content = string.Empty;
        if (int.TryParse(stringPasswordCharNumber, out var intPasswordCharNumber))
        {
            content= GeneratePassword(intPasswordCharNumber);
        }
        
        var infoWindow = new InfoWindow(content);
        
        
        infoWindow.ShowDialog(this);
    }

    private void OnNumericTextBox(object? sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            var newString = new string(textBox.Text?.Where(char.IsDigit).ToArray());
            if (newString != textBox.Text)
            {
                textBox.Text = newString;
            }
        }
    }

    string GeneratePassword(int length)
    {
        if (length < 3)
        {
            return string.Empty;
        }
        Random random = new Random();
        char[] password = new char[length];
//Metody w klasie Enumerable umożliwiają wykonywanie operacji takich jak
//    filtrowanie,
//    sortowanie,
//    agregowanie,
//    przekształcanie, itp.
// Oto niektóre popularne metody:
 //   Enumerable.Range() – generuje sekwencję liczb w określonym zakresie.
 //   Enumerable.Select() – stosuje funkcję do każdego elementu kolekcji, przekształcając ją.
 //   Enumerable.Where() – filtruje elementy kolekcji na podstawie warunku.
 //   Enumerable.First() – zwraca pierwszy element kolekcji, który spełnia warunek.
//    Enumerable.Aggregate() – wykonuje agregowanie (np. sumowanie) nad kolekcją.
//    Enumerable.ToList() – konwertuje sekwencję na listę.
 /*       return new string(Enumerable.Range(0, length)
            .Select(_ => lowerLetters[random.Next(lowerLetters.Length)]) // Select to taki map
            .ToArray());
*/
        
       /*
        klasyczna wersja
        char[] haslo = new char[length];
       
        for (int i = 0; i < length; i++)
        {
              haslo[i] = lowerLetters[random.Next(lowerLetters.Length)];
        }
       
        return new string(haslo);*/


       password = (Enumerable.Range(1, length))
           .Select(_ => lowerLetters[random.Next(lowerLetters.Length)]).ToArray();

       if (LettersCheckBox.IsChecked == true)
       {
           InsertRandomCharacter(password, upperLetters, random);
       }

       if (DigitsCheckbox.IsChecked == true)
       {
           InsertRandomCharacter(password, numbers, random);
       }

       if (SpecialCharCheckBox.IsChecked == true)
       {
          InsertRandomCharacter(password, specChars, random);
       }
       
       return new string (password);// aby przekształcić char[] na string
       
    }

    private void InsertRandomCharacter(char[] password, string s, Random random)
    {
        int randomIndex;
        do
        {
            randomIndex = random.Next(password.Length);
            
        } while ((upperLetters.Contains(password[randomIndex]) ||
                  numbers.Contains(password[randomIndex]) ||
                  specChars.Contains(password[randomIndex])));
        
        password[randomIndex] = s[random.Next(s.Length)];

    }
}