/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System.Linq;
using System.Collections.Generic;
using DL;

namespace BLL
{
    /// <summary>
    /// Manager class that handles Competence objects and inherits ListManager&lt;Competence&gt;.
    /// </summary>
    public class CompetenceManager : ListManager<Competence>
    {
        /// <summary>
        /// Adds a new competence.
        /// </summary>
        /// <param name="description">Competence description.</param>
        public void AddCompetence(string description)
        {
            Add(new Competence(Count > 0 ? GetAt(Count - 1).ID + 1 : 1, description));
        }

        /// <summary>
        /// Renames competence description.
        /// </summary>
        /// <param name="competence"></param>
        public void RenameCompetence(Competence competence)
        {
            ReplaceAt(competence, GetAll().IndexOf(GetAll().Single(t => t.ID == competence.ID)));
        }

        /// <summary>
        /// Removes a list of competences.
        /// </summary>
        /// <param name="competences">Competences to be removed.</param>
        public void RemoveCompetences(List<Competence> competences)
        {
            competences.ForEach(x => Remove(x));
        }

        /// <summary>
        /// Links a members in a team to a competence.
        /// </summary>
        /// <param name="competence">Comptence.</param>
        /// <param name="team">Team.</param>
        /// <param name="member">Member.</param>
        public void LinkMemberInTeamToCompetence(Competence competence, Team team, Member member)
        {
            Competence comp = GetAll().Single(c => c.ID == competence.ID);
            if (!comp.LinkedTo.Keys.Contains(team))
            {
                comp.LinkedTo.Add(team, new List<Member> { member });
            }
            else
            {
                if (!comp.LinkedTo[team].Contains(member))
                {
                    comp.LinkedTo[team].Add(member);
                }
            }      
        }

        /// <summary>
        /// Links a list of members in a list of teams to a list of competences.
        /// </summary>
        /// <param name="competences">List of comptences.</param>
        /// <param name="teams">List of teams.</param>
        /// <param name="members">List of members.</param>
        public void LinkMembersInTeamsToCompetences(List<Competence> competences, List<Team> teams, List<Member> members)
        {
            GetAll().Where(c => competences.Any(s => s == c)).ToList().ForEach(c => teams.ForEach(t => members.ForEach(m => LinkMemberInTeamToCompetence(c, t, m))));
        }

        /// <summary>
        /// Delinks a member in a team to a competence.
        /// </summary>
        /// <param name="competence">Comptence.</param>
        /// <param name="team">Team.</param>
        /// <param name="member">Member.</param>
        public void DelinkMemberInTeamFromCompetence(Competence target, Team team, Member member)
        {
            Competence competence = GetAll().Single(comp => comp.ID == target.ID);
            if (competence.LinkedTo.Keys.Contains(team))
            {
                if (competence.LinkedTo[team].Contains(member))
                {
                    competence.LinkedTo[team].Remove(member);
                    if (competence.LinkedTo[team].Count == 0)
                    {
                        competence.LinkedTo.Remove(team);
                    }
                }
            }
        }

        /// <summary>
        /// Delinks all members of a team from all competences.
        /// </summary>
        /// <param name="team">Team.</param>
        public void DelinkTeamFromAllCompetences(Team team)
        {
            GetAll().Where(c => c.LinkedTo.Keys.Contains(team)).ToList().ForEach(x => x.LinkedTo.Remove(team));
        }

        /// <summary>
        /// Delinks a member form all competences.
        /// </summary>
        /// <param name="member">Member.</param>
        public void DelinkMemberFromAllCompetences(Member member)
        {
            List<Competence> competences = GetAll().Where(c => c.LinkedTo.Values.Any(ms => ms.Any(m => m == member))).ToList();
            List<Team> teams = GetAll().SelectMany(c => c.LinkedTo.Keys.Where(t => c.LinkedTo[t].Any(m => m == member))).ToList();
            competences.ForEach(c => teams.ForEach(t => DelinkMemberInTeamFromCompetence(c, t, member)));
        }

        /// <summary>
        /// Delinks all members of a list of teams from all competences.
        /// </summary>
        /// <param name="teams">List of teams.</param>
        public void DelinkTeamsFromAllCompetences(List<Team> teams)
        {
            teams.ForEach(t => DelinkTeamFromAllCompetences(t));
        }

        /// <summary>
        /// Delinks a list of members from all competences.
        /// </summary>
        /// <param name="members">List of members.</param>
        public void DelinkMembersFromAllCompetences(List<Member> members)
        {
            members.ForEach(m => DelinkMemberFromAllCompetences(m));
        }

        /// <summary>
        /// Delinks all members of a list of teams from a list of competences.
        /// </summary>
        /// <param name="competences">List of competences.</param>
        /// <param name="teams">List of teams.</param>
        public void DelinkTeamsFromCompetences(List<Competence> competences, List<Team> teams)
        {
            GetAll().Where(c => competences.Any(s => s == c)).ToList().ForEach(c => teams.ForEach(t => c.LinkedTo.Remove(t)));
        }

        /// <summary>
        /// Delinks a list of members from a list of competences.
        /// </summary>
        /// <param name="competences">List of competences.</param>
        /// <param name="members">List of members.</param>
        public void DelinkMembersFromCompetences(List<Competence> competences, List<Member> members)
        {
            List<Team> teams = GetAll().SelectMany(c => c.LinkedTo.Keys.Where(t => c.LinkedTo[t].Any(m => members.Any(x => x == m)))).ToList();
            GetAll().Where(c => competences.Any(s => s == c)).ToList().ForEach(c => teams.ForEach(t => members.ForEach(m => DelinkMemberInTeamFromCompetence(c, t, m))));
        }

        /// <summary>
        /// Delinks a list of members from a list of teams from a list of competences.
        /// </summary>
        /// <param name="competences">List of competences.</param>
        /// <param name="teams">List of teams.</param>
        /// <param name="members">List of members.</param>
        public void DelinkMembersInTeamsFromCompetences(List<Competence> competences, List<Team> teams, List<Member> members)
        {
            GetAll().Where(c => competences.Any(s => s == c)).ToList().ForEach(c => teams.ForEach(t => members.ForEach(m => DelinkMemberInTeamFromCompetence(c, t, m))));
        }

        /// <summary>
        /// Updates a members name in all linked competences.
        /// </summary>
        /// <param name="member">Member.</param>
        public void RefreshLink(Member member)
        {
            GetAll().SelectMany(c => c.LinkedTo.SelectMany(pair => pair.Value.Where(m => m == member)).Select(m => m.Name = member.Name));
        }

        /// <summary>
        /// Updates a teams name in all competences.
        /// </summary>
        /// <param name="team">Team.</param>
        public void RefreshLink(Team team)
        {
            GetAll().SelectMany(c => c.LinkedTo.Keys.Where(t => t.Name == team.Name).Select(t => t.Name = team.Name));
        }
    }
}
