using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutTalk.Model
{
    //gives us a handle on our menu items
    public class MenuItem
    {
        public string IconFile { get; set; }
        public SoundCategory Category {get; set; }
    }
}
