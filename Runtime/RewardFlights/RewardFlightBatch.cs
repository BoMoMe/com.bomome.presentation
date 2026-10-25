using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bomome.Presentation.RewardFlights
{
    public sealed class RewardFlightBatch
    {
        private readonly List<RewardFlightHandle> flights = new List<RewardFlightHandle>();
        private bool sealedBatch;
        private int pending;
        public IReadOnlyList<RewardFlightHandle> Flights => flights;
        public bool IsDone { get; private set; }
        public bool AllCollected { get; private set; } = true;
        public event Action<RewardFlightBatch> Finished;

        public void Cancel()
        {
            // Completing a flight can execute user code. Do not enumerate a live mutable list.
            for (int i = 0; i < flights.Count; i++) flights[i].Cancel();
        }

        internal void Add(RewardFlightHandle handle)
        {
            if (sealedBatch) throw new InvalidOperationException("Batch is sealed.");
            flights.Add(handle);
            if (handle.IsDone) AllCollected &= handle.Status == RewardFlightStatus.Completed;
            else { pending++; handle.Finished += OnFlightFinished; }
        }

        internal void Seal() { sealedBatch = true; TryFinish(); }

        private void OnFlightFinished(RewardFlightHandle handle)
        {
            handle.Finished -= OnFlightFinished;
            AllCollected &= handle.Status == RewardFlightStatus.Completed;
            pending--;
            TryFinish();
        }

        private void TryFinish()
        {
            if (!sealedBatch || pending != 0 || IsDone) return;
            IsDone = true;
            var callbacks = Finished;
            Finished = null;
            if (callbacks == null) return;
            foreach (Action<RewardFlightBatch> callback in callbacks.GetInvocationList())
            {
                try { callback(this); }
                catch (Exception exception) { Debug.LogException(exception); }
            }
        }
    }
}
