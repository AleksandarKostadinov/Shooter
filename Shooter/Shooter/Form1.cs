#define My_Debug

using System;
using System.Drawing;
using System.Windows.Forms;
using Shooter.Properties;
using System.Media;

namespace Shooter
{
    public partial class WildShooter : Form
    {
        int FrameNum = 8;
        int SplatNum = 3;
        bool splat = false;
        int _splatTime = 0;
        int _gameFrame = 0;

        int _hit = 0;
        int _misses = 0;
        int _totalShots = 0;
        double _averageHits = 0;
        string _skillLevel = "Novice";

        CRobot _robot;
        CSplat _splat;
        CSign _sign;
        CScoreFrame _scoreFrame;
        Random rnd = new Random();
        SoundPlayer simpleSound = new SoundPlayer(Resources.LazerGun);

        public WildShooter()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(Resources.gun);
            this.Cursor = CustomCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

            _scoreFrame = new CScoreFrame() { Left = 10, Top = 10 };
            _sign = new CSign() { Left = 1050, Top = 10 };
            _robot = new CRobot() { Left = 630, Top = 389 };
            _splat = new CSplat();
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (_gameFrame >= FrameNum)
            {
                UpdateRobot();
                _gameFrame = 0;
            }
            if (splat)
            {
                if (_splatTime >= SplatNum)
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
                rnd.Next(Resources.Robot.Width, this.Width - Resources.Robot.Width),
                rnd.Next(this.Height / 2, this.Height - Resources.Robot.Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            if (splat)
            {
                _splat.DrawImage(dc);
            }
            else
            {
                _robot.DrawImage(dc);
            }

            _sign.DrawImage(dc);
            _scoreFrame.DrawImage(dc);

            //Put score on the screen
            TextFormatFlags flags = TextFormatFlags.Left;

            Font _font = new Font("Stencil", 22, FontStyle.Regular);
            Font _fontSkill = new Font("Stencil", 24, FontStyle.Regular);

            TextRenderer.DrawText(e.Graphics, "Shots:" + _totalShots.ToString(), _font, new Rectangle(60, 52, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Hits:" + _hit.ToString(), _font, new Rectangle(60, 82, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Misses:" + _misses.ToString(), _font, new Rectangle(60, 112, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Avg:" + _averageHits.ToString("F2") + "%", _font, new Rectangle(60, 142, 320, 200), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Skill: " + _skillLevel, _fontSkill, new Rectangle(60, 182, 320, 200), SystemColors.ControlText, flags);

            base.OnPaint(e);
        }


        private void WildShooter_MouseClick(object sender, MouseEventArgs e)
        {
            LazerGun();

            if (e.X > 1167 && e.X < 1239 && e.Y > 30 && e.Y < 50)
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
                // Does not count shots if the game is stoped
                if (!timerGameLoop.Enabled)
                {
                    return;
                }

                if (_robot.Hit(e.X, e.Y))
                {
                    splat = true;
                    _splat.Left = _robot.Left - Resources.EL.Width / 5;
                    _splat.Top = _robot.Top - Resources.EL.Height / 5;
                    _hit++;
                }
                else
                {
                    _misses++;
                }

                _totalShots = _hit + _misses;
                _averageHits = (double)_hit / _totalShots * 100.0;

                UpdateSkillLevel();
            }
        }

        private void LazerGun()
        {
            simpleSound.Play();
        }

        private void UpdateSkillLevel()
        {
            if (_totalShots < 15)
            {
                return;
            }

            if (_averageHits > 60)
            {
                _skillLevel = "God";
                FrameNum = 5;
            }
            else if (_averageHits == 0)
            {
                _skillLevel = "Master";
                FrameNum = 6;
            }
            else if (_averageHits >= 40)
            {
                _skillLevel = "Pro";
                FrameNum = 7;
            }
            else if (_averageHits >= 30)
            {
                _skillLevel = "Girl";
                FrameNum = 8;
            }
            else if (_averageHits >= 20)
            {
                _skillLevel = "Noob";
                FrameNum = 9;
            }
            else
            {
                _skillLevel = "Bot";
                FrameNum = 10;
            }
        }
    }
}
