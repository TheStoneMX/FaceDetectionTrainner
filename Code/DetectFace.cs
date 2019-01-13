//----------------------------------------------------------------------------
//  Copyright (C) 2004-2016 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.XFeatures2D;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


#if !(__IOS__ || NETFX_CORE)
using Emgu.CV.Cuda;
#endif

namespace RedBallTracker
{
   public static class DetectFace
   {

        static int counter = 0;
        static double DESIRED_LEFT_EYE_X = 0.16;     // Controls how much of the face is visible after preprocessing.
        static double DESIRED_LEFT_EYE_Y = 0.14;
        static double FACE_ELLIPSE_CY = 0.40;
        static double FACE_ELLIPSE_W = 0.45;         // Should be atleast 0.5
        static double FACE_ELLIPSE_H = 0.70;         // Controls how tall the face mask is.


        public static void Detect( Mat OriginalImage, String cascadeFileName, List<Rectangle> faces, bool tryUseCuda, out long detectionTime)
      {
         Stopwatch watch;

         #if !(__IOS__ || NETFX_CORE)
         if (tryUseCuda && CudaInvoke.HasCuda)
         {
            using (CudaCascadeClassifier face = new CudaCascadeClassifier(cascadeFileName))
            {
               face.ScaleFactor = 1.1;
               face.MinNeighbors = 10;
               face.MinObjectSize = Size.Empty;
               watch = Stopwatch.StartNew();

               using (CudaImage<Bgr, Byte> gpuImage = new CudaImage<Bgr, byte>(OriginalImage))
               using (CudaImage<Gray, Byte> gpuGray = gpuImage.Convert<Gray, Byte>())
               using (GpuMat region = new GpuMat())
               {
                  face.DetectMultiScale(gpuGray, region);
                  Rectangle[] faceRegion = face.Convert(region);
                  // add faces detected to the faces List<>   
                  faces.AddRange(faceRegion);
               }
               watch.Stop();
            }
         }
         else
         #endif
         {
                // If the input image is not grayscale, then convert the BGR or BGRA color image to grayscale.
                // 1.- then we convert image from BRG 2 Gray
                Mat grayImg = new Mat();
                if (OriginalImage.NumberOfChannels == 3)
                {
                    CvInvoke.CvtColor(OriginalImage, grayImg, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                }
                else
                if (OriginalImage.NumberOfChannels == 4)
                {
                    CvInvoke.CvtColor(OriginalImage, grayImg, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                }
                else
                {
                    // Access the input image directly, since it is already grayscale.
                    grayImg = OriginalImage;
                }


                // Possibly shrink the image, to run much faster.
                Mat ReSizeImg = new Mat();
                float scale = grayImg.Cols / (float)320;

                // 2.-  first we need to resize image
                if (grayImg.Cols > 320)
                {
                    // Shrink the image while keeping the same aspect ratio.
                    int scaledHeight = grayImg.Rows / (int)scale;
                    CvInvoke.Resize(grayImg, ReSizeImg, new Size(320, scaledHeight));

                }
                else
                {
                    // Access the input image directly, since it is already small.
                    ReSizeImg = grayImg;
                }

                // Standardize the brightness and contrast to improve dark images.
                // 3.- Then we normalize brightness and increases contrast of the image
                Mat equalizedImg = new Mat();
                CvInvoke.EqualizeHist(ReSizeImg, equalizedImg);

                //Read the HaarCascade objects
                using (CascadeClassifier face = new CascadeClassifier(cascadeFileName))
                {
                   watch = Stopwatch.StartNew();
                   try
                    {
                        {
                            // 4.- Then
                            //Detect the faces  from the gray scale image 
                            //and store the locations of faces as rectangle´s
                            //The first dimensional is the channel
                            //The second dimension is the index of the rectangle in the specific channel
                            Rectangle[] facesDetected = face.DetectMultiScale(equalizedImg, 
                                                                              1.2, // specifies how quickly OpenCV should increase the scale for face detections with each pass
                                                                              4,  // The minimum-neighbors threshold’ which sets the cutoff level for discarding or keeping rectangle groups as “face” or not
                                                                              new Size(25, 25)); // A good rule of thumb is to use some fraction of your input image's width or height as the minimum scale - 
                                                                                                 // for example, 1/4 of the image width. If you specify a minimum scale other than the default, 
                                                                                                 // be sure its aspect ratio (the ratio of width to height) is the same as the default's. i.e, 
                                                                                                 // aspect ratio should be 1:1.”

                                if (!(facesDetected.Length <= 0))
                                {
                                    // Enlarge the results if the image was temporarily shrunk before detection.
                                    if (OriginalImage.Cols > 320)
                                    {
                                        if (OriginalImage.Cols > 320)
                                        {
                                            for (int i = 0; i < (int)facesDetected.Length; i++)
                                            {
                                                facesDetected[i].X = facesDetected[i].X * (int)scale;
                                                facesDetected[i].Y = facesDetected[i].Y * (int)scale;
                                                facesDetected[i].Width = facesDetected[i].Width * (int)scale;
                                                facesDetected[i].Height = facesDetected[i].Height * (int)scale;
                                            }
                                        }
                                    }

                                    // Make sure the object is completely within the image, in case it was on a border.
                                    for (int i = 0; i < facesDetected.Length; i++)
                                    {
                                        if (facesDetected[i].X < 0)
                                            facesDetected[i].X = 0;
                                        if (facesDetected[i].Y < 0)
                                            facesDetected[i].Y = 0;
                                        if (facesDetected[i].X + facesDetected[i].Width > OriginalImage.Cols)
                                            facesDetected[i].X = OriginalImage.Cols - facesDetected[i].Width;
                                        if (facesDetected[i].Y + facesDetected[i].Height > OriginalImage.Rows)
                                            facesDetected[i].Y = OriginalImage.Rows - facesDetected[i].Height;
                                    }

                                    faces.AddRange(facesDetected);
                                }
                         }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Read the HaarCascade objects{0}", ex.ToString());
                    }
                    watch.Stop();
                }
         }
        detectionTime = watch.ElapsedMilliseconds;
      }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        //static public Image<Gray, Byte> CenterCrop(Image<Bgr, Byte> srcImage, Rectangle rect)
        static public Bitmap CenterCrop(Image<Bgr, Byte> srcImage, Rectangle rect)
        {
            Image<Gray, Byte> retImage = null;
            Image<Gray, Byte> newSize = null;
            Image<Gray, byte> imgBilateralFilter = null;

            try
            {
                var CenteredRect = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
                Image<Bgr, byte> srcImageCopy = srcImage.Copy(CenteredRect);
                
                // Conver2Gray
                // If the input image is not grayscale, then convert the BGR or BGRA color image to grayscale.
                // 1.- then we convert image from BRG 2 Gray
                Image<Gray, Byte> grayImg = new Image<Gray, Byte>(srcImageCopy.Size);
                CvInvoke.CvtColor(srcImageCopy, grayImg, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                // Standardize the brightness and contrast to improve dark images.
                // 2.- Then we normalize brightness and increases contrast of the image
                grayImg._EqualizeHist();
                //grayImg.Save(String.Format("C:\\Temp\\_EqualizeHist{0}.jpg", counter));

                // no we apply filter to get rid of Equalization Noise.
                imgBilateralFilter = new Image<Gray,Byte>(grayImg.Size); 
                CvInvoke.BilateralFilter(grayImg, imgBilateralFilter, 0, 20.0, 2.0);
                //ImageBilateral.Save(String.Format("C:\\Temp\\BilateralFilter{0}.jpg", counter));

                // no we go and alling the face.
                retImage = AlignFace(imgBilateralFilter);

                Point faceCenter = new Point(imgBilateralFilter.Width / 2, (int)Math.Round(imgBilateralFilter.Height * FACE_ELLIPSE_CY));
                Size size = new Size((int)Math.Round(imgBilateralFilter.Width * FACE_ELLIPSE_W), (int)Math.Round(imgBilateralFilter.Width * FACE_ELLIPSE_H));

                // Filter out the corners of the face, since we mainly just care about the middle parts.
                // Draw a filled ellipse in the middle of the face-sized image.
                Image<Gray, Byte> mask = new Image<Gray, Byte>(imgBilateralFilter.Size);

                // draw Ellipse on Mask
                CvInvoke.Ellipse(mask, faceCenter, size, 0, 0, 360, new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LineType.AntiAlias, 0);

                // Apply the elliptical mask on the face.
                Image<Gray, Byte> dstImg = imgBilateralFilter.Copy(mask);
                newSize = dstImg.Resize(100, 100, Inter.Linear);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            ////
            return newSize.ToBitmap();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        //static public Image<Gray, Byte> CenterCrop(Image<Bgr, Byte> srcImage, Rectangle rect)
        static public Image<Gray, Byte> CenterCropIamge(Image<Bgr, Byte> srcImage, Rectangle rect)
        {
            Image<Gray, Byte> retImage = null;
            Image<Gray, Byte> newSize = null;
            Image<Gray, byte> ImageBilateral = null;

            try
            {
                var CenteredRect = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
                Image<Bgr, byte> srcImageCopy = srcImage.Copy(CenteredRect);

                // Conver2Gray
                // If the input image is not grayscale, then convert the BGR or BGRA color image to grayscale.
                // 1.- then we convert image from BRG 2 Gray
                Image<Gray, Byte> grayImg = new Image<Gray, Byte>(srcImageCopy.Size);
                CvInvoke.CvtColor(srcImageCopy, grayImg, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                // Standardize the brightness and contrast to improve dark images.
                // 2.- Then we normalize brightness and increases contrast of the image
                grayImg._EqualizeHist();
                //grayImg.Save(String.Format("C:\\Temp\\_EqualizeHist{0}.jpg", counter));

                // no we apply filter to get rid of Equalization Noise.
                ImageBilateral = new Image<Gray, Byte>(grayImg.Size);
                CvInvoke.BilateralFilter(grayImg, ImageBilateral, 0, 20.0, 2.0);
                //ImageBilateral.Save(String.Format("C:\\Temp\\BilateralFilter{0}.jpg", counter));

                retImage = AlignFace(ImageBilateral);

                Point faceCenter = new Point(ImageBilateral.Width / 2, (int)Math.Round(ImageBilateral.Height * FACE_ELLIPSE_CY));
                Size size = new Size((int)Math.Round(ImageBilateral.Width * FACE_ELLIPSE_W), (int)Math.Round(ImageBilateral.Width * FACE_ELLIPSE_H));

                // Filter out the corners of the face, since we mainly just care about the middle parts.
                // Draw a filled ellipse in the middle of the face-sized image.
                Image<Gray, Byte> mask = new Image<Gray, Byte>(ImageBilateral.Size);
                
                // draw Ellipse on Mask
                CvInvoke.Ellipse(mask, faceCenter, size, 0, 0, 360, new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LineType.AntiAlias, 0);

                // Apply the elliptical mask on the face.
                Image<Gray, Byte>  dstImg = ImageBilateral.Copy(mask);
                newSize = dstImg.Resize(100, 100, Inter.Linear);
                //dstImg.Save(String.Format("C:\\Temp\\dstImg{0}.jpg", counter++));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return newSize; // ret;
        }



        /// <summary>
        /// This Method needs the Cropped face.
        /// Here, upper_face is the upper part of face from mid of nose. Simply, I will take sub rectangle of detected face image with height set to half of original.
        /// </summary>
        /// <param name="Face"></param>
        /// <returns></returns>
        static public Image<Gray, byte> AlignFace(Image<Gray, byte> Face)
        {
            try
            {
                int height = Face.Height / 2;
                // How detailed should the search be. Must be larger than 1.0.
                const float searchScaleFactor = 1.3f;

                // How much the detections should be filtered out. This should depend on how bad false detections are to your system.
                // minNeighbors=2 means lots of good+bad detections, and minNeighbors=6 means only good detections are given but some are missed.
                const int minNeighbors = 4;

                // Only search for just 1 object (the biggest in the image).
                //int flags = CASCADE_FIND_BIGGEST_OBJECT;// | CASCADE_DO_ROUGH_SEARCH;
                // Smallest object size.
                Size minFeatureSize = new Size(4, 4);


                Rectangle rect = new Rectangle(0, 0, Face.Width, height);
                Image<Gray, byte> upper_face = Face.GetSubRect(rect);

                //following variables are used to detect right eye and 
                //left eye for fixing position and hence used for face alignment
                CascadeClassifier haar_righteye = new CascadeClassifier("haarcascade_mcs_righteye.xml");
                CascadeClassifier haar_lefteye = new CascadeClassifier("haarcascade_mcs_lefteye.xml");

                //MCvAvgComp[][] Right_Eye = haar_righteye.DetectMultiScale(upper_face, searchScaleFactor, minNeighbors, minFeatureSize);
                Rectangle [] Right_Eye = haar_righteye.DetectMultiScale(upper_face, searchScaleFactor, minNeighbors, minFeatureSize);
                Rectangle[] Left_Eye  = haar_lefteye.DetectMultiScale (upper_face, searchScaleFactor, minNeighbors, minFeatureSize);

                if ((Right_Eye.Length > 0) && (Left_Eye.Length > 0))
                {
                    Debug.WriteLine("2 eyes found");
                    bool FLAG = false;
                    foreach (Rectangle R_eye in Right_Eye)
                    {
                        foreach (Rectangle L_eye in Left_Eye)
                        {
                            if (R_eye.X > (L_eye.X + L_eye.Width))
                            {
                                //upper_face.Draw(R_eye.rect, new Gray(200), 2);
                                //upper_face.Draw(L_eye.rect, new Gray(200), 2);
                                var deltaY = (L_eye.Y + L_eye.Height / 2) -
                                             (R_eye.Y + R_eye.Height / 2);
                                //using horizontal position and width attribute find out the variable deltaX
                                var deltaX = (L_eye.X + L_eye.Width / 2) -
                                             (R_eye.X + R_eye.Width / 2);
                                double degrees = (Math.Atan2(deltaY, deltaX) * 180) / Math.PI;//find out 
                                                                                              //the angle as per position of eyes


                                // To actual rotate the face, I will find out angle according to eye positions.
                                // Then, using this angle in calculation with 180 degree, the final angle to rotate face image is found out.
                                degrees = 180 - degrees;
                                Face = Face.Rotate(degrees, new Gray(220), true);
                                FLAG = true;
                                break;
                            }
                        }
                        if (FLAG == true)
                        {
                            break;
                        }
                    }

                }
                else
                    Debug.WriteLine("Not 2 eyes found");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" Align Error: " + ex.Message);
            }
            return Face;
        }


    }
}
