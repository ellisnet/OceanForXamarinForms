namespace Ocean.XamarinFormsSamples.Services {
    using System;
    using Plugin.TextToSpeech;
    using Plugin.TextToSpeech.Abstractions;

    public class CrossTextToSpeechService : ICrossTextToSpeechService {

        // Docs for CrossTextToSpeech https://components.xamarin.com/gettingstarted/texttospeechplugin
        public CrossTextToSpeechService() {
        }

        public void Speak(String text) {
            if (String.IsNullOrWhiteSpace(text)) {
                return;
            }
            CrossTextToSpeech.Current.Speak(text);
        }

        public void Speak(String text, Boolean queue, CrossLocale? crossLocale, float? pitch, float? speakRate, float? volume) {
            if (String.IsNullOrWhiteSpace(text)) {
                return;
            }
            CrossTextToSpeech.Current.Speak(text, queue, crossLocale, pitch, speakRate, volume);
        }

    }
}
