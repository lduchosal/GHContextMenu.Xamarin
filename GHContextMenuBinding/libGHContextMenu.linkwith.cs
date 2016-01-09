using ObjCRuntime;

[assembly: LinkWith ("libGHContextMenu.a", 
	LinkTarget.ArmV7 
	| LinkTarget.ArmV7s 
	| LinkTarget.Arm64 
	| LinkTarget.Simulator64 
	| LinkTarget.Simulator, 
	Frameworks = "CoreData, SystemConfiguration, MobileCoreServices, Foundation, CoreLocation, Security, QuartzCore", 
	SmartLink = true, ForceLoad = true)]
