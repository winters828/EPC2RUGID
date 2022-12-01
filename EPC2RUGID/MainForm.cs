/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Notes    
 *   //Section descriptions
 *      The Creation table (left side)
 *      
 *          <Recent> button
 *              If the default directory to the scanner is found, it should find the latest modified file 
 *          (most recently created) and automatically load the EPC numbers into the rows of the grid
 *          If the default directory is not found then the user is prompted to use the "Load" button 
 *          in order to select this specific file from the scanner.
 *          
 *          This PC\MC3300x\Internal shared storage\inventory
 *          
 *          
 *          <Import EPC> button 
 *              The creation table Import EPC button should simply allow the user to select the .csv file 
 *          themselves and load that information into the rows of the grid. 
 *          If the file is organized in an unfamiliar way, warn the user. 
 *          If the directory of the file isn't from the familiar device, show a message box asking 
 *          if the user wants to continue understanding there may be unexpected errors.
 *      
 *          <Import Data> button
 *              The creation table Import Data button should take in an associated file and load this
 *          information into the Creation table along side the EPC's 
 *          (you can use the example file Richard sent you)
 *          LocID, RugID, StockID and UPC
 *          
 *          <Match> button 
 *              If there is a loaded .xml file on the Static data table, this button will match 
 *          the newly scanned EPC numbers with the saved xml file's rug information based on matching
 *          EPC numbers
 *          Any EPC's that weren't matched should be highlighted on the right side to show
 *          they are missing.
 *          
 *          <Save> button 
 *              Saves the current constructed table from the creation table. This can later be loaded
 *          on the Save table.
 *          
 *          <Clear> button 
 *              Simply clears the Creation table 
 *              
 *              
 *      The Save table (right side)
 *      
 *          <Load> 
 *              Loads a previously saved .xml file to the Save table on the right. 
 *              
 *          <Clear>
 *              Simply clears the Save table
 *              
 *          <Export>
 *              Depending on what he wants to export, this could export the Creation table or
 *          the saved table (csv, xslx or both maybe?)
 *          
 *   //General notes and references
 *      - The terrible windows way of checking to see if a cell is empty
 *          if (String.IsNullOrEmpty(creationGridView.Rows[j].Cells[0].Value as String))
 *      - How to edit specific cells
 *          creationGridView.Rows[i].Cells[1].Value = "no longer unedited";
 *      - If you accidently created a reference through double clicking and you want to delete it, go to the object in designer
 *        click on it >> properties >> events >> find it in events list, click and delete.
 *      - In case you need the directory of the project
 *          //string workingDirectory = Environment.CurrentDirectory;
 *          //string projdir = Directory.GetParent(workingDirectory).Parent.FullName;
 *          //C:\Users\Birb\source\repos\EPC2RUGID\EPC2RUGID
 *      
 *   //To do list
 *      
 *      Priority
 *      - If the quantity of rugs is more than 1 create as many rows for 
 *      the same rug so that they're matched with their own EPC number
 *      - Create the match button so that when a save table is loaded and
 *      the button is clicked, it will match the EPC's on the creation table
 *      with the data on the Saved table 
 *      - Change the save button to save the file from the creation table then
 *      load it to the saved table
 *      - Get rid of noise upon pressing enter on the number of rows
 *      - Take all data from all the excel files and put them in a single one
 *      - autofill the matching EPC tags with the information from the save table
 *      - highlight the missing EPC's on the creation table from the save table
 *      - if you load a new file while there's data on the table, you need to clear
 *      the COLUMN not the whole table and replace the data
 *      - In the comparison, differentiate a single rug missing and all of them
 *      
 *      Less important
 *      https://stackoverflow.com/questions/3360324/check-last-modified-date-of-file-in-c-sharp
 *      - Recent button, look into how to check every file in the folder for the lastest
 *      date so then you have an option to get the latest file automatically
 *      Then you can select the associated file to combine the two
 *      - Perhaps allowing the user to change the source directory in case it's not accurate 
 *        
 *      - !!! make sure building the application is also possible!!!
 * 
 *   //Longterm considerations
 *      - repeating rug information 
 *      - There no longer needs to be 8 categories, however, you do need an organized
 *      csv file without empty rows or columns 
 *            
 *   //Done list (to show what's been worked on)
 *      - The creation table now highlights rows red when the rug id or epc column is null 
 *      - User can now load two seperate specific files either time and load the data
 *      - User is able to move any files from one directory to another
 *      - User is now able to control how many rows in the datagrid window
 *      - User is now able to check the extension of the file to make sure it's only .xlsx
 *      - User is now able to save the data from the table onto an xml file in any folder you'd like
 *      - User can now both save and open xml files onto the datagrid
 * 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

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
using System.Xml;
using System.Diagnostics;
using System.Collections;

