using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campus_Admin_App
{
    public partial class Login : Form
    {
        LectureDataOperation lecture = new LectureDataOperation();
        public Login()
        {
            InitializeComponent();
        }
        
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtStudentId.Text);
                GetSet.ID = id;

                if (lecture.getPass(id, txtPassword.Text) == true)
                {
                    new LecturerMain().Show();
                    this.Hide();
                }
                else if (lecture.getPassAdmin(id, txtPassword.Text) ==true)
                {
                    new AdminMain().Show();
                    this.Hide();
                }
                else if(lecture.getPass(id, txtPassword.Text) == false)
                {

                    MessageBox.Show("Incorrect password or ID");
                }
                else if (lecture.getPassAdmin(id, txtPassword.Text) == false)
                {

                    MessageBox.Show("Incorrect password or ID");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Please enter your correct Id number", a.Message);
            }

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtStudentId.Text = "";
        }
    }
}
