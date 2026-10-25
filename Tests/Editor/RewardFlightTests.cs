using Bomome.Presentation.RewardFlights;
using NUnit.Framework;

namespace Bomome.Presentation.Tests
{
    public sealed class RewardFlightTests
    {
        [Test]
        public void HandleSettlesOnceAndCannotCollectAfterCompletion()
        {
            var handle = new RewardFlightHandle(3);
            int callbacks = 0;
            handle.Finished += _ => callbacks++;
            handle.Collect();
            handle.Finish(RewardFlightStatus.Expired);
            handle.Finish(RewardFlightStatus.Completed);
            handle.Collect();
            Assert.AreEqual(RewardFlightStatus.Expired, handle.Status);
            Assert.AreEqual(1, handle.CollectedCount);
            Assert.AreEqual(1, callbacks);
        }

        [Test]
        public void BatchWaitsForEveryFlightAndDoesNotTreatExpirationAsCollection()
        {
            var batch = new RewardFlightBatch();
            var coins = new RewardFlightHandle(10);
            var gems = new RewardFlightHandle(6);
            batch.Add(coins);
            batch.Add(gems);
            batch.Seal();
            coins.Finish(RewardFlightStatus.Completed);
            Assert.IsFalse(batch.IsDone);
            gems.Finish(RewardFlightStatus.Expired);
            Assert.IsTrue(batch.IsDone);
            Assert.IsFalse(batch.AllCollected);
        }

        [Test]
        public void AlreadyRejectedFlightsAndEmptyBatchesSettle()
        {
            var batch = new RewardFlightBatch();
            var rejected = new RewardFlightHandle(0);
            rejected.Finish(RewardFlightStatus.Rejected);
            batch.Add(rejected);
            int callbacks = 0;
            batch.Finished += _ => callbacks++;
            batch.Seal();
            batch.Seal();
            Assert.AreEqual(1, callbacks);
            Assert.IsFalse(batch.AllCollected);
            var empty = new RewardFlightBatch();
            empty.Seal();
            Assert.IsTrue(empty.IsDone);
        }

        [Test]
        public void CancelSettlesEveryOwnedFlight()
        {
            var batch = new RewardFlightBatch();
            var first = new RewardFlightHandle(1);
            var second = new RewardFlightHandle(1);
            first.BindCancellation(() => first.Finish(RewardFlightStatus.Cancelled));
            second.BindCancellation(() => second.Finish(RewardFlightStatus.Cancelled));
            batch.Add(first);
            batch.Add(second);
            batch.Seal();
            batch.Cancel();
            Assert.IsTrue(batch.IsDone);
            Assert.IsFalse(batch.AllCollected);
            Assert.AreEqual(RewardFlightStatus.Cancelled, second.Status);
        }

        [Test]
        public void CompletedBatchAllowsIndependentNewBatches()
        {
            var batch = new RewardFlightBatch();
            var handle = new RewardFlightHandle(1);
            batch.Add(handle);
            batch.Seal();
            int callbacks = 0;
            batch.Finished += _ => callbacks++;
            handle.Finish(RewardFlightStatus.Completed);
            Assert.IsTrue(batch.AllCollected);
            Assert.AreEqual(1, callbacks);
        }
    }
}
