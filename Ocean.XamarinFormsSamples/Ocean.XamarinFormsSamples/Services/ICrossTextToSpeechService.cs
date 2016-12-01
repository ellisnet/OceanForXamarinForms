namespace Ocean.XamarinFormsSamples.Services {
    using System;
    using Plugin.TextToSpeech.Abstractions;

    public interface ICrossTextToSpeechService {

        void Speak(String text);

        void Speak(String text, Boolean queue, CrossLocale? crossLocale, float? pitch, float? speakRate, float? volume);

    }
}