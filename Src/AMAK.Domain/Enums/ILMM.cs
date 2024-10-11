using System.Runtime.Serialization;

namespace AMAK.Domain.Enums {
    public enum ILMM {
        [EnumMember(Value = "Gemini")]
        Gemini,

        [EnumMember(Value = "ChatGPT4")]
        ChatGPT4,

        [EnumMember(Value = "ChatGPT3.5")]
        ChatGPT3_5
    }
}