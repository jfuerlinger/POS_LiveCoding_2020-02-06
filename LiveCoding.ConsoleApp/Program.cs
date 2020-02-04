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
            IEnumerable<Pupil> pupils5abif =
                pupils
                    .Where(p => p.ClassName == "5ABIF")
                    .OrderBy(p => p.LastName);

            PrintResult("Schüler der 5ABIF", pupils5abif);


            // Filtere Schüler nach der Klasse => 5ABIF
            // Selektiere jeden zweiten Schüler
            // Sortiere nach dem Nachnamen
            IEnumerable<Pupil> pupils5abifIdx2 =
                pupils5abif
                    .Where((p, idx) => idx % 2 == 0);

            PrintResult("Jeden 2. Schüler der 5ABIF", pupils5abifIdx2);

            // Filtere Schüler nach der Klasse => 5ABIF
            // Selektiere jeden zweiten Schüler
            // Sortiere nach dem Nachnamen
            // Liefere nur Nr, Nachname

            var pupils5abifIdx2WithProjection =
                pupils5abifIdx2
                    .Select(p => new
                    {
                        Nr = p.Id,
                        Nachname = p.LastName
                    });

            PrintResult("Jeden 2. Schüler der 5ABIF", pupils5abifIdx2WithProjection);

            // Gruppiere die Schüler nach deren Klasse
            // Berechne die Anzahl der Schüler pro Klasse
            // Sortiere die Klasse nach der Anzahl der Schüler absteigend
            var classStatistics = pupils
                .GroupBy(p => p.ClassName)
                .Select(group => new
                {
                    Klasse = group.Key,
                    AnzSchueler = group.Count()
                })
                .OrderByDescending(classGroup => classGroup.AnzSchueler);
            PrintResult("Klassen und Schüleranzahl", classStatistics);

            // Führe einen GroupJoin (SQL: LEFT OUTER JOIN) zw. Klassen und Schülern durch
            // Gruppiere die Schüler nach deren Klasse
            // Berechne die Anzahl der Schüler pro Klasse
            // Sortiere die Klasse nach der Anzahl der Schüler absteigend
            var classStatistics2Enhanced =
                classes.GroupJoin(pupils,
                    c => c.Name,
                    p => p.ClassName,
                    (c, p) => new
                    {
                        ClassName = c.Name,
                        ClassType = c.ClassType,
                        PupilsCount = p.Count()
                    }
                )
                .OrderByDescending(g => g.PupilsCount);

            PrintResult("Klassen und Schüleranzahl (GroupJoin)", classStatistics2Enhanced);

            // Bilde die Ziffernsumme aller ungerader Zahlen zw. 1 und 99
            int sumOfOddNumbers = Enumerable
                .Range(1, 99)
                .Where(nr => nr % 2 == 1)
                .Sum();
            Console.WriteLine($"Ziffernsumme => {sumOfOddNumbers}");

            // Gib zweimal 5 Zufallszahlen (zw. 1 und 10) aus und vergleiche das Ergebnis
            // Wird zweimal das gleiche Ergebnis ausgegeben?+
            Random rand = new Random();
            IEnumerable<int> zufallszahlen =
                Enumerable
                .Range(1, 5)
                .Select(nr => rand.Next(1, 11));
            PrintResult("Zufallszahlen 1a", zufallszahlen);
            PrintResult("Zufallszahlen 1b", zufallszahlen);

            IEnumerable<int> zufallszahlenPersistiert = zufallszahlen.ToArray();
            PrintResult("Zufallszahlen 2a", zufallszahlenPersistiert);
            PrintResult("Zufallszahlen 2b", zufallszahlenPersistiert);
        }

        /// <summary>
        /// Reads all the pupils from the csv-file.
        /// </summary>
        private static IEnumerable<Pupil> LoadPupils()
        {
            return File.ReadAllLines(FileNamePupils, encoding: Encoding.UTF8)
                .Skip(1)
                .Select(line =>
                {
                    string[] parts = line.Split(';');

                    //TODO: Check for correct data formats

                    return new Pupil()
                    {
                        Id = int.Parse(parts[0]),
                        LastName = parts[1],
                        FirstName = parts[2],
                        ClassName = parts[3]

                    };
                })
                .OrderBy(pupil => pupil.ClassName)
                .ThenBy(pupil => pupil.LastName);
        }

        /// <summary>
        /// Reads all the pupils from the csv-file.
        /// </summary>
        private static IEnumerable<Class> LoadClasses()
        {
            return File.ReadAllLines(FileNameClasses, encoding: Encoding.UTF8)
                .Skip(1)
                .Select(line =>
                {
                    string[] parts = line.Split(';');

                    //TODO: Check for correct data formats

                    return new Class()
                    {
                        Name = parts[0],
                        ClassType = (ClassType)Enum.Parse(typeof(ClassType), parts[1], true)

                    };
                })
                .OrderBy(c => c.Name);
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
