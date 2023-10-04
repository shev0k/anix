using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AniX_APP.Forms_Utility
{
    public partial class User_Add_Edit : Form
    {
        public enum FormMode { Add, Edit }
        private FormMode currentMode;
        public User_Add_Edit(FormMode mode)
        {
            InitializeComponent();
            currentMode = mode;
        }

        private void User_Add_Edit_Load(object sender, EventArgs e)
        {
            if (currentMode == FormMode.Add)
            {
                lbFormTitle.Text = "Add User";
                // WIP
            }
            else
            {
                lbFormTitle.Text = "Edit User";
                // WIP
            }
        }

        private bool ValidateForm()
        {
            if (currentMode == FormMode.Add)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (currentMode == FormMode.Add)
                {
                    // Code to add a new user
                }
                else
                {
                    // Code to update an existing user
                }
            }
        }
    }
}
