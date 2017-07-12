using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;


namespace project4
{
    public partial class Form1 : Form
    {

        private int turn = 0;                             // Store the number of turns/queens
        private bool[,] taken = new bool[8, 8];           // Makes a 2D array to check if the tile is taken or not. 0 = no, 1 = yes
        private int OFFSET = 100;                         // Variable used for to offset creating the board
        private bool[,] queen_location = new bool[8, 8];  // Makes a 2d array to save the location of the queens;
        private Brush[] colors = new Brush[3];


        public Form1()
        {
            InitializeComponent();
            this.Text = "Eight Queens by Anderson Vanegas";
            this.Size = new Size(645, 645);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    // Draws the board and changes the colors
                    if (this.checkBox1.Checked && taken[i, j] == true)
                    {
                        colors[2] = Brushes.Red;
                        g.FillRectangle(colors[2], OFFSET + (i * 50), OFFSET + (j * 50), 50, 50);
                    }
                    else if ((i + j) % 2 == 1)
                    {
                        // When the row and column are an odd number, we set the square color to black, else if even white, else red
                        colors[0] = Brushes.Black;
                        g.FillRectangle(colors[0], OFFSET + (i * 50), OFFSET + (j * 50), 50, 50);       // Creates the black sqaures
                    }
                    else if ((i + j) % 2 == 0)
                    {
                        colors[1] = Brushes.White;
                        g.FillRectangle(colors[1], OFFSET + (i * 50), OFFSET + (j * 50), 50, 50);       // Creates the white squares
                    }
                    
                    g.DrawRectangle(Pens.Black, OFFSET + (i * 50), OFFSET + (j * 50), 50, 50);          // Creates black borders around all the sqaures
                    

                    // Draws the Queens
                    if (queen_location[i, j])
                    {
                        Font font = new Font("Arial", 30f, FontStyle.Bold);                                                         // Sets the style of the font

                        if (this.checkBox1.Checked && taken[i, j] == true)
                        {
                            g.DrawString("Q", font, Brushes.Black, OFFSET + i * 50 + 3, OFFSET + j * 50 + 3);
                        }
                        else if ((i + j) % 2 == 1)
                        {
                            g.DrawString("Q", font, Brushes.White, OFFSET + i * 50 + 3, OFFSET + j * 50 + 3);           // White Q if square is black
                        }
                        else
                        {
                            g.DrawString("Q", font, Brushes.Black, OFFSET + i * 50 + 3, OFFSET + j * 50 + 3);           // Black Q if square is white
                        }
                    }
                    this.label1.Text = "You have " + turn + " queens on the board.";
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Check first if mouseclick is in the board, otherwise will crash
            if (e.X >= 100 && e.X <= 500 && e.Y >= 100 && e.Y <= 500)
            {

                int row = (e.X - OFFSET) / 50;          // Finds the row of the sqaure we clicked on
                int column = (e.Y - OFFSET) / 50;

                if (e.Button == MouseButtons.Left)
                {
                    // Finds the column of the sqaure we clicked on
                    int x = row;                            // Used for diagnols

                    // We only add a Q there when the spot is not taken
                    if (taken[row, column] == false)
                    {
                        taken[row, column] = true;
                        queen_location[row, column] = true;
                        // Sets the entire row as taken
                        for (int i = 0; i < 8; i++)
                        {
                            taken[i, column] = true;
                        }

                        // Sets the entire column as taken
                        for (int j = 0; j < 8; j++)
                        {
                            taken[row, j] = true;
                        }

                        // Each diagnol checks to make sure both x and y are within bound 
                        // We set x equal to the x location of sqaure each time to be used again
                        // Sets diagnol upper-right
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j--)
                        {

                            taken[x, j] = true;
                            x++;
                        }
                        x = row;

                        // Sets diagnol upper-left
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j--)
                        {
                            taken[x, j] = true;
                            x--;
                        }
                        x = row;

                        // Sets diagnol lower-left
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j++)
                        {
                            taken[x, j] = true;
                            x--;
                        }
                        x = row;

                        // Sets diagnol lower-right
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j++)
                        {
                            taken[x, j] = true;
                            x++;
                        }
                        turn++;                                                             // Increment to show we added a queen

                        if (turn == 8)
                        {
                            MessageBox.Show("You did it!");
                        }
                        this.Invalidate();
                    }
                    else
                        System.Media.SystemSounds.Beep.Play();                                           // Play an error sound when a spot is considered taken

                }

                else
                {
                    // To remove we repeat the left click except we change all of the values to false
                    if (queen_location[row, column] == true)
                    {
                        --turn;
                        queen_location[row, column] = false;
                        taken[row, column] = false;
                        int x = row;
                        // Sets the entire row as taken
                        for (int i = 0; i < 8; i++)
                        {
                            taken[i, column] = false;
                        }

                        // Sets the entire column as taken
                        for (int j = 0; j < 8; j++)
                        {
                            taken[row, j] = false;
                        }

                        // Each diagnol checks to make sure both x and y are within bound 
                        // We set x equal to the x location of sqaure each time to be used again
                        // Sets diagnol upper-right
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j--)
                        {

                            taken[x, j] = false;
                            x++;
                        }
                        x = row;

                        // Sets diagnol upper-left
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j--)
                        {
                            taken[x, j] = false;
                            x--;
                        }
                        x = row;

                        // Sets diagnol lower-left
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j++)
                        {
                            taken[x, j] = false;
                            x--;
                        }
                        x = row;

                        // Sets diagnol lower-right
                        for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j++)
                        {
                            taken[x, j] = false;
                            x++;
                        }

                        this.Invalidate();
                    }

                    for (int m = 0; m < 8; m++)
                    {
                        for (int n = 0; n < 8; n++)
                        {
                            if (queen_location[m, n] == true)
                            {

                                int x = m;
                                taken[m, n] = true;
                                queen_location[m, n] = true;
                                // Sets the entire row as taken
                                for (int i = 0; i < 8; i++)
                                {
                                    taken[i, n] = true;
                                }

                                // Sets the entire column as taken
                                for (int j = 0; j < 8; j++)
                                {
                                    taken[m, j] = true;
                                }

                                // Each diagnol checks to make sure both x and y are within bound 
                                // We set x equal to the x location of sqaure each time to be used again
                                // Sets diagnol upper-right
                                for (int j = n; (x >= 0 && j >= 0) && (x < 8 && j < 8); j--)
                                {

                                    taken[x, j] = true;
                                    x++;
                                }
                                x = m;

                                // Sets diagnol upper-left
                                for (int j = column; (x >= 0 && j >= 0) && (x < 8 && j < 8); j--)
                                {
                                    taken[x, j] = true;
                                    x--;
                                }
                                x = m;

                                // Sets diagnol lower-left
                                for (int j = n; (x >= 0 && j >= 0) && (x < 8 && j < 8); j++)
                                {
                                    taken[x, j] = true;
                                    x--;
                                }
                                x = m;

                                // Sets diagnol lower-right
                                for (int j = n; (x >= 0 && j >= 0) && (x < 8 && j < 8); j++)
                                {
                                    taken[x, j] = true;
                                    x++;
                                }
                                // Increment to show we added a queen
                            }
                        }
                    }
                    if (turn == 8)
                    {
                        MessageBox.Show("You did it!");
                    }
                    this.Invalidate();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            queen_location = new bool[8, 8];
            taken = new bool[8, 8];
            turn = 0;
            this.Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
