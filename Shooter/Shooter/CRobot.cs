namespace Shooter
{
    using Shooter.Properties;
    using System.Drawing;

    class CRobot : CImageBase
    {
        private Rectangle _robotHotSpot = new Rectangle();

        public CRobot()
            :base(Resources.Robot)
        {
            _robotHotSpot.X = Left + 20;
            _robotHotSpot.Y = Top - 1;
            _robotHotSpot.Width = 170;
            _robotHotSpot.Height = 170;
        }
        
        public void Update(int X,int Y)
        {
            Left = X;
            Top = Y;
            _robotHotSpot.X = Left + 20;
            _robotHotSpot.Y = Top - 1;
        }

        public bool Hit(int X,int Y)
        {
            Rectangle c = new Rectangle(X, Y, 1, 1);//Create a cursor rect - quick way to check for hit

            if (_robotHotSpot.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}
