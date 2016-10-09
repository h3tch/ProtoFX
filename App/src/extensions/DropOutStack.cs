namespace System.Collections.Generic
{
    public class DropOutStack<T> : IEnumerable<T>
    {
        private T[] items;
        private int next;
        private int depth;
        private int current => Dec(next);

        public int Count => depth;

        public DropOutStack(int capacity)
        {
            items = new T[capacity];
            next = 0;
            depth = 0;
        }

        public void Push(T item)
        {
            items[next] = item;
            next = Inc(next);
            depth = Math.Min(items.Length, depth + 1);
        }

        public T Pop()
        {
            if (depth == 0)
                throw new InvalidOperationException("The stack is empty.");
            depth--;
            next = Dec(next);
            return items[next];
        }

        public T Peek()
        {
            if (depth == 0)
                throw new InvalidOperationException("The stack is empty.");
            return items[current];
        }

        public void Clear()
        {
            next = 0;
            depth = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = current, n = 0; n < depth; n++, i = Dec(i))
                yield return items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int Inc(int i) => (i + 1) % items.Length;

        private int Dec(int i) => (items.Length + i - 1) % items.Length;
    }
}
