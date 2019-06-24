/// Peter Phillips
/// 20.02.19

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Text;
using System;
using UnityEngine.Rendering.PostProcessing;

public class VoiceRecognition : MonoBehaviour
{
    public string[] _listOfNames;

    [SerializeField] private TextMesh _wordAmmo;

    private Dictionary<string, int> _keywords;
    private KeywordRecognizer keywordRecognizer;
    private enum DIFFICULTY {EASY, MEDIUM, HARD, OTHER};

    private float _timeScale;

    [SerializeField]
    private PostProcessProfile _defaultPostProfile;
    [SerializeField]
    private PostProcessProfile _otherPostProfile;

    void Awake ()
    {
        _listOfNames = new string[]
        {
            "art"  ,
            "ball" ,
            "cat"  ,
            "dog"  ,
            "edge" ,
            "food" ,
            "gold" ,
            "hat"  ,
            "ice"  ,
            "jump" ,
            "kick" ,
            "land" ,
            "meat" ,
            "note" ,
            "orb"  ,
            "page" ,
            "queen",
            "rice" ,
            "stop" ,
            "time" ,
            "unit" ,
            "voice",
            "wax"  ,
            "x-ray",
            "year" ,
            "zebra",

            "animal"     ,
            "brother"    ,
            "copper"     ,
            "danger"     ,
            "expert"     ,
            "fiction"    ,
            "government" ,
            "hospital"   ,
            "igloo"      ,
            "journey"    ,
            "kangaroo"   ,
            "leather"    ,
            "mountain"   ,
            "number"     ,
            "octopus"    ,
            "porridge"   ,
            "question"   ,
            "rabbit"     ,
            "surprise"   ,
            "thunder"    ,
            "universe"   ,
            "vulture"    ,
            "weather"    ,
            "xenon"      ,
            "yodel"      ,
            "zealot"     ,

            "advertisement" ,
            "behaviour"     ,
            "competition"   ,
            "distribution"  ,
            "entomologist"  ,
            "fraternity"    ,
            "graduation"    ,
            "hippopotamus"  ,
            "iridescence"   ,
            "juxtaposition" ,
            "kindergarten"  ,
            "logistics"     ,
            "multilingual"  ,
            "navigation"    ,
            "orthodontics"  ,
            "photosynthesis",
            "quaternion"    ,
            "revolutionist" ,
            "superlative"   ,
            "thermodynamics",
            "ultraviolet"   ,
            "valedictorian" ,
            "wheelbarrow"   ,
            "xylophone"     ,
            "yoctosecond"   ,
            "zillionaire"   ,
        };

        _keywords = new Dictionary<string, int>
        {
            {"art"  , (int)DIFFICULTY.EASY},
            {"ball" , (int)DIFFICULTY.EASY},
            {"cat"  , (int)DIFFICULTY.EASY},
            {"dog"  , (int)DIFFICULTY.EASY},
            {"edge" , (int)DIFFICULTY.EASY},
            {"food" , (int)DIFFICULTY.EASY},
            {"gold" , (int)DIFFICULTY.EASY},
            {"hat"  , (int)DIFFICULTY.EASY},
            {"ice"  , (int)DIFFICULTY.EASY},
            {"jump" , (int)DIFFICULTY.EASY},
            {"kick" , (int)DIFFICULTY.EASY},
            {"land" , (int)DIFFICULTY.EASY},
            {"meat" , (int)DIFFICULTY.EASY},
            {"note" , (int)DIFFICULTY.EASY},
            {"orb"  , (int)DIFFICULTY.EASY},
            {"page" , (int)DIFFICULTY.EASY},
            {"queen", (int)DIFFICULTY.EASY},
            {"rice" , (int)DIFFICULTY.EASY},
            {"stop" , (int)DIFFICULTY.EASY},
            {"time" , (int)DIFFICULTY.EASY},
            {"unit" , (int)DIFFICULTY.EASY},
            {"voice", (int)DIFFICULTY.EASY},
            {"wax"  , (int)DIFFICULTY.EASY},
            {"x-ray", (int)DIFFICULTY.EASY},
            {"year" , (int)DIFFICULTY.EASY},
            {"zebra", (int)DIFFICULTY.EASY},

            {"animal"     , (int)DIFFICULTY.MEDIUM},
            {"brother"    , (int)DIFFICULTY.MEDIUM},
            {"copper"     , (int)DIFFICULTY.MEDIUM},
            {"danger"     , (int)DIFFICULTY.MEDIUM},
            {"expert"     , (int)DIFFICULTY.MEDIUM},
            {"fiction"    , (int)DIFFICULTY.MEDIUM},
            {"government" , (int)DIFFICULTY.MEDIUM},
            {"hospital"   , (int)DIFFICULTY.MEDIUM},
            {"igloo"      , (int)DIFFICULTY.MEDIUM},
            {"journey"    , (int)DIFFICULTY.MEDIUM},
            {"kangaroo"   , (int)DIFFICULTY.MEDIUM},
            {"leather"    , (int)DIFFICULTY.MEDIUM},
            {"mountain"   , (int)DIFFICULTY.MEDIUM},
            {"number"     , (int)DIFFICULTY.MEDIUM},
            {"octopus"    , (int)DIFFICULTY.MEDIUM},
            {"porridge"   , (int)DIFFICULTY.MEDIUM},
            {"question"   , (int)DIFFICULTY.MEDIUM},
            {"rabbit"     , (int)DIFFICULTY.MEDIUM},
            {"surprise"   , (int)DIFFICULTY.MEDIUM},
            {"thunder"    , (int)DIFFICULTY.MEDIUM},
            {"universe"   , (int)DIFFICULTY.MEDIUM},
            {"vulture"    , (int)DIFFICULTY.MEDIUM},
            {"weather"    , (int)DIFFICULTY.MEDIUM},
            {"xenon"      , (int)DIFFICULTY.MEDIUM},
            {"yodel"      , (int)DIFFICULTY.MEDIUM},
            {"zealot"     , (int)DIFFICULTY.MEDIUM},

            {"advertisement" , (int)DIFFICULTY.HARD},
            {"behaviour"     , (int)DIFFICULTY.HARD},
            {"competition"   , (int)DIFFICULTY.HARD},
            {"distribution"  , (int)DIFFICULTY.HARD},
            {"entomologist"  , (int)DIFFICULTY.HARD},
            {"fraternity"    , (int)DIFFICULTY.HARD},
            {"graduation"    , (int)DIFFICULTY.HARD},
            {"hippopotamus"  , (int)DIFFICULTY.HARD},
            {"iridescence"   , (int)DIFFICULTY.HARD},
            {"juxtaposition" , (int)DIFFICULTY.HARD},
            {"kindergarten"  , (int)DIFFICULTY.HARD},
            {"logistics"     , (int)DIFFICULTY.HARD},
            {"multilingual"  , (int)DIFFICULTY.HARD},
            {"navigation"    , (int)DIFFICULTY.HARD},
            {"orthodontics"  , (int)DIFFICULTY.HARD},
            {"photosynthesis", (int)DIFFICULTY.HARD},
            {"quaternion"    , (int)DIFFICULTY.HARD},
            {"revolutionist" , (int)DIFFICULTY.HARD},
            {"superlative"   , (int)DIFFICULTY.HARD},
            {"thermodynamics", (int)DIFFICULTY.HARD},
            {"ultraviolet"   , (int)DIFFICULTY.HARD},
            {"valedictorian" , (int)DIFFICULTY.HARD},
            {"wheelbarrow"   , (int)DIFFICULTY.HARD},
            {"xylophone"     , (int)DIFFICULTY.HARD},
            {"yoctosecond"   , (int)DIFFICULTY.HARD},
            {"zillionaire"   , (int)DIFFICULTY.HARD},

            {"pause"   , (int)DIFFICULTY.OTHER},
            {"play"   , (int)DIFFICULTY.OTHER},
        };

        keywordRecognizer = new KeywordRecognizer(_keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += PhraseRecognised;
        keywordRecognizer.Start();
	}

    void PhraseRecognised(PhraseRecognizedEventArgs args)
    {
        if(!_wordAmmo)
        {
            _wordAmmo = GameObject.Find("WordAmmo").GetComponent<TextMesh>();
        }
        if (args.text == "pause" && Time.timeScale != 0)
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0f;
            if (Camera.main.GetComponent<PostProcessVolume>())
            {
                Camera.main.GetComponent<PostProcessVolume>().profile = _otherPostProfile;
            }
            return;
        }
        else if (args.text == "play" && Time.timeScale == 0)
        {
            Time.timeScale = _timeScale;
            if (Camera.main.GetComponent<PostProcessVolume>())
            {
                Camera.main.GetComponent<PostProcessVolume>().profile = _defaultPostProfile;
            }
            return;
        }

        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());

        _wordAmmo.text = args.text;
    }
}


//music speed