namespace EPC2RUGID
{
    public partial class MainForm : Form
    {
        //Directories
        String sourcedir = @"C:\Users\Birb\Desktop\sourcetest";
        String destinationdir = @"C:\Users\Birb\Desktop\destinationtest";

        //Dataset and corresponding Datatable 
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        public MainForm()
        { //The overall form where you can manipulate property values 
            InitializeComponent();
        }

        // Local Functions
        //Takes in a List of strings (file directories) and checks the extension of the file.
        //Returns true if all files are .xlsx, returns false and shows message box warning the user of mixed files. 
        //This false value will be used to NOT move the files from the source folder. 
        private bool Extension_Check(List<String> Excelfiles)
        {
            // Goes through each file to check if the folder only contains .xlsx extension 
            foreach (String file in Excelfiles)
            {
                FileInfo eFile = new FileInfo(file);

                String ext = "";
                for (int i = 4; i >= 1; i--)
                {
                    ext += eFile.Name[eFile.Name.Length - i].ToString();
                }
                //last letter out of four x=-4 s=-3 l=-2 x=-1
                if (ext != "xlsx")
                {
                    MessageBox.Show("The source directory currently contains files other than .xlsx files\nMove aborted", "Invalid file detected");
                    return false;
                }

            }
            MessageBox.Show("File successfully moved", "Success!");
            return true;
        }

        // Buttons
        private void Move_Click(object sender, EventArgs e)
        {// We may not even need this now, keep the button off to the side

            //MessageBox.Show(text: $"Hello your button currently works");

            //We'll check to see if the folder exists if not, create one.
            DirectoryInfo dirInfo = new DirectoryInfo(destinationdir);
            if (dirInfo.Exists == false)
                Directory.CreateDirectory(destinationdir);

            //Creating a list of String from source directory
            List<String> Excelfiles = Directory.GetFiles(sourcedir, "*.*", SearchOption.AllDirectories).ToList();

            // Goes through an extension check of each file and upon returning true, carries out the movement of files.
            if(Extension_Check(Excelfiles))
            {
                foreach(String file in Excelfiles)
                {
                    FileInfo eFile = new FileInfo(file);
                    if (new FileInfo(dirInfo + "\\" + eFile.Name).Exists == false)
                    {
                        eFile.MoveTo(dirInfo + "\\" + eFile.Name);
                    }
                }
            }

        }// End of move_click

        private void recentBtn_Click(object sender, EventArgs e)
        {
            //string[] files = Directory.GetFiles(@"This PC\MC3300x\Internal shared storage\inventory");
            string[] files = Directory.GetLogicalDrives();
            string all = "";
            foreach (string file in files)
            {
                all += "\n" + file;
            }

            MessageBox.Show(all);
        }

        private void importEPCBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            ofd.Title = "Import EPC file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Clear the Rows to make way for loaded file, clear the dataset for re-use
                    //creationGridView.Rows.Clear();

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
                        if(cell != "")
                            if (cell[0] == 'E')
                                processedcells.Add(cell);
                    string[] rows = processedcells.ToArray();

                    if(rows.Length != creationGridView.Rows.Count && rows.Length > creationGridView.RowCount)
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
            if (creationGridView.Rows.Count != 0)
                for (int j = 0; j < creationGridView.Rows.Count; j++)
                    if ((String.IsNullOrEmpty(creationGridView.Rows[j].Cells[0].Value as String))
                    || (String.IsNullOrEmpty(creationGridView.Rows[j].Cells[3].Value as String)))
                    {
                        creationGridView.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        creationGridView.Rows[j].DefaultCellStyle.BackColor = Color.White;
                    }

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
                    List<String> rawrow = new List<String>();
                    using (var reader = new StreamReader(ofd.FileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            rawrow.Add(reader.ReadLine());
                        }
                    }

                    int difference;

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
                    
