﻿using System;
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
    public partial class FormSupply : Form
    {
        public FormSupply()
        {
            InitializeComponent();
            ShowAgents();
            ShowClients();
            ShowRealEstate();
            ShowSupplySet();
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
        void ShowRealEstate()
        {
            comboBoxRealEstate.Items.Clear();
            foreach (RealEstateSet realEstateSet in Program.wftDb.RealEstateSet)
            {
                string[] item = { realEstateSet.Id.ToString() + ".", realEstateSet.Address_City + ",", realEstateSet.Address_Street + ",", "д. " + realEstateSet.Address_House + ",", "кв. " + realEstateSet.Address_Number };
                comboBoxRealEstate.Items.Add(string.Join(" ", item));
            }
        }
        private void comboBoxAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxRealEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null && textBoxPrice.Text != "")
            {
                {
                    SupplySet supply = new SupplySet();
                    supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                    supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                    supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                    supply.Pirce = Convert.ToInt64(textBoxPrice.Text);
                    Program.wftDb.SupplySet.Add(supply);
                    Program.wftDb.SaveChanges();
                    ShowSupplySet();
                }
            }
            else MessageBox.Show("Данные не выбраны!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ShowSupplySet()
        {
            listViewSupplySet.Items.Clear();
            foreach (SupplySet supply in Program.wftDb.SupplySet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                   supply.IdAgent.ToString(), supply.IdClient.ToString(), supply.IdRealEstate.ToString(), supply.Pirce.ToString()
                });
                item.Tag = supply;
                listViewSupplySet.Items.Add(item);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1)
            {
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                supply.Pirce = Convert.ToInt64(textBoxPrice.Text);
                Program.wftDb.SaveChanges();
                ShowSupplySet();
            }
        }

        private void listViewSupplySet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1)
            {
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(supply.IdAgent.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(supply.IdClient.ToString());
                comboBoxRealEstate.SelectedIndex = comboBoxRealEstate.FindString(supply.IdRealEstate.ToString());
                textBoxPrice.Text = supply.Pirce.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem= null;
                comboBoxRealEstate.SelectedItem = null;
                textBoxPrice.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSupplySet.SelectedItems.Count == 1)
                {
                    SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                    Program.wftDb.SupplySet.Remove(supply);
                    Program.wftDb.SaveChanges();
                    ShowSupplySet();
                }
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstate = null;
                textBoxPrice.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
