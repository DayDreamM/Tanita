namespace Tanita.model
{
    class Activity
    {
        public string id { get; set; }
        public string ActivityName { get; set; }
        public string activityDate { get; set; }
        public int isFile { get; set; }
        public string syschronizeDateTime { get; set; }
        public int isSynchronizeSuccess { get; set; }
        public string synchronizeFailureCause { get; set; }
        public string activityInfo { get; set; }
    }
}
