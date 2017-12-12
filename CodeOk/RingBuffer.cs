using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells
{
    public class RingBuffer<T> : Buffer<T>
    {
        public RingBuffer(int capacity) : base(capacity) { }

        public override bool IsFull()
        {
            return false;
        }

        public override void Put(T value)
        {
            buffer[Count] = value;
            ++Count;
            Count %= Capacity;
        }
    }
}
