namespace CharHammer.Helpers;

public static class ArrayHelper
{
  /// <summary>Diviser une liste en autant de listes que nécessaires afin que chacune contienne un maximum de <paramref name="maxLength"/> éléments.</summary>
  //public static IEnumerable<IEnumerable<T>> SplitOnMaxLength<T>(this T[] array, int maxLength)
  //{
  //    for (var i = 0; i < (float)array.Length / maxLength; i++)
  //    {
  //        yield return array.Skip(i * maxLength).Take(maxLength);
  //    }
  //}

  /// <summary>Diviser une liste en <paramref name="slicesCount"/> listes de tailles égales (sauf la dernière).</summary>
  public static IEnumerable<IEnumerable<T>> SplitOnMaxSlices<T>(this T[] array, int slicesCount)
  {
    var maxLength = array.Length / slicesCount + (array.Length % slicesCount == 0 ? 0 : 1);

    for (var i = 0; i < slicesCount; i++)
    {
      yield return array.Skip(i * maxLength).Take(maxLength);
    }
  }
}
