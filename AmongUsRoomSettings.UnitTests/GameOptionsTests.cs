using AmongUsRoomSettings.AmongUs;
using Assert = NUnit.Framework.Assert;

namespace AmongUsRoomSettings.UnitTests;

[TestFixture]
internal class GameOptionsTests
{
    private const string DefaultCore = "CoQAAAEAAQ8IAAAAAAAAgD8AAIA/AADAPwAANEIBAQIBAAAAAwEPAAAAeAAAAAAPAQEAAAAJBQABZAMAAAAKHgIAAWQCAAAPBQQAAWQDAAA8CgADAAFkAgAAHg8IAAFkAgAACgEJAAFkAgAADx4KAAFkAwAADx4BDAABZAEAAAMSAAFkAQAADw==";

    [SetUp]
    public void Setup() { }

    [Test]
    [TestCase(DefaultCore)]
    public void GetDebugStringValues_WithVariousValidInputs_DoesNotThrowException(string input)
    {
        // Act & Assert
        Assert.DoesNotThrow(() => OptionsHelper.GetDebugStringValues(input));
    }
    
    [Test]
    public void PrintSettingsBasedOnStringValues()
    {
        var settings = DefaultCore;
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
        // Arrange
        var options = InternalOptionsCreator.GetDefault();
        
        var settings = new RoomSettings().GetBase64String(options);

        Console.WriteLine("DefaultCore: \n" + OptionsHelper.GetDebugStringValues(DefaultCore));
        Console.WriteLine("Default: \n" + OptionsHelper.GetDebugStringValues(settings));
        
        // Assert
        Assert.That(settings, Is.EqualTo(DefaultCore));
    } 
}