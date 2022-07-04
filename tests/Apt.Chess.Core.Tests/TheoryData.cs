namespace Apt.Chess.Core.Tests;

// credit:  https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/
public abstract class TheoryData : IEnumerable<object[]>
{
   readonly List<object?[]> _data = new();

   protected void AddRow(params object?[] values) =>
      _data.Add(values);

   public IEnumerator<object[]> GetEnumerator() =>
      _data.GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() =>
      GetEnumerator();
}

public class TheoryData<T1> : TheoryData
{
   public void AddCase(T1 p1) =>
      AddRow(p1);
}

public class TheoryData<T1, T2> : TheoryData
{
   public void AddCase(T1 p1, T2 p2) =>
      AddRow(p1, p2);
}

public class TheoryData<T1, T2, T3> : TheoryData
{
   public void AddCase(T1 p1, T2 p2, T3 p3) =>
      AddRow(p1, p2, p3);
}

public class TheoryData<T1, T2, T3, T4> : TheoryData
{
   public void AddCase(T1 p1, T2 p2, T3 p3, T4 p4) =>
      AddRow(p1, p2, p3, p4);
}

public class TheoryData<T1, T2, T3, T4, T5> : TheoryData
{
   public void AddCase(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5) =>
      AddRow(p1, p2, p3, p4, p5);
}
