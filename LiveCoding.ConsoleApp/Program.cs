using LiveCoding.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace LiveCoding.ConsoleApp
{
    class Program
    {
        const string FileNamePupils = "pupils.csv";
        const string FileNameClasses = "classes.csv";

        static void Main(string[] args)
        {
            IEnumerable<Pupil> pupils = LoadPupils();
            PrintResult("Schüler", pupils);


            IEnumerable<Class> classes = LoadClasses();
            PrintResult("Klassen", classes);

            // Filtere Schüler nach der Klasse => 5ABIF
            // Sortiere nach dem Nachnamen
            


            // Filtere Schüler nach der Klasse => 5ABIF
            // Selektiere jeden zweiten Schüler
            // Sortiere nach dem Nachnamen
            

            // Filtere Schüler nach der Klasse => 5ABIF
            // Selektiere jeden zweiten Schüler
            // Sortiere nach dem Nachnamen
            // Liefere nur Nr, Nachname

            

            // Gruppiere die Schüler nach deren Klasse
            // Berechne die Anzahl der Schüler pro Klasse
            // Sortiere die Klasse nach der Anzahl der Schüler absteigend
            

            // Führe einen GroupJoin (SQL: LEFT OUTER JOIN) zw. Klassen und Schülern durch
            // Gruppiere die Schüler nach deren Klasse
            // Berechne die Anzahl der Schüler pro Klasse
            // Sortiere die Klasse nach der Anzahl der Schüler absteigend
            

            // Bilde die Ziffernsumme aller ungerader Zahlen zw. 1 und 99
            

            // Gib zweimal 5 Zufallszahlen (zw. 1 und 10) aus und vergleiche das Ergebnis
            // Wird zweimal das gleiche Ergebnis ausgegeben?+
            
        }

        /// <summary>
        /// Reads all the pupils from the csv-file.
        /// </summary>
        private static IEnumerable<Pupil> LoadPupils()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads all the pupils from the csv-file.
        /// </summary>
        private static IEnumerable<Class> LoadClasses()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Methode zur Ausgabe einer Überschrift
        /// </summary>
        /// <param name="caption"></param>
        private static void PrintCaption(string caption)
        {
            ConsoleColor initialColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + caption);
            Console.ForegroundColor = initialColor;
        }

        /// <summary>
        ///     Methode zur Ausgabe einer Überschrift, gefolgt von einer generischen Collection (ToString()
        ///     Methode ToString() der Listenelemente muss sinnvoll überschrieben sein!)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="caption"></param>
        /// <param name="res"></param>
        private static void PrintResult<T>(string caption, IEnumerable<T> res)
        {
            PrintCaption(caption);
            foreach (T item in res)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
    }
}
