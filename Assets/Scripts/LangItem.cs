namespace Assets.Scripts {
    using UnityEngine;

    public enum WhatText
    {
        LanguageValue,
        PointsText,
        MusicText,
        SoundText,
        CurrentScoreTitle,
        BestScoreTitle, 
        ShareText,
        PointsText1,
        PointsText2,
        PointsText3,
        GetReady,
		TutorialText1,
		TutorialText2,
		TutorialText3,
        PauseText,
        UpdateText,
        UpdateButtonText,
        TimeText,
        TimerText,
        LaterText, 
        RateUsText
		
    }

    public enum Language {
        English,
        Russian
    }

    public static class Texts
    {
        private static string[,] value = {
            {"English","Русский"},
            {"Points","Очки"},
            {"Music","Музыка"},
            {"Sounds","Звуки"},
            {"Score","Счет"},
            {"Best","Лучший"},
            {"I've gained","Я набрал"},
            {"points","очко"},
            {"points","очков"},
            {"points","очка"},
            {"Are you ready?", "Вы готовы?"},
			{"Tap to help Zombie to catch the eye.","Нажимай на экран, чтобы помочь Зомби не потерять выпавший глаз."},
			{"Tap to change the hand's direction. Catching of the eye is your goal!","Нажми и измени направление руки. Твоя цель поймать глаз!"},
			{"Zombie mustn't lose his eye in space! Keep it inside his space suit.","Помоги зомби не потерять его глаз в открытом космосе! Удержи его в скафандре."},
            {"Pause","Пауза"},           
            {"New version is available! Download it here!","Доступна новая версия! Обновитесь здесь!"},
            {"Update", "Обновить"},
            {"Time","Время"},
            {"Timer","Таймер"},
            {"Later", "Позже"},
            {"Rate us", "Оцените нас"}
        };

        public static string GetText (WhatText what)
        {
            return value [(int)what, (int)LangItem.language];
        }
    }

    public class LangItem : MonoBehaviour {
        public WhatText whatIsThis;
        public string textBefore, textAfter;
        public UnityEngine.UI.Text textUI;
        public static Language language {
            get {
                switch (Application.systemLanguage) {
                    case SystemLanguage.Russian:
                        return Language.Russian;
                    default:
                        return Language.English;
                }
            }
        }
        private static System.Collections.Generic.List<LangItem> all;
        public static void UpdateAll () {
            if (all != null) {
                foreach (LangItem item in all) {
                    item.Change ();
                }
            }
        }
        private static void AddToAll (LangItem item) {
            if (all == null) {
                all = new System.Collections.Generic.List<LangItem> ();
            }
            all.Add (item);
        }
        private static void RemoveFromAll (LangItem item) {
            if (all != null) {
                all.Remove (item);
            }
        }

        public void SetText (WhatText what) {
            whatIsThis = what;
            Change ();
        }

        void Change () {
            if (textUI == null) {
                textUI = this.GetComponent<UnityEngine.UI.Text> ();
            }
            textUI.text = textBefore+Texts.GetText(whatIsThis)+textAfter;
        }

        void OnEnable ()
        {
            AddToAll (this);
            Change ();
        }
        void OnDisable () {
            RemoveFromAll (this);
        }

    }
}