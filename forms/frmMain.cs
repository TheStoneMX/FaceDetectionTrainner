// RedBallTracker.cs
//
// Emgu CV 3.0.0
//
// put this code in your main form, for example frmMain.vb
//
// add the following components to your form:
// tlpOuter (TableLayoutPanel)
// tlpInner (TableLayoutPanel)
// ibOriginal (Emgu ImageBox)
// ibThresh (Emgu ImageBox)
// btnPauseOrResume (Button)
// txtXYRadius (TextBox)
//
// NOTE: Do NOT copy/paste the entire text of this file into Visual Studio !! It will not work if you do !!
// Follow the video on my YouTube channel to create the project and have Visual Studio write part of the code for you,
// then copy/pase the remaining text as needed

using System;
using System.Drawing;
using System.Collections.Generic;

using System.Windows.Forms;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //

///////////////////////////////////////////////////////////////////////////////////////////////////
namespace RedBallTracker
{

    ///////////////////////////////////////////////////////////////////////////////////////////////
    public partial class frmMain : Form
    {
        long detectionTime;
        bool _train = false;
        static int counter = 0;

        //ADD Picture box and label to a panel for each face
        int faces_count = 0;
        int faces_panel_Y = 0;
        int faces_panel_X = 0;

        //Classifier with default training location
        Classifier_Train Eigen_Recog = new Classifier_Train();
        public CascadeClassifier Face = new CascadeClassifier("haarcascade_frontalface_default.xml"); //Our face detection method 
        //public CascadeClassifier Face = new CascadeClassifier(Application.StartupPath + "/Cascades/haarcascade_frontalface_default.xml");//Our face detection method 


        List<Rectangle> faces = new List<Rectangle>();
        List<Rectangle> eyes = new List<Rectangle>();

        //The cuda cascade classifier doesn't seem to be able to load "haarcascade_frontalface_default.xml" file in this release
        //disabling CUDA module for now
        bool tryUseCuda = false;

        enum MODES { MODE_STARTUP = 0, MODE_DETECTION, MODE_COLLECT_FACES, MODE_TRAINING, MODE_RECOGNITION, MODE_DELETE_ALL, MODE_END };
        static String[] MODE_NAMES = { "Startup", "Detection", "Collect Faces", "Training", "Recognition", "Delete All", "ERROR!" };
        MODES m_mode = MODES.MODE_STARTUP;


        // member variables ///////////////////////////////////////////////////////////////////////
        Capture capWebcam;
        bool blnCapturingInProcess = false;

