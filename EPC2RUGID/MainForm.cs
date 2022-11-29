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
 *   
 *      We'll have to talk about not changing the associated .xslx file around to much and 
 *      keeping it within a certain format for now. It would be really cool, to identify the header
 *      and scan it into the program but we'll take things step at a time.
 *      
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
 *      https://stackoverflow.com/questions/3360324/check-last-modified-date-of-file-in-c-sharp
 *      - Then look into how to check every file in the folder for the lastest
 *      date so then you have an option to get the latest file automatically
 *      Then you can select the associated file to combine the two
 *      - Perhaps allowing the user to change the source directory in case it's not accurate 
 *      - Get rid of noise upon pressing enter on the number of rows
 *      - Take all data from all the excel files and put them in a single one
 *        
 *      - !!! make sure building the application is also possible!!!
 * 
 *   //Longterm considerations
 *      - repeating rug information 
 *      - There need to be 8 headers on the imported data file, lists may fix this in the future
 *      but for now stick with the arrays
 *            
 *   //Done list (to show what's been worked on)
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

        private void savetable_Click(object sender, EventArgs e)
        {
            //Saving xml notes 
            //I don't think we're going to need the text box 
            //Save the table to a dataset THEN use dataset to write to an xml

            //Adding the columns 
            foreach (DataGridViewColumn column in savedGridView.Columns)
            {
                if(dt.Columns != null) //explore what's null here and stop it. you may get it to work this way.
                    dt.Columns.Add(column.HeaderText);
            }
            //Adding the rows
            foreach (DataGridViewRow row in savedGridView.Rows)
            {
                dt.Rows.Add();
                foreach(DataGridViewCell cell in row.Cells)
                {
                    if(cell.Value != null)
                    {//If the cell has no information, replace cell string with the string "null"
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                    else
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = "null";
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

        }

        private void opentables_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
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
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        savedGridView.Rows.Add(row["EPC Number"].ToString(), row["Rug ID"].ToString());
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
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Clear the Rows to make way for loaded file, clear the dataset for re-use
                    creationGridView.Rows.Clear();
                    ds.Clear();
                    ds.Reset();
                    //To read the .csv file, we need to convert each row to a list
                    List<String> cells = new List<String>();
                    using(var reader = new StreamReader(ofd.FileName))
                    {
                        while (!reader.EndOfStream)
                        {
                            cells.Add(reader.ReadLine());
                        }
                    }
                    //You can remove from a list but it was bugging out for some reason
                    //Here we convert the list to an array for easy processing
                    string[] rows = cells.ToArray();
                    //We only want EPC numbers, nothing else added to the list
                    for(int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i] != "")
                            if (rows[i][0] == 'E') // consider checking for the F at the end 
                            {//for extra accuracy
                                creationGridView.Rows.Add(rows[i].Split(',')[0] + "\n", "load here");
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }// End of import EPC button click

        private void importDataBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
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
                    string[] rows = rawrow.ToArray();
                    string[] table = new string[8]; //Must have 8 matching categories

                    for (int i = 0; i < rows.Length; i++)
                    {
                        string[] data = rows[i].Split(',');
                        for (int j = 0; j < 8; j++) //We'll know that there's 8 categories
                        {
                            table[j] += data[j] + ",";

                        }
                    }
                    // The data is organized and matched together to match with the proper column
                    // and fill out the data

                    //locid=1 loctype=2 RugId=3 size=4 upc=5 stockno=6 type=7 sysq=8
                    for(int i = 0; i < 8; i++)
                    {
                        string[] header = table[i].Split(','); //[0] is the header

                        if (header[0] == "Location ID:")
                        {
                            for(int j = 0; j < creationGridView.RowCount; j++)
                            {
                                creationGridView.Rows[j].Cells[1].Value = header[j].ToString();
                            }
                                MessageBox.Show("Location ID");
                        } else if (header[0] == "Location Type:")
                        {
                            for (int j = 0; j < creationGridView.RowCount; j++)
                            {
                                creationGridView.Rows[j].Cells[2].Value = header[j].ToString();
                            }
                                MessageBox.Show("Location Type");
                        } else if (header[0] == "Rug ID")
                        {
                            for (int j = 0; j < creationGridView.RowCount; j++)
                            {
                                creationGridView.Rows[j].Cells[3].Value = header[j].ToString();
                            }
                            MessageBox.Show("Rug ID");
                        }
                    }

                    //creationGridView.Columns[0].HeaderText;
                    //creationGridView.Rows[i].Cells[1].Value = "no longer unedited";

                    //I'm also thinking you can create a list for each available column
                    //then you would be able to do the length of the list and move the 
                    //information over. 


                }
                catch (Exception ex)
                {
                    MessageBox.Show("The file is likely open in another process, please close and try again","Already Opened");
                    Console.WriteLine(ex);
                }
                
            }
        }
    }// End of Class
}// End of namespace
