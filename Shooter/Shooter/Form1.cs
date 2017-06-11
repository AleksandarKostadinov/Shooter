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

namespace Shooter
{
    public partial class WildShooter : Form
    {
#if My_Debug
        int _cursX = 0;
        int _cursY = 0;
#endif
        CRobot _robot;
        CSplat _splat;
        CSign _sign;
        CScoreFrame _scoreFrame;

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

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            _sign.DrawImage(dc);
            _scoreFrame.DrawImage(dc);

            _robot.DrawImage(dc);
#if My_Debug
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc , "X=" + _cursX.ToString() + ":Y=" + _cursY.ToString(), _font,
                new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);
#endif
            base.OnPaint(e);
        }

        private void WildShooter_MouseMove(object sender, MouseEventArgs e)
        {
#if My_Debug
            _cursX = e.X;
            _cursY = e.Y;
#endif
            this.Refresh();
        }

        private void WildShooter_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
