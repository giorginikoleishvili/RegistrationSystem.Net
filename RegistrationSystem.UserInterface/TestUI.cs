using RegistrationSystem.Repository.Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationSystem.UserInterface
{
    public partial class TestUI : Form
    {
        public TestUI()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var a = Repository.Layer.Repository.GetRepositoryInstance;
            var c = a.GetAllUser();
        }
    }
}
