using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells
{
    public class Buffer<T>
    {
        protected T[] buffer;

        public int Capacity { get; protected set; }
        public int Count { get; protected set; }
                
        public Buffer(int capacity)
        {
            Capacity = capacity;
            Count = 0;
            buffer = new T[capacity];
        }

        /// <summary>
        /// Add data.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">In case of adding into a full buffer</exception>
        /// <param name="value"></param>
        public virtual void Put(T value)
        {
            buffer[Count] = value;
            ++Count;
        }

        public virtual bool IsFull()
        {
            return Count == Capacity;
        }

        public virtual void Clear()
        {
            buffer = new T[Capacity];
            Count = 0;
        }

        public virtual IEnumerable<T> GetData()
        {
            return buffer;
        }
    }
}
