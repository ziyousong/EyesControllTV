using System;
using System.Windows;

namespace EyesControllTV
{
    public class GazeDataStream
    {
        public double eyeX { get; set; }
        public double eyeY { get; set; }
        private static double Sheight, Swidth, DivHeight, DivWidth;

        private EyeXHost _eyeXHost = new EyeXHost();
        GazePointDataStream stream;

        public GazeDataStream()
        {
            Sheight = SystemParameters.PrimaryScreenHeight;
            Swidth = SystemParameters.PrimaryScreenWidth;
            DivHeight = Sheight / 10;
            DivWidth = Swidth / 10;
        }

        public void startEyeTrack()
        {
            stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);
            stream.Next += new EventHandler<GazePointEventArgs>(getEyeData);
            _eyeXHost.Start();
        }

        private void getEyeData(object sender, GazePointEventArgs e)
        {
            eyeX = Math.Round(e.X / DivWidth, 0);
            eyeY = Math.Round(e.Y / DivHeight, 0);
        }

        public double getEyeX()
        {
            return eyeX;
        }

        public double getEyeY()
        {
            return eyeY;
        }

    }
}
