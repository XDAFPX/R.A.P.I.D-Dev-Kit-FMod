using System;
using System.Collections;
using System.Collections.Generic;
using DAFP.TOOLS.ECS.Audio;
using DAFP.TOOLS.ECS.Services;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace DAFP.FMOD
{
    public class FMODAudioSys : IAudioSystem
    {
        //     public List<AssetReference> Banks = new List<AssetReference>();
        //     public string Scene;
        //
        //     private static int numberOfCompletedCallbacks;
        //
        //     void Awake()
        //     {
        //         StartCoroutine(LoadBanksAsync());
        //     }
        //
        //     private Action Callback = () => { numberOfCompletedCallbacks++; };
        //
        //     IEnumerator LoadBanksAsync()
        //     {
        //         Banks.ForEach(b => FMODUnity.RuntimeManager.LoadBank(b, true, Callback));
        //
        //         while (numberOfCompletedCallbacks < Banks.Count)
        //             yield return null;
        //
        //         while (FMODUnity.RuntimeManager.AnySampleDataLoading())
        //             yield return null;
        //
        //         AsyncOperation async = SceneManager.LoadSceneAsync(Scene);
        //
        //         while (!async.isDone)
        //         {
        //             yield return null;
        //         }
        //
        //         Banks.ForEach(b => b.ReleaseAsset());
        //     }


        public IAudioInstance Play(IAudioSettings settings, string audio, params object[] additionaldata)
        {
            var _inst = FMODUnity.RuntimeManager.CreateInstance(audio);
            _inst.setVolume(settings.Volume);
            _inst.setPitch(settings.Pitch);
            if (settings.AttachedToPosition().TryGetVector3(out var _vector3))
            {
                _inst.set3DAttributes(_vector3.To3DAttributes());
            }
            else if (settings.AttachedToPosition().TryGetTransform(out var _transform))
            {
                if (_transform.TryGetComponent(out Rigidbody _rb))
                {
                    RuntimeManager.AttachInstanceToGameObject(_inst, _transform.gameObject, _rb);
                }
                else if (_transform.TryGetComponent(out Rigidbody2D _rb2))
                {
                    RuntimeManager.AttachInstanceToGameObject(_inst, _transform.gameObject, _rb2);
                }
                else
                {
                    RuntimeManager.AttachInstanceToGameObject(_inst, _transform.gameObject, true);
                }
            }
            else
            {
                _inst.set3DAttributes(Vector3.zero.To3DAttributes());
            }


            _inst.start();
            return new FModAudioInstance(_inst);
        }

        public void DeleteInstance(IAudioInstance instance)
        {
            instance.Dispose();
        }

        public void PlayOneShot(IAudioSettings settings, string audio, params object[] additionaldata)
        {
            Play(settings, audio, additionaldata).Dispose();
        }
    }
}