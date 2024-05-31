using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace utils
{
    public partial class Form1 : Form
    {
        private readonly Plugin plugin;

        public Form1(Plugin plugin)
        {
            InitializeComponent();
            this.plugin = plugin;

            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
        }

        private void RemoveTipBonesButton_Click(object sender, EventArgs e)
        {
            plugin.RemoveTipBones();
        }

        private void SortMaterialByTexButton_Click(object sender, EventArgs e)
        {
            plugin.SortMaterialByTexture();
        }

        private void RemoveAndReplaceButton_Click(object sender, EventArgs e)
        {
            plugin.RemoveAndReplaceBone();
        }
    }
}
