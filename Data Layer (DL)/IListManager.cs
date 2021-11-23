/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2018-09-27
/// Modified: n/a
/// ---------------------------

using System.Collections.Generic;

namespace DL
{
    /// <summary>
    /// Interface for implementation by manager classes hosting a collection of the type List&lt;T&gt;
    /// where T can be any object type. In this documentation, the collection is referred to 
    /// as m_list. IListManager&lt;T&gt; can be inherited by different classes passing any T at 
    /// declaration but then T must have the same type in all methods. 
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    interface IListManager<T>
    {
        /// <summary>
        /// Return the number of items in the collection m_list
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add an object to end of the collection m_list.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Add(T type);

        /// <summary>
        /// Return an object at a given position from the collection m_list.
        /// </summary>
        /// <param name="index">Index of object to be returned.</param>
        /// <returns>.</returns>
        T GetAt(int index);

        /// <summary>
        /// Remove the first occurance of a specific object
        /// from the collection m_list.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Remove(T type);

        /// <summary>
        /// Remove an object from the collection m_list at
        /// a given position.
        /// </summary>
        /// <param name="index">Index of object to be removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool RemoveAt(int index);

        /// <summary>
        /// Remove several objects from the collection m_list at
        /// given positions.
        /// </summary>
        /// <param name="indecies">Array of indecies of objects to removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool RemoveAt(int[] indecies);

        /// <summary>
        /// Replace an object from the collection at a given index with a new object.
        /// </summary>
        /// <param name="type">Object to be replaced.</param>
        /// <param name="index">index to element to be replaced by a new object.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool ReplaceAt(T type, int index);

        /// <summary>
        /// Control that a given index is >= 0 and less than the number of items in 
        /// the collection.
        /// </summary>
        /// <param name="index">Index to be checked.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool CheckIndex(int index);

        /// <summary>
        /// Removes all objects of the collection and set the collection to null.
        /// </summary>
        bool RemoveAll();

        /// <summary>
        /// Prepare a list of strings where each string represents info
        /// about an object in the collection. The info can typically come
        /// from the object's ToString method.
        /// </summary>
        /// <returns>The collection containing strings representing an object in
        /// the collection.</returns>
        List<string> ToStringList();

        /// <summary>
        /// Prepare an array of strings where each string represents info
        /// about an object in the collection. The info can typically come
        /// from the object's ToString method.
        /// </summary>
        /// <returns>The collection containing strings representing an object in
        /// the collection.</returns>
        string[] ToStringArray();
    }
}
