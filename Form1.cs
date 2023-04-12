using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecorder
{
    public partial class Form1 : Form
    {
        bool folderSelected = false;
        string outputPath = "";
        string finalVidName = "FinalVideo.mp4";
        // Screen recorder object:
        ScreenRecorder screenRec = new ScreenRecorder(new Rectangle(), "");

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Create output path:
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select an Output Folder";


            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputPath = @folderBrowser.SelectedPath;
                folderSelected = true;

                //Finish screen recorder object:
                Rectangle bounds = Screen.FromControl(this).Bounds;
                screenRec = new ScreenRecorder(bounds, outputPath);
            }
            else
            {
                MessageBox.Show("Please select an output folder.", "Error");
            }
        }

        private void tmrRecord_Tick(object sender, EventArgs e)
        {
            screenRec.RecordVideo();
            screenRec.RecordAudio();
            lblTimer.Text = screenRec.getElapsed();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderSelected)
            {
                screenRec.setVideoName(finalVidName);
                tmrRecord.Start();
            }

            else
            {
                MessageBox.Show("You must select a output folder before recording", "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tmrRecord.Stop();
            screenRec.Stop();
            Application.Restart();
        }
    }
}
