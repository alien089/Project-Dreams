namespace Misc
{
    public static class DialogueEventList
    {
        /// <summary>
        /// params type: string
        /// </summary>
        public const string CHANGE_SENTENCE = "CHANGE_SENTENCE";
        
        /// <summary>
        /// params type: string
        /// </summary>
        public const string CHANGE_NAME = "CHANGE_NAME";
        
        /// <summary>
        /// params type: List<Sprite[]>
        /// </summary>
        public const string CHANGE_IMAGE = "CHANGE_IMAGE";
        
        /// <summary>
        /// params type: none
        /// </summary>
        public const string END_DIALOGUE = "END_DIALOGUE";
        
        /// <summary>
        /// params type: List<Dialogue>, Dialogue
        /// </summary>
        public const string START_DIALOGUE_ELAB = "START_DIALOGUE_ELAB";
        
        /// <summary>
        /// params type: none
        /// </summary>
        public const string START_DIALOGUE = "START_DIALOGUE";
        
        /// <summary>
        /// params type: List<DialogueSO[]>, List<string[]>
        /// </summary>
        public const string START_CHOICE = "START_CHOICE";
        
        /// <summary>
        /// params type: none
        /// </summary>
        public const string HIDE_CHOICE = "HIDE_CHOICE";
    }
}