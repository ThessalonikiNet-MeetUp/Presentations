using System;
using Twitter;
using UIKit;

namespace iSunshine
{
	public partial class DetailViewController : UIViewController
	{
		public object DetailItem { get; set; }

		public DetailViewController (IntPtr handle) : base (handle)
		{
		}

		public void SetDetailItem (object newDetailItem)
		{
			if (DetailItem != newDetailItem) {
				DetailItem = newDetailItem;
				
				// Update the view
				ConfigureView ();
			}
		}

		void ConfigureView ()
		{
			// Update the user interface for the detail item
			if (IsViewLoaded && DetailItem != null)
				detailDescriptionLabel.Text = DetailItem.ToString ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			ConfigureView ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void ShareButton_TouchUpInside (UIButton sender)
		{
			var twit = new Twitter.TWTweetComposeViewController();
			twit.SetInitialText (string.Format("The wheather here will be {0}",DetailItem.ToString ()));

			twit.SetCompletionHandler((TWTweetComposeViewControllerResult r) =>{
				DismissModalViewController(true); // hides the tweet
				if (r == TWTweetComposeViewControllerResult.Cancelled) {
					new UIAlertView("Canceled", "You just canceled twitt", null, "ok").Show();
					// user cancelled the tweet
				} else {
					// user sent the tweet (they may have edited it first)
				}

			});

			PresentModalViewController(twit, true);
		}
	}
}


