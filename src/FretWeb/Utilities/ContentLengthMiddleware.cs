using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace FretWeb.Utilities;

public static class ContentLengthMiddleware
{
    private const int MaxSize = 32 * 1024;
    private static readonly ConcurrentDictionary<(string, string), long> KnownLengths = new();

    private static readonly ObservableUpDownCounter<int> Counter =
        Observability.Meter.CreateObservableUpDownCounter("ContentLengthCacheSize", () => KnownLengths.Count);
    
    public static async Task SetContentLength(HttpContext context, Func<Task> next)
    {
        if (!NeedContentLength(context))
        {
            await next();
            return;
        }
        
        var wrapper = new BodyWrapper(context.Response.Body);
        context.Response.Body = wrapper;
        try
        {
            await next();
            var key = (context.Request.Path.Value ?? string.Empty, context.Request.QueryString.Value ?? string.Empty);
            KnownLengths[key] = wrapper.Written;
        }
        finally
        {
            context.Response.Body = wrapper.OriginalStream;
        }
    }

    private static bool NeedContentLength(HttpContext context)
    {
        if (context.Request.Method != "GET") return false;
        
        // Not doing compound pages
        if (context.Request.Path.Value is not null && context.Request.Path.Value.Contains('+')) return false;

        var key = (context.Request.Path.Value ?? string.Empty, context.Request.QueryString.Value ?? string.Empty);
        if (KnownLengths.TryGetValue(key, out long length))
        {
            context.Response.ContentLength = length;
            return false;
        }

        return KnownLengths.Count < MaxSize;
    }

    private sealed class BodyWrapper : Stream
    {
        public readonly Stream OriginalStream;
        public long Written;

        public BodyWrapper(Stream originalStream)
        {
            OriginalStream = originalStream;
        }

        public override void Flush()
        {
            OriginalStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return OriginalStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return OriginalStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            OriginalStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            OriginalStream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Written += count;
            return OriginalStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            Written += buffer.Length;
            return OriginalStream.WriteAsync(buffer, cancellationToken);
        }

        public override void Write(ReadOnlySpan<byte> buffer)
        {
            Written += buffer.Length;
            OriginalStream.Write(buffer);
        }

        public override bool CanRead => OriginalStream.CanRead;

        public override bool CanSeek => OriginalStream.CanSeek;

        public override bool CanWrite => OriginalStream.CanWrite;

        public override long Length => OriginalStream.Length;

        public override long Position
        {
            get => OriginalStream.Position;
            set => OriginalStream.Position = value;
        }
    }
}