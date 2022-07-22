namespace Barracuda.Janus.Core.BlobProxy.Server.Tests;

[TestFixture]
public class BlobProxyServiceTests
{
    [Test]
    public async Task BlobProxyServiceReadBlob_GivenBlobExists_ReturnsBytes()
    {
        var service = new BlobProxyService();
        var blob = await service.ReadBlob(new Barracuda.ReadBlobRequest() { Key = "dog.jpg", }, null!);

        Assert.IsNotNull(blob);
        Assert.IsNotNull(blob.Data);
        Assert.That(blob.Data.Length, Is.EqualTo(476689));
    }
}