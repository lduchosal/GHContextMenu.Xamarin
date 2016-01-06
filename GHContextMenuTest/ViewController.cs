using System;

using UIKit;
using GHContextMenuBinding;
using CoreGraphics;
using ObjCRuntime;
using Foundation;

namespace GHContextMenuTest
{
	internal class MenuDataSource : GHContextOverlayViewDataSource {
		#region implemented abstract members of GHContextOverlayViewDataSource

		public override UIImage ImageForItemAtIndex (nint index)
		{

			string imageName = null;
			switch (index) {
			case 0:
				imageName = @"facebook-white";
				break;
			case 1:
				imageName = @"twitter-white";
				break;
			case 2:
				imageName = @"google-plus-white";
				break;
			case 3:
				imageName = @"linkedin-white";
				break;
			case 4:
				imageName = @"pinterest-white";
				break;

			default:
				break;
			}
			return UIImage.FromFile (imageName);
		}

		public override nint NumberOfMenuItems {
			get {
				return 3;
			}
		}

		#endregion


	}

	internal class MenuDelegate : GHContextOverlayViewDelegate {
		#region implemented abstract members of GHContextOverlayViewDelegate

		public override void ForMenuAtPoint (nint selectedIndex, CGPoint point)
		{

			string msg = string.Empty;
			switch (selectedIndex) {
			case 0:
				msg = @"Facebook Selected";
				break;
			case 1:
				msg = @"Twitter Selected";
				break;
			case 2:
				msg = @"Google Plus Selected";
				break;
			case 3:
				msg = @"Linkedin Selected";
				break;
			case 4:
				msg = @"Pinterest Selected";
				break;

			default:
				break;
			}

			var alertView = new UIAlertView(null, msg, null, @"OK", null);
			alertView.Show();		}

		#endregion


	}

	public partial class ViewController : UIViewController
	{
		UILongPressGestureRecognizer _longPressRecognizer;
		readonly UIImageView _imageView;

		public ViewController() {
			_imageView = new UIImageView ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var source = new MenuDataSource ();
			var deleg = new MenuDelegate ();
			var overlay = new GHContextMenuView ();
			overlay.DataSource = source;
			overlay.Delegate = deleg;

			_longPressRecognizer = new UILongPressGestureRecognizer (overlay.LongPressDetected);
			_imageView.UserInteractionEnabled = true;
			View.AddGestureRecognizer (_longPressRecognizer);
			View.Add (_imageView);
			View.BackgroundColor = UIColor.White;
		}





	}
}

