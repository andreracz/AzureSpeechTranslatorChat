using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;
using System.Text.Json;
using System.Text.Json.Serialization;
using NAudio.CoreAudioApi;

namespace net
{
    public partial class Form1 : Form
    {

        static readonly string SPEECH__SUBSCRIPTION__KEY = "USE_YOURS";

        static readonly string SPEECH__SERVICE__REGION = "brazilsouth";

        static readonly string SERVICEBUS__SAS = "USE_YOURS";

        ServiceBusClient client;
        
        Dictionary<string, ServiceBusSender> senders = new Dictionary<string, ServiceBusSender>();

        string subscriptionName;
        ServiceBusProcessor processor;
        ServiceBusAdministrationClient administrationClient;

        string langCode;
        string selectedGender;
        string selectedNickname;
        bool OwnAudio;

        SpeechTranslationConfig translationConfig;
        TranslationRecognizer recognizer;
        Dictionary<string, string> languageToVoiceMap = new Dictionary<string, string>
        {
            ["en-US:female"] = "en-US-AriaNeural",
            ["en-US:male"] = "en-US-GuyNeural",
            ["pt-BR:female"] = "pt-BR-FranciscaNeural",
            ["pt-BR:male"] = "pt-BR-AntonioNeural",
            ["es-AR:female"] = "es-AR-ElenaNeural",
            ["es-AR:male"] = "es-AR-TomasNeural",
        };
        Dictionary<string, string> languageNameMap = new Dictionary<string, string>
        {
            ["English"] = "en-US",
            ["Portuguese (Brazil)"] = "pt-BR",
            ["Spanish"] = "es-AR"
        };

        Dictionary<string, string> langVersionMap = new Dictionary<string, string>
        {
            ["en"] = "en-US",
            ["pt"] = "pt-BR",
            ["es"] = "es-AR"
        };

        SpeechConfig maleSpeechConfig;
        SpeechSynthesizer maleSynthesizer;

        SpeechConfig femaleSpeechConfig;
        SpeechSynthesizer femaleSynthesizer;
        string guid = "";

        static readonly bool DEBUG = false;
        
        


