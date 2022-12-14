/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Notes    
 *   
 *   
 *   //General notes and references
 *      - The terrible windows way of checking to see if a cell is null
 *          if (String.IsNullOrEmpty(creationGridView.Rows[j].Cells[0].Value as String))
 *      - How to edit specific cells
 *          creationGridView.Rows[i].Cells[1].Value = "no longer unedited";
 *      - In case you need the directory of the project
 *          string workingDirectory = Environment.CurrentDirectory;
 *          string projdir = Directory.GetParent(workingDirectory).Parent.FullName;
 *      
 *      Note for the next developer who may see this. 
 *          If you understand Winforms and the DataGridView class then you'll have no problem with this code. 
 *      I'm still a low experienced programmer so I hope my notes help, I tried to revise them a little bit. 
 *      All the best.
 * 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.Collections;

namespace EPC2RUGID
{
    public partial class MainForm : Form
    {

        //Dataset and corresponding Datatable 
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        //Global variables
        bool qtycolcheck = false;

        public MainForm()
        { //Start upon creation of the form, all prior actions are done here
            InitializeComponent();


            /* 
            // Keep this code as it allows access to the users desktop which may prove helpful in the future.
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string usernamedir = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
            string[] usernameAt1 = usernamedir.Split('\\');
            //C:\Users\Birb\Desktop
            string folderPath = "C:\\Users\\" + usernameAt1[1] + "\\Desktop\\EPC2RugID Data Files\\";
            MessageBox.Show(folderPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Directory.CreateDirectory(folderPath + "SPARS Files\\");
                Directory.CreateDirectory(folderPath + "Saved Table XML Files\\");
                Directory.CreateDirectory(folderPath + "Exported Files\\");
            }
            */
        }

        // Local Functions

        private void epcClear() // Clears EPC numbers column for the creation table
        {
            if (creationGridView.RowCount != 0)
                for (int i = 0; i < creationGridView.RowCount; i++)
                    creationGridView.Rows[i].Cells[0].Value = "";
        }

        private void dataClear() // Clears data side information for the creation table
        {
            if (creationGridView.RowCount != 0)
                for (int i = 0; i < creationGridView.RowCount; i++)
                    for (int j = 1; j <= 8; j++)
                        creationGridView.Rows[i].Cells[j].Value = "";
            highlight_unmatched();
        }

        private void clearEmptyRows()
        {
            //Get all the indexes first, then remove at all of the indexes
            List<int> indexes = new List<int>();
            bool contains = false;
            if (creationGridView.RowCount != 0)
            {
                for (int i = 0; i < creationGridView.RowCount; i++)
                {
                    for (int k = 0; k <= 8; k++)
                    {
                        if (!String.IsNullOrEmpty(creationGridView.Rows[i].Cells[k].Value as String))
                            if (creationGridView.Rows[i].Cells[k].Value.ToString() != "")
                                contains = true;

                    }
                    if (!contains)
                    {
                        indexes.Add(i);
                    }
                    contains = false;
                } //You need to remove from bottom up or else you'll change the indexes of the rows as you go down
                for (int i = indexes.Count - 1; i >= 0; i--)
                    creationGridView.Rows.RemoveAt(indexes[i]);
            }

        }

        //Not a button
        private void highlight_unmatched()
        {
            //Highlight the unmatched rows.
            if (creationGridView.Rows.Count != 0)
                for (int j = 0; j < creationGridView.Rows.Count; j++)
                {   //All quantities should be one if the above code was successful 

                    //if this row and cell[8] is not null then you can add 1
                    if (!String.IsNullOrEmpty(creationGridView.Rows[j].Cells[8].Value as String))
                        creationGridView.Rows[j].Cells[8].Value = "1";

                    if ((String.IsNullOrEmpty(creationGridView.Rows[j].Cells[0].Value as String))
                    || (String.IsNullOrEmpty(creationGridView.Rows[j].Cells[3].Value as String)))
                    {
                        creationGridView.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        creationGridView.Rows[j].DefaultCellStyle.BackColor = Color.White;
                    }

                    if (creationGridView.Rows[j].DefaultCellStyle.BackColor == Color.White || !String.IsNullOrEmpty(creationGridView.Rows[j].Cells[3].Value as String))
                        creationGridView.Rows[j].Cells[8].Value = "1";
                }

        }

