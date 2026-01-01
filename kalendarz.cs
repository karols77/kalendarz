// See https://aka.ms/new-console-template for more information
using System.Data;

Console.WriteLine("Program do wyświetlania kalendarza");

//Wyswietlenie roku
WyswietlMiesiac(2026, 1);

static void WyswietlMiesiac(int rok, int miesiac)
{
    //Ustawienie odpowiedniej daty
    DateTime data = new DateTime(rok, miesiac, 1);

    //Wyswietlenie poczatku miesiaca
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    string sline = "+";
    for (int i = 0; i < 21; i++)
        sline += "-";
    sline += "+";
    Console.WriteLine(sline);
    Console.Write("+");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write($"{data.ToString("MMMM"),-21}");
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("+");
    Console.WriteLine(sline);

    //Wyswietlenie miesiaca
    int dzien = 1;
    do
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("|");
        for (int dzientyg = 1; dzientyg <= 7; dzientyg++)
        {
            //Sprawdzennie czy jest w zakresie na poczatkowym
            if (dzientyg < ((int)data.DayOfWeek + 15)%8 )
            {
                Console.WriteLine("   ");
                continue;
            }

            //Sprawdzenie w zakresie końcowym
            if(dzientyg)

        }
    }
}