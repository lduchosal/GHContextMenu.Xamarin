using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;


namespace GHContextMenuBinding
{

	// @interface GHContextMenuView : UIView
	[BaseType (typeof(UIView))]
	interface GHContextMenuView
	{
		// @property (assign, nonatomic) id<GHContextOverlayViewDataSource> dataSource;
		[Export ("dataSource", ArgumentSemantic.Assign)]
		GHContextOverlayViewDataSource DataSource { get; set; }

		[Wrap ("WeakDelegate")]
		GHContextOverlayViewDelegate Delegate { get; set; }

		// @property (assign, nonatomic) id<GHContextOverlayViewDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) GHContextMenuActionType menuActionType;
		[Export ("menuActionType", ArgumentSemantic.Assign)]
		GHContextMenuActionType MenuActionType { get; set; }

		// -(void)longPressDetected:(UIGestureRecognizer *)gestureRecognizer;
		[Export ("longPressDetected:")]
		void LongPressDetected (UIGestureRecognizer gestureRecognizer);
	}

	// @protocol GHContextOverlayViewDataSource <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GHContextOverlayViewDataSource
	{
		// @required -(NSInteger)numberOfMenuItems;
		[Abstract]
		[Export ("numberOfMenuItems")]
		nint NumberOfMenuItems { get; }

		// @required -(UIImage *)imageForItemAtIndex:(NSInteger)index;
		[Abstract]
		[Export ("imageForItemAtIndex:")]
		UIImage ImageForItemAtIndex (nint index);

		// @optional -(BOOL)shouldShowMenuAtPoint:(CGPoint)point;
		[Export ("shouldShowMenuAtPoint:")]
		bool ShouldShowMenuAtPoint (CGPoint point);
	}

	// @protocol GHContextOverlayViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GHContextOverlayViewDelegate
	{
		// @required -(void)didSelectItemAtIndex:(NSInteger)selectedIndex forMenuAtPoint:(CGPoint)point;
		[Abstract]
		[Export ("didSelectItemAtIndex:forMenuAtPoint:")]
		void ForMenuAtPoint (nint selectedIndex, CGPoint point);
	}

}
