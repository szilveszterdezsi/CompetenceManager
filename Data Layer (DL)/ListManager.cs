/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2018-09-27
/// Modified: 2019-10-20
/// ---------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DL
{
    /// <summary>
    /// Manager class for hosting a collection referred to as m_list of the type BindingList&lt;T&gt;
    /// where T can be any object type. ListManager&lt;T&gt; implements the interface
    /// IListManager&lt;T&gt; and can be inherited by different classes passing any T at 
    /// declaration but then T must have the same type in all methods. 
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    [Serializable]
    public class ListManager<T> : IListManager<T>
    {
        private BindingList<T> m_list;

        /// <summary>
        /// Constructor inizializes a new empty instance of the collection m_list
        /// </summary>
        public ListManager()
        {
            m_list = new BindingList<T>();
        }

        /// <summary>
        /// Return the number of items <T> in the collection m_list
        /// </summary>
        public int Count => m_list.Count;

        /// <summary>
        /// Add an object to end of the collection m_list.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Add(T type)
        {
            m_list.Add(type);
            return true;
        }

        /// <summary>
        /// Return an object at a given position from the collection m_list.
        /// </summary>
        /// <param name="index">Index of object to be returned.</param>
        /// <returns>The requested object.</returns>
        public T GetAt(int index)
        {
            return m_list.ElementAt(index);
        }

        /// <summary>
        /// Remove the first occurance of a specific object
        /// from the collection m_list.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Remove(T type)
        {
            if (m_list.Remove(type))
                return true;
            return false;
        }

        /// <summary>
        /// Remove an object from the collection m_list at
        /// a given position.
        /// </summary>
        /// <param name="index">Index of object to be removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool RemoveAt(int index)
        {
            if (CheckIndex(index))
                m_list.RemoveAt(index);
            else
                return false;
            return true;
        }

        /// <summary>
        /// Remove several objects from the collection m_list at
        /// given positions.
        /// </summary>
        /// <param name="indecies">Array of indecies of objects to removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool RemoveAt(int[] indecies)
        {
            Array.Sort(indecies);
            for (int i = indecies.Length - 1; i >= 0; i--)
                RemoveAt(indecies[i]);
            return true;
        }

        /// <summary>
        /// Replace an object from the collection at a given index with a new object.
        /// </summary>
        /// <param name="type">Object to be replaced.</param>
        /// <param name="index">index to element to be replaced by a new object.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ReplaceAt(T type, int index)
        {
            if (CheckIndex(index))
                m_list[index] = type;
            else
                return false;
            return true;
        }

        /// <summary>
        /// Control that a given index is >= 0 and less than the number of items in 
        /// the collection.
        /// </summary>
        /// <param name="index">Index to be checked.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool CheckIndex(int index)
        {
            if (index >= 0 && m_list.Count() > index)
                return true;
            return false;
        }

        /// <summary>
        /// Removes all objects of the collection and set the collection to null.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool RemoveAll()
        {
            m_list.Clear();
            return true;
        }

        /// <summary>
        /// Returns the collection.
        /// </summary>
        /// <returns>The collection containing all objects.</returns>
        public BindingList<T> GetAll()
        {
            return m_list;
        }

        /// <summary>
        /// Prepare a list of strings where each string represents info
        /// about an object in the collection. The info can typically come
        /// from the object's ToString method.
        /// </summary>
        /// <returns>The collection containing strings representing an object in
        /// the collection.</returns>
        public List<string> ToStringList()
        {
            return m_list.Cast<object>().Select(o => o.ToString()).ToList();
        }

        /// <summary>
        /// Prepare an array of strings where each string represents info
        /// about an object in the collection. The info can typically come
        /// from the object's ToString method.
        /// </summary>
        /// <returns>The collection containing strings representing an object in
        /// the collection.</returns>
        public string[] ToStringArray()
        {
            return m_list.Cast<object>().Select(o => o.ToString()).ToArray();
        }
    }
}
