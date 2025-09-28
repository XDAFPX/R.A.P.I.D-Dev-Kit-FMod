using DAFP.TOOLS.ECS.Audio;
using FMOD.Studio;

namespace DAFP.FMOD
{
    public class FModAudioInstance : IAudioInstance
    {
        private  EventInstance instance;

        public FModAudioInstance(EventInstance instance)
        {
            this.instance = instance;
        }

        public void Dispose()
        {
            instance.release();
        }

        public void Play()
        {
            instance.start();
        }

        public void Stop()
        {
            instance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}