namespace Disintegrate
{
    /// <summary>
    /// The frequency at which states may be emitted from a <see cref="PresenceProvider"/>.
    /// </summary>
    public enum StateFrequency
    {
        /// <summary>
        /// States are emitted from the <see cref="PresenceProvider"/> as fast as possible. As such, not all states
        /// will be send to RPC.
        /// </summary>
        FastAsPossible,

        /// <summary>
        /// States are emitted roughly at controlled, steady timed intervals, e.g. every 3 seconds. All states will be
        /// sent to RPC.
        /// </summary>
        TimeControlled
    }
}
