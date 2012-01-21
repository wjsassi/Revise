﻿#region License

/**
 * Copyright (C) 2011 Jack Wakefield
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using System.IO;
using NUnit.Framework;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="Revise.Files.HLP"/> class.
    /// </summary>
    [TestFixture]
    public class HLPTests {
        private const string TEST_FILE = "Files/HELP.HLP";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int PAGE_COUNT = 69;

            Stream stream = File.OpenRead(TEST_FILE);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            HLP hlp = new HLP();
            hlp.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.AreEqual(streamPosition, fileSize, "Not all of the file was read");
            Assert.AreEqual(hlp.Pages.Count, PAGE_COUNT, "Incorrect page count");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            HLP hlp = new HLP();
            hlp.Load(TEST_FILE);

            MemoryStream savedStream = new MemoryStream();
            hlp.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            HLP savedHLP = new HLP();
            savedHLP.Load(savedStream);

            savedStream.Close();

            Assert.AreEqual(hlp.Pages.Count, savedHLP.Pages.Count, "Page counts do not match");

            TestNodes(hlp.RootNode, savedHLP.RootNode);

            for (int i = 0; i < hlp.Pages.Count; i++) {
                Assert.AreEqual(hlp.Pages[i].Title, savedHLP.Pages[i].Title, "Page title values do not match");
                Assert.AreEqual(hlp.Pages[i].Content, savedHLP.Pages[i].Content, "Page content values do not match");
            }
        }

        /// <summary>
        /// Tests the two nodes by comparing the names and the children.
        /// </summary>
        /// <param name="nodeA">The first node.</param>
        /// <param name="nodeB">The second node.</param>
        private void TestNodes(HelpNode nodeA, HelpNode nodeB) {
            Assert.AreEqual(nodeA.Name, nodeB.Name, "Node name values do not match");
            Assert.AreEqual(nodeA.Children.Count, nodeB.Children.Count, "Child counts do not match");

            for (int i = 0; i < nodeA.Children.Count; i++) {
                TestNodes(nodeA.Children[i], nodeA.Children[i]);
            }
        }
    }
}