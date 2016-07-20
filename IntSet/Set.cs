using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntSet
{
    public class Set<T> : IEnumerable<T> where T: class 
    {
        private T[] set;
        private int current = 0;
        private int startCapacity = 2;


        public Set()
        {
            set = new T[startCapacity];
        }

        public Set(params T[] a)
        {
            if (a == null)
            {
                throw new ArgumentNullException();
            }
            set = new T[a.Length];
            Add(a);
        }
        public int Count
        {
            get
            {
                return current;
            }
        }
        public void Add(T elem)
        {
            if (Contains(elem))
            {
                return;
            }
            if (current == set.Length - 1)
            {
                Increasing();
            }
            set[current] = elem;
            current++;
        }
        public void Add(T[] elems)
        {
            foreach (var a in elems)
            {
                Add(a);
            }
        }
        public bool Contains(T elem)
        {
            if (elem == null)
            {
                return false;
            }
            for (int i = 0; i < current; i++)
            {
                if (elem == set[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void Increasing()
        {
            T[] newArray = new T[set.Length * 2];
            for (int i = 0; i < set.Length; i++)
            {
                newArray[i] = set[i];
            }
            set = newArray;
        }
        public bool IsEmpty()
        {
            if (current == 0)
            {
                return true;
            }
            return false;
        }

        public Set<T> Union(Set<T> additional)
        {
            Set<T> result = new Set<T>(set);

            foreach (T item in additional.set)
            {
                if (!Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Intersection(Set<T> additional)
        {
            Set<T> result = new Set<T>();

            foreach (T item in set)
            {
                if (additional.set.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
        public Set<T> Difference(Set<T> additional)
        {
            Set<T> result = new Set<T>();

            foreach (var elem in set)
            {
                if (additional.set.Contains(elem) == false)
                    result.Add(elem);
            }
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < current; i++)
            {
                yield return set[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        public override string ToString()
        {
            if (current == 0)
                return "Empty set";
            String result = "";
            foreach (T a in this)
                result += "a ";
            return result;
        }
    }
}
