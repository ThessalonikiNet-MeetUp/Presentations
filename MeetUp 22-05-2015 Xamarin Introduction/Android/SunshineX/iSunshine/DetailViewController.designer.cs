// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iSunshine
{
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel detailDescriptionLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ShareButton { get; set; }

		[Action ("ShareButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ShareButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (detailDescriptionLabel != null) {
				detailDescriptionLabel.Dispose ();
				detailDescriptionLabel = null;
			}
			if (ShareButton != null) {
				ShareButton.Dispose ();
				ShareButton = null;
			}
		}
	}
}
