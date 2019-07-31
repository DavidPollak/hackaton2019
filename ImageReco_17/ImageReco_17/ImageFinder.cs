using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using Emgu.CV;




namespace ImageReco_17
{
    public class ImageFinder
    {
        /// <summary>
        /// Constructs the ImageFinder class
        /// </summary>
        /// <param name="main">The image to search</param>
        /// <param name="sub">The partial image to look for in the 'main' image</param>
        /// <param name="tolerance">The tolerance in percents that find will work above it</param>
        public ImageFinder(Bitmap main, Bitmap sub, int tolerance)
        {
            this.main = main;
            this.sub = sub;
            maxTolerance = (double)tolerance / 100.0;
        }

        public ImageFinder(Bitmap main, Bitmap sub) : this(main, sub, 85)
        {

        }

        /// <summary>
        /// Finds an image within an image
        /// <remarks>
        /// An exact match is found.
        /// Image formats should be 24bpp
        /// </remarks>
        /// </summary>
        /// <returns>(-1, -1, 0, 0) if no match was found, otherwise the rectangle of the sub image within the main image</returns>
        public System.Windows.Rect Find()
        {
            System.Windows.Rect retVal = new System.Windows.Rect(-1, -1, 0, 0);
            try
            {
                using (var m = new Image<Emgu.CV.Structure.Bgr, Byte>(main))
                {
                    using (var s = new Image<Emgu.CV.Structure.Bgr, Byte>(sub))
                    {
                        try
                        {
                            using (var r = m.MatchTemplate(s, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed /* TM_TYPE.CV_TM_CCOEFF_NORMED*/))
                            {
                                //double[] min, max;
                                int width = s.Width, height = s.Height;
                                //Point[] minLoc, maxLoc;
                                r.MinMax(out double[] min, out double[] max, out Point[] minLoc, out Point[] maxLoc);
                                if (max[0] >= maxTolerance)
                                {
                                    retVal = new System.Windows.Rect(maxLoc[0].X, maxLoc[0].Y, width, height);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                            Logger.Fatal(ex.Message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Logger.Fatal(ex.Message.ToString());
            }

            return retVal;
        }
        double maxTolerance;
        Bitmap main, sub;
    }
}