        //Duplicates rows if true that we have a quantity column 
        //Also reloads the EPC numbers so that there aren't blanks
        private void row_duplication()
        {
            //If there is no quantity column, why would we duplicate rows
            if (!qtycolcheck)
                return;

            String epcs = "";
            String[] epcsplit;
            //Saving the EPC list in order to close the gaps
            if (!String.IsNullOrEmpty(creationGridView.Rows[0].Cells[0].Value as String))
                for (int i = 0; i < creationGridView.Rows.Count; i++) //You're loading EPC's first in this case, so you should be fine
                {
                    epcs += creationGridView.Rows[i].Cells[0].Value + ",";
                    creationGridView.Rows[i].Cells[0].Value = "";
                }

            //Quantity Duplication (it'd look better if you put this in its own function then called it)
            //Handling quantites > 1. windows forms is an embarassment and I need to recreate duplicating rows
            List<int> rowindexcount = new List<int>();
            List<int> rowindex = new List<int>();

            //first we need to know what rows need to be duplicated and how many times sysq = 8
            for (int rowi = 0; rowi < creationGridView.Rows.Count; rowi++)
            {
                //Debug.WriteLine(rowi + ": " + creationGridView.Rows[rowi].Cells[8].Value.ToString());
                if (!String.IsNullOrEmpty(creationGridView.Rows[rowi].Cells[8].Value as String))
                    if (Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()) > 1)
                    {
                        rowindex.Add(rowi); // we have what index
                        rowindexcount.Add(Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()));
                        // we have how many times
                    }
            }

            //Now that we know which row and how many times, create the empty rows
            for (int rowi = rowindex.Count - 1; rowi >= 0; rowi--)
                creationGridView.Rows.Insert(rowindex[rowi] + 1, rowindexcount[rowi] - 1);

