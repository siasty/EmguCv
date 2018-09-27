using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

#if _WINDOWS_
private const string nativeLibName = "cvextern.dll";
#elif _LINUX_
private const string nativeLibName = "cvextern.so";
#endif

namespace EmguCv
{
    class Program
    {
        private static VideoCapture _capture = null;
        private static Mat _frame;
        private static Mat _grayFrame;
        private static Mat _smallGrayFrame;
        private static Mat _smoothedGrayFrame;
        private static Mat _cannyFrame;

        static void Main(string[] args)
        {
            try
            {
                _capture = new VideoCapture(0);
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException excpt)
            {
               Console.WriteLine(excpt.Message);
            }

            _frame = new Mat();
            _grayFrame = new Mat();
            _smallGrayFrame = new Mat();
            _smoothedGrayFrame = new Mat();
            _cannyFrame = new Mat();

        }

        private static void ProcessFrame(object sender, EventArgs arg)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                _capture.Retrieve(_frame, 0);

                CvInvoke.CvtColor(_frame, _grayFrame, ColorConversion.Bgr2Gray);

                CvInvoke.PyrDown(_grayFrame, _smallGrayFrame);

                CvInvoke.PyrUp(_smallGrayFrame, _smoothedGrayFrame);

                CvInvoke.Canny(_smoothedGrayFrame, _cannyFrame, 100, 60);

                //captureImageBox.Image = _frame;
                //grayscaleImageBox.Image = _grayFrame;
                //smoothedGrayscaleImageBox.Image = _smoothedGrayFrame;
                //cannyImageBox.Image = _cannyFrame;
            }
        }

    }
}
