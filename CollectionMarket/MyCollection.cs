using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Products;

namespace CollectionMarket
{
    [Serializable]
    public class MyCollection<T>:IEnumerator
    {

        protected T[] array;
        public int Count => count;
        protected int count = 0;
        public int Capacity => array.Length;
        public MyCollection(int capacity) => array = new T[capacity];
        public MyCollection(params T[] array) => this.array = array;

        public int position = -1;

        public object Current => array[position];

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }
        public MyCollection() => array = new T[0];

        public void Add(T value)
        {
            Resize();
            array[Capacity - 1] = value;
            count++;
        }
       
        public MyCollection(MyCollection<T> collection)
        {
            this.array = collection.array;
            this.count = collection.count;
        }
        /// <summary>
        /// Проверка элемента на содержание элементов
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i].Equals(item))
                    return true;
            return false;
        }
        /// <summary>
        /// Переход к следующему элементу
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            position++;
            return position < array.Length;
        }

        public T[] toArray() => array;

        public void Reset() => position = -1;

        private void Resize()
        {
            T[] temp = new T[array.Length + 1];
            array.CopyTo(temp, 0);
            array = temp;
        }

        public void Remove(T value)
        {
            T[] temp = new T[array.Length - 1];
            for (int i = 0; i < array.Length; i++)
                if (!value.Equals(array[i]))
                    temp[i] = array[i];
            count--;
        }
        /// <summary>
        /// Сортировка элементов коллекции
        /// </summary>
        public void Sort()
        {

        }
        /// <summary>
        /// Очищение элементов коллекции
        /// </summary>
        public void Clear()
        {
            array = new T[array.Length];
            count = 0;
        }

        public void Remove(int index)
        {
            T[] temp = new T[array.Length - 1];
            array[index] = default(T);

            if (index == 0)
            {
                for (int i = 1; i < array.Length - 1; i++)
                    if (i != index)
                        temp[i] = array[i];
            }
            else
            {
                for (int i = 0; i < index; i++)
                    temp[i] = array[i];

                for (int i = index + 1; i < array.Length; i++)
                    temp[i] = array[i];
            }

            array = temp;
            count--;
        }


        public IEnumerator GetEnumerator() => array.GetEnumerator();

    }

}
