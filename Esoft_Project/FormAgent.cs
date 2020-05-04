using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormAgent : Form
    {
        public FormAgent()
        {
            InitializeComponent();
            ShowAgent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AgentsSet agentsSet = new AgentsSet();

            agentsSet.FirstName = textBoxFirstName.Text;
            agentsSet.MiddleName = textBoxMiddleName.Text;
            agentsSet.LastName = textBoxLastName.Text;
            agentsSet.DealShare = textBoxDealShare.Text;

            Program.wftDb.AgentsSet.Add(agentsSet);

            Program.wftDb.SaveChanges();
            ShowAgent();
        }
        void ShowAgent()
        {
            listViewAgent.Items.Clear();

            foreach (AgentsSet agentsSet in Program.wftDb.AgentsSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    agentsSet.Id.ToString(), agentsSet.FirstName, agentsSet.MiddleName, agentsSet.LastName, agentsSet.DealShare
                });
                item.Tag = agentsSet;

                listViewAgent.Items.Add(item);
            }
            listViewAgent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewAgent.SelectedItems.Count == 1)
            {
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                agentsSet.FirstName = textBoxFirstName.Text;
                agentsSet.MiddleName = textBoxMiddleName.Text;
                agentsSet.LastName = textBoxLastName.Text;
                agentsSet.DealShare = textBoxDealShare.Text;
                
                Program.wftDb.SaveChanges();
                ShowAgent();
            }
        }
        private void listViewAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAgent.SelectedItems.Count == 1)
            {
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                agentsSet.FirstName = textBoxFirstName.Text;
                agentsSet.MiddleName = textBoxMiddleName.Text;
                agentsSet.LastName = textBoxLastName.Text;
                agentsSet.DealShare = textBoxDealShare.Text;

                Program.wftDb.SaveChanges();
                ShowAgent();
            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                    Program.wftDb.AgentsSet.Remove(agentsSet);
                    Program.wftDb.SaveChanges();
                    ShowAgent();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
