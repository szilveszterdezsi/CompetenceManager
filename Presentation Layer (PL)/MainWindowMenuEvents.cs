/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2018-09-27
/// Modified: 2019-10-20
/// ---------------------------

using System;
using System.Windows;
using Microsoft.Win32;

namespace PL
{
    /// <summary>
    /// Partial presentation class that handles File-menu events and I/O interaction with the user.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Checks "save status" and lets user choose "Yes, No or Cancel" if status is "unsaved" and content is not empty.
        /// If user chooses "Yes" the method SaveCommand_Executed is fired as if user clicked "Save" in menu.
        /// If user chooses "No" or "Cancel" nothing happens.
        /// </summary>
        /// <returns>True if status is "saved" and if user chooses "Yes" or "No", otherwise false.</returns>
        private bool SaveCheck()
        {
            if (!controller.SessionSaved() && lbAddedTeamNames.Items.Count > 0 && lbCompetenceNames.Items.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save current session?", "Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveCommand_Executed(null, null);
                    return true;
                }
                else if (result == MessageBoxResult.No)
                    return true;
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Detects when "New" is clicked in the File-menu and performs a SaveCheck.
        /// If SaveCheck returns true controller will attempt to reset to start-up.
        /// If attempt fails an error info message is displayed.
        /// </summary>
        /// <param name="sender">Component clicked.</param>
        /// <param name="e">Routed event.</param>
        private void NewCommand_Executed(object sender, RoutedEventArgs e)
        {
            if (SaveCheck())
            {
                try 
                {
                    controller.New();
                    RefreshID(tbTeamID, Teams);
                    RefreshID(tbCompetenceID, Competences);
                    miSaveAs.IsEnabled = false;
                    MessageBox.Show("New session initialized.", "New", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when "Open" is clicked in the File-menu and performs a SaveCheck.
        /// If SaveCheck returns true a dialog to select file opens and controller will attempt to load data from selected file.
        /// If attempt fails an error info message is displayed.
        /// </summary>
        /// <param name="sender">Component clicked.</param>
        /// <param name="e">Routed event.</param>
        private void OpenCommand_Executed(object sender, RoutedEventArgs e)
        {
            if (SaveCheck())
            {
                OpenFileDialog op = new OpenFileDialog { Title = "Open", Filter = "Data files (*.dat)|*.dat" };
                if (op.ShowDialog() == true)
                {
                    try
                    {
                        controller.Open(op.FileName);
                        miSaveAs.IsEnabled = true;
                        MessageBox.Show("Successfully opened selected file.", "Open", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Detects when "Save" is clicked in the File-menu and checks "never saved" status.
        /// If "never saved" status returns false the method SaveAsCommand_Executed is fired as if user clicked "SaveAs" in menu.
        /// If "never saved" status returns true controller attempts to save current session to default save file.
        /// If attempt fails an error info message is displayed.
        /// </summary>
        /// <param name="sender">Component clicked.</param>
        /// <param name="e">Routed event.</param>
        private void SaveCommand_Executed(object sender, RoutedEventArgs e)
        {
            if (controller.NeverSaved())
                SaveAsCommand_Executed(sender, e);
            else
            {
                try
                {
                    controller.Save();
                    MessageBox.Show("Successfully saved to file.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when "SaveAs" is clicked in the File-menu.
        /// A dialog to select file opens and controller attempts to save current session to selected save file.
        /// If attempt fails an error info message is displayed.
        /// </summary>
        /// <param name="sender">Component clicked.</param>
        /// <param name="e">Routed event.</param>
        private void SaveAsCommand_Executed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog { Title = "Save as...", Filter = "Data files (*.dat)|*.dat" };
            if (op.ShowDialog() == true)
            {
                try
                {
                    controller.SaveAs(op.FileName);
                    miSaveAs.IsEnabled = true;
                    MessageBox.Show("Successfully saved to selected file.", "Save As", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Detects when "Exit" is clicked in the File-menu and closes the application.
        /// </summary>
        /// <param name="sender">Component clicked.</param>
        /// <param name="e">Routed event.</param>
        private void ExitCommand_Executed(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Overrides and detects when any shutdown event is triggered and performs a SaveCheck.
        /// If SaveCheck returns true close of application is aborted.
        /// If SaveCheck returns false application closes as usual.
        /// </summary>
        /// <param name="e">Cancel event.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (SaveCheck())
                e.Cancel = false;
        }
    }
}
