/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System.Collections.Generic;
using System.ComponentModel;
using DAL;
using DL;

namespace BLL
{
    /// <summary>
    /// Controller class of the Black Jack game that serves the presentation layer.
    /// </summary>
    public class Controller
    {
        private bool sessionSaved = false;
        private bool neverSaved = true;
        private string defaultFilePath = "Default.dat";
        private TeamManager teamManager;
        private CompetenceManager competenceManager;

        /// <summary>
        /// Constructor inizializes instances of TeamManager and CompetenceManager.
        /// </summary>
        public Controller()
        {
            teamManager = new TeamManager();
            competenceManager = new CompetenceManager();
            //AddTestData(5);
        }

        /// <summary>
        /// Adds test data, only used during development.
        /// </summary>
        /// <param name="count"></param>
        public void AddTestData(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                Team team = new Team(i, "Team " + $"{i:00}");
                Competence competence = new Competence(i, "Competenece " + $"{i:00}");
                competenceManager.Add(competence);
                for (int y = 1; y <= count; y++)
                {
                    Member member = new Member("Member " + $"{y:00}");
                    team.AddMember(member);
                    competenceManager.LinkMemberInTeamToCompetence(competence, team, member);
                }  
                teamManager.Add(team);
            }
        }

        /// <summary>
        /// Returns all teams from TeamManager.
        /// </summary>
        /// <returns>List of all teams.</returns>
        public BindingList<Team> GetAllTeams()
        {
            return teamManager.GetAll();
        }

        /// <summary>
        /// Returns all competences from CompetenceManager.
        /// </summary>
        /// <returns>List of all competences.</returns>
        public BindingList<Competence> GetAllCompetences()
        {
            return competenceManager.GetAll();
        }

        /// <summary>
        /// Adds a new team to TeamManager.
        /// </summary>
        /// <param name="name">Team name.</param>
        /// <param name="members">Teams member names.</param>
        public void AddTeam(string name, string members)
        {
            teamManager.AddTeam(name, members);
        }

        /// <summary>
        /// Adds a new member to a team in TeamManager.
        /// </summary>
        /// <param name="team">Team.</param>
        /// <param name="member">Team member.</param>
        public void AddMember(Team team, string member)
        {
            teamManager.AddMember(team, member);
            sessionSaved = false;
        }

        /// <summary>
        /// Adds a new comptence to CompetenceManager.
        /// </summary>
        /// <param name="description">Comptetence description.</param>
        public void AddCompetence(string description)
        {
            competenceManager.AddCompetence(description);
            sessionSaved = false;
        }

        /// <summary>
        /// Removes a list of teams from TeamManager.
        /// </summary>
        /// <param name="teams">List of teams.</param>
        public void RemoveTeams(List<Team> teams)
        {
            competenceManager.DelinkTeamsFromAllCompetences(teams);
            teamManager.RemoveTeams(teams);
            sessionSaved = false;
        }

        /// <summary>
        /// Removes a list of members from a team in TeamManager.
        /// </summary>
        /// <param name="team">Team.</param>
        /// <param name="members">List of members.</param>
        public void RemoveMembers(List<Team> teams, List<Member> members)
        {
            competenceManager.DelinkMembersFromAllCompetences(members);
            teamManager.RemoveMembers(teams, members);
            sessionSaved = false;
        }

        /// <summary>
        /// Removes a list of competences from CompetenceManager.
        /// </summary>
        /// <param name="competences">List of competences.</param>
        public void RemoveCompeteneces(List<Competence> competences)
        {
            competenceManager.RemoveCompetences(competences);
            sessionSaved = false;
        }

        /// <summary>
        /// Renames a team in TeamManager and updates link in CompetenceManager.
        /// </summary>
        /// <param name="team">Team.</param>
        public void RenameTeam(Team team)
        {
            teamManager.RenameTeam(team);
            competenceManager.RefreshLink(team);
            sessionSaved = false;
        }

        /// <summary>
        /// Renames a member of a team in TeamManager and updates link in CompetenceManager.
        /// </summary>
        /// <param name="member">Member.</param>
        public void RenameMember(Member member)
        {
            teamManager.RenameMember(member);
            competenceManager.RefreshLink(member);
            sessionSaved = false;
        }

