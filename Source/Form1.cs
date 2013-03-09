// Copyright (c) 2013 Jay Bazuzi (JAY@BAZUZI.COM)

#region MIT LICENSE
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

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
            switch (e.KeyCode)
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
