using Microsoft.AspNetCore.Mvc; 

namespace web_asignment1_Y2S1_2022.Models
{
    public class Index
    {
        [FromBody]
        [BindProperty(Name = "data", SupportsGet = true)]
        public int IndexPosition { get; set; }
    }
}
