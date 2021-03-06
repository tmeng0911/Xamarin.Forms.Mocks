﻿using System;
using NUnit.Framework;

namespace Xamarin.Forms.Mocks.Tests
{
    [TestFixture]
    public class DeviceTests
    {
        [Test]
        public void BeginInvokeOnMainThreadIsSynchronous()
        {
            MockForms.Init();

            bool success = false;
            Device.BeginInvokeOnMainThread(() => success = true);
            Assert.IsTrue(success);
        }

        [Test]
        public void RuntimePlatformDefault()
        {
            MockForms.Init();

            Assert.AreEqual("Test", Device.RuntimePlatform);
        }

        [Test]
        public void RuntimePlatformiOS()
        {
            MockForms.Init(Device.iOS);

            Assert.AreEqual(Device.iOS, Device.RuntimePlatform);
            Assert.AreEqual(TargetPlatform.iOS, Device.OS);
        }

        [Test]
        public void RuntimePlatformAndroid()
        {
            MockForms.Init(Device.Android);

            Assert.AreEqual(Device.Android, Device.RuntimePlatform);
            Assert.AreEqual(TargetPlatform.Android, Device.OS);
        }

        [Test]
        public void RuntimePlatformWindows()
        {
            MockForms.Init(Device.Windows);

            Assert.AreEqual(Device.Windows, Device.RuntimePlatform);
            Assert.AreEqual(TargetPlatform.Windows, Device.OS);
        }

        [Test]
        public void RuntimePlatformWinPhone()
        {
            MockForms.Init(Device.WinPhone);

            Assert.AreEqual(Device.WinPhone, Device.RuntimePlatform);
            Assert.AreEqual(TargetPlatform.WinPhone, Device.OS);
        }

        [Test]
        public void OpenUri()
        {
            MockForms.Init();

            Uri actual = null;
            int callCount = 0;

            MockForms.OpenUriAction = u =>
            {
                actual = u;
                callCount++;
            };

            var expectedUri = new Uri("https://www.google.com");

            Device.OpenUri(expectedUri);

            Assert.AreEqual(expectedUri, actual);
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void InitClearsOpenUri()
        {
            MockForms.OpenUriAction = delegate { };
            MockForms.Init();
            Assert.IsNull(MockForms.OpenUriAction);
        }

        [Test]
        public void OpenUriDoesNotThrowOnNull()
        {
            MockForms.Init();
            MockForms.OpenUriAction = null;
            Device.OpenUri(new Uri("https://www.google.com"));
        }
    }
}
