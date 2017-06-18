#define My_Debug

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shooter.Properties;
using System.Media;

namespace Shooter
{
    public partial class WildShooter : Form
    {
        const int FrameNum = 2;
        const int SplatNum = 7;
        bool splat = false;
        int _splatTime = 0;
        int _gameFrame = 0;

        int _hit = 0;
        int _misses = 0;
        int _totalShots = 0;
        double _averageHits = 0;

        CRobot _robot;
        CSplat _splat;
        CSign _sign;
        CScoreFrame _scoreFrame;
        Random rnd = new Random();

        public WildShooter()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(Resources.gun);
            this.Cursor = CustomCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _scoreFrame = new CScoreFrame() { Left = 10, Top = 10 };
            _sign = new CSign() {Left=1050,Top=10 };
            _robot = new CRobot() { Left = 630, Top = 389 };
            _splat = new CSplat();
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (_gameFrame>=SplatNum)
            {
                UpdateRobot();
                _gameFrame = 0;
            }
            if (splat)
            {
                if (_splatTime >= FrameNum)
                {
                    splat = false;
                    _splatTime = 0;
                    UpdateRobot();
                }
                _splatTime++;
            }
            _gameFrame++;
            this.Refresh();

        }
        private void UpdateRobot()
        {
            _robot.Update(
                rnd.Next(Resources.Robot.Width, 1200),
                rnd.Next(330, 600));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            if (splat == true)
            {
                _splat.DrawImage(dc);
            }
            else
            {
                _robot.DrawImage(dc);
            }
            
            _sign.DrawImage(dc);
            _scoreFrame.DrawImage(dc);


            //put score on the screen
            TextFormatFlags flags = TextFormatFlags.Left;
            Font _font = new System.Drawing.Font("Stencil", 32, FontStyle.Regular);
            TextRenderer.DrawText(e.Graphics, "Shots:" + _totalShots.ToString(), _font, new Rectangle(60, 52, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Hits:" + _hit.ToString(), _font, new Rectangle(60, 92, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Misses:" + _misses.ToString(), _font, new Rectangle(60, 132, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Avg:" + _averageHits.ToString("F2")+"%", _font, new Rectangle(60, 172, 320, 200), SystemColors.ControlText, flags);
            base.OnPaint(e);
        }


        private void WildShooter_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X>1167 && e.X<1239 && e.Y > 30 && e.Y < 50)
            {
                timerGameLoop.Start();
            }
            else if (e.X > 1167 && e.X < 1239 && e.Y > 60 && e.Y < 80)
            {
                timerGameLoop.Stop();
            }
            else if (e.X > 1167 && e.X < 1239 && e.Y > 100 && e.Y < 120)
            {
                timerGameLoop.Stop();
                Application.Restart();
            }
            else if (e.X > 1167 && e.X < 1239 && e.Y > 130 && e.Y < 150)
            {
                timerGameLoop.Stop();
                Application.Exit();
            }
            else
            {
                if (_robot.Hit(e.X, e.Y))
                {
                    splat = true;
                    _splat.Left = _robot.Left - Resources.EL.Width / 3;
                    _splat.Top = _robot.Top - Resources.EL.Height / 3;
                    _hit++;
                }
                else
                {
                _misses++;

                }
                _totalShots = _hit + _misses;
                _averageHits = (double)_hit /(double) _totalShots*100.0;
            }
           LazerGun();

        }
        private void LazerGun()
        {
            SoundPlayer simpleSound = new SoundPlayer(Resources.LazerGun);
            simpleSound.Play();
        }
    }
}
