using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


//User plays a Simple game of TicTacToe with CPU
//Last 5 outcomes are recorded 
//Restart Game when over or button is clicked

namespace TicTacToe
{

    public partial class Form1 : Form
    {


        //Declare Variables 
        bool isGameOver = false;
        List<Button> buttons;
        readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();

            //Game restarts everytime game loads
            RestartGame();
        }

        //Player Move Logic
        private void PlayerMove(object sender, EventArgs e)
        {
            //Game is Over if true 
            if (isGameOver) return;

            //Game is not Over...User is (X)
            var button = (Button)sender;
            button.Text = "X";
            button.Enabled = false;
            button.BackColor = Color.ForestGreen;
            //Remove button from list so it isn't repeated
            buttons.Remove(button);
            //Win Checking Logic 
            CheckWin();
            //Start CPU timer so CPU can play
            CPUtimer.Start();
        }


        //CPU Move logic
        private void CPUmove(object sender, EventArgs e)
        {
            //Game is Over if true
            if (isGameOver) return;

            //Positions available (buttons still in list)
            if (buttons.Count > 0)
            {
                //random position is assigned
                int index = random.Next(buttons.Count);
                buttons[index].Enabled = true;
                buttons[index].Text = "O";
                buttons[index].BackColor = Color.DarkRed;
                //remove position (button) from list so it's non-replayable
                buttons.Remove(buttons[index]);
                //Win Checking Logic
                CheckWin();
                //CPU Timer is Stopped to complete playable move
                CPUtimer.Stop();
            }
        }

        //Call to Restart Game via button click (overloaded)
        private void RestartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        //Restart Game logic
        private void RestartGame()
        {

            isGameOver = false;
            //Assign positions on board via List
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            //for each button in list reset to original look
            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = "";
                x.BackColor = DefaultBackColor;
            }

        }
        //Record the last 5 game outcomes
        private void Record()
        {

            if (listView1.Items.Count > 5)
            {
                listView1.Items.Remove(listView1.Items[0]);
            }

        }
        //check win logic (diagonal and straight)
        private void CheckWin()
        {
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X" ||
                button4.Text == "X" && button5.Text == "X" && button6.Text == "X" ||
                button7.Text == "X" && button8.Text == "X" && button9.Text == "X" ||
                button1.Text == "X" && button4.Text == "X" && button7.Text == "X" ||
                button2.Text == "X" && button5.Text == "X" && button8.Text == "X" ||
                button3.Text == "X" && button6.Text == "X" && button9.Text == "X" ||
                button1.Text == "X" && button5.Text == "X" && button9.Text == "X" ||
                button3.Text == "X" && button5.Text == "X" && button7.Text == "X"
                )
            {
                //if diagonal or straight occurs for User
                CPUtimer.Stop();
                MessageBox.Show("Player Wins");
                listView1.Items.Add("Player Wins");
                Record();
                //Game Over
                isGameOver = true;
                RestartGame();

            }

            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O" ||
                button4.Text == "O" && button5.Text == "O" && button6.Text == "O" ||
                button7.Text == "O" && button8.Text == "O" && button9.Text == "O" ||
                button1.Text == "O" && button4.Text == "O" && button7.Text == "O" ||
                button2.Text == "O" && button5.Text == "O" && button8.Text == "O" ||
                button3.Text == "O" && button6.Text == "O" && button9.Text == "O" ||
                button1.Text == "O" && button5.Text == "O" && button9.Text == "O" ||
                button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                //if diagonal or straight occurs for CPU
                CPUtimer.Stop();
                MessageBox.Show("CPU Wins");
                listView1.Items.Add("CPU Wins");
                Record();
                isGameOver = true;
                RestartGame();
            }
            //If neither while all positions are filled (Tie!)
            else
            {
                if (buttons.Count == 0)
                {
                    MessageBox.Show("Tie !");
                    listView1.Items.Add("Tie !");
                    isGameOver = true;
                    RestartGame();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
