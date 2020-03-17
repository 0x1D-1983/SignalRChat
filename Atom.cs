using System;
using System.Threading;

namespace SignalRChat
{
    public sealed class Atom<T> where T : class
    {
        private volatile T value;
        public T Value => value;

        public Atom(T value)
        {
            this.value = value;
        }

        public T Swap(Func<T, T> func)
        {
            T original;
            T temp;

            do
            {
                original = value;
                temp = func(original);
            }
            while (Interlocked.CompareExchange(ref value, temp, original) != original);

            return original;
        }
    }
}
