﻿//
// UniqueIdRangeTests.cs
//
// Author: Jeffrey Stedfast <jeff@xamarin.com>
//
// Copyright (c) 2013-2016 Xamarin Inc. (www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Collections.Generic;

using NUnit.Framework;

using MailKit;

namespace UnitTests {
	public class UniqueIdRangeTests
	{
		[Test]
		public void TestEnumerator ()
		{
			const string example = "20:1";
			UniqueIdRange uids;

			Assert.IsTrue (UniqueIdRange.TryParse (example, 20160117, out uids), "Failed to parse uids.");
			Assert.AreEqual (20160117, uids.Validity, "Validity");
			Assert.AreEqual (20, uids.Start.Id, "Start");
			Assert.AreEqual (1, uids.End.Id, "End");
			Assert.AreEqual (1, uids.Min.Id, "Min");
			Assert.AreEqual (20, uids.Max.Id, "Max");
			Assert.AreEqual (example, uids.ToString (), "ToString");
			Assert.AreEqual (20, uids.Count);

			for (int i = 0; i < uids.Count; i++)
				Assert.AreEqual (20 - i, uids[i].Id);

			var list = new List<UniqueId> ();
			foreach (var uid in uids) {
				Assert.AreEqual (20160117, uid.Validity);
				list.Add (uid);
			}

			for (int i = 0; i < list.Count; i++)
				Assert.AreEqual (20 - i, list[i].Id);
		}

		[Test]
		public void TestCopyTo ()
		{
			const string example = "1:20";
			var copy = new UniqueId[20];
			UniqueIdRange uids;

			Assert.IsTrue (UniqueIdRange.TryParse (example, 20160117, out uids), "Failed to parse uids.");
			Assert.AreEqual (20160117, uids.Validity, "Validity");
			Assert.AreEqual (1, uids.Start.Id, "Start");
			Assert.AreEqual (20, uids.End.Id, "End");
			Assert.AreEqual (1, uids.Min.Id, "Min");
			Assert.AreEqual (20, uids.Max.Id, "Max");
			Assert.AreEqual (example, uids.ToString (), "ToString");
			Assert.AreEqual (20, uids.Count);

			for (int i = 0; i < uids.Count; i++) {
				Assert.AreEqual (20160117, uids[i].Validity);
				Assert.AreEqual (i + 1, uids[i].Id);
			}

			uids.CopyTo (copy, 0);

			for (int i = 0; i < copy.Length; i++) {
				Assert.AreEqual (20160117, copy[i].Validity);
				Assert.AreEqual (i + 1, copy[i].Id);
			}
		}
	}
}
