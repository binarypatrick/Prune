using Microsoft.Extensions.FileProviders;
using Moq;

namespace BinaryPatrick.Prune.Unit.Fakes
{
    public class FileInfoMockFactory
    {
        public static IEnumerable<IFileInfo> GetFileInfoCollectionFake(int fileCount, DateTimeOffset timestamp, TimeSpan fileSpacing)
        {
            IEnumerable<IFileInfo> files = Enumerable.Range(0, fileCount)
                .Select(x => x * fileSpacing)
                .Select(timestamp.Subtract)
                .Select(CreateFileInfoMock)
                .ToList();

            return files;
        }

        public static IFileInfo CreateFileInfoMock(DateTimeOffset timestamp)
        {
            Mock<IFileInfo> fileInfoMock = new Mock<IFileInfo>();
            fileInfoMock.SetupGet(x => x.Name).Returns(timestamp.ToString("s"));
            fileInfoMock.SetupGet(x => x.LastModified).Returns(timestamp);
            return fileInfoMock.Object;
        }
    }
}
