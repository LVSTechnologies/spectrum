using Foundation;
using System;
using UIKit;

namespace SignInUser
{
    public partial class SuccessViewController : UIViewController
    {
        public SuccessViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            btnLoginNow.TouchUpInside += BtnLoginNow_TouchUpInside;
            imageSuccess.Image = UIImage.FromBundle("spectrumlogo.png");

        }

        private void BtnLoginNow_TouchUpInside(object sender, EventArgs e)
        {
            DismissModalViewController(true);
        }
    }
}