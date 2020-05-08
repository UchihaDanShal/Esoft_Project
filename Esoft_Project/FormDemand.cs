using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormDemand : Form
    {
        public FormDemand()
        {
            InitializeComponent();
            ShowAgents();
            ShowClients();
            ShowDemandSet();
        }
        void ShowAgents()
        {
            comboBoxAgents.Items.Clear();
            foreach (AgentsSet agentsSet in Program.wftDb.AgentsSet)
            {
                string[] item = { agentsSet.Id.ToString() + ".", agentsSet.FirstName, agentsSet.MiddleName, agentsSet.LastName };
                comboBoxAgents.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            comboBoxClients.Items.Clear();
            foreach (ClientsSet clientsSet in Program.wftDb.ClientsSet)
            {
                string[] item = { clientsSet.Id.ToString() + ".", clientsSet.FirstName, clientsSet.MiddleName, clientsSet.LastName };
                comboBoxClients.Items.Add(string.Join(" ", item));
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && textBoxType.Text != "")
            {
                {
                    DemandSet demand = new DemandSet();
                    demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                    demand.Type = textBoxType.Text;
                    demand.MinPrice = textBoxMinPrice.Text;
                    demand.MaxPrice = textBoxMaxPrice.Text;
                    demand.MinArea = textBoxMinArea.Text;
                    demand.MaxArea = textBoxMaxArea.Text;
                    demand.MinRooms = textBoxMinRooms.Text;
                    demand.MaxRooms = textBoxMaxRooms.Text;
                    demand.MinFloor = textBoxMinFloor.Text;
                    demand.MaxFloor = textBoxMaxFloor.Text;
                    demand.MinFloors = textBoxMinFloors.Text;
                    demand.MaxFloors = textBoxMaxFloors.Text;                     
                    Program.wftDb.DemandSet.Add(demand);
                    Program.wftDb.SaveChanges();
                    ShowDemandSet();
                }
            }
            else MessageBox.Show("Данные не выбраны!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ShowDemandSet()
        {
            listViewDemandSet.Items.Clear();
            foreach (DemandSet demand in Program.wftDb.DemandSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                   demand.IdAgent.ToString(), demand.IdClient.ToString(), demand.Type, demand.MinArea, demand.MaxArea, demand.MinPrice, demand.MaxPrice, demand.MinRooms, demand.MaxRooms, demand.MinFloor, demand.MaxFloor, demand.MinFloors, demand.MaxFloors
                });
                item.Tag = demand;
                listViewDemandSet.Items.Add(item);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewDemandSet.SelectedItems.Count == 1)
            {
                DemandSet demand = listViewDemandSet.SelectedItems[0].Tag as DemandSet;
                demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                demand.Type = textBoxType.Text;
                demand.MinPrice = textBoxMinPrice.Text;
                demand.MaxPrice = textBoxMaxPrice.Text;
                demand.MinArea = textBoxMinArea.Text;
                demand.MaxArea = textBoxMaxArea.Text;
                demand.MinRooms = textBoxMinRooms.Text;
                demand.MaxRooms = textBoxMaxRooms.Text;
                demand.MinFloor = textBoxMinFloor.Text;
                demand.MaxFloor = textBoxMaxFloor.Text;
                demand.MinFloors = textBoxMinFloors.Text;
                demand.MaxFloors = textBoxMaxFloors.Text;
                Program.wftDb.SaveChanges();
                ShowDemandSet();
            }
        }

        private void listViewDemandSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDemandSet.SelectedItems.Count == 1)
            {
                DemandSet demand = listViewDemandSet.SelectedItems[0].Tag as DemandSet;
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                demand.Type = textBoxType.Text;
                demand.MinPrice = textBoxMinPrice.Text;
                demand.MaxPrice = textBoxMaxPrice.Text;
                demand.MinArea = textBoxMinArea.Text;
                demand.MaxArea = textBoxMaxArea.Text;
                demand.MinRooms = textBoxMinRooms.Text;
                demand.MaxRooms = textBoxMaxRooms.Text;
                demand.MinFloor = textBoxMinFloor.Text;
                demand.MaxFloor = textBoxMaxFloor.Text;
                demand.MinFloors = textBoxMinFloors.Text;
                demand.MaxFloors = textBoxMaxFloors.Text;
                
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                textBoxType.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxFloor.Text = "";
                textBoxMinFloors.Text = "";
                textBoxMaxFloors.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDemandSet.SelectedItems.Count == 1)
                {
                    DemandSet demand = listViewDemandSet.SelectedItems[0].Tag as DemandSet;
                    Program.wftDb.DemandSet.Remove(demand);
                    Program.wftDb.SaveChanges();
                    ShowDemandSet();
                }
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                textBoxType.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxPrice.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxFloor.Text = "";
                textBoxMinFloors.Text = "";
                textBoxMaxFloors.Text = "";

            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
