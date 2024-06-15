using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportUtils
{
    public partial class Form1 : Form
    {
        private readonly Plugin plugin;

        public Form1(Plugin plugin)
        {
            InitializeComponent();

            this.plugin = plugin;

            FormClosed += (o, e) =>
            {
                Hide();
            };
        }

        private void RemoveSelectedBonesButton_Click(object sender, EventArgs e)
        {
            plugin.RemoveSelectedBones();
        }

        private void RemoveUnweightedBonesButton_Click(object sender, EventArgs e)
        {
            plugin.RemoveUnweightedBones();
        }

        private void SortMaterialByTextureButton_Click(object sender, EventArgs e)
        {
            plugin.SortMaterialByTexture();
        }

        private void ReplaceTexturePathButton_Click(object sender, EventArgs e)
        {
            var find = findText.Text;
            var replace = replaceText.Text;
            plugin.ReplaceTexturePath(find, replace);
        }
    }
}
