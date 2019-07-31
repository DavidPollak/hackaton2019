using System;
using System.Windows;
using System.IO;
using System.Drawing;
 

namespace ImageReco_17
{

    public class PO_Image_Bmp 
    {
        public void imagePoling()
        {
            Logger.Debug("imagePoling");
            String RefImagesPath = "C:\\Retalix\\Hackathon\\Images";
            String LiveImagesPath = "C:\\Retalix\\Hackathon\\LiveImages";
            String IDTextPath = "C:\\Retalix\\Hackathon\\ItemsIDs";

            string[] RefImages = Directory.GetFiles(RefImagesPath);
            
            try
            {
                while (true)
                {
                    try
                    {
                        System.Threading.SpinWait.SpinUntil(() => Directory.GetFiles(LiveImagesPath).Length > 0);
                        string[] images= Directory.GetFiles(LiveImagesPath);
                        foreach(string liveImagePath in images)
                        {
                            Bitmap LiveImage;
                            using (FileStream myStream = new FileStream(liveImagePath, FileMode.Open))
                            {
                                try
                                {
                                    LiveImage = Image.FromStream(myStream) as Bitmap;
                                }
                                finally
                                {
                                    myStream.Close();
                                    myStream.Dispose();
                                }
                            }

                            foreach (string refImagePath in RefImages)
                            {

                                Bitmap RefImage;
                                using (FileStream myStream = new FileStream(refImagePath, FileMode.Open))
                                {
                                    try
                                    { 
                                    RefImage = Image.FromStream(myStream) as Bitmap;
                                    }
                                    finally
                                    {
                                        myStream.Close();
                                        myStream.Dispose();
                                    }

                                }
                                ImageFinder imageFinder = new ImageFinder( LiveImage, RefImage);
                                Rect r = imageFinder.Find();
                                if(r.Width>0)
                                {
                                    string IDStr=Path.GetFileNameWithoutExtension(refImagePath);
                                    string IDFileName = IDStr + ".txt";
                                    File.Create(Path.Combine(IDTextPath, IDFileName)).Close();
                                    break;
                                }
                                else
                                {
                                    File.Create("0.txt").Close();
                                }
                               
                            }

                            File.Delete(liveImagePath);
                        }
                        

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        Logger.Fatal(ex.Message.ToString());
                        File.Create("0.txt").Close();

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Logger.Fatal(ex.Message.ToString());
                File.Create("0.txt").Close();
            }

        }
        private Rect ImageRect(string imageFile)
        {
            Bitmap mainImage = ScreenCapturer.Capture(false);
            //Bitmap mainImage = screenShot.ReturnScreenShot();
            Bitmap subImage = Image.FromFile(imageFile) as Bitmap;

            Logger.Debug("Sub image size captured " + subImage.Width + " , " + subImage.Height);

            ImageFinder imageFinder = new ImageFinder(mainImage, subImage);
            Rect r = imageFinder.Find();
            return r;
        }
    }
}
