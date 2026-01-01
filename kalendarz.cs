// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Data;
using System.Diagnostics;

Console.WriteLine("Program do wyświetlania kalendarza\n");

//Wyswietlenie roku
for (int i = 1; i <= 12; i++)
{
    WyswietlMiesiac(2026, i);
    Console.WriteLine();
}

static void WyswietlMiesiac(int rok, int miesiac)
{
    //Ustawienie odpowiedniej daty
    DateTime data = new DateTime(rok, miesiac, 1);

    //Ustawienie nazw dni
    string[] dni =
    {
        "pn",
        "wt",
        "śr",
        "cz",
        "pt",
        "so",
        "ni"
    };

    //Wyswietlenie poczatku miesiaca
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    string sline = "+";
    for (int i = 0; i < 21; i++)
        sline += "-";
    sline += "+";
    Console.WriteLine(sline);
    Console.Write("|");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write($"{data.ToString("MMMM"),-21}");
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("|");
    Console.WriteLine(sline);
    Console.Write("|");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    foreach (string dzientyg in dni)
        Console.Write($"{dzientyg,3}");
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("|");
    Console.WriteLine(sline);
    Console.ResetColor();

    //Wyswietlenie miesiaca
    int dzien = 1;
    do
    {
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
                    break;
            }

            //Sprawdzennie czy jest w zakresie na poczatkowym i końcowym
            if (dzientyg != ((int)data.DayOfWeek + 6) % 7 + 1)
            {
                Console.Write("   ");
                continue;
            }
            //Wypisanie daty
            if (dzien < DateTime.DaysInMonth(rok, miesiac))
            {
                Console.Write($"{dzien,3}");
                dzien++;
                data = data.AddDays(1);
                Console.ResetColor();
            }
            else
            {
                Console.Write($"{dzien,3}");
                dzien++;
            }
        }

        //Wypisanie końca wiersza
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("|");
        Console.ResetColor();
    }
    while (dzien <= DateTime.DaysInMonth(rok, miesiac));

    //Wyswietlenie końca miesiąca
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine(sline);
    Console.ResetColor();
}