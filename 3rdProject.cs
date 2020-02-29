using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
    class Item
    {
        public double Value { get; set; }
        public Item Next { get; set; }
    }

    class MaxElement
    {
        public Item head;
        public Item tail;
        public void Push(double value)
        {
            var item = new Item { Value = value };
            if(head == null)
            {
                head = tail = item;
            }
            else
            {
                tail.Next = item;
                tail = item;
            }
        }
        public double Pop()
        {
            if (head == null) throw new InvalidOperationException();
            var result = head.Value;
            head = head.Next;
            if (head == null) tail = null;
            return result;
        }
    }
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
            int first = 0;
            var queue = new Queue<double>();
            var result = new MaxElement();
            foreach (var e in data)
            {
                var value = e.OriginalY;
                if(first == 0 || value > result.head.Value)
                {
                    result.Push(value);
                }

                queue.Enqueue(value);
                if(queue.Count > windowWidth)
                {
                    queue.Dequeue();
                    result.Pop();

                }
                e.MaxY = result.tail.Value;
                yield return e;
            }
        }
	}
}
