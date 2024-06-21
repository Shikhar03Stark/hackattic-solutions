using System.Diagnostics;

namespace HelpMeUnpack.Playground;

[TestClass]
public class Playground
{

    private string access_token = "";

    public Playground()
    {
        access_token = Environment.GetEnvironmentVariable("ACCESS_TOKEN") ?? "";
    }

    [TestMethod]
    public async Task VerifySuccess()
    {
        await Program.Main([access_token]);
    }
}