            //Going through every row, we check if the row is null or if Qty is higher than 1 after that
            //we go through as many extra new rows as neccessary for each row with a qty above 1, then we fill 
            //the rows with the duplicate data
            for (int rowi = 0; rowi < creationGridView.Rows.Count; rowi++)
                if (!String.IsNullOrEmpty(creationGridView.Rows[rowi].Cells[8].Value as String)
                    && (Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()) > 1))
                    for (int i = 1; i < Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()); i++)
                        for (int j = 1; j < 8; j++)
                            creationGridView.Rows[rowi + i].Cells[j].Value = creationGridView.Rows[rowi].Cells[j].Value;

            //Reloading the EPC list so there aren't gaps where inserts were created
            epcsplit = epcs.Split(',');
            for (int i = 0; i < epcsplit.Length - 1; i++)
                creationGridView.Rows[i].Cells[0].Value = epcsplit[i];

            //reseting the global variable 
            qtycolcheck = false;
        }

        private void clearSavedTableHighlights()
        {
            if (savedGridView.Rows.Count != 0)
                for (int i = 0; i < savedGridView.Rows.Count; i++)
                    savedGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
        }

        // Buttons
        private void importEPCBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            ofd.Title = "Import EPC file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //If there's a saved table and the user changes the EPC's we want to reset the saved table highlights
                clearSavedTableHighlights();

                try
                {
                    //Clear the Rows to make way for loaded file, clear the dataset for re-use
                    //creationGridView.Rows.Clear();
                    epcClear();
                    ds.Clear();
                    ds.Reset();
                    //To read the .csv file, we need to convert each row to a list
                    List<String> cells = new List<String>();
                    List<String> processedcells = new List<String>();
                    using (var reader = new StreamReader(ofd.FileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            cells.Add(reader.ReadLine());
                        }
                    }

                    //Remove non-epc lines and store in array
                    foreach (string cell in cells)
                        if (cell != "")
                            if (cell[0] == 'E')
                                processedcells.Add(cell);
                    string[] rows = processedcells.ToArray();

                    //If you have zero rows, ask the important question and then stop
                    if (processedcells.Count == 0)
                    {
                        MessageBox.Show("Did you load an empty or inappropriately formated file?", "Incompatible file");
                        return;
                    }

                    if (rows.Length != creationGridView.Rows.Count && rows.Length > creationGridView.RowCount)
                    {
                        int difference = Math.Abs(rows.Length - creationGridView.Rows.Count);
                        for (int i = 0; i < difference; i++)
                            creationGridView.Rows.Add();
                    }

                    for (int i = 0; i < rows.Length; i++)
                    {
                        creationGridView.Rows[i].Cells[0].Value = rows[i].Split(',')[0];
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The file is likely open in another process, please close and try again", "Already Opened");
                    Console.WriteLine(ex);
                }
            }

            //Highlight the unmatched rows.
            highlight_unmatched();
            clearEmptyRows();

        }// End of import EPC button click

        private void importDataBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            ofd.Title = "Import Data file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataClear();
                    List<String> rawrow = new List<String>();
                    using (var reader = new StreamReader(ofd.FileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            rawrow.Add(reader.ReadLine());
                        }
                    }

                    int difference; bool rugidcolcheck = false;

                    string[] rows = rawrow.ToArray();
                    string[] numcol = rows[0].Split(','); //Just to know how many columns in the file
                    string[] shuffle = new string[numcol.Length]; //Must have 8 matching categories

                    // The stream reader creates double and triple quotes, not sure why
                    // but it's an easy fix. Then organize the data with its respective header
                    for (int i = 0; i < rows.Length; i++)
                    {
                        rows[i] = rows[i].Replace("\"\"\"", "\"");
                        rows[i] = rows[i].Replace("\"\"", "\"");
                        string[] data = rows[i].Split(',');
                        for (int j = 0; j < numcol.Length; j++)
                            shuffle[j] += data[j] + ",";
                    }

                    //Checking to see if user is loading the proper file (At least have a Rug ID column
                    for (int i = 0; i < numcol.Length; i++)
                    {
                        string[] headercheck = shuffle[i].Split(',');
                        if (headercheck[0] == "Rug ID")
                            rugidcolcheck = true;
                        if (headercheck[0] == "System Qty")
                            qtycolcheck = true;

                    }
                    if (!rugidcolcheck)
                    {
                        MessageBox.Show("Did you import a file missing a Rug ID column?\nPerhaps the wrong or even empty file?", "Incompatible file");
                        return;
                    }

                    // The data is organized and matched together to match with the proper column
                    // and fill out the data
                    // locid=1 loctype=2 RugId=3 size=4 upc=5 stockno=6 type=7 sysq=8
                    for (int i = 0; i < numcol.Length; i++)
                    {
                        string[] table = shuffle[i].Split(','); //[0] is the table

                        //Adding rows in case there's more 
                        if (table.Length > creationGridView.Rows.Count)
                        {
                            difference = Math.Abs(table.Length - creationGridView.Rows.Count);
                            for (int j = 0; j < difference - 1; j++)
                                creationGridView.Rows.Add();
                        }

                        if (table[0] == "Location ID:")
                        {
                            for (int j = 1; j < table.Length - 1; j++)
                                creationGridView.Rows[j - 1].Cells[1].Value = table[j].ToString();
                        }
                        else if (table[0] == "Location Type:")
                        {
                            for (int j = 1; j < table.Length - 1; j++)
                                creationGridView.Rows[j - 1].Cells[2].Value = table[j].ToString();
                        }
                        else if (table[0] == "Rug ID")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j - 1].Cells[3].Value = table[j].ToString();
                        }
                        else if (table[0] == "Size")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j - 1].Cells[4].Value = table[j].ToString();
                        }
                        else if (table[0] == "UPC")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j - 1].Cells[5].Value = table[j].ToString();
                        }
                        else if (table[0] == "Stock No")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j - 1].Cells[6].Value = table[j].ToString();
                        }
                        else if (table[0] == "Type")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j - 1].Cells[7].Value = table[j].ToString();
                        }
                        else if (table[0] == "System Qty")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j - 1].Cells[8].Value = table[j].ToString();
                        }

                    }// End of for loop

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The file is likely open in another process, please close and try again\nPlease note," +
                        " this could be another Exception", "Already Opened");
                    Console.WriteLine(ex);
                }

            }// End of loading file

            row_duplication();
            highlight_unmatched();
            clearEmptyRows();

        }// End of function

        //Needs to be redone to fit new table, maybe make more maliable as well.
        private void loadTable_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
            ofd.Title = "Choosing an XML file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Clear the Rows to make way for loaded file
                    savedGridView.Rows.Clear();

                    //Reading the XML file into the DataSet
                    XmlReader xmlFile = XmlReader.Create(ofd.FileName, new XmlReaderSettings());
                    ds.ReadXml(xmlFile);
                    //We need to update the test files with a real saved xml file
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        savedGridView.Rows.Add(row["EPC"].ToString(), row["Location ID:"].ToString(), row["Location Type:"].ToString(), row["Rug ID"].ToString(), row["Size"].ToString(), row["UPC"].ToString(), row["Stock No"].ToString(), row["Type"].ToString(), row["System Qty"].ToString());
                    }

                    //don't forget to clear the dataset... datatable? before loading again
                    ds.Clear();
                    ds.Reset();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }// End of load function 

        private void savetable_Click(object sender, EventArgs e)
        {

            //Adding the columns 
            foreach (DataGridViewColumn column in creationGridView.Columns)
                if (dt.Columns != null)
                    dt.Columns.Add(column.HeaderText);

            //Checking if any rows are red (missing necessary data)
            //if so, warn the user with a window
            foreach (DataGridViewRow row in creationGridView.Rows)
                if (row.DefaultCellStyle.BackColor == Color.Red)
                {
                    MessageBox.Show("\tPlease be advised\n\tThere are currently red rows in this table\n\tThis means there's " +
                        "important missing data that WON'T be \n\tincluded in the saved table" +
                        " if you decide to continue.", "Missing Data");
                    break;
                }

            //Adding the rows
            foreach (DataGridViewRow row in creationGridView.Rows)
            {
                if (row.DefaultCellStyle.BackColor != Color.Red)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {//If the cell has no information, replace cell string with the string "null"
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = "This shouldn't be seen";
                        }
                    }
                }
            }
            ds.Tables.Add(dt);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ds.WriteXml(sfd.FileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            //Clearing the datatable for reuse
            dt.Clear();
            dt.Reset();
            ds.Clear();
            ds.Reset();

        }// End of save table function 
         //Over here you!
        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (savedGridView.Rows.Count == 0)
            {
                MessageBox.Show("The saved table on the right side is currently empty", "Missing Loaded Table");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV|*.csv";
            string header = "EPC,Location ID:,Location Type:,Rug ID,Size,UPC,Stock No,Type,System Qty";
            StreamWriter writer = null;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    writer = new StreamWriter(sfd.FileName);

                    writer.WriteLine(header);
                    for (int i = 0; i < savedGridView.Rows.Count; i++)
                    {
                        writer.WriteLine(savedGridView.Rows[i].Cells[0].Value.ToString() + "," + savedGridView.Rows[i].Cells[1].Value.ToString()
                             + "," + savedGridView.Rows[i].Cells[2].Value.ToString() + "," + savedGridView.Rows[i].Cells[3].Value.ToString()
                              + "," + savedGridView.Rows[i].Cells[4].Value.ToString() + "," + savedGridView.Rows[i].Cells[5].Value.ToString()
                               + "," + savedGridView.Rows[i].Cells[6].Value.ToString() + "," + savedGridView.Rows[i].Cells[7].Value.ToString()
                                + "," + savedGridView.Rows[i].Cells[8].Value.ToString());
                    }
                    writer.Close();
                    MessageBox.Show("Successfully exported!", "Success");

                } catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }// End of Export function

        private void matchBtn_Click(object sender, EventArgs e)
        {
            List<String> matches = new List<String>();
            List<String> nonmatches = new List<String>();

            //Of course, to match two tables you need to make sure they aren't empty
            if ((creationGridView.Rows.Count == 0) || (savedGridView.Rows.Count == 0))
            {
                MessageBox.Show("It seems that one of the tables are empty\nYou need two tables in order to match them", "Missing Table");
                return;
            }

            //Clear the data side information and the empty rows it leaves behind
            dataClear();
            clearEmptyRows();

            //test: making the saved table completely red (we'll change it when there's a match)
            for (int i = 0; i < savedGridView.Rows.Count; i++)
                savedGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;

            //Matching the EPCS of both sides then adding the matches to a list
            for (int i = 0; i < savedGridView.Rows.Count; i++)
                for (int j = 0; j < creationGridView.Rows.Count; j++)
                    if (creationGridView.Rows[j].Cells[0].Value.ToString() == savedGridView.Rows[i].Cells[0].Value.ToString())
                    {
                        savedGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        matches.Add(creationGridView.Rows[j].Cells[0].Value.ToString());
                    }

            //Adding all nonmatches to a list for later EPC row reconstruction
            for (int i = 0; i < creationGridView.Rows.Count; i++)
                if (!matches.Contains(creationGridView.Rows[i].Cells[0].Value.ToString()))
                    nonmatches.Add(creationGridView.Rows[i].Cells[0].Value.ToString());

            matches.AddRange(nonmatches); //Appending the nonmatches list to the end of the matches list
            epcClear(); // clearing the EPC's but not the rows

            //Now the EPC's should come out organized with matches on the top and nonmatching on the bottom 
            for (int i = 0; i < creationGridView.Rows.Count; i++)
                creationGridView.Rows[i].Cells[0].Value = matches[i].ToString();

            int adjust = 0;
            //Now you just need to add the matching data 
            for (int i = 0; i < savedGridView.Rows.Count; i++)
                if (savedGridView.Rows[i].DefaultCellStyle.BackColor == Color.White)
                {
                    for (int j = 1; j <= 8; j++)
                        creationGridView.Rows[i - adjust].Cells[j].Value = savedGridView.Rows[i].Cells[j].Value.ToString();
                }
                else
                {
                    adjust++;
                }

            highlight_unmatched();
        }// End of matchBtn_Click

        private void clearBtn_Click(object sender, EventArgs e)
        {
            creationGridView.Rows.Clear();
            clearSavedTableHighlights();
        }

        private void clearsavedtableBtn_Click(object sender, EventArgs e)
        {
            savedGridView.Rows.Clear();
        }


    }// End of Class
}// End of namespace
