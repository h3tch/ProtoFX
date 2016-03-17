using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    /// <summary>
    /// A trie, also called digital tree and sometimes radix tree or prefix tree
    /// (as they can be searched by prefixes), is an ordered tree data structure
    /// that is used to store a dynamic set or associative array where the keys
    /// are usually strings. Unlike a binary search tree, no node in the tree
    /// stores the key associated with that node; instead, its position in the
    /// tree defines the key with which it is associated. All the descendants of
    /// a node have a common prefix of the string associated with that node, and
    /// the root is associated with the empty string. Values are not necessarily
    /// associated with every node. Rather, values tend only to be associated
    /// with leaves, and with some inner nodes that correspond to keys of
    /// interest. For the space-optimized presentation of prefix tree, see
    /// compact prefix tree.
    /// </summary>
    /// <typeparam name="TValue">Value to store in the leaves.</typeparam>
    public class Trie<TValue> : TrieNode<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public Trie(IEnumerable<string> keys, IEnumerable<TValue> values)
        {
            keys.Zip(values, (k, v) => Add(k, v));
        }

        /// <summary>
        /// Retrieve all keys starting with the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<TValue> this[string query] => Retrieve(query, 0);

        /// <summary>
        /// Retrieve the number of keys starting with the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int Count(string query) => RetrieveCount(query, 0);

        /// <summary>
        /// Check if the trie has any key starting with the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool HasAny(string query) => RetrieveHasAny(query, 0);

        /// <summary>
        /// Add value to the tier.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, TValue value) => Add(key, 0, value);
    }

    public class TrieNode<TValue> : TrieNodeBase<TValue>
    {
        private readonly Dictionary<char, TrieNode<TValue>> nodes;
        private readonly Queue<TValue> values;

        protected TrieNode()
        {
            nodes = new Dictionary<char, TrieNode<TValue>>();
            values = new Queue<TValue>();
        }

        protected override int KeyLength => 1;

        protected override IEnumerable<TrieNodeBase<TValue>> Children() => nodes.Values;

        protected override IEnumerable<TValue> Values() => values;

        protected override int Count() => values.Count;

        protected override TrieNodeBase<TValue> GetOrCreateChild(char key)
        {
            TrieNode<TValue> result;
            if (!nodes.TryGetValue(key, out result))
                nodes.Add(key, result = new TrieNode<TValue>());
            return result;
        }

        protected override TrieNodeBase<TValue> GetChildOrNull(string query, int position)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            TrieNode<TValue> childNode;
            return nodes.TryGetValue(query[position], out childNode) ? childNode : null;
        }

        protected override void AddValue(TValue value) => values.Enqueue(value);
    }

    public abstract class TrieNodeBase<TValue>
    {
        protected abstract int KeyLength { get; }

        protected abstract IEnumerable<TValue> Values();

        protected abstract int Count();

        protected abstract IEnumerable<TrieNodeBase<TValue>> Children();

        public void Add(string key, int position, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("Key must not be zero.");

            if (EndOfString(position, key))
                AddValue(value);
            else
                GetOrCreateChild(key[position]).Add(key, position + 1, value);
        }

        protected abstract void AddValue(TValue value);

        protected abstract TrieNodeBase<TValue> GetOrCreateChild(char key);

        protected virtual IEnumerable<TValue> Retrieve(string query, int position)
            => EndOfString(position, query) ? ValuesDeep() : SearchDeep(query, position);

        protected virtual int RetrieveCount(string query, int position)
            => EndOfString(position, query) ? ValuesDeepCount() : SearchDeepCount(query, position);

        protected virtual bool RetrieveHasAny(string query, int position)
            => EndOfString(position, query) ? ValuesDeepHasAny() : SearchDeepHasAny(query, position);

        protected virtual IEnumerable<TValue> SearchDeep(string query, int position)
        {
            var nextNode = GetChildOrNull(query, position);
            return nextNode?.Retrieve(query, position + nextNode.KeyLength) ?? Enumerable.Empty<TValue>();
        }

        protected virtual int SearchDeepCount(string query, int position)
        {
            var nextNode = GetChildOrNull(query, position);
            return nextNode?.RetrieveCount(query, position + nextNode.KeyLength) ?? 0;
        }

        protected virtual bool SearchDeepHasAny(string query, int position)
        {
            var nextNode = GetChildOrNull(query, position);
            return nextNode?.RetrieveHasAny(query, position + nextNode.KeyLength) ?? false;
        }

        protected abstract TrieNodeBase<TValue> GetChildOrNull(string query, int position);

        private static bool EndOfString(int position, string text) => position >= text.Length;

        private IEnumerable<TValue> ValuesDeep() => Subtree().SelectMany(node => node.Values());

        private int ValuesDeepCount() => Subtree().Select(node => node.Count()).Sum();

        private bool ValuesDeepHasAny() => Subtree().Where(node => node.Count() > 0).Any();

        protected IEnumerable<TrieNodeBase<TValue>> Subtree()
            => Enumerable.Repeat(this, 1).Concat(Children().SelectMany(child => child.Subtree()));
    }
}