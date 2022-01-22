
using System;
using UniRx;
using UnityEngine;

public class DogCeoService
{
    [System.Serializable]
    private class DogCeoApiResult
    {
        public string message;
        public string status;
    }


    public static IObservable<Texture2D> GetRandomDogTexture(Doggos doggo)
    {
        var tex2d = new Texture2D(0, 0);
        var path = string.Format("https://dog.ceo/api/breed/{0}/images/random", doggo.ToUriString());

        return ObservableWWW.Get(path)
             .Select(r =>
             {
                 var result = JsonUtility.FromJson<DogCeoApiResult>(r);
                 if (result.status.Equals("success"))
                 {
                     return result.message;
                 }
                 throw new UnityException(string.Format("DogCeo failed. {0}", path));
             })
             .SelectMany(path =>
             {
                 return ObservableWWW.GetAndGetBytes(path)
                         .Select(res =>
                         {
                             if (ImageConversion.LoadImage(tex2d, res))
                             {
                                 return tex2d;
                             }
                             else
                             {
                                 throw new UnityException(string.Format("Could not load image file. {0}", path));
                             }
                         });
             });
    }
}

