using Mono.Unix;
using System.IO;
using Xunit;

namespace cliscd
{
    public class Tests
    {
        [Fact]
        public void UserExists()
        {
            var user = new UnixUserInfo("digitalhigh");
            Assert.NotEqual(0, user.UserId);
            Assert.NotEqual(0, user.GroupId);
            Assert.Equal("digitalhigh", user.GroupName);
            Assert.Equal("/home/digitalhigh", user.HomeDirectory);
        }

        [Fact]
        public void MachineFileExists()
        {
            Assert.True(File.Exists("/etc/digitalhigh/cliscd.machine.config"));
        }

        [Fact]
        public void UserFileExists()
        {
            Assert.True(File.Exists("/home/digitalhigh/.cliscd/cliscd.user.config"));
        }

        [Theory]
        [InlineData("/var/log/digitalhigh")]
        [InlineData("/var/run/digitalhigh")]
        [InlineData("/var/lib/digitalhigh")]
        [InlineData("/etc/digitalhigh")]
        public void LinuxFolderExists(string path)
        {
            Assert.True(Directory.Exists(path));

            var directoryInfo = new UnixDirectoryInfo(path);
            Assert.Equal("digitalhigh", directoryInfo.OwnerGroup.GroupName);
            Assert.Equal("digitalhigh", directoryInfo.OwnerUser.UserName);
        }
    }
}
