using GitClient.Interfaces;
using GitClient.Models;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using System.Threading.Tasks;

namespace Scarecrow.Test.Infrastructure.GitClient {
    public class TestClientAdapter : IGitClient {
        private readonly IFileSystem _fs;
        public TestClientAdapter(IFileSystem fs) {
            _fs = fs;
        }

        public Task<List<GitRepository>> GetRepositories(string organization) {
            return Task.FromResult(new List<GitRepository>() {
                {new GitRepository("test", "test") }
            });
        }

        public void Clone(string repoUrl, string path) {
            var executionPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(executionPath).FullName).FullName).FullName;
            try {
                CopyFilesRecursively(Path.Combine(basePath, "Infrastructure", "GitClient", "TestRepos", "Repo1"), path);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void CopyFilesRecursively(string sourcePath, string targetPath) {
            //Now Create all of the directories
            foreach (string dirPath in _fs.Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories)) {
                _fs.Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in _fs.Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories)) {
                _fs.File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
    }
}
