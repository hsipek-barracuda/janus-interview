using Azure.Storage.Blobs.Models;
using Grpc.Core;

namespace Barracuda.Janus.Core.BlobProxy.Server;

public class BlobProxyService : Barracuda.BlobProxy.BlobProxyBase
{
    private readonly string _blobAccount;
    private readonly string _blobContainer;

    public BlobProxyService()
    {
        _blobAccount = Environment.GetEnvironmentVariable("BLOB_ACCOUNT") ?? throw new ArgumentNullException("Could not find the environment variable BLOB_ACCOUNT");
        _blobContainer = Environment.GetEnvironmentVariable("BLOB_CONTAINER") ?? throw new ArgumentNullException("Could not find the environment variable BLOB_CONTAINER");
    }

    public override async Task<ReadBlobResponse> ReadBlob(ReadBlobRequest request, ServerCallContext context)
    {

        Azure.Storage.Blobs.Specialized.BlockBlobClient blobClient = GetBlockBlobClient(request.Key);
        Azure.Response<BlobDownloadResult> blobDownloadResponse = await blobClient.DownloadContentAsync();

        return new ReadBlobResponse { Data = Google.Protobuf.ByteString.CopyFrom(blobDownloadResponse.Value.Content) };
    }

    private Azure.Storage.Blobs.Specialized.BlockBlobClient GetBlockBlobClient(string key)
    {
        return new Azure.Storage.Blobs.Specialized.BlockBlobClient(GetBlobUri(key));
    }

    private Uri GetBlobUri(string key)
    {
        return new Uri($@"https://{this._blobAccount}.blob.core.windows.net/{this._blobContainer}/{key}");
    }
}