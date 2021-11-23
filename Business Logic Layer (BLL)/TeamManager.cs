/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System.Collections.Generic;
using System.Linq;
using DL;

namespace BLL
{
    /// <summary>
    /// Manager class that handles Team objects and inherits ListManager&lt;Team&gt;.
    /// </summary>
    public class TeamManager : ListManager<Team>
    {
        /// <summary>
        /// Adds a new team.
        /// </summary>
        /// <param name="name">Team name.</param>
        /// <param name="members">Teams member names.</param>
        public void AddTeam(string name, string members)
        {
            Add(new Team(Count > 0 ? GetAt(Count - 1).ID + 1 : 1, name, members));
        }

        /// <summary>
        /// Adds a new member to a team.
        /// </summary>
        /// <param name="team">Team.</param>
        /// <param name="member">Team member.</param>
        public void AddMember(Team team, string member)
        {
            GetAll().Where(t => t == team).Single().AddMember(member);
        }

        /// <summary>
        /// Removes a list of teams.
        /// </summary>
        /// <param name="teams">List of teams.</param>    
        public void RemoveTeams(List<Team> teams)
        {
            teams.ForEach(x => Remove(x));
        }

        /// <summary>
        /// Removes a list of members from a list of teams.
        /// </summary>
        /// <param name="teams">List of teams.</param>
        /// <param name="members">List of members.</param>
        public void RemoveMembers(List<Team> teams, List<Member> members)
        {
            GetAll().Where(team => teams.Any(s => s == team)).ToList().ForEach(team => team.RemoveMembers(members));
        }

        /// <summary>
        /// Renames a team.
        /// </summary>
        /// <param name="team">Team.</param>
        public void RenameTeam(Team team)
        {
            ReplaceAt(team, GetAll().IndexOf(GetAll().Single(t => t.ID == team.ID)));
        }

        /// <summary>
        /// Renames a member of a team.
        /// </summary>
        /// <param name="member">Member.</param>
        public void RenameMember(Member member)
        {
            GetAll().SelectMany(t => t.Members.Where(m => m == member).Select(m => m.Name = member.Name));
        }
    }
}
