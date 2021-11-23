/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DL;

namespace PL
{
    /// <summary>
    /// Partial presentation class that handles listbox events and I/O interaction with the user.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Detects when team name listbox selection changes and updates team members and team competences listbox.
        /// Single selection will update with team members in selected team as well as competences present.
        /// Multiple selections will update with common team members and competences for all selected teams.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void LbAddedTeamNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbAddedTeamNames.SelectedItems.Cast<Team>()
                .SelectMany(team => team.Members)
                .Distinct()
                .OrderBy(member => member.ID)
                .ToListBox(lbAddedTeamMembers);
            LbAddedTeamMembers_SelectionChanged(sender, e);
            RefreshButtonsEnabled();
        }

        /// <summary>
        /// Detects when team member listbox selection changes and updates team competences listbox.
        /// Single selection will update with team competences present for selected team member.
        /// Multiple selections will update with common team competences present for all selected team members.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void LbAddedTeamMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCompetenceNames.Items.Cast<Competence>()
                .Where(competence => competence.LinkedTo.Values
                    .Any(linked_members => linked_members
                        .Any(linked_member => lbAddedTeamMembers.SelectedItems.Count > 0 ?
                            lbAddedTeamMembers.SelectedItems.Cast<Member>()
                                .Any(selected_member => selected_member == linked_member)
                        :
                            lbAddedTeamMembers.Items.Cast<Member>()
                                .Any(selected_member => selected_member == linked_member)
                        )
                    )
                )
                .Distinct()
                .ToListBox(lbAddedTeamCompetences);
            RefreshButtonsEnabled();
        }

        /// <summary>
        /// Detects when competence name listbox selection changes and updates present in teams and held by members listboxes.
        /// Single selection will filter present in teams and held by members listboxes with teams and members where selected
        /// competence is present. Multiple selections will filter present in teams and held by members listboxes with teams
        /// and members where a selected combination of competences is present.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void LbCompetenceNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCompetenceNames.SelectedItems.Cast<Competence>()
                .SelectMany(competence => competence.LinkedTo.Keys)
                .Where(team => lbCompetenceNames.SelectedItems.Cast<Competence>()
                    .All(c => c.LinkedTo.Keys
                        .Any(teams => teams == team)
                    )
                )
                .Distinct()
                .OrderBy(team => team.ID)
                .ToListBox(lbCompetencesPresentInTeams);
            LbCompetencesPresentInTeams_SelectionChanged(sender, e);
            RefreshButtonsEnabled();
        }

        /// <summary>
        /// Detects when present in teams listbox selection changes and updates held by members listbox. Single selection
        /// will filter held by members listboxes with teams where selected competence is present. Multiple selections will
        /// filter present in held by members listbox with members where a selected combination of competences is present.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void LbCompetencesPresentInTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCompetenceNames.SelectedItems.Cast<Competence>()
                .SelectMany(competence => competence.LinkedTo
                    .SelectMany(pair => pair.Value)
                    .Where(member => lbCompetencesPresentInTeams.SelectedItems.Count > 0 ?
                        lbCompetencesPresentInTeams.SelectedItems.Cast<Team>()
                            .Any(team => competence.LinkedTo[team]
                                .Any(linked_members => linked_members == member)
                            )
                    :
                        lbCompetenceNames.SelectedItems.Cast<Competence>()
                            .All(selected_competence => selected_competence.LinkedTo.Values
                                .Any(linked_members => linked_members
                                   .Any(linked_member => linked_member == member)
                                )
                            )
                    )
                )
                .Distinct()
                .OrderBy(member => member.ID)
                .ToListBox(lbCompetencesHeldByMembers);            
            RefreshButtonsEnabled();
        }

        /// <summary>
        /// Detects when present in teams and held by members listbox selecteion changes in order to update them after a delink event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void LbDelink_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshButtonsEnabled();
        }
    }

    /// <summary>
    /// Nested extension class used to populate listboxes.
    /// </summary>
    public static class ListBoxExtensions
    {
        /// <summary>
        /// Similar to IList&lt;T&gt's "ForEach" extension method but implemented for IEnumerable&lt;T&gt.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="enumerable">List of items to be modified.</param>
        /// <param name="action">Action.</param>
        public static void ForEachEnumerableItem<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Extension method that fills a ListBox with a generic list (IEnumerable&lt;T&gt) of items.
        /// ListBox is cleared before list items are added.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">List of items to be added.</param>
        /// <param name="listbox">ListBox to be filled.</param>
        public static void ToListBox<T>(this IEnumerable<T> list, ListBox listbox)
        {
            listbox.Items.Clear();
            ForEachEnumerableItem(list, item => listbox.Items.Add(item));
        }
    }
}
