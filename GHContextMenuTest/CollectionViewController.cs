using System;
using GHContextMenuBinding;
using UIKit;
using CoreGraphics;
using Foundation;

namespace GHContextMenuTest
{

	internal class CollectionMenuDataSource : GHContextOverlayViewDataSource {

		private readonly UICollectionView _collectionview;
		public CollectionMenuDataSource(UICollectionView collectionview) {
			_collectionview = collectionview;
		}


		#region implemented abstract members of GHContextOverlayViewDataSource
		public override bool ShouldShowMenuAtPoint (CoreGraphics.CGPoint point)
		{
			var indexPath = _collectionview.IndexPathForItemAtPoint(point);
			if (indexPath == null) {
				return false;
			}
			var cell = _collectionview.CellForItem(indexPath);
			return cell != null;
		}
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

	internal class CollectionMenuDelegate : GHContextOverlayViewDelegate {

		private readonly UICollectionView _collectionview;
		public CollectionMenuDelegate(UICollectionView collectionview) {
			_collectionview = collectionview;
		}

		#region implemented abstract members of GHContextOverlayViewDelegate

		public override void ForMenuAtPoint (nint selectedIndex, CGPoint point)
		{

			var indexPath = _collectionview.IndexPathForItemAtPoint(point);

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
			msg = msg + " At cell: " + indexPath.Row;
			var alertView = new UIAlertView(null, msg, null, @"OK", null);
			alertView.Show();		
		}

		#endregion


	}


	public class CollectionViewController : UICollectionViewController
	{
		UILongPressGestureRecognizer _longPressRecognizer;

		public CollectionViewController (UICollectionViewLayout layout) : base (layout) {
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			CollectionView.RegisterClassForCell (typeof (NumberCell), NumberCell.Key);

			var source = new CollectionMenuDataSource (CollectionView);
			var deleg = new CollectionMenuDelegate (CollectionView);
			var overlay = new GHContextMenuView ();
			overlay.DataSource = source;
			overlay.Delegate = deleg;

			_longPressRecognizer = new UILongPressGestureRecognizer (overlay.LongPressDetected);
			CollectionView.AddGestureRecognizer (_longPressRecognizer);
			CollectionView.BackgroundColor = UIColor.White;
		}

		public override nint NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return 20;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			var cell = (NumberCell)collectionView.DequeueReusableCell(NumberCell.Key, indexPath);
			cell.ContentView.BackgroundColor = UIColor.Gray;
			cell.Label.Text = string.Format("{0}", indexPath.Row +1);
			return cell;

		}

	}

	internal class NumberCell : UICollectionViewCell {

		public static readonly NSString Key = new NSString ("NumberCell");
		private readonly UILabel _label;
		public UILabel Label {
			get { return _label; }
		}
		private readonly UIView _root;

		[Export ("initWithFrame:")]
		public NumberCell (CGRect frame) : base (frame)
		{
			_root = new UIView ();
			_root.TranslatesAutoresizingMaskIntoConstraints = false;

			_label = new UILabel (Bounds);
			_label.TranslatesAutoresizingMaskIntoConstraints = false;

			_root.AddSubview (_label);

			ContentView.AddSubview (_root);
			UpdateConstraints ();
		}

		public override void UpdateConstraints ()
		{
			if (!NeedsUpdateConstraints ()) {
				return;
			}
			base.UpdateConstraints ();

			var metrics = new object[]{
				"root", _root,
				"label", _label,
				"margin", 1,
			};

			var rootH1 = NSLayoutConstraint.FromVisualFormat (
				"H:|-margin-[root]-margin-|",
				NSLayoutFormatOptions.DirectionLeftToRight,
				metrics
			);

			var rootV1 = NSLayoutConstraint.FromVisualFormat (
				"V:|-margin-[root]-margin-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing,
				metrics
			);

			ContentView.AddConstraints (rootH1);
			ContentView.AddConstraints (rootV1);

			var labelH1 = NSLayoutConstraint.FromVisualFormat (
				"H:|-margin-[label]-margin-|",
				NSLayoutFormatOptions.DirectionLeftToRight,
				metrics
			);

			var labelV1 = NSLayoutConstraint.FromVisualFormat (
				"V:|-margin-[label]-margin-|",
				NSLayoutFormatOptions.DirectionLeadingToTrailing,
				metrics
			);

			_root.AddConstraints (labelH1);
			_root.AddConstraints (labelV1);

		}

		public override void PrepareForReuse ()
		{
			base.PrepareForReuse ();
			_label.Text = null;

		}
	}
}


