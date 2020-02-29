using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
            var MaxElement = new LinkedList<double>();
            var queue = new Queue<double>();
            foreach (var e in data)
            {
                var value = e.OriginalY;
                MaxElement.AddFirst(value);
                queue.Enqueue(value);
                if (queue.Count > windowWidth)
                {
                    if (queue.Peek() >= MaxElement.Last())
                    {
                        MaxElement.Remove(MaxElement.Last());
                    }
                    queue.Dequeue();
                }
                if (MaxElement.Count >= 2)
                {
                    for (var i = 0; i < MaxElement.Count - 1; i++)
                    {
                        if (MaxElement.First.Value > MaxElement.First.Next.Value)
                            MaxElement.Remove(MaxElement.First.Next);
                    }

                }
                
                e.MaxY = MaxElement.First();
                yield return e;
            }
        }
	}
}
