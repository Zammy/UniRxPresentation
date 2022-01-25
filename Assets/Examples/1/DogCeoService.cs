
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
        var url = string.Format("https://dog.ceo/api/breed/{0}/images/random", doggo.ToUriString());
        return ObservableWWW.Get(url)
            .Select(response =>
            {
                var result = JsonUtility.FromJson<DogCeoApiResult>(response);
                if (result.status.Equals("success"))
                {
                    return result.message;
                }
                throw new UnityException(string.Format("DogCeo failed. {0}", url));
            })
            .SelectMany(dogUrl =>
            {
                return ObservableWWW.GetAndGetBytes(dogUrl)
                    .Select(response =>
                    {
                        if (ImageConversion.LoadImage(tex2d, response))
                        {
                            return tex2d;
                        }
                        else
                        {
                            throw new UnityException(string.Format("Could not load image file. {0}", response));
                        }
                    });
        });
    }
}

