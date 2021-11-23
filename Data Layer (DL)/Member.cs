/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System;

namespace DL
{
    /// <summary>
    /// Class for handeling a member.
    /// </summary>
    [Serializable]
    public class Member
    {
        private string id;
        private string name;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Member()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Member name.</param>
        public Member(string name)
        {
            ID = "00:00";
            Name = name;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Member ID.</param>
        /// <param name="name">Member name.</param>
        public Member(string id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        /// Gets and sets member ID.
        /// </summary>
        public string ID { get => id; set => id = value; }

        /// <summary>
        /// Gets and sets member name.
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Presentation.
        /// </summary>
        /// <returns>Member formated for presentation.</returns>
        public override string ToString()
        {
            return $"{id} : {name}";
        }
    }
}