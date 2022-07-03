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
    public partial class LecturerMain : Form
    {
        //Initialising objects from database clase classes to carry out database operations
        LectureDataOperation lectureData = new LectureDataOperation();
        CrudeOperation crude = new CrudeOperation();
        DbConnection connection = new DbConnection();

        public LecturerMain()
        {
            InitializeComponent();
            panelSudentAssignments.Visible = false;
        }

        private void LecturerMain_Load(object sender, EventArgs e)
        {
            getDocuments(0, label_1, panel_0, "documents_lecturer");
            getDocuments(1, label_2, panel_1, "documents_lecturer");
            getDocuments(2, label_3, panel_2, "documents_lecturer");
            getDocuments(3, label_4, panel_3, "documents_lecturer");
            getDocuments(4, label_5, panel_4, "documents_lecturer");
            getDocuments(5, label_6, panel_5, "documents_lecturer");
            getDocuments(6, label_7, panel_6, "documents_lecturer");
            getDocuments(7, label_8, panel_7, "documents_lecturer");
            getDocuments(8, label_9, panel_8, "documents_lecturer");


            //getting students documents
            getDocuments(0, label__0, panel__0, "documents_lecturer");
            getDocuments(1, label__1, panel__1, "documents_lecturer");
            getDocuments(2, label__2, panel__2, "documents_lecturer");
            getDocuments(3, label__3, panel__3, "documents_lecturer");
            getDocuments(4, label__4, panel__4, "documents_lecturer");
            getDocuments(5, label__5, panel__5, "documents_lecturer");
            getDocuments(6, label__6, panel__6, "documents_lecturer");
            getDocuments(7, label__7, panel__7, "documents_lecturer");
            getDocuments(8, label__8, panel__8, "documents_lecturer");


        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button_Upload(object sender, EventArgs e)
        {
            try
            {
                int LectureId = int.Parse(txtModule.Text);
                string documentName = txtDocumentName.Text;
                //getting file
                FileStream file = File.OpenRead(txtFilename.Text);
                byte[] content = new byte[file.Length];
                file.Read(content, 0, (int)file.Length);
                lectureData.upLoadDocument(documentName, LectureId, content);
            }catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
            
        }

        private void Button_SelectFile(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "select photo(*.pdf)|*.pdf";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                txtFilename.Text = opf.FileName;
            }
        }

        private void Button_Delete(object sender, EventArgs e)
        {

        }
        public void getDocuments(int index, Label label, Panel panel,string tableName)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM `"+tableName+"` WHERE 1", connection.GetConnection);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);

                label.Text = (string)table.Rows[index]["FileName"];
            }
            catch (IndexOutOfRangeException e)
            {
                panel.Visible = false;
            }
            catch (AggregateException e)
            {
                MessageBox.Show("Failed to establish a connection to the server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_social_Click(object sender, EventArgs e)
        {
            panelSudentAssignments.Visible = true;
            lecturePanel.Visible = false;
        }

        private void Btn_lecture_Click(object sender, EventArgs e)
        {
            panelSudentAssignments.Visible = false;
            lecturePanel.Visible = true;
        }
    }
}
