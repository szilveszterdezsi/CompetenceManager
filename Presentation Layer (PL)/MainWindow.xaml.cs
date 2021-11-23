/// ---------------------------
/// Author: Szilveszter Dezsi
/// Created: 2019-10-20
/// Modified: n/a
/// ---------------------------

using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using BLL;
using DL;

namespace PL
{
    /// <summary>
    /// Partial presentation class that initializes GUI components.
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<Competence> Competences { get { return controller.GetAllCompetences(); } }
        private BindingList<Team> Teams { get { return controller.GetAllTeams(); } }
        private WindowInputDialog windowInput;
        private Controller controller;

        /// <summary>
        /// Constructor that initializes GUI componenets and controller.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            controller = new Controller();
            //DataContext = this;
            lbAddedTeamNames.ItemsSource = Teams;
            lbCompetenceNames.ItemsSource = Competences;
            RefreshID(tbTeamID, Teams);
            RefreshID(tbCompetenceID, Competences);
        }

        /// <summary>
        /// Refresh ID textbox based on current items in list.
        /// </summary>
        private void RefreshID(TextBox idBox, dynamic list)
        {
            int id, count = list.Count;
            if (count > 0)
                id = list[count-1].ID + 1;
            else
                id = 1;
            idBox.Text = id.ToString();
        }
    }
}
