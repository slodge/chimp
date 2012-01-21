using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FaceLight.Mobile.SimpleBitmap;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using NinetyNinePercent.Helpers;

namespace NinetyNinePercent
{
    public partial class MainPage : PhoneApplicationPage
    {
        private WriteableBitmap _chimpWriteableBitmap;
        private WriteableBitmap _jediWriteableBitmap;
        global::FaceLight.Mobile.FaceLight faceLight = new global::FaceLight.Mobile.FaceLight(new global::FaceLight.Mobile.SimpleBitmap.DefaultSimpleBitmapFactory());

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            LoadChimp();
        }

        private void LoadChimp()
        {
            var image = new BitmapImage(new Uri("/Images/Chimp1.png", UriKind.Relative));
            image.ImageOpened += (sender, args) => { _chimpWriteableBitmap = new WriteableBitmap(image); };
            SelectedImage.Source = image;
        }

        private void DisplayResult(BitmapImage bitmapImage, FaceRestAPI.FaceAPI result)
        {
            DisplayChimpResult(bitmapImage, result);
        }

        private void DisplayChimpResult(BitmapImage bitmapImage, FaceRestAPI.FaceAPI result)
        {
            var writeable = new WriteableBitmap(bitmapImage);

            foreach (var tag in result.photos[0].tags)
            {
                // Uses the segment's center and half width, height
                var c = tag.center;
                Debug.WriteLine("Center ({0} ,{1}), size ({2}, {3})", c.x, c.y, tag.width,
                                tag.height);
                var destRect = new Rect(bitmapImage.PixelWidth * 0.01 * (tag.center.x - tag.width / 2),
                                        bitmapImage.PixelHeight * 0.01 * (tag.center.y - tag.height / 2),
                                        bitmapImage.PixelWidth * 0.01 * tag.width,
                                        bitmapImage.PixelHeight * 0.01 * tag.height);
                var sourceRect = new Rect(0, 0, _chimpWriteableBitmap.PixelWidth, _chimpWriteableBitmap.PixelHeight);
                writeable.Blit(destRect, _chimpWriteableBitmap, sourceRect);
            }

            writeable.Invalidate();
            SelectedImage.Source = writeable;

            writeable.SaveToFile("SaveMe.jpg", (stream) =>
            {
                var library = new MediaLibrary();
                Picture pic = library.SavePicture("SavedPicture.jpg", stream);
            });
        }

        private void ApplicationBarIconButton_Camera_Click(object sender, EventArgs e)
        {
            var task = new CameraCaptureTask();
            DoPhotoTask(task);
        }

        private void ApplicationBarIconButton_Picture_Click(object sender, EventArgs e)
        {
            var task = new PhotoChooserTask()
            {
                ShowCamera = true
            };
            DoPhotoTask(task);
        }

