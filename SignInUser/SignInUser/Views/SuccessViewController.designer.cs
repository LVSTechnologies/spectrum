// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SignInUser
{
    [Register ("SuccessViewController")]
    partial class SuccessViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLoginNow { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSuccess { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnLoginNow != null) {
                btnLoginNow.Dispose ();
                btnLoginNow = null;
            }

            if (imageSuccess != null) {
                imageSuccess.Dispose ();
                imageSuccess = null;
            }
        }
    }
}