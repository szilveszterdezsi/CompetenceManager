/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System;
using System.Collections.Generic;

namespace DL
{
    /// <summary>
    /// Class for handeling a competence.
    /// </summary>
    [Serializable]
    public class Competence
    {
        private int id;
        private string description;
        private Dictionary<Team, List<Member>> linkedTo;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Competence ID.</param>
        /// <param name="description">Competence description.</param>
        public Competence(int id, string description)
        {
            ID = id;
            Description = description;
            LinkedTo = new Dictionary<Team, List<Member>>();
        }

        /// <summary>
        /// Gets and sets competence ID.
        /// </summary>
        public int ID { get => id; set => id = value; }

        /// <summary>
        /// Gets and sets competence description.
        /// </summary>
        public string Description { get => description; set => description = value; }

        /// <summary>
        /// Gets a sets dictionary of linked teams (keys) and list of members (values).
        /// </summary>
        public Dictionary<Team, List<Member>> LinkedTo { get => linkedTo; set => linkedTo = value; }

        /// <summary>
        /// Presentation.
        /// </summary>
        /// <returns>Competence formated for presentation.</returns>
        public override string ToString()
        {
            return $"{id:00} : {description}";
        }
    }
}
