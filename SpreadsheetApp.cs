using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Q4_gui
{
    public partial class SpreadsheetApp : Form
    {
        static ShareableSpreadSheet sheet;
        
        //Constructor of the Form
        public SpreadsheetApp()
        {
            InitializeComponent();
            MessageBox.Show("Welcome to the SpreadsheetApp!");
            Boolean good_input = false;
            int row = 0;
            int col = 0;

            //Checks input
            while (!good_input)
            {
                String row_input = Interaction.InputBox("Please enter an initial row size: ");
                String col_input = Interaction.InputBox("Please enter an initial column size: ");

                //User pressed cancel button or pressed OK without text or entered not a number
                if (String.IsNullOrEmpty(row_input) || String.IsNullOrEmpty(col_input) || !int.TryParse(row_input, out row) || !int.TryParse(col_input, out col) || Convert.ToInt32(row_input) == 0 || Convert.ToInt32(col_input) == 0)
                {
                    MessageBox.Show("Wrong input - try again please!");
                    continue;
                }

                good_input = true;
            }


            MessageBox.Show("Generating a default " + row + "x" + col + " SpreadSheet...");
            sheet = new ShareableSpreadSheet(row, col); //Creates a new ShareableSpreadSheet
            sheet.Test_fill();
            

            //Creates a new datagridView to display the ShareableSpreadSheet data
            dataGridView1.ColumnCount = col;
            dataGridView1.RowCount = row;
            
            //Fills the datagridView with the data from sheet
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = sheet.getCell(i + 1, j + 1);
                }
            }
        }

        //Load button click event
        private void LoadButton_Click(object sender, EventArgs e)
        {
            //Creates a new OpenFileDialog to select easily the file to load
            OpenFileDialog openFileDialog1;
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            DialogResult result = openFileDialog1.ShowDialog();
            
            //Extracts the file's path
            String path = openFileDialog1.FileName;
            if (path != null && path != "")
            {
                //Loads the sheet using the file's path
                Boolean res = sheet.load(path);

                if (res)
                {
                    //Resizes the datagridview
                    int rows = 0, cols = 0;
                    sheet.getSize(ref rows, ref cols);
                    dataGridView1.ColumnCount = cols;
                    dataGridView1.RowCount = rows;

                    //Copies every cell from the sheet to the datagridview
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = sheet.getCell(i + 1, j + 1);
                        }
                    }
                    MessageBox.Show("File loaded successfully");
                }
                else
                {
                    MessageBox.Show("Unable to load file");
                }
            }
            
        }

        //Set cell button click event
        private void SetCellButton_Click(object sender, EventArgs e)
        {
            String rowStr = null;
            String colStr = null;
            String value = null;
            int row;
            int col;
            
            //Retrieves data from the user
            rowStr = Interaction.InputBox("Please enter a row number");
            if (String.IsNullOrEmpty(rowStr) || !int.TryParse(rowStr, out row))//User pressed cancel button or pressed OK without text or entered not a number
            {
                return;
            }


            colStr = Interaction.InputBox("Please insert a column number");
            if (String.IsNullOrEmpty(colStr) || !int.TryParse(colStr, out col))//User pressed cancel button or pressed OK without text or entered not a number
            {
                return;
            }


            value = Interaction.InputBox("Please insert Value");
            if (String.IsNullOrEmpty(value))//User pressed cancel button or pressed OK without text
            {
                return;
            }


            //Set cell in the sheet and updates the datagridview
            Boolean res = sheet.setCell(row, col, value);
            if (res)
            {
                dataGridView1.Rows[row-1].Cells[col-1].Value = value;
                MessageBox.Show("Cell set succesfully");
            }
            else
            {
                MessageBox.Show("Unable to set cell");
            }

        }


        //Search String button click event
        private void SearchStringButton_Click(object sender, EventArgs e)
        {
            int row = 0, col = 0;
            String str = null;
            
            //Retrieves data from user
            str = Interaction.InputBox("Please insert string to search");

            if(String.IsNullOrEmpty(str)) //User pressed cancel button or pressed OK without text
            {
                MessageBox.Show("Please insert string again.");
                return;
            }

            //Searches the string in the sheet and shows answer to user
            Boolean res = sheet.searchString(str,ref row,ref col);
            if (res)
            {
                MessageBox.Show("String found in cell [" + row + "," + col + "].");
            }
            else {
                MessageBox.Show("String not found");
            }
        }



        //Add row button click event
        private void AddRowButton_Click(object sender, EventArgs e)
        {
            //Retrieves data from user
            int row;
            String rowStr = Interaction.InputBox("Please insert after which row do you want to add a new row");
            if (String.IsNullOrEmpty(rowStr) || !int.TryParse(rowStr, out row))//User pressed cancel button or pressed OK without text or entered not a number
            {
                MessageBox.Show("Wrong input");
                return;
            }

            //Adds a new row in the sheet
            Boolean res = sheet.addRow(row);
            if (res)
            {

                //Resizes the datagridview
                int rows = 0, cols = 0;
                sheet.getSize(ref rows, ref cols);
                dataGridView1.ColumnCount = cols;
                dataGridView1.RowCount = rows;

                //Copies every cell from the sheet to the datagridview
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = sheet.getCell(i + 1, j + 1);
                    }
                }
                MessageBox.Show("Row added successfully");
            }
            else {
                MessageBox.Show("Unable to add new row");

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //Add column button click event
        private void AddColumnButton_Click(object sender, EventArgs e)
        {
            //Retrieves data from user
            int col;
            String colStr = Interaction.InputBox("Please insert after which column do you want to add a new column");

            if (String.IsNullOrEmpty(colStr) || !int.TryParse(colStr, out col))//User pressed cancel button or pressed OK without text or entered not a number
            {
                MessageBox.Show("Wrong input");
                return;
            }

            //Adds a new column in the sheet
            Boolean res = sheet.addCol(col);
            if (res)
            {

                //Resizes the datagridview
                int rows = 0, cols = 0;
                sheet.getSize(ref rows, ref cols);
                dataGridView1.ColumnCount = cols;
                dataGridView1.RowCount = rows;

                //Copies every cell from the sheet to the datagridview
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = sheet.getCell(i + 1, j + 1);
                    }
                }
                MessageBox.Show("Column added successfully");
            }
            else
            {
                MessageBox.Show("Unable to add new Column");
            }
        }


        //Save button click event
        private void SaveButton_Click(object sender, EventArgs e)
        {
            //Open a SaveFileDialog to easily extracts the wanted path to save to file
            SaveFileDialog SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            SaveFileDialog1.Filter = "txt files (*.txt) | *.txt |*.dat| All files (*.*)"; //Saving formats
            SaveFileDialog1.RestoreDirectory = true;
            DialogResult result = SaveFileDialog1.ShowDialog();

            //Retrieves the path location
            String path = SaveFileDialog1.FileName;
            if (!String.IsNullOrEmpty(path))
            {
                //Saves the spreadsheet at the desired path
                Boolean res = sheet.save(path);
                if (res)
                {
                    MessageBox.Show("File saved successfully");
                }

                else
                {
                    MessageBox.Show("Unable to save the file");
                }
            }      

        }

        
    }
}