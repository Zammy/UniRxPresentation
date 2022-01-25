using System;

public enum Doggos
{
    african, affenpinscher, airedale, akita, appenzeller, shepherd_australian, basenji, beagle, bluetick, borzoi, bouvier, boxer, brabancon, briard, norwegian_buhund, boston_bulldog, english_bulldog, french_bulldog, staffordshire_bullterrier, australian_cattledog, chihuahua, chow, clumber, cockapoo, border_collie, coonhound, cardigan_corgi, cotondetulear, dachshund, dalmatian, great_dane, scottish_deerhound, dhole, dingo, doberman, norwegian_elkhound, entlebucher, eskimo, lapphund_finnish, bichon_frise, germanshepherd, italian_greyhound, groenendael, havanese, afghan_hound, basset_hound, blood_hound, english_hound, ibizan_hound, plott_hound, walker_hound, husky, keeshond, kelpie, komondor, kuvasz, labradoodle, labrador, leonberg, lhasa, malamute, malinois, maltese, bull_mastiff, english_mastiff, tibetan_mastiff, mexicanhairless, mix, bernese_mountain, swiss_mountain, newfoundland, otterhound, caucasian_ovcharka, papillon, pekinese, pembroke, miniature_pinscher, pitbull, german_pointer, germanlonghair_pointer, pomeranian, miniature_poodle, standard_poodle, toy_poodle, pug, puggle, pyrenees, redbone, chesapeake_retriever, curly_retriever, flatcoated_retriever, golden_retriever, rhodesian_ridgeback, rottweiler, saluki, samoyed, schipperke, giant_schnauzer, miniature_schnauzer, english_setter, gordon_setter, irish_setter, english_sheepdog, shetland_sheepdog, shiba, shihtzu, blenheim_spaniel, brittany_spaniel, cocker_spaniel, irish_spaniel, japanese_spaniel, sussex_spaniel, welsh_spaniel, english_springer, stbernard, american_terrier, australian_terrier, bedlington_terrier, border_terrier, cairn_terrier, dandie_terrier, fox_terrier, irish_terrier, kerryblue_terrier, lakeland_terrier, norfolk_terrier, norwich_terrier, patterdale_terrier, russell_terrier, scottish_terrier, sealyham_terrier, silky_terrier, tibetan_terrier, toy_terrier, welsh_terrier, westhighland_terrier, wheaten_terrier, yorkshire_terrier, tervuren, vizsla, spanish_waterdog, weimaraner, whippet, irish_wolfhound,
}

public static class DoggosEx
{
    public static string ToUriString(this Doggos doggo)
    {
        var name = doggo.ToString();
        if (name.Contains("_"))
        {
            var split = name.Split('_');
            return string.Format("{1}/{0}", split[0], split[1]);
        }
        else
        {
            return name;
        }
    }

    public static string ToString(this Doggos doggo)
    {
        return Enum.GetName(typeof(Doggos), doggo);
    }
}