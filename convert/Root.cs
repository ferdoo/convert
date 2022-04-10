using System;

namespace convert
{
    public class Root
    {
        public string result { get; set; }
        public string provider { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public int time_last_update_unix { get; set; }
        public DateTime time_last_update_utc { get; set; }
        public int time_next_update_unix { get; set; }
        public DateTime time_next_update_utc { get; set; }
        public int time_eol_unix { get; set; }
        public string base_code { get; set; }
        public Rates conversion_rates { get; set; }
        
    }
}