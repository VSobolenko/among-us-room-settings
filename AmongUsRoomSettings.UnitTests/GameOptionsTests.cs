using AmongUsRoomSettings.AmongUs;
using Assert = NUnit.Framework.Assert;

namespace AmongUsRoomSettings.UnitTests;

[TestFixture]
internal class GameOptionsTests
{
    private const string LegacyDefaultCore = "CoQAAAEAAQ8IAAAAAAAAgD8AAIA/AADAPwAANEIBAQIBAAAAAwEPAAAAeAAAAAAPAQEAAAAJBQABZAMAAAAKHgIAAWQCAAAPBQQAAWQDAAA8CgADAAFkAgAAHg8IAAFkAgAACgEJAAFkAgAADx4KAAFkAwAADx4BDAABZAEAAAMSAAFkAQAADw==";
    private const string RealSampleCore = "C4wAAAEAAA8AAQAAAgAAgD8AAIA/AADAPwAAcEEBAQIBAAAAAQEPAAAAeAAAAAEAAQEAAAAKBQAAAAMAAAAKCAIAAAACAAAPBQQAAAADAAA8CgADAAAAAgAAHg8JAAAAAgAADx4KAAAAAwAADx4BCAAAAAIAAAoBDAAAAAEAAAMSAAAAAQAADxMAAAABAAAy";

    [SetUp]
    public void Setup() { }

    [Test]
    public void GetDebugStringValues_WithVariousValidInputs_DoesNotThrowException()
    {
        var currentDefault = new RoomSettings().GetBase64String(InternalOptionsCreator.GetDefault());

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => OptionsHelper.GetDebugStringValues(LegacyDefaultCore));
            Assert.DoesNotThrow(() => OptionsHelper.GetDebugStringValues(RealSampleCore));
            Assert.DoesNotThrow(() => OptionsHelper.GetDebugStringValues(currentDefault));
        });
    }
    
    [Test]
    public void PrintSettingsBasedOnStringValues()
    {
        var settings = RealSampleCore;
        var result = OptionsHelper.GetDebugStringValues(settings);

        Console.WriteLine(result);
        Assert.Pass();
    } 
    
    [Test]
    public void GenerateAndCopyRoomOptions()
    {
        var options = InternalOptionsCreator.GetCustom();
        var settings = new RoomSettings().GetBase64String(options);

        TextCopy.ClipboardService.SetText(settings); // Ctrl + C
        Console.WriteLine(settings);
        Assert.Pass();
    } 
    
    [Test]
    public void DefaultOptionsEqualsDefaultString()
    {
        var options = InternalOptionsCreator.GetDefault();
        var settings = new RoomSettings().GetBase64String(options);
        var result = OptionsHelper.GetDebugStringValues(settings);

        Console.WriteLine("LegacyDefault: \n" + OptionsHelper.GetDebugStringValues(LegacyDefaultCore));
        Console.WriteLine("Default: \n" + result);

        Assert.Multiple(() =>
        {
            Assert.That(result, Does.Contain("Version: 11"));
            Assert.That(result, Does.Contain("Length: 140"));
            Assert.That(result, Does.Contain(">>>>> Count Roles: 10"));
            Assert.That(result, Does.Contain("[JUDGE] TaskRequirementPercentage: 50"));
            Assert.That(settings, Does.StartWith("C4w"));
        });
    } 
}