namespace System.Collections.Generic
{
    public class DropOutStack<T> : IEnumerable<T>
    {
        #region FIELDS

        private T[] items;
        private int next;
        private int depth;
        private int current => Dec(next);

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Returns the number of items in the stack.
        /// </summary>
        public int Count => depth;

        #endregion
        
        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="capacity">The maximum capacity of the stack.</param>
        public DropOutStack(int capacity)
        {
            items = new T[capacity];
            next = 0;
            depth = 0;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Add a new item to the top of the stack.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Push(T item)
        {
            items[next] = item;
            next = Inc(next);
            depth = Math.Min(items.Length, depth + 1);
        }

        /// <summary>
        /// Get the topmost item on the stack and remove it.
        /// </summary>
        /// <returns>Returns the topmost item and removes it.</returns>
        public T Pop()
        {
            if (depth == 0)
                throw new InvalidOperationException("The stack is empty.");
            depth--;
            next = Dec(next);
            return items[next];
        }

        /// <summary>
        /// Get the topmost item on the stack without removing it.
        /// </summary>
        /// <returns>Returns the topmost item without removing it.</returns>
        public T Peek()
        {
            if (depth == 0)
                throw new InvalidOperationException("The stack is empty.");
            return items[current];
        }

        /// <summary>
        /// Clear all values from the stack.
        /// </summary>
        public void Clear()
        {
            next = 0;
            depth = 0;
        }

        #endregion

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = items.Length + current, I = i - depth; i > I; i--)
                yield return items[i % items.Length];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region HELPERS

        /// <summary>
        /// Increase i by shifting the index to the next position.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Inc(int i) => (i + 1) % items.Length;

        /// <summary>
        /// Decrease i by shifting the index to the previous position.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Dec(int i) => (items.Length + i - 1) % items.Length;

        #endregion
    }
}
