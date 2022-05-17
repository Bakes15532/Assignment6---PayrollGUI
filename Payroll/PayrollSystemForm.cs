// Payroll.cs
//
// Joshua Baker
// 
// Payroll System Main Form.

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Payroll
{
    public partial class PayrollSystemForm : Form
    {

        public PayrollSystemForm()
        {
            InitializeComponent();

            // populate the state combobox
            foreach (string state in Address.StateNames)
                cmbState.Items.Add(state);

            // clear all fields
            clear();
        }

        private void PayrollSystemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the system?",
                "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                e.Cancel = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void abroutPayrollSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payroll Information System V1.0, by Huimin Zhao",
                "About Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The printing function is still under construction!",
                "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void pageSetupStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The page setup function is still under construction!",
                "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The print preview function is still under construction!",
                "Payroll System", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstFacultyMembers.Items.Count == 0)
                return;

            // object for serializing Records in binary format
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream output = null; // stream for writing to a file

            DialogResult result;
            string fileName; // name of file to save data

            // create dialog box enabling user to save file
            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // allow user to create file
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }

            // exit event handler if user clicked "Cancel"
            if (result == DialogResult.Cancel)
                return;

            // show error if user specified invalid file
            if (fileName == "" || fileName == null)
            {
                MessageBox.Show("Invlaid File Name", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // save file via FileStream if user specified valid file
            try
            {
                // open file with write access
                output = new FileStream(fileName,
                   FileMode.OpenOrCreate, FileAccess.Write);
                // save records to file
                foreach (FacultyMember item in lstFacultyMembers.Items)
                {
                    formatter.Serialize(output, item);
                }
            } // end try
            // handle exception if there is a problem opening the file
            catch (IOException)
            {
                // notify user if file does not exist
                MessageBox.Show("Error opening file", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
            // handle exception if there is a problem in serialization
            catch (SerializationException)
            {
                MessageBox.Show("Error writing to file", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
            finally
            {
                if (output != null)
                    output.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // object for deserializing Record in binary format
            BinaryFormatter reader = new BinaryFormatter();
            FileStream input = null; // stream for reading from a file

            
            DialogResult result;
            string fileName; // name of file containing data

            // create dialog box enabling user to open file
            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }


            // exit event handler if user clicked Cancel
            if (result == DialogResult.Cancel)
                return;

            // show error if user specified invalid file
            if (fileName == "" || fileName == null)
            {
                MessageBox.Show("Invalid File Name", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // create FileStream to obtain read access to file
                input = new FileStream(
                   fileName, FileMode.Open, FileAccess.Read);

                // read records from file
                lstFacultyMembers.Items.Clear();
                FacultyMember facultyMember = (FacultyMember)reader.Deserialize(input);
                while (facultyMember != null)
                {
                    lstFacultyMembers.Items.Add(facultyMember);
                    facultyMember = (FacultyMember)reader.Deserialize(input);
                }
            }
            catch
            {
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstFacultyMembers.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to update the following facultyMember?\n" +
                (FacultyMember)lstFacultyMembers.SelectedItem,
               "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if(add())
                lstFacultyMembers.Items.Remove(lstFacultyMembers.SelectedItem);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstFacultyMembers.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the following facultyMember?\n" +
                (FacultyMember)lstFacultyMembers.SelectedItem,
               "Payroll System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                lstFacultyMembers.Items.Remove(lstFacultyMembers.SelectedItem);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void lstFacultyMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure an item is selected in the list view.
            if (lstFacultyMembers.SelectedItems.Count == 0)
            {
                return;
            }

            FacultyMember facultyMember = (FacultyMember)lstFacultyMembers.SelectedItem;
            clear();
            displayFacultyMember(facultyMember);
        }

        // clear all fields
        private void clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            radMale.Checked = true;
            chkMarried.Checked = false;
            dtpStartDate.Text = DateTime.Now.ToShortDateString();
            txtYearsWorked.Text = "";
            txtStreet.Text = "";
            txtCity.Text = "Milwaukee";
            cmbState.Text = "WISCONSIN";
            txtZip.Text = "53201";
            txtHomePhone.Text = "(414)";
            txtWorkPhone.Text = "(414)"; 
            radSalaried.Checked = true;
            txtIsAdjunctRate.Text = "";
            txtHoursWorked.Text = "";
            txtRank.Text = "";
            txtAcademicTitle.Text = "";
            txtEarnings.Text = "";
            txtRetirementPercentage.Text = "";
            txtRetirementAmount.Text = "";
        }

        // add a new facultyMember
        private bool add()
        {
            FacultyMember facultyMember;
            if (radSalaried.Checked)
                facultyMember = new Professor();
            else
                facultyMember = new Lecturer();
            if (getInputs(facultyMember) == true)
            {
                lstFacultyMembers.Items.Add(facultyMember);
                displayFacultyMember(facultyMember);
                return true;
            }
            return false;
        }

        // get input fields of facultyMember
        private bool getInputs(FacultyMember facultyMember)
        {
            try
            {
                facultyMember.FirstName = txtFirstName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,	Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            try
            {
                facultyMember.LastName = txtLastName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            try
            {
                facultyMember.HomePhone = new PhoneNumber(txtHomePhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHomePhone.Focus();
                return false;
            }

            try
            {
                facultyMember.WorkPhone = new PhoneNumber(txtWorkPhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWorkPhone.Focus();
                return false;
            }

            facultyMember.IsMale = radMale.Checked;
            
            facultyMember.Married = chkMarried.Checked;
            
            try
            {
                facultyMember.StartDate = dtpStartDate.Value;
                txtYearsWorked.Text = facultyMember.YearsWorked.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartDate.Focus();
                return false;
            }
            
            try
            {
                facultyMember.HomeAddress = new Address(txtStreet.Text, txtCity.Text, cmbState.Text, txtZip.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbState.Focus();
                return false;
            }

            if (facultyMember is Professor)
            {
                try
                {
                    if (txtRank.Text == "")
                        txtRank.Text = "0";
                    ((Professor)facultyMember).Rank = (int)decimal.Parse(txtRank.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRank.Focus();
                    return false;
                }
            }
            else if(facultyMember is Lecturer)
            {
                try
                {
                    if (txtIsAdjunctRate.Text == "")
                        txtIsAdjunctRate.Text = "0";
                    ((Lecturer)facultyMember).IsAdjunct = bool.Parse(txtIsAdjunctRate.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIsAdjunctRate.Focus();
                    return false;
                }
                try
                {
                    if (txtHoursWorked.Text == "")
                        txtHoursWorked.Text = "0";
                    ((Lecturer)facultyMember).Salary = decimal.Parse(txtHoursWorked.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoursWorked.Focus();
                    return false;
                }                
            }

            return true;
        }

        // display facultyMember
        private void displayFacultyMember(FacultyMember facultyMember)
        {
            txtFirstName.Text = facultyMember.FirstName;
            txtLastName.Text = facultyMember.LastName;
            txtHomePhone.Text = facultyMember.HomePhone.ToString();
            txtWorkPhone.Text = facultyMember.WorkPhone.ToString();
            chkMarried.Checked = facultyMember.Married;
            radMale.Checked = facultyMember.IsMale;
            radFemale.Checked = facultyMember.IsFemale;
            dtpStartDate.Text = facultyMember.StartDate.ToShortDateString();
            txtYearsWorked.Text = facultyMember.YearsWorked.ToString();
            txtStreet.Text = facultyMember.HomeAddress.Street;
            txtCity.Text = facultyMember.HomeAddress.City;
            cmbState.Text = facultyMember.HomeAddress.State;
            txtZip.Text = facultyMember.HomeAddress.Zip;
            txtAcademicTitle.Text = facultyMember.AcademicTitle;
            txtEarnings.Text = facultyMember.Salary.ToString("C");
            txtRetirementPercentage.Text = facultyMember.RetirementPercentage.ToString();
            txtRetirementAmount.Text = facultyMember.RetirementAmount.ToString("C");
            if (facultyMember is Professor)
            {
                radSalaried.Checked = true;
                txtRank.Text = ((Professor)facultyMember).Rank.ToString();
            }
            else
            {
                radHourly.Checked = true;
                txtIsAdjunctRate.Text = ((Lecturer)facultyMember).IsAdjunct.ToString();
                txtHoursWorked.Text = ((Lecturer)facultyMember).Salary.ToString();
            }
        }
    }
}