        public Form1()
        {
            InitializeComponent();
            foreach (var key in languageNameMap.Keys) {
                this.originIdiom.Items.Add(key);
           }
            var enumerator = new MMDeviceEnumerator();
            foreach (var endpoint in
                     enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
            {
                this.audioInputs.Items.Add(endpoint.FriendlyName);
            }
            this.audioInputs.SelectedIndex = 0;
        }

        private async Task SendMessage(ServiceBusSender sender, string message, string language)
        {
            try
            {
                if (sender == null)
                {
                    return;
                }
                if (sender.IsClosed)
                {
                    return;
                }
                ChatMessage chatMessage = new ChatMessage
                {
                    Message = message,
                    LanguageCode = language,
                    Gender = selectedGender,
                    SenderName = selectedNickname,
                    Guid = guid,
                };
                Debug(JsonSerializer.Serialize(chatMessage) + "\r\n");
                ServiceBusMessage messageToSend = new ServiceBusMessage(JsonSerializer.SerializeToUtf8Bytes(chatMessage))
                {
                    ContentType = "application/json",
                };
                await sender.SendMessageAsync(messageToSend);

            }catch(Exception e)
            {
                AppendMyText(e.Message + "\r\n");
            }
        }

        private async Task ReceiveMessage(ProcessMessageEventArgs args)
        {
            ChatMessage message = JsonSerializer.Deserialize<ChatMessage>(args.Message.Body);
            var fullText = $"[{message.SenderName}]: {message.Message}\r\n";
            AppendChatText(fullText);
            if (message.Guid != guid || OwnAudio)
            {
                if (message.Gender == "male")
                {
                    await maleSynthesizer.SpeakTextAsync(message.Message);
                }
                else
                {
                    await femaleSynthesizer.SpeakTextAsync(message.Message);
                }
            }

        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            AppendChatText("Error: " + args.Exception.ToString() + "\r\n");
            return Task.CompletedTask;
        }

        private async void Recognized(object s, TranslationRecognitionEventArgs e)
        {
            var fullText = "";
            fullText += "Recognized Text:" + e.Result.Text + "\r\n";
            await SendMessage(senders[langCode], e.Result.Text, langCode);
            foreach (var (lang, text) in e.Result.Translations)
            {
                var theLang = lang;
                if (!theLang.Contains("-"))
                {
                    theLang = langVersionMap[theLang];
                }
                fullText += "  Translation[" + theLang + "]=" + text + "\r\n";
                try
                {
                    await SendMessage(senders[theLang], text, theLang); ;
                }catch(Exception ex)
                {
                    AppendMyText("Error: " + ex.Message + "\r\n");
                }
                
            }
            AppendMyText(fullText);
            SetTalkEnabled(true);
        }

        
        private async Task InitializeRecognitionAndConnect()
        {
            selectedGender = this.gender.SelectedItem as string;
            selectedNickname = this.nickName.Text;

            var language = this.originIdiom.SelectedItem as string;
            langCode = this.languageNameMap[language];

            client = new ServiceBusClient(SERVICEBUS__SAS);

            administrationClient = new ServiceBusAdministrationClient(SERVICEBUS__SAS);
            guid = Guid.NewGuid().ToString();
            subscriptionName = "subs-" + guid;


            await administrationClient.CreateSubscriptionAsync("chat-" + langCode, subscriptionName);
            processor = client.CreateProcessor("chat-" + langCode, subscriptionName);
            processor.ProcessMessageAsync += ReceiveMessage;
            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();
 

            translationConfig = SpeechTranslationConfig.FromSubscription(SPEECH__SUBSCRIPTION__KEY, SPEECH__SERVICE__REGION);
            translationConfig.SpeechRecognitionLanguage = langCode;
            Debug(langCode + "\r\n");

            foreach (var lang in this.languageNameMap.Keys)
            {
                var currentLangCode = (this.languageNameMap[lang]);
                var sender = client.CreateSender("chat-" + currentLangCode);
                senders.Add(currentLangCode, sender);
                Debug("lang=" + lang + "\r\n");
                Debug("language=" + language + "\r\n");
                if (lang != language)
                {
                    Debug("target=" + currentLangCode + "\r\n");
                    translationConfig.AddTargetLanguage(currentLangCode);
                }
            }


            var audioDeviceName = this.audioInputs.SelectedItem as string;
            string audioDeviceId = null;

            if (audioDeviceName != null)
            {
                var enumerator = new MMDeviceEnumerator();
                foreach (var endpoint in
                         enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                {
                    if (endpoint.FriendlyName == audioDeviceName)
                    {
                        audioDeviceId = endpoint.ID;
                        break;
                    }
                }
            }
            using var audioConfig = AudioConfig.FromMicrophoneInput(audioDeviceId);
            recognizer = new TranslationRecognizer(translationConfig, audioConfig);

            recognizer.Recognized += Recognized;
            recognizer.SessionStarted += Recognizer_SessionStarted;
            recognizer.SessionStopped += Recognizer_SessionStopped;
            recognizer.SpeechStartDetected += Recognizer_SpeechStartDetected;
            recognizer.SpeechEndDetected += Recognizer_SpeechEndDetected;
            recognizer.Recognizing += Recognizer_Recognizing;
            


            maleSpeechConfig = SpeechConfig.FromSubscription(SPEECH__SUBSCRIPTION__KEY, SPEECH__SERVICE__REGION);

            maleSpeechConfig.SpeechSynthesisVoiceName = languageToVoiceMap[langCode + ":male"];
            maleSynthesizer = new SpeechSynthesizer(maleSpeechConfig, AudioConfig.FromDefaultSpeakerOutput());
            maleSynthesizer.SynthesisStarted += Synthesizer_SynthesisStarted;
            maleSynthesizer.SynthesisCompleted += Synthesizer_SynthesisCompleted;
            maleSynthesizer.SynthesisCanceled += Synthesizer_SynthesisCanceled;

            femaleSpeechConfig = SpeechConfig.FromSubscription(SPEECH__SUBSCRIPTION__KEY, SPEECH__SERVICE__REGION);

            femaleSpeechConfig.SpeechSynthesisVoiceName = languageToVoiceMap[langCode + ":female"];
            femaleSynthesizer = new SpeechSynthesizer(femaleSpeechConfig, AudioConfig.FromDefaultSpeakerOutput());

            
        }

        private async Task Disconnect()
        {
            await recognizer.StopContinuousRecognitionAsync();
            await processor.StopProcessingAsync();
            foreach (var sender in senders.Values)
            {
                await sender.CloseAsync();
            }
            senders.Clear();
            await administrationClient.DeleteSubscriptionAsync("chat-" + langCode, subscriptionName);
        }

        // UI and Events

        private void Synthesizer_SynthesisCanceled(object sender, SpeechSynthesisEventArgs e)
        {
            Debug("Synthesizer_SynthesisCanceled: r=" + e.Result + "\r\n");
        }

        private void Synthesizer_SynthesisCompleted(object sender, SpeechSynthesisEventArgs e)
        {
            Debug("Synthesizer_SynthesisCompleted: r=" + e.Result + "\r\n");
        }

        private void Synthesizer_SynthesisStarted(object sender, SpeechSynthesisEventArgs e)
        {
            Debug("Synthesizer_SynthesisStarted: r=" + e.Result + "\r\n");
        }

        private void Recognizer_Recognizing(object sender, TranslationRecognitionEventArgs e)
        {
            Debug("Recognizer_Recognizing:" + e.SessionId + " r=" + e.Result + "\r\n");
        }

        private void Recognizer_SpeechEndDetected(object sender, RecognitionEventArgs e)
        {
            Debug("Recognizer_SpeechEndDetected:" + e.SessionId + "\r\n");
        }

        private void Recognizer_SpeechStartDetected(object sender, RecognitionEventArgs e)
        {
            Debug("Recognizer_SpeechStartDetected:" + e.SessionId + "\r\n");
        }

        private void Recognizer_SessionStopped(object sender, SessionEventArgs e)
        {
            Debug("Recognizer Session Started:" + e.SessionId + "\r\n");
        }

        private void Recognizer_SessionStarted(object sender, SessionEventArgs e)
        {
            Debug("Recognizer Session Started:" + e.SessionId + "\r\n");
        }

        delegate void SetTextCallback(string text);

        private void AppendMyText(string text)
        {
            if (this.mytext.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AppendMyText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.mytext.AppendText(text);
            }
        }

        private void AppendChatText(string text)
        {
            if (this.chatText.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AppendChatText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.chatText.AppendText(text);
            }
        }



        private async void connect_Click(object sender, EventArgs e)
        {
            await InitializeRecognitionAndConnect();
            disconnect.Enabled = true;
            connect.Enabled = false;
            talk.Enabled = true;
            this.gender.Enabled = false;
            this.nickName.Enabled = false;
            this.originIdiom.Enabled = false;
            this.audioInputs.Enabled = false;

        }

        private async void disconnect_Click(object sender, EventArgs e)
        {
            await Disconnect();
            disconnect.Enabled = false;
            connect.Enabled = true;
            talk.Enabled = false;
            this.gender.Enabled = true;
            this.nickName.Enabled = true;
            this.originIdiom.Enabled = true;
            this.audioInputs.Enabled = true;
        }

        private async void talk_Click(object sender, EventArgs e)
        {
            talk.Enabled = false;
            await recognizer.RecognizeOnceAsync();
        }

        private async void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                await Disconnect();
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void hearOwnAudio_CheckedChanged(object sender, EventArgs e)
        {
            this.OwnAudio = this.hearOwnAudio.Checked; 
        }

        delegate void SetEnabledCallback(bool status);

        private void SetTalkEnabled(bool status)
        {
            if (this.talk.InvokeRequired)
            {
                SetEnabledCallback d = new SetEnabledCallback(SetTalkEnabled);
                this.Invoke(d, new object[] { status });
            }
            else
            {
                this.talk.Enabled = status;
            }
        }

        private void Debug(string text)
        {
            if (DEBUG)
            {
                AppendMyText(text);
            }
        }

    }
}
