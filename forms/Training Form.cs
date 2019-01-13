using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Emgu.CV.UI;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using System.IO;
using System.Drawing.Imaging;
using System.Xml;
using System.Threading;


namespace RedBallTracker
{
    public partial class Training_Form : Form
    {
        #region Variables
        //Camera specific
        Capture grabber;

        //Images for finding face
        Image<Bgr, Byte> currentFrame;
        Image<Gray, byte> result = null;
        Image<Gray, byte> gray_frame = null;
        List<Rectangle> faces = new List<Rectangle>();
        bool tryUseCuda = false;
        long detectionTime;

        //Classifier
        CascadeClassifier Face;

        //For aquiring 10 images in a row
        //List<Image<Gray, byte>> resultImages = new List<Image<Gray, byte>>();
        List<Bitmap> resultImages = new List<Bitmap>();
        int results_list_pos = 0;
        const int num_faces_to_aquire = 20;
        bool RECORD = false;

        //Saving Jpg
        List<Image<Gray, byte>> ImagestoWrite = new List<Image<Gray, byte>>();
        EncoderParameters ENC_Parameters = new EncoderParameters(1);
        EncoderParameter ENC = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);
        ImageCodecInfo Image_Encoder_JPG;

        //Saving XAML Data file
        List<string> NamestoWrite = new List<string>();
        List<string> NamesforFile = new List<string>();
        XmlDocument docu = new XmlDocument();

        //Variables
        frmMain Parent;
        #endregion

        public Training_Form(frmMain _Parent)
        {
            InitializeComponent();
            Parent = _Parent;
            Face = Parent.Face;
            //Face = new HaarCascade(Application.StartupPath + "/Cascades/haarcascade_frontalface_alt2.xml");
            ENC_Parameters.Param[0] = ENC;
            Image_Encoder_JPG = GetEncoder(ImageFormat.Jpeg);
            initialise_capture();
        }

