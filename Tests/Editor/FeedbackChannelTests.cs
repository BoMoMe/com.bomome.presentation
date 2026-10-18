using System;
using System.Reflection;
using Bomome.UI.Feedbacks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Bomome.Presentation.Tests
{
    public sealed class FeedbackChannelTests
    {
        private FeedbackChannel channel;
        private GameObject root;

        [SetUp]
        public void SetUp()
        {
            channel = ScriptableObject.CreateInstance<FeedbackChannel>();
            root = new GameObject("sender", typeof(RectTransform));
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(root);
            UnityEngine.Object.DestroyImmediate(channel);
        }

        [Test]
        public void DispatchPreservesChannelAndSender()
        {
            int calls = 0;
            channel.Subscribe((c, sender) => { Assert.AreSame(channel, c); Assert.AreSame(root, sender); calls++; });
            channel.Raise(root);
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void UnsubscribedListenerIsNotInvoked()
        {
            int calls = 0;
            Action<FeedbackChannel, GameObject> listener = (c, sender) => calls++;
            channel.Subscribe(listener);
            channel.Unsubscribe(listener);
            channel.Raise(root);
            Assert.AreEqual(0, calls);
        }

        [Test]
        public void RecursiveTriggerIsSuppressedAndNextDispatchWorks()
        {
            int calls = 0;
            channel.Subscribe((c, sender) => { calls++; c.Raise(sender); });
            channel.Raise(root);
            channel.Raise(root);
            Assert.AreEqual(2, calls);
        }

        [Test]
        public void ListenerExceptionDoesNotLeaveChannelDispatching()
        {
            Action<FeedbackChannel, GameObject> failing = (c, sender) => throw new InvalidOperationException();
            channel.Subscribe(failing);
            Assert.Throws<InvalidOperationException>(() => channel.Raise(root));
            channel.Unsubscribe(failing);
            int calls = 0;
            channel.Subscribe((c, sender) => calls++);
            channel.Raise(root);
            Assert.AreEqual(1, calls);
        }

        [Test]
        public void ChannelsRemainIndependent()
        {
            var other = ScriptableObject.CreateInstance<FeedbackChannel>();
            try
            {
                int calls = 0;
                other.Subscribe((c, sender) => calls++);
                channel.Raise(root);
                Assert.AreEqual(0, calls);
            }
            finally { UnityEngine.Object.DestroyImmediate(other); }
        }

        [Test]
        public void ButtonReenableDoesNotDuplicateSubscriptions()
        {
            var button = AddTransmitter();
            int calls = 0;
            channel.Subscribe((c, sender) => calls++);
            button.onClick.Invoke();
            InvokeLifecycle(root.GetComponent<FeedbackButtonTransmitter>(), "OnDisable");
            root.SetActive(false);
            button.onClick.Invoke();
            root.SetActive(true);
            InvokeLifecycle(root.GetComponent<FeedbackButtonTransmitter>(), "OnEnable");
            button.onClick.Invoke();
            Assert.AreEqual(2, calls);
        }

        [Test]
        public void NonInteractableButtonDoesNotTransmit()
        {
            var button = AddTransmitter();
            int calls = 0;
            channel.Subscribe((c, sender) => calls++);
            button.interactable = false;
            button.onClick.Invoke();
            Assert.AreEqual(0, calls);
        }

        [Test]
        public void ButtonRetainsExistingCommands()
        {
            var button = root.AddComponent<Button>();
            int commands = 0;
            button.onClick.AddListener(() => commands++);
            AddTransmitter();
            button.onClick.Invoke();
            Assert.AreEqual(1, commands);
        }

        private Button AddTransmitter()
        {
            root.SetActive(false);
            var button = root.GetComponent<Button>() ?? root.AddComponent<Button>();
            var transmitter = root.AddComponent<FeedbackButtonTransmitter>();
            typeof(FeedbackButtonTransmitter).GetField("channel", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(transmitter, channel);
            root.SetActive(true);
            // EditMode tests explicitly drive callbacks; Play-mode checks cover Unity lifecycle delivery.
            InvokeLifecycle(transmitter, "Awake");
            InvokeLifecycle(transmitter, "OnEnable");
            return button;
        }

        private static void InvokeLifecycle(FeedbackButtonTransmitter transmitter, string method)
        {
            typeof(FeedbackButtonTransmitter).GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(transmitter, null);
        }
    }
}
