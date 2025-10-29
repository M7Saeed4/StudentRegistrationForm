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

namespace StudentRegistrationForm
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IbIResult_Click(object sender, EventArgs e)
        {

        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblSelectedColor.Text = $"Selected Color: {colorDialog.Color.Name}";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // جمع البيانات من الحقول
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string gender = rdoMale.Checked ? "Male" : rdoFemale.Checked ? "Female" : "Other";
            string birthdate = dtpBirthdate.Value.ToString("yyyy-MM-dd");
            string country = cmbCountry.SelectedItem?.ToString() ?? "Not Selected";
            string color = lblSelectedColor.Text.Replace("Selected Color: ", "");

            // عرض النتيجة في MessageBox
            string result = $"Name: {name}\nEmail: {email}\nGender: {gender}\n" +
                            $"Birthdate: {birthdate}\nCountry: {country}\nFavorite Color: {color}";

            MessageBox.Show(result, "Registration Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // مسح حقول النص
            txtName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";

            // إلغاء اختيار الجنس
            rdoMale.Checked = false;
            rdoFemale.Checked = false;


            // إعادة تعيين التاريخ للتاريخ الحالي
            dtpBirthdate.Value = DateTime.Now;

            // إعادة تعيين الدولة
            cmbCountry.SelectedIndex = -1;

            // إعادة تعيين اللون
            lblSelectedColor.Text = "No Color Selected";

            // إعادة التركيز على أول حقل
            txtName.Focus();

            // أضف في دالة Form_Load

            picStudent.Image = null;

            MessageBox.Show("Reset Successfuly!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // التحقق من الحقول الأساسية
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Name and Email before saving!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // جمع البيانات (بما فيها مسار الصورة)
            string imagePath = "No Image";
            if (picStudent.Image != null)
            {
                string imageName = $"student_{txtName.Text}_image.jpg";
                imagePath = Path.Combine("Student Images", imageName);
            }

            string data = $"Name: {txtName.Text}\n" +
                          $"Email: {txtEmail.Text}\n" +
                          $"Gender: {(rdoMale.Checked ? "Male" : "Female")}\n" +
                          $"Birthdate: {dtpBirthdate.Value:yyyy-MM-dd}\n" +
                          $"Country: {cmbCountry.SelectedItem}\n" +
                          $"Favorite Color: {lblSelectedColor.Text.Replace("Selected Color: ", "")}\n" +
                          $"Image Path: {imagePath}\n" +  // ⬅️ الجديد
                          $"Registration Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            try
            {
                string fileName = $"student_{txtName.Text}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                string projectPath = Directory.GetParent(Application.StartupPath).Parent.FullName;
                string databaseFolder = Path.Combine(projectPath, "database");
                Directory.CreateDirectory(databaseFolder);

                string filePath = Path.Combine(databaseFolder, fileName);
                File.WriteAllText(filePath, data);

                // حفظ الصورة إذا موجودة
                if (picStudent.Image != null)
                {
                    string imagesFolder = Path.Combine(projectPath, "database", "Student Images");
                    Directory.CreateDirectory(imagesFolder);
                    string imageSavePath = Path.Combine(imagesFolder, $"student_{txtName.Text}_image.jpg");
                    picStudent.Image.Save(imageSavePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                System.Diagnostics.Process.Start("explorer.exe", databaseFolder);
                MessageBox.Show($"Data saved successfully!\nFile: {fileName}", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picStudent.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
