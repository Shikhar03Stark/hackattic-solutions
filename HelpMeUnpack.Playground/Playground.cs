using System.Diagnostics;

namespace HelpMeUnpack.Playground;

[TestClass]
public class Playground
{
    [TestMethod]
    public async Task VerifySuccess()
    {
        await Program.Main(["83e76e14c65f7def"]);
    }
}