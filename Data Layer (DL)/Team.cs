/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DL
{
    /// <summary>
    /// Class for handeling a team.
    /// </summary>
    [Serializable]
    public class Team
    {
        private int id;
        private string name;
        private BindingList<Member> members;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Team ID.</param>
        /// <param name="name">Team name.</param>
        public Team(int id, string name)
        {
            ID = id;
            Name = name;
            Members = new BindingList<Member>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Team ID.</param>
        /// <param name="name">Team name.</param>
        /// <param name="members">Team members.</param>
        public Team(int id, string name, string members)
        {
            ID = id;
            Name = name;
            Members = new BindingList<Member>();
            Regex.Split(members, "\r\n").ToList().ForEach(x => Members.Add(new Member($"{id:00}.{Members.Count + 1:00}", x)));
        }

        /// <summary>
        /// Gets and sets team ID.
        /// </summary>
        public int ID { get => id; set => id = value; }

        /// <summary>
        /// Gets and sets team name.
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Gets and sets a list of team members.
        /// </summary>
        public BindingList<Member> Members { get => members; set => members = value; }

        /// <summary>
        /// Adds a new member.
        /// </summary>
        /// <param name="member">Member.</param>
        public void AddMember(Member member)
        {
            int mid = Members.Count > 0 ? int.Parse(Members.Last().ID.Substring(Members.Last().ID.IndexOf('.') + 1))+1 : 1;
            member.ID = $"{ID:00}.{mid:00}";
            Members.Add(member);
        }

        /// <summary>
        /// Adds a new member.
        /// </summary>
        /// <param name="name">Member name.</param>
        public void AddMember(string name)
        {
            int mid = Members.Count > 0 ? int.Parse(Members.Last().ID.Substring(Members.Last().ID.IndexOf('.') + 1)) + 1 : 1;
            Members.Add(new Member($"{ID:00}.{mid:00}", name));
        }

        /// <summary>
        /// Removes a list of members from team.
        /// </summary>
        /// <param name="members">List of members.</param>
        public void RemoveMembers(List<Member> members)
        {
            members.ForEach(m => Members.Remove(m));
        }

        /// <summary>
        /// Returns a string of all members with each members on a new line.
        /// </summary>
        /// <returns>String with list of members.</returns>
        public string MembersToNameString()
        {
            return Members.ToNameString();
        }

        /// <summary>
        /// Presentation.
        /// </summary>
        /// <returns>Team formated for presentation.</returns>
        public override string ToString()
        {
            return $"{ID:00} : {name}";
        }
    }

    /// <summary>
    /// Nested extension class used by team.
    /// </summary>
    public static class TeamExtensions
    {
        /// <summary>
        /// Returns a string of a list of objects based on separator.
        /// </summary>
        /// <typeparam name="T">Object type in list.</typeparam>
        /// <param name="source">Source list.</param>
        /// <param name="selector">Action.</param>
        /// <param name="seperator">Separator string.</param>
        /// <returns></returns>
        public static string Concat<T>(this IEnumerable<T> source, Func<T, string> selector, string seperator)
        {
            var builder = new StringBuilder();
            foreach (var item in source)
            {
                if (builder.Length > 0)
                    builder.Append(seperator);

                builder.Append(selector(item));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns a string of a list of members as a string with each member on a new line.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>String with list of members.</returns>
        public static string ToNameString(this IEnumerable<Member> source)
        {
            return Concat(source, i => i.Name, "\r\n");
        }
    }
}
