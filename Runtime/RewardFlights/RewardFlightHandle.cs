using System;
using UnityEngine;

namespace Bomome.Presentation.RewardFlights
{
    public enum RewardFlightStatus { Pending, Completed, Expired, Cancelled, TargetLost, Rejected }

    /// <summary>A visual operation, never an economy transaction.</summary>
    public sealed class RewardFlightHandle
    {
        private Action cancel;
        public RewardFlightStatus Status { get; private set; }
        public bool IsDone => Status != RewardFlightStatus.Pending;
        public int VisualCount { get; }
        public int CollectedCount { get; private set; }
        public event Action<RewardFlightHandle> Finished;

        public RewardFlightHandle(int visualCount) { VisualCount = visualCount; }
        public void Cancel() { if (!IsDone) cancel?.Invoke(); }

        internal void BindCancellation(Action action) => cancel = action;
        internal void Collect() { if (!IsDone) CollectedCount++; }

        internal void Finish(RewardFlightStatus status)
        {
            if (IsDone) return;
            if (status == RewardFlightStatus.Pending) throw new ArgumentException("A terminal status is required.");
            Status = status;
            cancel = null;
            var callbacks = Finished;
            Finished = null;
            if (callbacks == null) return;
            foreach (Action<RewardFlightHandle> callback in callbacks.GetInvocationList())
            {
                try { callback(this); }
                catch (Exception exception) { Debug.LogException(exception); }
            }
        }
    }
}
