using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace TestSuite.TestManagement.FileSystemRepository.Test
{
    [TestClass]
    public class FileSystemRepository_TestLockOnFileName
    {
        List<string> log = new List<string>();

        [TestMethod]
        [Ignore("Only to prove that locking on filename is possible and wouldn't affect other file names.")]
        public void Test()
        {
            // Arrange

            // Act
            Task.WaitAll(
                Task.Run(() => LogTask("Task1", "LockA")),
                Task.Run(() => LogTask("Task2", "LockA")),
                Task.Run(() => LogTask("Task3", "LockB")),
                Task.Run(() => LogTask("Task4", "LockC"))
            );

            // Assert
            log.Count.ShouldBeGreaterThan(0);
        }

        private void LogTask(string taskName, string lockName)
        {
            log.Add($"{DateTime.Now.ToLocalTime()} {taskName}: entering lock {lockName}");
            lock(lockName)
            {
                log.Add($"{DateTime.Now.ToLocalTime()} {taskName}: lock {lockName} entered");
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }
    }
}
