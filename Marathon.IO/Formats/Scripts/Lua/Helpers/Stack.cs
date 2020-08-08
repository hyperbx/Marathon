using System;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class Stack<T>
    {
        private readonly List<T> data;

        public Stack() => data = new List<T>();

        public bool isEmpty() => data.Count == 0;

        public T peek() => data[data.Count - 1];

        public T pop()
        {
            T value = data[data.Count - 1];
            data.Remove(value);
            return value;
        }

        public void push(T item)
        {
            if (data.Count > 65536)
                throw new Exception("Trying to push more than 65536 items!");

            data.Add(item);
        }

        public int size() => data.Count;

        public void reverse() => data.Reverse();
    }
}
