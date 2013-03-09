using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BazuziTetris
{
    public partial class Form1 : Form
    {
        Game game = new Game();
        public Form1()
        {
            InitializeComponent();
            this.timer1.Start();
        }

        const int blockSize = 20;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Brush brushBlank = new SolidBrush(Color.Black);
            Brush brushBlock = new SolidBrush(Color.Red);

            var gameBitmap = game.Well.Union(game.CurrentPiece.Bitmap, game.CurrentPieceLocation);

            foreach (var x in gameBitmap.HorizontalRange)
            {
                foreach (var y in gameBitmap.VerticalRange)
                {
                    if (gameBitmap[x, y])
                    {
                        g.FillRectangle(brushBlock, x * blockSize, (gameBitmap.Height - y) * blockSize, blockSize, blockSize);
                    }

                    else
                    {
                        g.FillRectangle(brushBlank, x * blockSize, (gameBitmap.Height - y) * blockSize, blockSize, blockSize);
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Down:
                    this.game.CurrentPieceDropOneStep();
                    break;

                case Keys.Up:
                    this.game.CurrentPieceRotate();
                    break;

                case Keys.Left:
                    this.game.MoveLeft();
                    break;

                case Keys.Right:
                    this.game.MoveRight();
                    break;

                case Keys.Space:
                    this.game.DropAllTheWay();
                    break;
            }

            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.game.OnTick();
            Invalidate();
        }
    }
}
