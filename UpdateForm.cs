using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FallPresence
{
    public partial class UpdateForm : Form
    {
        Image updateButton;
        Image updateButtonPressed;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        public UpdateForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponent();



            //assign the stuff again
            ignoreButton = Properties.Resources.btnignore;
            ignoreButtonPressed = Properties.Resources.btnignore_pressed;
            updateButton = Properties.Resources.btnupdate;
            updateButtonPressed = Properties.Resources.btnupdate_pressed;

            //tell the pictureboxes what to do when i click them
            //these are all seperate, so that when clicking the event happens just once and when holding the colour doesnt just change for 1 tick
            picboxButtonUpdate.Click += new EventHandler(picboxButtonUpdate_Click);

            picboxButtonUpdate.MouseUp += new MouseEventHandler(picboxButtonUpdate_MouseUp);

            picboxButtonUpdate.MouseDown += new MouseEventHandler(picboxButtonUpdate_MouseDown);

            Focus();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //no idea what it does but it gets rid of the x button
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }


        public void picboxButtonUpdate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/wafflethings/FallPresence/releases/");
        }

        public void picboxButtonUpdate_MouseDown(object sender, EventArgs e)
        {
            //only happens when enabled
            picboxButtonUpdate.Image = updateButtonPressed;
        }

        public void picboxButtonUpdate_MouseUp(object sender, EventArgs e)
        {
            picboxButtonUpdate.Image = updateButton;
        }
    }
}