        /// <summary>
        /// Renames a competence in CompetenceManager.
        /// </summary>
        /// <param name="competence"></param>
        public void RenameCompetence(Competence competence)
        {
            competenceManager.RenameCompetence(competence);
            sessionSaved = false;
        }

        /// <summary>
        /// Links a list of members in a list of teams to a list of competences.
        /// </summary>
        /// <param name="competences">List of comptences.</param>
        /// <param name="teams">List of teams.</param>
        /// <param name="members">List of members.</param>
        public void LinkMembersInTeamsToCompetences(List<Competence> competences, List<Team> teams, List<Member> members)
        {
            competenceManager.LinkMembersInTeamsToCompetences(competences, teams, members);
            sessionSaved = false;
        }

        /// <summary>
        /// Delinks a list of members in a list of teams to a list of competences.
        /// </summary>
        /// <param name="competences">List of comptences.</param>
        /// <param name="teams">List of teams.</param>
        /// <param name="members">List of members.</param>
        public void DelinkMembersInTeamsFromCompetences(List<Competence> competences, List<Team> teams, List<Member> members)
        {
            competenceManager.DelinkMembersInTeamsFromCompetences(competences, teams, members);
            sessionSaved = false;
        }

        /// <summary>
        /// Delinks all members of a list of teams from a list of competences.
        /// </summary>
        /// <param name="competences">List of competences.</param>
        /// <param name="teams">List of teams.</param>
        public void DelinkTeamsFromCompetences(List<Competence> competences, List<Team> teams)
        {
            competenceManager.DelinkTeamsFromCompetences(competences, teams);
            sessionSaved = false;
        }

        /// <summary>
        /// Delinks a list of members from a list of competences.
        /// </summary>
        /// <param name="competences">List of competences.</param>
        /// <param name="members">List of members.</param>
        public void DelinkMembersFromCompetences(List<Competence> competences, List<Member> members)
        {
            competenceManager.DelinkMembersFromCompetences(competences, members);
            sessionSaved = false;
        }

        /// <summary>
        /// Gets save status about the current estate list session.
        /// </summary>
        /// <returns>True if session is saved, false if unsaved.</returns>
        public bool SessionSaved()
        {
            return sessionSaved;
        }

        /// <summary>
        /// Gets status on whether current session has never been saved.
        /// </summary>
        /// <returns>True if session has never been saved, otherwise false.</returns>
        public bool NeverSaved()
        {
            return neverSaved;
        }

        /// <summary>
        /// Inizializes a new empty session by clearing the TeamManager and CompetenceManager.
        /// </summary>
        public void New()
        {
            teamManager.RemoveAll();
            competenceManager.RemoveAll();
            neverSaved = true;
            sessionSaved = false;
        }

        /// <summary>
        /// Inizializes a new session by loading data from selected file into the TeamManager and CompetenceManager.
        /// </summary>
        /// <param name="filePath">Path of the selected file.</param>
        public void Open(string filePath)
        {
            defaultFilePath = filePath;
            List<dynamic> items = Serialization.BinaryDeserializeFromFile<List<dynamic>>(filePath);
            teamManager.RemoveAll();
            competenceManager.RemoveAll();
            foreach (Team team in items[0]) 
                teamManager.Add(team);
            foreach (Competence competence in items[1])
                competenceManager.Add(competence);
            neverSaved = false;
            sessionSaved = true;
        }

        /// <summary>
        /// Saves the currect session to the default file.
        /// </summary>
        public void Save()
        {
            List<dynamic> items = new List<dynamic>() { teamManager.GetAll(), competenceManager.GetAll() };
            Serialization.BinarySerializeToFile(items, defaultFilePath);
            neverSaved = false;
            sessionSaved = true;
        }

        /// <summary>
        /// Saves the currect session to the selected file.
        /// </summary>
        /// <param name="filePath">Path of the selected file.</param>
        public void SaveAs(string filePath)
        {
            defaultFilePath = filePath;
            Save();
        }
    }
}
