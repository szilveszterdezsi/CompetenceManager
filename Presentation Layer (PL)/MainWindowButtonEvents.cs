/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System;
using System.Linq;
using System.Windows;
using DL;

namespace PL
{
    /// <summary>
    /// Partial presentation class that handles button events and I/O interaction with the user.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Detects when the "Add Team" button is clicked.
        /// If all inputs are filled correctly controller will add a new team based on input.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            if (TeamInputCheck())
            {
                try
                {
                    controller.AddTeam(tbTeamName.Text, tbTeamMembers.Text);
                    RefreshID(tbTeamID, Teams);
                    tbTeamName.Text = "";
                    tbTeamMembers.Text = "";
                    MessageBox.Show("Successfully added new team.", "Add Team", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when the "Add Competence" button is clicked.
        /// If all inputs are filled correctly controller will add a new competence based on input.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnAddCompetence_Click(object sender, RoutedEventArgs e)
        {
            if (CompetenceInputCheck())
            {
                try
                {
                    controller.AddCompetence(tbCompetenceDescription.Text);
                    RefreshID(tbCompetenceID, Competences);
                    tbCompetenceDescription.Text = "";
                    MessageBox.Show("Successfully added new competence.", "Add Competence", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when the "Add Member" button is clicked.
        /// If all inputs are filled correctly controller will add a new member based on input and selected team.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnAddMember_Click(object sender, RoutedEventArgs e)
        {
            windowInput = new WindowInputDialog("Add Team Member", "Name", "", "Save");
            windowInput.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowInput.Topmost = true;
            windowInput.ShowDialog();
            if (windowInput.ValueSet)
            {
                try
                {
                    controller.AddMember((Team)lbAddedTeamNames.SelectedItem, windowInput.InputText);
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully added new member to selected team.", "Add Team Member", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when the "Remove Team" button is clicked.
        /// Controller will attempt to remove a list of team(s) based on selection.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnRemoveTeams_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove selected team(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    controller.RemoveTeams(lbAddedTeamNames.SelectedItems.Cast<Team>().ToList());
                    RefreshID(tbTeamID, Teams);
                    LbAddedTeamMembers_SelectionChanged(sender, null);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully removed selected team(s).", "Remove", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // do nothing
            }
        }

        /// <summary>
        /// Detects when the "Remove Member" button is clicked.
        /// Controller will attempt to remove a list of members(s) based on selection.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnRemoveMembers_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove selected member(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    controller.RemoveMembers(lbAddedTeamNames.SelectedItems.Cast<Team>().ToList(), lbAddedTeamMembers.SelectedItems.Cast<Member>().ToList());
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully removed selected member(s).", "Remove", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // do nothing
            }
        }

        /// <summary>
        /// Detects when the "Remove Competence" button is clicked.
        /// Controller will attempt to remove a list of competence(s) based on selection.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnRemoveCompetences_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove selected competence(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    controller.RemoveCompeteneces(lbCompetenceNames.SelectedItems.Cast<Competence>().ToList());
                    RefreshID(tbCompetenceID, Competences);
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully removed selected competence(s).", "Remove", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // do nothing
            }
        }

        /// <summary>
        /// Detects when the "Rename Team" button is clicked.
        /// Controller will attempt to rename selected team.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnRenameTeam_Click(object sender, RoutedEventArgs e)
        {
            Team team = lbAddedTeamNames.SelectedItem as Team;
            windowInput = new WindowInputDialog("Rename Team", "Name", team.Name, "Save");
            windowInput.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowInput.Topmost = true;
            windowInput.ShowDialog();
            if (windowInput.ValueSet)
            {
                try
                {
                    team.Name = windowInput.InputText;
                    controller.RenameTeam(team);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully renamed selected team.", "Rename Team", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when the "Rename Member" button is clicked.
        /// Controller will attempt to rename selected member.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnRenameMember_Click(object sender, RoutedEventArgs e)
        {
            Member member = lbAddedTeamMembers.SelectedItem as Member;
            windowInput = new WindowInputDialog("Rename Team Member", "Name", member.Name, "Save");
            windowInput.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowInput.Topmost = true;
            windowInput.ShowDialog();
            if (windowInput.ValueSet)
            {
                try
                {
                    member.Name = windowInput.InputText;
                    controller.RenameMember(member);
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully renamed selected team member.", "Rename Team Member", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when the "Rename Competence" button is clicked.
        /// Controller will attempt to rename selected competence.
        /// </summary>
        private void BtnRenameCompetence_Click(object sender, RoutedEventArgs e)
        {
            Competence competence = lbCompetenceNames.SelectedItem as Competence;
            windowInput = new WindowInputDialog("Rename Competence", "Name", competence.Description, "Save");
            windowInput.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowInput.Topmost = true;
            windowInput.ShowDialog();
            if (windowInput.ValueSet)
            {
                try
                {
                    competence.Description = windowInput.InputText;
                    controller.RenameCompetence(competence);
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully renamed selected competence.", "Rename Competence", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when the "Link Competence(s)" button is clicked.
        /// Controller will attempt to link selected competence(s) to selected team member(s).
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnLinkMembersInTeamsToCompetences_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                controller.LinkMembersInTeamsToCompetences(lbCompetenceNames.SelectedItems.Cast<Competence>().ToList(), lbAddedTeamNames.SelectedItems.Cast<Team>().ToList(), lbAddedTeamMembers.SelectedItems.Cast<Member>().ToList());
                LbAddedTeamNames_SelectionChanged(sender, null);
                LbCompetenceNames_SelectionChanged(sender, null);
                MessageBox.Show("Successfully linked selected competence(s) to team members.", "Link Competence(s)", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Detects when the "Delink Competence(s)" button is clicked.
        /// Controller will attempt to delink all selected competence(s) from all member(s) in team(s) or
        /// all selected member(s) in selected team(s) depending on if any members are selected.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnDelinkMembersOrTeamsFromCompetences_Click(object sender, RoutedEventArgs e)
        {
            if (lbAddedTeamMembers.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delink all selected competence(s) from selected team member(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        controller.DelinkMembersInTeamsFromCompetences(lbAddedTeamCompetences.SelectedItems.Cast<Competence>().ToList(), lbAddedTeamNames.SelectedItems.Cast<Team>().ToList(), lbAddedTeamMembers.SelectedItems.Cast<Member>().ToList());
                        MessageBox.Show("Successfully delinked all selected competence(s) from selected team member(s).", "Delink", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else
                {
                    // do nothing
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delink all selected competence(s) from all member(s) in selected team(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        controller.DelinkTeamsFromCompetences(lbAddedTeamCompetences.SelectedItems.Cast<Competence>().ToList(), lbAddedTeamNames.SelectedItems.Cast<Team>().ToList());
                        MessageBox.Show("Successfully delinked all selected competence(s) from all member(s) in selected team(s).", "Delink", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    // do nothing
                }
            }
            LbAddedTeamNames_SelectionChanged(sender, null);
            LbCompetenceNames_SelectionChanged(sender, null);
        }

        /// <summary>
        /// Detects when the "Delink Competence(s)" button is clicked.
        /// Controller will attempt to delink all selected competence(s) from all member(s) in selected team(s).
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnDelinkTeamsFromCompetences_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delink all selected competence(s) from all member(s) in selected team(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    controller.DelinkTeamsFromCompetences(lbCompetenceNames.SelectedItems.Cast<Competence>().ToList(), lbCompetencesPresentInTeams.SelectedItems.Cast<Team>().ToList());
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully delinked all selected competence(s) from all member(s) in selected team(s).", "Delink", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                // do nothing
            }
        }

        /// <summary>
        /// Detects when the "Delink Competence(s)" button is clicked.
        /// Controller will attempt to delink all selected member(s) in selected team(s).
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">EventArgs.</param>
        private void BtnDelinkMembersFromCompetences_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delink all selected competence(s) from selected team member(s)?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    controller.DelinkMembersFromCompetences(lbCompetenceNames.SelectedItems.Cast<Competence>().ToList(), lbCompetencesHeldByMembers.SelectedItems.Cast<Member>().ToList());
                    LbAddedTeamNames_SelectionChanged(sender, null);
                    LbCompetenceNames_SelectionChanged(sender, null);
                    MessageBox.Show("Successfully delinked all selected competence(s) from selected team member(s).", "Delink", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                // do nothing
            }
        }

        /// <summary>
        /// Enables/disables buttons based on ListBox selections.
        /// </summary>
        private void RefreshButtonsEnabled()
        {
            btnRenameTeam.IsEnabled = lbAddedTeamNames.SelectedItems.Count == 1;
            btnRemoveTeams.IsEnabled = lbAddedTeamNames.SelectedItems.Count > 0;
            btnAddMember.IsEnabled = lbAddedTeamNames.SelectedItems.Count == 1;
            btnRenameMember.IsEnabled = lbAddedTeamMembers.SelectedItems.Count == 1;
            btnRemoveMembers.IsEnabled = lbAddedTeamMembers.SelectedItems.Count > 0;
            btnRenameCompetence.IsEnabled = lbCompetenceNames.SelectedItems.Count == 1;
            btnRemoveCompetences.IsEnabled = lbCompetenceNames.SelectedItems.Count > 0;
            btnLinkMembersInTeamsToCompetences.IsEnabled = lbCompetenceNames.SelectedItems.Count > 0 && lbAddedTeamMembers.SelectedItems.Count > 0;
            btnDelinkMembersOrTeamsFromCompetences.IsEnabled = lbAddedTeamNames.SelectedItems.Count > 0 && lbAddedTeamCompetences.SelectedItems.Count > 0;
            btnDelinkTeamsFromCompetences.IsEnabled = lbCompetencesPresentInTeams.SelectedItems.Count > 0;
            btnDelinkMembersFromCompetences.IsEnabled = lbCompetencesHeldByMembers.SelectedItems.Count > 0;
        }

        /// <summary>
        /// Checks all team inputs and displays a messages if an input is empty.
        /// </summary>
        /// <returns>True is all inputs are filled, false if anything is left empty.</returns>
        private bool TeamInputCheck()
        {
            string title = "Missing Input";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage image = MessageBoxImage.Warning;
            if (string.IsNullOrEmpty(tbTeamName.Text))
                MessageBox.Show("Please enter a name!", title, button, image);
            else if (string.IsNullOrEmpty(tbTeamMembers.Text))
                MessageBox.Show("Please enter a team member!", title, button, image);
            else
                return true;
            return false;
        }

        /// <summary>
        /// Checks all competence inputs and displays a messages if an input is empty.
        /// </summary>
        /// <returns>True is all inputs are filled, false if anything is left empty.</returns>
        private bool CompetenceInputCheck()
        {
            string title = "Missing Input";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage image = MessageBoxImage.Warning;
            if (string.IsNullOrEmpty(tbCompetenceDescription.Text))
                MessageBox.Show("Please enter a competence description!", title, button, image);
            else
                return true;
            return false;
        }
    }
}
