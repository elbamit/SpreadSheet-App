using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.IO;


namespace Q4_gui
{
   
    //A class of a shearable spreadsheet. The spreadsheet is a String matrix and each operation has thread safe measures.
    //Operations are divided in 3 types: Reader, writer and structure. Different types cannot perform simultaneously

    //Reader operations only read from the spreadsheet, without making any changes. There can be many readers operations simultaneously.
    //Writers operations change data inside the spreadsheet. There can be many writers operations simultaneously.
    //Structure operations structural changes to the spreadsheet or changes to many data inside the spreadsheet. Structure operations can only perform one by one.
    class ShareableSpreadSheet
    {
        private String[,] grid; //The spreadsheet itself (String matrix)
        private Mutex Reader_mut; //Mutex for reader operations
        private Mutex Writer_mut; //Mutex for writer operations
        private Mutex Structure_mut; //Mutex for structure operations
        private Semaphore read_write_changer; //Binary semaphore that controls which type of operations can be done now
        private SemaphoreSlim search_concurrent; //Unitialized semaphore that can be set during run and that limits the number of search operations that can perform simultaneously
        private Mutex[] row_locks; //Array of Mutex - one for each row of the spreadsheet

        private int readcount; //Counter for number of readers operations that run simultaneously
        private int writecount; //Counter for number of writers operations that run simultaneously

        private int rows; //Number of rows in the spreadsheet
        private int cols; //Number of columnss in the spreadsheet

        private int nThreads; //Number of threads running

        //Constructor
        public ShareableSpreadSheet(int nRows, int nCols)
        {
            // Construct a nRows*nCols spreadsheet
            this.grid = new String[nRows, nCols];

            //Initializes all the fields
            this.Reader_mut = new Mutex();
            this.Writer_mut = new Mutex();
            this.Structure_mut = new Mutex();
            this.read_write_changer = new Semaphore(1, 1);
            this.search_concurrent = null;
            this.row_locks = new Mutex[nRows];

            this.readcount = 0;
            this.writecount = 0;

            this.rows = nRows;
            this.cols = nCols;

            for (int i = 0; i < nRows; i++)
            {
                this.row_locks[i] = new Mutex();
            }

        }


        // Returns the string at [row,col]
        public String getCell(int row, int col)
        {
            Entry_section_reader();
            int real_row = row - 1;
            int real_col = col - 1;
            string ret = null;

            if (Check_boundaries(real_row, real_col))
            {
                ret = this.grid[real_row, real_col];
            }

            Exit_section_reader();
            return ret;
        }

        // Sets the string at [row,col]
        public bool setCell(int row, int col, String str)
        {
            bool ret = false;
            int real_row = row - 1;
            int real_col = col - 1;
            /*int real_row = row;
            int real_col = col;*/
            Entry_section_writer(real_row, real_col);


            if (Check_boundaries(real_row, real_col))
            {
                this.grid[real_row, real_col] = str;
                ret = true;
            }

            Exit_section_writer(real_row, real_col);
            return ret;
        }

        // Search the cell with string str, and return true/false accordingly.
        // Stores the location in row,col.
        // Return the first cell that contains the string (search from first row to the last row)
        public bool searchString(String str, ref int row, ref int col)
        {

            Entry_section_reader();
            Entry_section_search();

            Boolean res = false;

            //Iterates on every cell of the spreadsheet
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    if (this.grid[i, j] == str) //Checks if cell contains the given String
                    {
                        row = i + 1;
                        col = j + 1;
                        res = true;
                        break;
                    }
                }
            }

            Exit_section_search();
            Exit_section_reader();

