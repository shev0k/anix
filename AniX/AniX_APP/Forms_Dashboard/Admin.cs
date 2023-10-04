using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anix_Shared.DomainModels;

namespace AniX_APP.Forms_Dashboard
{
    public partial class Admin : Form
    {
        private User _loggedInUser;
        public Admin(User loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            btnUser.Text = $" {_loggedInUser.Username}";
        }
    }
}