        private void ApplicationBarIconButton_About_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                // prevent against double taps
            }
        }

        private void DoPhotoTask(ChooserBase<PhotoResult> task)
        {
            try
            {
                task.Completed += (t, args) =>
                {
                    if (args.ChosenPhoto != null)
                    {
                        ShowDisplay(DisplayState.Processing);
                        var bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(args.ChosenPhoto);
                        var writeableBitmap = new WriteableBitmap(bitmapImage);
                        ProcessImage(writeableBitmap);
                    }
                    else
                    {
                        // hope the user doesn't mind being told nothing
                    }
                };
                task.Show();
            }
            catch (Exception exception)
            {
                // hope the user doesn't mind being told nothing
            }
        }

        private void ProcessImage(WriteableBitmap writeable)
        {
            var verySimpleBitmap = new VerySimpleBitmap(writeable.PixelWidth, writeable.PixelHeight);
            writeable.Pixels.CopyTo(verySimpleBitmap.Pixels, 0);
            var detectedSegments = faceLight.Process(verySimpleBitmap);

            var percentage = 0;
            switch (detectedSegments.Count())
            {
                case 0:
                    percentage = 0;
                    break;
                case 1:
                    percentage = 90;
                    break;
                default:
                    percentage = 99;
                    break;
            }

            Debug.WriteLine("{0} segments detected", detectedSegments.Count());
            foreach (var segment in detectedSegments)
            {
                if (segment.Width > 0 && segment.Height > 0)
                {
                    // Uses the segment's center and half width, height
                    var c = segment.Center;
                    Debug.WriteLine("Center ({0} ,{1}), size ({2}, {3})", c.X, c.Y, segment.Width,
                                    segment.Height);
                    var destRect = new Rect(segment.Center.X - segment.Width / 2,
                                              segment.Center.Y - segment.Height / 2,
                                              segment.Width,
                                              segment.Height);
                    var sourceRect = new Rect(0, 0, _chimpWriteableBitmap.PixelWidth, _chimpWriteableBitmap.PixelHeight);
                    writeable.Blit(destRect, _chimpWriteableBitmap, sourceRect);
                }
            }

            writeable.Invalidate();
            SelectedImage.Source = writeable;
            writeable.SaveToFile(AppConstants.IsoFileName, (stream) => { /* do nothing */ });
            ShowDisplay(DisplayState.Finished);
            NavigateToPhotoPage(percentage);
        }

        private void NavigateToPhotoPage(int percentage)
        {
            Dispatcher.BeginInvoke(() =>
                                       {
                                           try
                                           {
                                               NavigationService.Navigate(new Uri("/PhotoPage.xaml?percentage=" + percentage, UriKind.Relative));
                                           }
                                           catch (Exception)
                                           {
                                               // prevent against double navigations
                                           }
                                       });
        }


        private enum DisplayState
        {
            Intro,
            Processing,
            Finished
        }

        private void ShowDisplay(DisplayState state)
        {
            switch (state)
            {
                case DisplayState.Intro:
                    IntroPanel.Visibility = Visibility.Visible;
                    ProcessingPanel.Visibility = Visibility.Collapsed;
                    FinishedPanel.Visibility = Visibility.Collapsed;                    
                    EnableApplicationBar(true);
                    break;
                case DisplayState.Processing:
                    ProcessingPanel.Visibility = Visibility.Visible;
                    IntroPanel.Visibility = Visibility.Collapsed;
                    FinishedPanel.Visibility = Visibility.Collapsed;                    
                    EnableApplicationBar(false);
                    break;
                case DisplayState.Finished:
                    ProcessingPanel.Visibility = Visibility.Collapsed;
                    IntroPanel.Visibility = Visibility.Collapsed;
                    FinishedPanel.Visibility = Visibility.Visible;                    
                    EnableApplicationBar(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }

        private void EnableApplicationBar(bool isEnabled)
        {
            foreach (ApplicationBarIconButton button in ApplicationBar.Buttons)
            {
                button.IsEnabled = isEnabled;
            }
        }
    }
}




/*
private void DisplayJediResult(BitmapImage bitmapImage, FaceRestAPI.FaceAPI result)
{
    var writeable = _jediWriteableBitmap;

    foreach (var tag in result.photos[0].tags)
    {
        // Uses the segment's center and half width, height
        var c = tag.center;
        Debug.WriteLine("Center ({0} ,{1}), size ({2}, {3})", c.x, c.y, tag.width,
                        tag.height);
        var sourceRect = new Rect(bitmapImage.PixelWidth * 0.01 * (tag.center.x - tag.width / 2),
                                bitmapImage.PixelHeight * 0.01 * (tag.center.y - tag.height / 2),
                                bitmapImage.PixelWidth * 0.01 * tag.width,
                                bitmapImage.PixelHeight * 0.01 * tag.height);
        var destRect = new Rect(288, 103, 63, 86);
        writeable.Blit(destRect, new WriteableBitmap(bitmapImage), sourceRect);
    }

    writeable.Invalidate();
    JediImage.Source = writeable;

    writeable.SaveToFile("SaveMe.jpg", (stream) =>
                                            {
                                                MediaLibrary library = new MediaLibrary();
                                                Picture pic = library.SavePicture("SavedPicture.jpg", stream);
                                            });
}
*/


/*
private void LoadJedi()
{
    var image = new BitmapImage(new Uri("/Images/Jedi1.jpg", UriKind.Relative));
    image.ImageOpened += (sender, args) => { _jediWriteableBitmap = new WriteableBitmap(image); };
    JediImage.Source = image;
}
*/

/*
private void ProcessImage2(BitmapImage bitmapImage)
{
    string tempFileName = "temp.jpg";
    bitmapImage.SaveToFile(tempFileName);
    string apikey = "96fbe5ef96fb2ea460999459b92bb951";
    string secretkey = "d6505a7ebe7c7f51e72d4e75c46082f8";
    var api = new FaceRestAPI(apikey, secretkey, "", false, "json", "", "");
    api.faces_detect(null, tempFileName, null, string.Empty,
                     (result) =>
                         {
                             Debug.WriteLine("Hello " + result);
                             Dispatcher.BeginInvoke(() =>DisplayResult(bitmapImage, result));
                         },
                         (error) => Debug.WriteLine("Bummer " + error));
}
 */
