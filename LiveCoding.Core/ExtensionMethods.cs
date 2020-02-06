using System;
using System.Collections.Generic;
using System.Text;

namespace LiveCoding.Core
{
  public static class ExtensionMethods
  {
    public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> values, Func<T, bool> predicate)
    {
      List<T> result = new List<T>();

      foreach (T value in values)
      {
        if (predicate(value))
        {
          result.Add(value);
        }
      }

      return result;
    }
  }
}
