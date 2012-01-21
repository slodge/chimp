using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using NinetyNinePercent.Helpers;

namespace NinetyNinePercent
{
    public partial class PhotoPage : PhoneApplicationPage
    {
        private string _fileToSaveName = "chimp";

        public PhotoPage()
        {
            InitializeComponent();
        }

        private static string HackyFindPercentageInUri(Uri uri)
        {
            var query = uri.OriginalString;
            var amountString = "";
            if (string.IsNullOrEmpty(query))
                return amountString;

            for (var i = query.Length - 1; i >= 0; i--)
            {
                var current = query[i];
                if (!char.IsNumber(current))
                    break;

                amountString = current + amountString;
            }
            return amountString;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var percentage = HackyFindPercentageInUri(e.Uri);
            if (string.IsNullOrEmpty(percentage))
                percentage = "99";
            PageTitle.Text =  percentage + "% Chimp";
            _fileToSaveName = percentage + "PercentChimp.jpg";
            SelectedImage.Source = FileIO.LoadFromFile(AppConstants.IsoFileName);
        }

        private void AppBarSaveButton_Click(object sender, EventArgs e)
        {
            var library = new MediaLibrary();
            Picture picture;
            FileIO.LoadFromFile(AppConstants.IsoFileName, (stream) => picture = library.SavePicture(_fileToSaveName, stream));

            var toast = new ToastPrompt()
                            {
                                Message = "Saved",
                                MillisecondsUntilHidden = 1500,
                                Title = "98% Chimp"
                            };
            toast.Show();
            
            /*
             * 
             * would love to open the picture here!
            var launcher = new MediaPlayerLauncher()
            {
                Media = picture.
            }
            ;
             */
        }

        
    }
}