using Foundation;
using UIKit;
using CoreGraphics;

namespace GHContextMenuTest
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Window = new UIWindow (UIScreen.MainScreen.Bounds);

			var layout = new UICollectionViewFlowLayout {
				ScrollDirection = UICollectionViewScrollDirection.Vertical,
				MinimumInteritemSpacing = 10,
				MinimumLineSpacing = 2,
				ItemSize = new CGSize(80, 120)
			};	

			var controller = new ViewController ();
			var collectioncontroller = new CollectionViewController (layout);
			var tabbar = new UITabBarController ();
			tabbar.ViewControllers = new UIViewController[] { controller, collectioncontroller };

			Window.RootViewController = tabbar;
			Window.MakeKeyAndVisible ();

			return true;
		}

	}
}