        // constructor ////////////////////////////////////////////////////////////////////////////
        public frmMain()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                capWebcam = new Capture();
            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to read from webcam, error: " + Environment.NewLine + Environment.NewLine +
                                ex.Message + Environment.NewLine + Environment.NewLine +
                                "exiting program");
                Environment.Exit(0);
                return;
            }
            Application.Idle += processFrameAndUpdateGUI;       // add process image function to the application's list of tasks
            blnCapturingInProcess = true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        void processFrameAndUpdateGUI(object sender, EventArgs arg)
        {
            try
            {
                Mat imgOriginal = capWebcam.QueryFrame();
                if (imgOriginal == null)
                {
                    MessageBox.Show("unable to read from webcam" + Environment.NewLine + Environment.NewLine +
                                    "exiting program");
                    Environment.Exit(0);
                    return;
                }
                //
                DetectFace.Detect(imgOriginal, "haarcascade_frontalface_default.xml", faces, tryUseCuda, out detectionTime); // works
                //DetectFace.Detect(imgOriginal, "haarcascade_frontalface_alt.xml", faces, tryUseCuda, out detectionTime); // works
                //DetectFace.Detect(imgOriginal, "haarcascade_frontalface_alt2.xml", faces, tryUseCuda, out detectionTime); // works

                if (faces.Count > 0)
                {
                        // write to 
                        txtXYRadius.Text = detectionTime.ToString();
                        // here we have the locations of each face so we can draw rectangles
                        // on each face.
                        foreach (Rectangle face in faces)
                        {
                            ///
                            if (_train == true)
                            {
                                //Stop Camera
                                stop_capture();

                                //OpenForm
                                Training_Form TF = new Training_Form(this);
                                TF.Show();
                            }

                            ////////
                            if (Eigen_Recog.IsTrained)
                            {
                                Image<Gray, Byte> GropImg = DetectFace.CenterCropIamge(imgOriginal.ToImage<Bgr, byte>(), face); ;

                                String name = Eigen_Recog.Recognise(GropImg);
                                int match_value = (int)Eigen_Recog.Get_Eigen_Distance;

                                //int x = Convert.ToInt32(Eigne_threshold_txtbx.ToString());

                                if (match_value > 2000 )
                                            ADD_Face_Found(GropImg, name, match_value);
                            }
                            
                            //////
                            CvInvoke.Rectangle(imgOriginal, face, new Bgr(Color.Green).MCvScalar, 2);
                        }

                }

                //
                faces.Clear();
                ibOriginal.Image = imgOriginal;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                faces.Clear();
            }
        }
        private void stop_capture()
        {
            Application.Idle -= new EventHandler(processFrameAndUpdateGUI);

            if (capWebcam != null)
            {
                capWebcam.Dispose();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private void btnPauseOrResume_Click(object sender, EventArgs e)
        {
            if (blnCapturingInProcess == true)
            {                    // if we are currently processing an image, user just choose pause, so . . .
                Application.Idle -= processFrameAndUpdateGUI;       // remove the process image function from the application's list of tasks
                blnCapturingInProcess = false;                      // update flag variable
                btnPauseOrResume.Text = " Resume ";                 // update button text
            }
            else
            {                                                // else if we are not currently processing an image, user just choose resume, so . . .
                Application.Idle += processFrameAndUpdateGUI;       // add the process image function to the application's list of tasks
                blnCapturingInProcess = true;                       // update flag variable
                btnPauseOrResume.Text = " Pause ";                  // new button will offer pause option
            }
        }

        private void btnTraing_Click(object sender, EventArgs e)
        {
            _train = true;
        }

        public void initialise_capture()
        {
            capWebcam = new Capture();
            capWebcam.QueryFrame();
            _train = false;

            Application.Idle += new EventHandler(processFrameAndUpdateGUI);
        }
        public void retrain()
        {

            Eigen_Recog = new Classifier_Train();
            if (Eigen_Recog.IsTrained)
            {
                txtXYRadius.Text = "Training Data loaded";
            }
            else
            {
                txtXYRadius.Text = "No training data found, please train program using Train menu option";
            }
        }

        void Clear_Faces_Found()
        {
            this.Faces_Found_Panel.Controls.Clear();
            faces_count = 0;
            faces_panel_Y = 0;
            faces_panel_X = 0;
        }
        void ADD_Face_Found(Image<Gray, Byte> img_found, string name_person, int match_value)
        {
            PictureBox PI = new PictureBox();
            PI.Location = new Point(faces_panel_X, faces_panel_Y);
            PI.Height = 80;
            PI.Width = 80;
            PI.SizeMode = PictureBoxSizeMode.StretchImage;
            PI.Image = img_found.ToBitmap();
            Label LB = new Label();
            LB.Text = string.Format("{0} {1}", name_person, match_value);
            LB.Location = new Point(faces_panel_X, faces_panel_Y + 80);
            //LB.Width = 80;
            LB.Height = 15;

            this.Faces_Found_Panel.Controls.Add(PI);
            this.Faces_Found_Panel.Controls.Add(LB);
            faces_count++;
            if (faces_count == 2)
            {
                faces_panel_X = 0;
                faces_panel_Y += 100;
                faces_count = 0;
            }
            else faces_panel_X += 85;

            if (Faces_Found_Panel.Controls.Count > 10)
            {
                Clear_Faces_Found();
            }

        }
    }   // end class

}   // end namespace
