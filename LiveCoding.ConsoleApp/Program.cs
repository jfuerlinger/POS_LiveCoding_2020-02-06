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
    #region Constants
    const string FileNamePupils = "pupils.csv";
    const string FileNameClasses = "classes.csv";
    #endregion

    static void Main(string[] args)
    {

      DoSomeLinqStuff();

      IEnumerable<Pupil> pupils = LoadPupils();
      PrintResult("Schüler", pupils);


      IEnumerable<Class> classes = LoadClasses();
      PrintResult("Klassen", classes);

      // Filtere Schüler nach der Klasse => 5ABIF
      // Sortiere nach dem Nachnamen
      PrintResult("Schüler der 5ABIF sortiert nach Nachnamen", pupils
        .Where(p => p.ClassName == "5ABIF")
        .OrderBy(p => p.LastName));


      // Filtere Schüler nach der Klasse => 5ABIF
      // Selektiere jeden zweiten Schüler
      // Sortiere nach dem Nachnamen
      PrintResult("Schüler (jeder zweite) der 5ABIF sortiert nach Nachnamen", pupils
        .Where((p, idx) => idx % 2 == 0 && p.ClassName == "5ABIF")
        .OrderBy(p => p.LastName));


      // Filtere Schüler nach der Klasse => 5ABIF
      // Selektiere jeden zweiten Schüler
      // Sortiere nach dem Nachnamen
      // Liefere nur Nr, Nachname

      var result1 = pupils
        .Where((p, idx) => idx % 2 == 0 && p.ClassName == "5ABIF")
        .OrderBy(p => p.LastName)
        .Select(p => new
        {
          Nr = p.Id,
          Nachname = p.LastName
        });

      PrintResult("Schüler (jeder zweite) der 5ABIF sortiert nach Nachnamen => Nr, Nachname", result1);

      // Gruppiere die Schüler nach deren Klasse
      // Berechne die Anzahl der Schüler pro Klasse
      // Sortiere die Klasse nach der Anzahl der Schüler absteigend
      PrintResult("Resultat",
        pupils
          .GroupBy(p => p.ClassName)
          .Select(group => new
          {
            Klasse = group.Key,
            AnzSchueler = group.Count()
          })
          .OrderByDescending(group => group.AnzSchueler));


      // Führe einen GroupJoin (SQL: LEFT OUTER JOIN) zw. Klassen und Schülern durch
      // Gruppiere die Schüler nach deren Klasse
      // Berechne die Anzahl der Schüler pro Klasse
      // Sortiere die Klasse nach der Anzahl der Schüler absteigend
      PrintResult("Resultat",
        classes
          .GroupJoin(
            pupils,
            c => c.Name,
            p => p.ClassName,
            (c, p) =>
            {
              return new
              {
                Klasse = c.Name,
                AnzSchueler = p.Count()
              };
            })
          .OrderByDescending(g => g.AnzSchueler)
      );

      // Bilde die Summe aller ungerader Zahlen zw. 1 und 99
      int summe = Enumerable.Range(1, 99)
        .Where(nr => nr % 2 == 1)
        .Sum();
      Console.WriteLine($"Summe={summe}");


      // Gib zweimal 5 Zufallszahlen (zw. 1 und 10) aus und vergleiche das Ergebnis
      // Wird zweimal das gleiche Ergebnis ausgegeben?

      Random rand = new Random();
      var randomNumbers = Enumerable.Range(1, 5)
        .Select(_ => rand.Next(1, 10 + 1));

      PrintResult("1a. Zufallszahlen", randomNumbers);
      PrintResult("2a. Zufallszahlen", randomNumbers);

      var persistedNumbers = randomNumbers.ToArray();

      PrintResult("1b. Zufallszahlen", persistedNumbers);
      PrintResult("2b. Zufallszahlen", persistedNumbers);
    }

    private static void DoSomeLinqStuff()
    {
      //Ausgangssituation: eine C# Liste mit Abteilungsmitarbeitern (Name, Abteilung, Gehalt, etc.) 
      List<Employee> employees = new List<Employee>()
      {
        new Employee() {FirstName = "Max", LastName = "Müller", Salary = 1500, Department = "Human Ressources"},
        new Employee() {FirstName = "Lea", LastName = "Mustermann", Salary = 2500, Department = "Finance"},
        new Employee() {FirstName = "Franz", LastName = "Schatzl", Salary = 1800, Department = "Operations"},
        new Employee() {FirstName = "Heinz", LastName = "Huber", Salary = 1750, Department = "Operations"},
        new Employee() {FirstName = "Simone", LastName = "Mair", Salary = 2400, Department = "Finance"},
        new Employee() {FirstName = "Moritz", LastName = "Meier", Salary = 1800, Department = "Human Ressources"},
        new Employee() {FirstName = "Tom", LastName = "Zweimüller", Salary = 3900, Department = "Operations"},
        new Employee() {FirstName = "Angelika", LastName = "Goiserer", Salary = 1500, Department = "Finance"},
        new Employee() {FirstName = "Toni", LastName = "Aufreiter", Salary = 3700, Department = "Services"},
        new Employee() {FirstName = "Hubert", LastName = "Zauner", Salary = 4400, Department = "Finance"},
        new Employee() {FirstName = "Wolfgang", LastName = "Hinterholzer", Salary = 1500, Department = "Services"},
        new Employee() {FirstName = "Andrea", LastName = "Leeb", Salary = 2900, Department = "Services"},
      };

      // TODO: Gruppieren Sie nach der Abteilung und bilden Sie die Summe der Gehälter pro Abteilung
      // TODO: Erstellen Sie einen anonymen Typ, welcher nur den Abteilungsnamen und die Gehaltssumme enthält
      // TODO: Geben Sie die Abteilungen inkl. deren Gehälter absteigend nach deren Gesamtgehalt auf der Konsole aus.



      var departmentsBySalary = employees
        .MyWhere(e => e.Salary > 2000)
        .GroupBy(e => e.Department)
        .Select(g => new
        {
          Name = g.Key,
          Sum = g.Sum(emp => emp.Salary)
        })
        .OrderByDescending(r => r.Sum);


      var result = departmentsBySalary.Select(d => d.Name);

      PrintResult("Abteilungen per Gehalt", result);
    }


    /// <summary>
    /// Reads all the pupils from the csv-file.
    /// </summary>
    private static IEnumerable<Pupil> LoadPupils()
    {
      // Alternative Implementierung      
      //List<Pupil> pupils = new List<Pupil>();

      //string[] lines = File.ReadAllLines(FileNamePupils);
      //foreach (string line in lines)
      //{
      //  string[] parts = line.Split(';');
      //  int nr = int.Parse(parts[0]);
      //  string nachname = parts[1];
      //  string vorname = parts[2];
      //  string klasse = parts[3];
      //  pupils.Add(new Pupil() { Id = nr, FirstName = vorname, LastName = nachname, ClassName = klasse });
      //}

      //return pupils;

      // LINQ
      return File.ReadAllLines(FileNamePupils)
        .Skip(1)
        .Select(line =>
        {
          string[] parts = line.Split(';');
          int nr = int.Parse(parts[0]);
          string nachname = parts[1];
          string vorname = parts[2];
          string klasse = parts[3];
          return new Pupil() { Id = nr, FirstName = vorname, LastName = nachname, ClassName = klasse };
        })
        .OrderBy(p => p.LastName);
    }

    /// <summary>
    /// Reads all the pupils from the csv-file.
    /// </summary>
    private static IEnumerable<Class> LoadClasses()
      => File.ReadAllLines(FileNameClasses)
        .Skip(1)
        .Select(line =>
        {
          string[] parts = line.Split(';');

          return new Class()
          {
            Name = parts[0],
            ClassType = (ClassType)Enum.Parse(typeof(ClassType), parts[1], true)
          };
        })
        .OrderBy(c => c.Name);


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