        private void Training_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_capture();
            Parent.retrain();
            Parent.initialise_capture();
        }

        //Camera Start Stop
        public void initialise_capture()
        {
            grabber = new Capture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
        }
        private void stop_capture()
        {
            Application.Idle -= new EventHandler(FrameGrabber);
            if (grabber != null)
            {
                grabber.Dispose();
            }
            //Initialize the FrameGraber event
        }

        //Process Frame
        void FrameGrabber(object sender, EventArgs e)
        {

            try
            {
                //Get the current frame form capture device
                //Mat currentFrame = grabber.QueryFrame();
                //Mat equlized = new Mat();
                Bitmap result = null;
                using (Mat currentFrame = grabber.QueryFrame())
                using (Mat equlized = new Mat())
                {
                    //Convert it to Grayscale
                    if (currentFrame != null)
                    {
                        //Face Detector
                        DetectFace.Detect(currentFrame, "haarcascade_frontalface_default.xml", faces, tryUseCuda, out detectionTime); // works

                        if (faces.Count > 0)
                        {
                            // write to 
                            // here we have the locations of each face so we can draw rectangles
                            // on each face
                            foreach (Rectangle face in faces)
                            {
                                //Crop Face
                                result = DetectFace.CenterCrop(currentFrame.ToImage<Bgr, byte>(), face);

                                if (result == null)
                                    return;

                                face_PICBX.Image = result;

                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                            }

                            if (RECORD && faces.Count > 0 && resultImages.Count < num_faces_to_aquire)
                            {
                                resultImages.Add(result);

                                count_lbl.Text = "Count: " + resultImages.Count.ToString();

                                if (resultImages.Count == num_faces_to_aquire)
                                {
                                    ADD_BTN.Enabled = true;
                                    NEXT_BTN.Visible = true;
                                    PREV_btn.Visible = true;
                                    count_lbl.Visible = false;
                                    Single_btn.Visible = true;
                                    ADD_ALL.Visible = true;
                                    RECORD = false;
                                    Application.Idle -= new EventHandler(FrameGrabber);
                                }
                            }
                            faces.Clear();
                            image_PICBX.Image = currentFrame.Bitmap;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
            finally
            {
                faces.Clear();
            }

        }

        //Saving The Data
        private bool save_training_data(Image face_data)
        {
            try
            {
                Random rand = new Random();
                bool file_create = true;
                string facename = "face_" + NAME_PERSON.Text + "_" + rand.Next().ToString() + ".jpg";
                while (file_create)
                {

                    if (!File.Exists(Application.StartupPath + "/TrainedFaces/" + facename))
                    {
                        file_create = false;
                    }
                    else
                    {
                        facename = "face_" + NAME_PERSON.Text + "_" + rand.Next().ToString() + ".jpg";
                    }
                }


                //if(Directory.Exists(Application.StartupPath + "/TrainedFaces/"))
                if (Directory.Exists("C:\\TrainedFaces\\"))
                {
                    face_data.Save("C:\\TrainedFaces\\" + facename, ImageFormat.Jpeg);
                }
                else
                {
                    Directory.CreateDirectory("C:\\TrainedFaces\\");
                    face_data.Save("C:\\TrainedFaces\\" + facename, ImageFormat.Jpeg);
                }

                ///////
                //if (File.Exists(Application.StartupPath + "/TrainedFaces/TrainedLabels.xml"))
                if (File.Exists("C:\\TrainedFaces\\TrainedLabels.xml"))
                {
                    //File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", NAME_PERSON.Text + "\n\r");
                    bool loading = true;
                    while (loading)
                    {
                        try
                        {
                            docu.Load("C:\\TrainedFaces\\TrainedLabels.xml");
                            loading = false;
                        }
                        catch
                        {
                            docu = null;
                            docu = new XmlDocument();
                            Thread.Sleep(10);
                        }
                    }

                    //Get the root element
                    XmlElement root = docu.DocumentElement;

                    XmlElement face_D = docu.CreateElement("FACE");
                    XmlElement name_D = docu.CreateElement("NAME");
                    XmlElement file_D = docu.CreateElement("FILE");

                    //Add the values for each nodes
                    //name.Value = textBoxName.Text;
                    //age.InnerText = textBoxAge.Text;
                    //gender.InnerText = textBoxGender.Text;
                    name_D.InnerText = NAME_PERSON.Text;
                    file_D.InnerText = facename;

                    //Construct the Person element
                    //person.Attributes.Append(name);
                    face_D.AppendChild(name_D);
                    face_D.AppendChild(file_D);

                    //Add the New person element to the end of the root element
                    root.AppendChild(face_D);

                    //Save the document
                    docu.Save("C:\\TrainedFaces\\TrainedLabels.xml");
                    //XmlElement child_element = docu.CreateElement("FACE");
                    //docu.AppendChild(child_element);
                    //docu.Save("TrainedLabels.xml");
                }
                else
                {
                    //FileStream FS_Face = File.OpenWrite(Application.StartupPath + "/TrainedFaces/TrainedLabels.xml");
                    FileStream FS_Face = File.OpenWrite("C:\\TrainedFaces\\TrainedLabels.xml");
                    using (XmlWriter writer = XmlWriter.Create(FS_Face))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Faces_For_Training");

                        writer.WriteStartElement("FACE");
                        writer.WriteElementString("NAME", NAME_PERSON.Text);
                        writer.WriteElementString("FILE", facename);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                    FS_Face.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
                
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        //Delete all the old training data by simply deleting the folder
        private void Delete_Data_BTN_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "/TrainedFaces/"))
            {
                Directory.Delete(Application.StartupPath + "/TrainedFaces/", true);
                Directory.CreateDirectory(Application.StartupPath + "/TrainedFaces/");
            }
        }
        
        //Add the image to training data
        private void ADD_BTN_Click(object sender, EventArgs e)
        {
            if (resultImages.Count == num_faces_to_aquire)
            {
                if (!save_training_data(face_PICBX.Image)) MessageBox.Show("Error", "Error in saving file info. Training data not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                stop_capture();
                if (!save_training_data(face_PICBX.Image)) MessageBox.Show("Error", "Error in saving file info. Training data not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                initialise_capture();
            }
        }
        private void Single_btn_Click(object sender, EventArgs e)
        {
            RECORD = false;
            resultImages.Clear();
            NEXT_BTN.Visible = false;
            PREV_btn.Visible = false;
            Application.Idle += new EventHandler(FrameGrabber);
            Single_btn.Visible = false;
            count_lbl.Text = "Count: 0";
            count_lbl.Visible = true;
        }
        //Get 10 image to train
        private void RECORD_BTN_Click(object sender, EventArgs e)
        {
            if (RECORD)
            {
                RECORD = false;
            }
            else
            {
                if (resultImages.Count == 10)
                {
                    resultImages.Clear();
                    Application.Idle += new EventHandler(FrameGrabber);
                }
                RECORD = true;
                ADD_BTN.Enabled = false;
            }

        }
        private void NEXT_BTN_Click(object sender, EventArgs e)
        {
            if (results_list_pos < resultImages.Count - 1)
            {
                face_PICBX.Image = resultImages[results_list_pos];
                results_list_pos++;
                PREV_btn.Enabled = true;
            }
            else
            {
                NEXT_BTN.Enabled = false;
            }
        }
        private void PREV_btn_Click(object sender, EventArgs e)
        {
            if (results_list_pos > 0)
            {
                results_list_pos--;
                face_PICBX.Image = resultImages[results_list_pos];
                NEXT_BTN.Enabled = true;
            }
            else
            {
                PREV_btn.Enabled = false;
            }
        }
        private void ADD_ALL_Click(object sender, EventArgs e)
        {
            for(int i = 0; i<resultImages.Count;i++)
            {
                face_PICBX.Image = resultImages[i];
                if (!save_training_data(face_PICBX.Image)) MessageBox.Show("Error", "Error in saving file info. Training data not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Thread.Sleep(100);
            }
            ADD_ALL.Visible = false;
            //restart single face detection
            Single_btn_Click(null, null);
        }

    }
}
