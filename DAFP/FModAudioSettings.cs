using DAFP.TOOLS.Common;
using DAFP.TOOLS.ECS.Audio;
using UnityEngine;

namespace DAFP.FMOD
{
    public readonly struct FModAudioSettings : IAudioSettings
    {
        private readonly PositionTarget target;

        public FModAudioSettings(PositionTarget target, float volume, float pitch)
        {
            this.target = target;
            Volume = volume;
            Pitch = pitch;
        }

        public float Volume { get; }
        public float Pitch { get; }
        public PositionTarget AttachedToPosition()
        {
            return target;
        }
    }
}