using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenAl;
using System.IO;

namespace Engine
{
    public class SoundManager
    {
        struct SoundSource
        {
            public int bufferId;
            public string filePath;
            public SoundSource(int bufferId, string filePath)
            {
                this.bufferId = bufferId;
                this.filePath = filePath;
            }
        }

        Dictionary<string, SoundSource> soundIdentifier = new 
            Dictionary<string, SoundSource>();

        readonly int MaxSoundChannels = 256;
        List<int> soundChannels = new List<int>();

        public SoundManager()
        {
            Alut.alutInit();
            DiscoverSoundChannels();
        }

        private void DiscoverSoundChannels()
        {
            while (soundChannels.Count < MaxSoundChannels)
            {
                int src;
                Al.alGenSources(1, out src);
                if (Al.alGetError() == Al.AL_NO_ERROR)
                {
                    soundChannels.Add(src);
                }
                else
                {
                    break; // there's been an error - we've filled all the channels
                }
            }
        }

        public void LoadSound(string soundId, string path)
        {
            // Generate a buffer.
            int buffer = -1;
            Al.alGenBuffers(1, out buffer);

            int errorCode = Al.alGetError();
            System.Diagnostics.Debug.Assert(errorCode == Al.AL_NO_ERROR);

            int format;
            float frequency;
            int size;
            System.Diagnostics.Debug.Assert(File.Exists(path));
            IntPtr data = Alut.alutLoadMemoryFromFile(path, out format, out size, out frequency);
            string error2 = Alut.alutGetErrorString(Alut.alutGetError());

            System.Diagnostics.Debug.Assert(data != IntPtr.Zero, error2);
            // Load wav data into the generated buffer.
            Al.alBufferData(buffer, format, data, size, (int)frequency);
            // Every seems ok, add it to the library.
            soundIdentifier.Add(soundId, new SoundSource(buffer, path));
        }

        public Sound PlaySound(string soundId)
        {
            // Default play sound doesn't loop
            return PlaySound(soundId, false);
        }

        public Sound PlaySound(string soundId, bool loop)
        {
            int channel = FindNextFreeChannel();
            if (channel != -1)
            {
                Al.alSourceStop(channel);
                Al.alSourcei(channel, Al.AL_BUFFER, 
                    soundIdentifier[soundId].bufferId);
                Al.alSourcef(channel, Al.AL_PITCH, 1.0f);
                Al.alSourcef(channel, Al.AL_GAIN, 1.0f);

                if (loop)
                {
                    Al.alSourcei(channel, Al.AL_LOOPING, 1);
                }
                else
                {
                    Al.alSourcei(channel, Al.AL_LOOPING, 0);
                }
                Al.alSourcePlay(channel);
                return new Sound(channel);
            }
            return null;
        }

        public bool IsSoundPlaying(Sound sound)
        {
            return false;
        }

        private bool IsChannelPlaying(int channel)
        {
            int value = 0;
            Al.alGetSourcei(channel, Al.AL_SOURCE_STATE, out value);
            return (value == Al.AL_PLAYING);
        }

        private int FindNextFreeChannel()
        {
            foreach (int slot in soundChannels)
            {
                if (!IsChannelPlaying(slot))
                {
                    return slot;
                }
            }
            return -1;
        }

        public void StopSound(Sound sound)
        {

        }
    }

}
