using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campus_Admin_App
{
    public partial class AdminMain : Form
    {
        DbConnection conn = new DbConnection();
        CrudeOperation crude = new CrudeOperation();
        // Global variables 

        public AdminMain()
        {
            InitializeComponent();
            
            panelAddCourse.Visible = false;
            panelFees.Visible = false;
            //galobal variables 
            
        }

       

        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select photo(*.jpg;*.png;*.gif;)|*.jpg;*.png;*.gif";
            if(opf.ShowDialog()== DialogResult.OK)
            {
                pictureBoxUpload.Image = Image.FromFile(opf.FileName);
            }
        }
        public void showTable(DataGridView dataGrid,string tableName)
        {
            dataGrid.DataSource = crude.getTable(tableName);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGrid.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        public void showTableCourse(DataGridView dataGrid, string tableName)
        {
            dataGrid.DataSource = crude.getTable(tableName);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            
        }


        private void AdminMain_Load(object sender, EventArgs e)
        {
            
           
            showTable(dataGridViewLecturer, "lecturer");
            showTable(dataGridViewStudents, "student");
            showTableCourse(dataGridViewCourse, "module");
        }
        // to display data from text box
        private void DataGridViewLecturer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxLectureId.Text = dataGridViewLecturer.CurrentRow.Cells[0].Value.ToString();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            dataGridViewLecturer.DataSource = crude.searchTable(textBoxSearch.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridViewLecturer.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            int fid = int.Parse(textBoxLectureId.Text);
            string fname =textBoxName.Text;
            string fsurname = textBoxSurname.Text;
            string fModule = textBoxModule.Text;
            string fProgram = textBoxProgram.Text;
            string tableName = "program";
            string tableName2 = "module";
            string tableName3 = "lecturer";
            // getting photo from picture box
            MemoryStream memory = new MemoryStream();
            pictureBoxUpload.Image.Save(memory, pictureBoxUpload.Image.RawFormat);
            byte[] img = memory.ToArray();
            crude.insert(fid, fModule, tableName2);
            crude.insert(fid, fProgram, tableName);

            if (crude.insert(fid, fname, fsurname, img,tableName3))
            {
                MessageBox.Show("Youre succsefuly registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            int fid = int.Parse(textBoxLectureId.Text);
            string fname = textBoxName.Text;
            string fsurname = textBoxSurname.Text;
            string fModule = textBoxModule.Text;
            string fProgram = textBoxProgram.Text;
            string tableName = "program";
            string tableName2 = "module";
            string tableName3 = "lecturer";
            MemoryStream memory = new MemoryStream();
            pictureBoxUpload.Image.Save(memory, pictureBoxUpload.Image.RawFormat);
            byte[] img = memory.ToArray();


            if (crude.Update(fid,fname,fsurname,img))
            {
                MessageBox.Show("Youre succsefuly Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            int fid = int.Parse(textBoxLectureId.Text);
            crude.delete(fid,"lecturer");
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            int fid = int.Parse(textBoxID.Text);
            decimal amount = decimal.Parse(textBoxFees.Text);
            crude.payFees(fid, amount);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panelFees.Visible = true;
            lecturePanel.Visible = false;
            
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            panelAddCourse.Visible = true;
            panelFees.Visible = false;
            lecturePanel.Visible = false;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int fid = int.Parse(textBoxCourseId.Text);
            string fname = textBoxCourse.Text;
            int lectId = int.Parse(textBoxLecture_id.Text);
            crude.delete(fid, "student");
            crude.addModule(fid, fname, lectId);
        }

        private void BtnDocuments_Click(object sender, EventArgs e)
        {
            lecturePanel.Visible = true;
            panelAddCourse.Visible = false;
            panelFees.Visible = false;
        }
    }
}
