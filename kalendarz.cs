// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

Console.WriteLine("Program do wyświetlania kalendarza\n");

//Wyswietlenie roku
while (true)
{
    try
    {
        //Zapytanie o rok
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Podaj rok kalendarzowy [1-10000] lub \'q\' aby wyjść: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        string srok = Console.ReadLine();
        Console.ResetColor();
        if (srok == "q")
            break;
        int rok = int.Parse(srok);
        if (rok < 1 || rok > 10000)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Niepoprawny zakres roku. Powinien być w zakresie [1-10000].");
            continue;
        } 

        //Zapytanie o liczbę miesięcy w rzędzie    
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("Podaj liczbe miesięcy w rzędzie [1-12] lub \'q\' aby wyjść: ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        string srzad = Console.ReadLine();
        Console.ResetColor();
        if (srzad == "q")
            break;
        int rzad = int.Parse(srzad);
        if (rzad < 1 || rzad > 12)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Niepoprawny zakres ilości miesięcy w rzędzie. Powinny być z zakresie [1-12].");
            Console.ResetColor();
            continue;
        }

        //Wypisanie roku
        Console.WriteLine();
        Rok.WypiszRok(rok, rzad);
    }
    catch
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Należy podać dane w rządanym formacie!!!");
        Console.ResetColor();
    }
}
class Rok
{
    //Zdefiniowanie zmiennych
    readonly int _rok;
    readonly int _ilemrzad;
    readonly Miesiac[] _miesiace;

    #region Funkcje statyczne
    static public void WypiszRok(int rokrzadany, int ilemrzad)
    {
        //Zdefiniowanie klasy
        Rok rok = new Rok(rokrzadany, ilemrzad);
        rok.WypiszRok();
    }
    #endregion

    #region Rok konstruktor
    public Rok(int rokkalendarzowy, int ilemrzad)
    {
        //Sprawdzenie warunku
        if (ilemrzad > 12)
            throw new Exception("Liczba miesięcy nie może być większa niż 12!!!");

        //Przypisanie zmiennych
        _rok = rokkalendarzowy;
        _ilemrzad = ilemrzad;
        _miesiace = new Miesiac[12];
        for (int i = 1; i <= 12; i++)
            _miesiace[i - 1] = new Miesiac(_rok, i);
    }
    #endregion

    #region Wypisanie roku
    public void WypiszRok()
    {
        //Wypisanie nagłówka
        for (int i = 1; i < _ilemrzad / 2 - 4 - _rok.ToString().Length; i++)
            Console.Write(" ");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Rok ");
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(_rok);
        Console.WriteLine();
        Console.ResetColor();

        //Wypisanie miesiecy
        int rzadmiesiac = 1;
        bool zakonczkalendarz = false;
        do
        {
            bool zapisanyrzad = false;
            for (int i = 0; i < _ilemrzad && i + rzadmiesiac <= 12; i++)
            {
                zapisanyrzad = _miesiace[i + rzadmiesiac - 1].WypiszWiersz()
                || zapisanyrzad;
                Console.Write("  ");
            }
            Console.WriteLine();
            if (!zapisanyrzad)
            {
                rzadmiesiac += _ilemrzad;
                if (rzadmiesiac > 12)
                    zakonczkalendarz = true;
            }
        }
        while (!zakonczkalendarz);
    }
    #endregion
}

class Miesiac
{
    //Zdeklarowanie zmiennych
    readonly int _rok;
    readonly int _miesiac;
    readonly string[] _dni;
    readonly string _sline;
    int _dzien;
    int _wiersz;
    int _tryb;
    DateTime _data;

    //Wyliczenie
    enum Tryb : int { Naglowek, Data, Koncowka, Pusty }

    #region Miesiac_Konstruktor
    public Miesiac(int rok, int miesiac)
    {
        //Przypisanie podstawowych zmiennych
        _rok = rok;
        _miesiac = miesiac;
        _dni = ["pn", "wt", "śr", "cz", "pt", "so", "nd"];
        _dzien = 1;
        _wiersz = 1;
        _sline = "+";
        for (int i = 0; i < 21; i++)
            _sline += "-";
        _sline += "+";
        _tryb = (int)Tryb.Naglowek;
        _data = new DateTime(rok, miesiac, 1);
    }
    #endregion

    #region Wypisywanie kalendarza
    public bool WypiszWiersz()
    {
        //Wypisanie wierszy
        bool wypisane = true;
        switch (_tryb)
        {
            case (int)Tryb.Naglowek:
                WypiszNaglowek();
                break;
            case (int)Tryb.Data:
                WypiszDaty();
                break;
            case (int)Tryb.Koncowka:
                WypiszKoniec();
                break;
            default:
                WypiszPuste();
                wypisane = false;
                break;
        }
        _wiersz++;
        return wypisane;
    }
    private void WypiszPuste()
    {
        for (int i = 0; i < 23; i++)
            Console.Write(" ");
    }
    private void WypiszNaglowek()
    {
        //Wypisanie nagłówka kalendarza
        switch (_wiersz)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(_sline);
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{_data.ToString("MMMM"),-21}");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("|");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("|");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                foreach (string dzien in _dni)
                    Console.Write($"{dzien,3}");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("|");
                break;
            case 4:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(_sline);
                _tryb = (int)Tryb.Data;
                break;
        }
        Console.ResetColor();
    }
    private bool WypiszDaty()
    {
        //Wypisanie dat w kalendarzu
        bool niezakonczone = true;

        //Wypisanie początku wiersza
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("|");
        Console.ResetColor();

        //Wypisanie tygodnia
        for (int dzientyg = 1; dzientyg <= 7; dzientyg++)
        {
            //Ustawienie koloru dnia
            switch (dzientyg)
            {
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            //Sprawdzennie czy jest w zakresie na poczatkowym i końcowym
            if (dzientyg != ((int)_data.DayOfWeek + 6) % 7 + 1)
            {
                Console.Write("   ");
                continue;
            }
            //Wypisanie daty
            if (_dzien < DateTime.DaysInMonth(_rok, _miesiac))
            {
                Console.Write($"{_dzien++,3}");
                _data = _data.AddDays(1);
                Console.ResetColor();
            }
            else
            {
                Console.Write($"{_dzien++,3}");
                niezakonczone = false;
                _tryb = (int)Tryb.Koncowka;
            }
        }

        //Wypisanie końca wiersza
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("|");
        Console.ResetColor();

        return niezakonczone;
    }
    void WypiszKoniec()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(_sline);
        Console.ResetColor();
        _tryb = (int)Tryb.Pusty;
    }
    #endregion
}