            return res;
        }

        // Exchanges the content of row1 and row2
        public bool exchangeRows(int row1, int row2)
        {

            Entry_section_structure();
            int real_row1 = row1 - 1;
            int real_row2 = row2 - 1;

            if (!Check_boundaries(real_row1) || !Check_boundaries(real_row2))
            {
                Exit_section_structure();
                return false;
            }

            String temp;

            //Iterates over every cell of given row
            for (int i = 0; i < this.cols; i++)
            {
                //Uses temp to store 1 cell, then makes the exchange, and assigns temp to other cell
                temp = this.grid[real_row1, i];
                this.grid[real_row1, i] = this.grid[real_row2, i];
                this.grid[real_row2, i] = temp;
            }

            Exit_section_structure();

            return true;
        }

        // Exchanges the content of col1 and col2
        public bool exchangeCols(int col1, int col2)
        {

            Entry_section_structure();
            int real_col1 = col1 - 1;
            int real_col2 = col2 - 1;

            if (!Check_boundaries(0, real_col1) || !Check_boundaries(0, real_col2))
            {
                Exit_section_structure();
                return false;
            }

            String temp;

            //Iterates over every cell of given column
            for (int i = 0; i < this.rows; i++)
            {
                //Uses temp to store 1 cell, then makes the exchange, and assigns temp to other cell
                temp = this.grid[i, real_col1];
                this.grid[i, real_col1] = this.grid[i, real_col2];
                this.grid[i, real_col2] = temp;
            }

            Exit_section_structure();

            return true;
        }

        // Performs search in specific row
        public bool searchInRow(int row, String str, ref int col)
        {

            Entry_section_reader();
            Entry_section_search();

            int real_row = row - 1;

            Boolean res = false;
            if (!Check_boundaries(real_row))
            {
                Exit_section_search();
                Exit_section_reader();
                return false;
            }

            //Iterates over every cell of the given row
            for (int i = 0; i < this.cols; i++)
            {
                if (this.grid[real_row, i] == str) //Checks if cell contains given String
                {
                    col = i + 1;
                    res = true;
                }
            }

            Exit_section_search();
            Exit_section_reader();

            return res;
        }

        // Performs search in specific column
        public bool searchInCol(int col, String str, ref int row)
        {
            Entry_section_reader();
            Entry_section_search();

            int real_col = col - 1;

            Boolean res = false;
            if (!Check_boundaries(0, real_col))
            {
                Exit_section_search();
                Exit_section_reader();
                return false;
            }

            //Iterates over every cell of the given column
            for (int i = 0; i < this.rows; i++)
            {
                if (this.grid[i, real_col] == str) //Checks if cell contains given String
                {
                    row = i + 1;
                    res = true;
                }
            }

            Exit_section_search();
            Exit_section_reader();

            return res;
        }

        public bool searchInRange(int col1, int col2, int row1, int row2, String str, ref int row, ref int col)
        {
            // perform search within spesific range: [row1:row2,col1:col2] 
            //includes col1,col2,row1,row2
            Entry_section_reader();
            Entry_section_search();

            int real_col1 = col1 - 1;
            int real_col2 = col2 - 1;
            int real_row1 = row1 - 1;
            int real_row2 = row2 - 1;

            Boolean res = false;

            //Checks arguments boundaries
            if (!Check_boundaries(real_row1, real_col1) || !Check_boundaries(real_row2, real_col2) || real_row1 > real_row2 || real_col1 > real_col2)
            {
                Exit_section_search();
                Exit_section_reader();
                return false;
            }

            for (int i = real_row1; i <= real_row2; i++)
            {
                for (int j = real_col1; j <= real_col2; j++)
                {
                    if (this.grid[i, j] == str)
                    {
                        row = i + 1;
                        col = j + 1;
                        res = true;
                    }
                }
            }

            Exit_section_search();
            Exit_section_reader();

            return res;
        }

        //Adds a row after row1
        public bool addRow(int row1)
        {
            Entry_section_structure();

            int real_row1 = row1 - 1;

            if (!Check_boundaries(real_row1))
            {
                Exit_section_structure();
                return false;
            }

            //Create a new matrix with the updated rows and column
            String[,] temp_grid = new String[this.rows + 1, this.cols];

            //Copies from original matrix to the new matrix every cell until given row
            for (int i = 0; i <= real_row1; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    temp_grid[i, j] = this.grid[i, j];
                }
            }

            //Copies from original matrix to the new matrix every cell from after the new row, until the end of the new matrix
            for (int i = real_row1 + 2; i < this.rows + 1; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    temp_grid[i, j] = this.grid[i - 1, j];
                }
            }

            //Updates the grid and it's data (including the mutex array)
            this.grid = temp_grid;
            this.rows++;
            this.row_locks = new Mutex[this.rows];
            for (int i = 0; i < this.rows; i++)
            {
                this.row_locks[i] = new Mutex();
            }


            Exit_section_structure();

            return true;
        }

        //Adds a column after col1
        public bool addCol(int col1)
        {
            Entry_section_structure();

            int real_col1 = col1 - 1;

            if (!Check_boundaries(0, real_col1))
            {
                Exit_section_structure();
                return false;
            }

            //Create a new matrix with the updated rows and column
            String[,] temp_grid = new String[this.rows, this.cols + 1];

            //Copies from original matrix to the new matrix every cell until given column
            for (int i = 0; i <= real_col1; i++)
            {
                for (int j = 0; j < this.rows; j++)
                {
                    temp_grid[j, i] = this.grid[j, i];
                }
            }

            //Copies from original matrix to the new matrix every cell from after the new column, until the end of the new matrix
            for (int i = real_col1 + 2; i < this.cols + 1; i++)
            {
                for (int j = 0; j < this.rows; j++)
                {
                    temp_grid[j, i] = this.grid[j, i - 1];
                }
            }

            //Updates the grid and it's data
            this.grid = temp_grid;
            this.cols++;

            Exit_section_structure();

            return true;
        }

        // Return the size of the spreadsheet in nRows, nCols
        public void getSize(ref int nRows, ref int nCols)
        {
            Entry_section_reader();
            nRows = this.rows;
            nCols = this.cols;
            Exit_section_reader();

        }

        // This function aims to limit the number of users that can perform the search operations concurrently.
        // The default is no limit. When the function is called, the max number of concurrent search operations is set to nUsers. 
        // In this case additional search operations will wait for existing search to finish.
        public bool setConcurrentSearchLimit(int nUsers)
        {
            Boolean res = true;
            Entry_section_structure();

            //If nUsers is smaller than number of threads, return false
            if (nUsers < this.get_Thread_number())
            {
                res = false;
            }

            //Creates a new semaphoreSlim with the appropriate number of users
            else
            {
                this.search_concurrent = new SemaphoreSlim(nUsers - 1, nUsers);
            }

            Exit_section_structure();
            return res;
        }

        // Save the spreadsheet to a file fileName.
        public bool save(String fileName)
        {

            Entry_section_structure();
            string path = fileName;

            //Make sure the file is overwritten if it exists
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Create a new file and appends each cell in UTF-8 encoding
            using (StreamWriter sw = File.AppendText(path))
            {
                //Saves the meta data of the grid (row and col size)
                sw.WriteLine(this.rows);
                sw.WriteLine(this.cols);

                for (int i = 0; i < this.rows; i++)
                {
                    for (int j = 0; j < this.cols; j++)
                    {
                        //Encodes to UTF-8 and appends to the file
                        sw.WriteLine(this.grid[i, j]);
                    }
                }
            }

            Exit_section_structure();
            return true;
        }

        // Load the spreadsheet from fileName
        // Replace the data and size of the current spreadsheet with the loaded data
        public bool load(String fileName)
        {
            Entry_section_structure();

            //Checks if path is legal
            if (!File.Exists(fileName))
            {
                Exit_section_structure();
                return false;
            }

            String path = fileName;
            int row, col;

            //Extracts data from the file
            using (StreamReader sr = new StreamReader(path))
            {
                //Extracts row and column sizes. Creates a new empty grid with matching sizes
                row = Int32.Parse(sr.ReadLine());
                col = Int32.Parse(sr.ReadLine());
                String[,] new_grid = new String[row, col];

                //Updates the rows, cols and grid to the new values
                this.rows = row;
                this.cols = col;
                this.grid = new_grid;

                //Extracts the grid data and assigns it to the new grid
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        this.grid[i, j] = sr.ReadLine();
                        /*setCell(i, j, sr.ReadLine());*/
                    }
                }

                Exit_section_structure();
            }
            return true;
        }

        //Helper function that fills every cell of the spreadsheet with "testcellXY" - X Y are the cell's index
        public bool Test_fill()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    setCell(i+1 , j+1 , "testcell" + (i+1 ) + (j+1 ));
                }
            }
            return true;
        }

        //Helper function that sets the number of threads that the spreadsheet uses 
        public void set_Thread_number(int number)
        {
            this.nThreads = number;
        }

        //Helper function that gets the number of threads that the spreadsheet uses 
        public int get_Thread_number()
        {
            return this.nThreads;
        }




        //To synchronize threads that perform on shared resources, each type of spreadsheet operation has a set of locks and operation (entry section) that has to be done before entering it's critical section.
        //After the critical section is done - another set of operation (exit section) is done to free all the used locks and let another spreadsheet operation take place

        //Entry section of all readers operations - getCell, searchString, searchInRow, searchInCol, searchInRange, getSize
        private void Entry_section_reader()
        {
            //Reader Mutex is locked
            this.Reader_mut.WaitOne();

            //Atomic incrementation of the readcount variable
            Interlocked.Increment(ref this.readcount);

            //Locks the read_write_changer semaphore only if it is the first reader operation to be executed in this sequence of operations
            if (this.readcount == 1)
            {
                this.read_write_changer.WaitOne();
            }

            //Releases the reader's mutex
            this.Reader_mut.ReleaseMutex();
        }




        //Exit section of all readers operations
        private void Exit_section_reader()
        {
            //Locks the reader's mutex
            this.Reader_mut.WaitOne();

            //Atomic decrementation of the readcount variable
            Interlocked.Decrement(ref this.readcount);

            //Unlocks the read_write_changer semaphore only if it is the last reader operation to be executed in this sequence of operations
            if (this.readcount == 0)
            {
                this.read_write_changer.Release();
            }

            //Releases the reader's mutex
            this.Reader_mut.ReleaseMutex();
        }



        //Entry section for all writers operations - setCell
        private void Entry_section_writer(int row, int col)
        {
            //Checks that row and col indexes are within the spreadsheet boundaries
            if (Check_boundaries(row, col))
            {
                //Locks the writer's mutex
                this.Writer_mut.WaitOne();

                //Atomic incrementation of the writecount variable
                Interlocked.Increment(ref this.writecount);

                //Locks the read_write_changer semaphore only if it is the first writer operation to be executed in this sequence of operations
                if (this.writecount == 1)
                {
                    this.read_write_changer.WaitOne();
                }

                //Releases the writer's mutex
                this.Writer_mut.ReleaseMutex();

                //Locks the appropriate mutex from the mutex's array - it means that another thread cannot write to the same row until current thread finishes
                this.row_locks[row].WaitOne();
            }
        }



        //Exit section of all writers operations
        private void Exit_section_writer(int row, int col)
        {
            //Checks that row and col indexes are within the spreadsheet boundaries
            if (Check_boundaries(row, col))
            {
                //Unlocks the appropriate mutex from the mutex's array - allows another thread to write to the same row
                this.row_locks[row].ReleaseMutex();

                //Locks the writer's mutex
                this.Writer_mut.WaitOne();


                //Atomic decrementation of the writecount variable
                Interlocked.Decrement(ref this.writecount);

                //Unlocks the read_write_changer semaphore only if it is the last writer operation to be executed in this sequence of operations
                if (this.writecount == 0)
                {
                    this.read_write_changer.Release();
                }

                //Releases the writer's mutex
                this.Writer_mut.ReleaseMutex();
            }

        }



        //Entry section for all structure operations - exchangeRows, exchangeCols, addRow, addCol
        private void Entry_section_structure()
        {

            //Locks the structure mutex
            this.Structure_mut.WaitOne();

            //Locks the read_write_changer until the critical section of the operation finishes
            this.read_write_changer.WaitOne();

        }


        //Exit section for structure operations
        private void Exit_section_structure()
        {

            //Unlocks the read_write_changer semaphore, allowing other operations (of different types also) to perform
            this.read_write_changer.Release();

            //Release the structure mutex
            this.Structure_mut.ReleaseMutex();
        }



        //If SetConcurrentSearchLimit method has been called - it limits the number of threads that can perform search operation concurrently
        //Entry section for all search operations - searchString, searchInRow, searchInCol, searchInRange
        private void Entry_section_search()
        {
            if (this.search_concurrent != null)
            {
                //Signals the search_concurrent semaphore (does -- to it). If semaphore has reached 0, it means the limit of search operations has reached
                this.search_concurrent.Wait();
            }

        }

        //Exit section for all search operations
        private void Exit_section_search()
        {
            if (this.search_concurrent != null)
            {
                //Releases the search_concurrent semaphore (does ++ to it).
                this.search_concurrent.Release();
            }

        }

        //Helper function that receives a row and col arguments, and returns a boolean value if they are within the range of the spreadsheet matrix
        private bool Check_boundaries(int row = 0, int col = 0)
        {
            if (row > this.rows - 1 || row < 0 || col > this.cols - 1 || col < 0)
            {
                return false;
            }

            return true;
        }


    }


}