                    // The data is organized and matched together to match with the proper column
                    // and fill out the data
                    // locid=1 loctype=2 RugId=3 size=4 upc=5 stockno=6 type=7 sysq=8
                    for(int i = 0; i < numcol.Length; i++)
                    {
                        string[] table = shuffle[i].Split(','); //[0] is the table

                        //Adding rows in case there's more 
                        if(table.Length > creationGridView.Rows.Count)
                        {
                            difference = Math.Abs(table.Length - creationGridView.Rows.Count);
                            for (int j = 0; j < difference-1; j++)
                                creationGridView.Rows.Add();
                        }

                        if (table[0] == "Location ID:")
                        {
                            for (int j = 1; j < table.Length-1; j++)
                                creationGridView.Rows[j-1].Cells[1].Value = table[j].ToString();
                        } else if (table[0] == "Location Type:")
                        {
                            for (int j = 1; j < table.Length-1; j++)
                                creationGridView.Rows[j-1].Cells[2].Value = table[j].ToString();
                        } else if (table[0] == "Rug ID")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j-1].Cells[3].Value = table[j].ToString();
                        } else if (table[0] == "Size")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j-1].Cells[4].Value = table[j].ToString();
                        } else if (table[0] == "UPC")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j-1].Cells[5].Value = table[j].ToString();
                        } else if (table[0] == "Stock No")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j-1].Cells[6].Value = table[j].ToString();
                        } else if (table[0] == "Type")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j-1].Cells[7].Value = table[j].ToString();
                        } else if (table[0] == "System Qty")
                        {
                            for (int j = 1; j < table.Length; j++)
                                creationGridView.Rows[j-1].Cells[8].Value = table[j].ToString();
                        }
                        
                    }// End of for loop

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The file is likely open in another process, please close and try again\nPlease note," +
                        " this could be another Exception","Already Opened");
                    Console.WriteLine(ex);
                }
                //The variable table has an extra empty row at the end, just remove it
                creationGridView.Rows.RemoveAt(creationGridView.Rows.Count-1);

                
            }// End of loading file

//testing area
            //Handling quantites > 1. windows forms is an embarassment and I need to recreate duplicating rows
            List<int> rowindexcount = new List<int>();
            List<int> rowindex = new List<int>();
            //first we need to know what rows need to be duplicated and how many times sysq = 8
            for (int rowi = 0; rowi < creationGridView.Rows.Count; rowi++)
            {
                if (Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()) > 1)
                {
                    rowindex.Add(rowi); // we have what index
                    rowindexcount.Add(Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()));
                    // we have how many times
                }
            }
            //Now that we know which row and how many times, create the empty rows
            for (int rowi = rowindex.Count-1; rowi >= 0; rowi--)
            {
                Debug.WriteLine("rowindex: " + rowindex[rowi] + "  rowindexcount: " + rowindexcount[rowi]);
                creationGridView.Rows.Insert(rowindex[rowi]+1, rowindexcount[rowi]-1);
            }
            //Now we need to load the row from before into the new rows below
            for (int rowi = 0; rowi < creationGridView.Rows.Count; rowi++)
            {
                if (!String.IsNullOrEmpty(creationGridView.Rows[rowi].Cells[8].Value as String) 
                    && (Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()) > 1))
                {
                    for(int i = 1; i < Int32.Parse(creationGridView.Rows[rowi].Cells[8].Value.ToString()); i++)
                    {
                        creationGridView.Rows[rowi + i].Cells[3].Value = creationGridView.Rows[rowi].Cells[3].Value;
                    }
                }
            }//You crazy sob you did it. do it with all columns now clean up, then move to the to do list

            //To do after
            //load the duplicate data to the empty cells
            //change the number in the qty column to 1 (they should all be one)
            //reload the EPC numbers for when they're loaded first 

//testing area

            //Highlight the unmatched rows.
            if (creationGridView.Rows.Count != 0)
                for (int j = 0; j < creationGridView.Rows.Count; j++)
                    if ((String.IsNullOrEmpty(creationGridView.Rows[j].Cells[0].Value as String))
                    || (String.IsNullOrEmpty(creationGridView.Rows[j].Cells[3].Value as String)))
                    {
                        creationGridView.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        creationGridView.Rows[j].DefaultCellStyle.BackColor = Color.White;
                    }

        }// End of function 

        private void clearBtn_Click(object sender, EventArgs e)
        {
            creationGridView.Rows.Clear();
        }

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
        }
        
        private void savetable_Click(object sender, EventArgs e)
        {

            //Adding the columns 
            foreach (DataGridViewColumn column in creationGridView.Columns)
            {
                if (dt.Columns != null) //explore what's null here and stop it. you may get it to work this way.
                    dt.Columns.Add(column.HeaderText);
            }
            //Checking if any rows are red (missing necessary data)
            //if so, warn the user with a window
            foreach (DataGridViewRow row in creationGridView.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.Red)
                {
                    MessageBox.Show("\tPlease be advised\n\tThere are currently red rows in this table\n\tThis means there's " +
                        "important missing data that WON'T be included in the saved table" +
                        " if you decide to continue.","Missing Data");
                    break;
                }
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

    }// End of Class
}// End of namespace
