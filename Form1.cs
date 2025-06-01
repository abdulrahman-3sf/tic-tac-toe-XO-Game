using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        enum enPlayer
        {
            player1,
            player2
        }
        enum enWinner {
            player1,
            player2,
            draw,
            gameInProgress
        }
        struct stGameStatus
        {
            public short playCount;
            public enWinner winner;
            public bool gameOver;
        }

        stGameStatus gameStatus;
        enPlayer player = enPlayer.player1;

        public void endGame()
        {
            lblPlayer.Text = "Game Over";
            gameStatus.playCount = 0;
            switch (gameStatus.winner)
            {
                case enWinner.player1:
                    lblWinner.Text = "Player1";
                    break;

                case enWinner.player2:
                    lblWinner.Text = "Player2";
                    break;

                case enWinner.draw:
                    lblWinner.Text = "Draw";
                    break;

                default:
                    lblWinner.Text = "On Going";
                    break;
            }

            MessageBox.Show("Congrats Game Over!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }

        public bool checkValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.Black;
                btn2.BackColor = Color.Black;
                btn3.BackColor = Color.Black;

                if (btn1.Tag.ToString() == "X")
                {
                    gameStatus.winner = enWinner.player1;
                } else
                {
                    gameStatus.winner = enWinner.player2;
                }

                gameStatus.gameOver = true;
                endGame();
                return true;
            }

            gameStatus.gameOver = false;
            return false;
        }

        public void checkWinner()
        {
            if (checkValues(button1, button2, button3))
                return;

            if (checkValues(button4, button5, button6))
                return;

            if (checkValues(button7, button8, button9))
                return;

            if (checkValues(button1, button4, button7))
                return;

            if (checkValues(button2, button5, button8))
                return;

            if (checkValues(button3, button6, button9))
                return;

            if (checkValues(button1, button5, button9))
                return;

            if (checkValues(button3, button5, button7))
                return;
        }

        public void changeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (player)
                {
                    case enPlayer.player1:
                        btn.Tag = "X";
                        btn.Image = Resources.X;
                        player = enPlayer.player2;
                        lblPlayer.Text = "Player2";
                        gameStatus.playCount++;
                        checkWinner();
                        break;
                    case enPlayer.player2:
                        btn.Tag = "O";
                        btn.Image = Resources.O;
                        player = enPlayer.player1;
                        lblPlayer.Text = "Player1";
                        gameStatus.playCount++;
                        checkWinner();
                        break;
                }
            } else
            {
                MessageBox.Show("This one is selected already!", "Error Choice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (gameStatus.playCount == 9)
            {
                gameStatus.winner = enWinner.draw;
                gameStatus.gameOver = true;
                endGame();
            }
        }

        private void resetButton(Button btn)
        {
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
            btn.BackColor = Color.Transparent;
            btn.Enabled = true;
        }

        private void restartGame()
        {
            resetButton(button1);
            resetButton(button2);
            resetButton(button3);
            resetButton(button4);
            resetButton(button5);
            resetButton(button6);
            resetButton(button7);
            resetButton(button8);
            resetButton(button9);

            player = enPlayer.player1;
            lblPlayer.Text = "Player1";
            gameStatus.playCount = 0;
            gameStatus.winner = enWinner.gameInProgress;
            gameStatus.gameOver = false;
            lblWinner.Text = "On Going";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color black = Color.FromArgb(255, 0, 0, 0);

            Pen pen = new Pen(black);
            pen.Width = 5;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 450, 150, 450, 400);
            e.Graphics.DrawLine(pen, 550, 150, 550, 400);

            e.Graphics.DrawLine(pen, 360, 230, 640, 230);
            e.Graphics.DrawLine(pen, 360, 330, 640, 330);
        }
        private void button_Click(object sender, EventArgs e)
        {
            changeImage((Button)sender);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            restartGame();
        }
    }
}
