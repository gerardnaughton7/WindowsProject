using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutTalk.Model
{
    class SoundManager
    {
        //returns all sounds
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allSounds = getSounds();
            sounds.Clear();
            allSounds.ForEach(p => sounds.Add(p));
        }

        //returns sounds by category
        public static void GetSoundsByCategory(ObservableCollection<Sound> sounds, SoundCategory soundCategory)
        {
            var allSounds = getSounds();
            var filteredSounds = allSounds.Where(p => p.Category == soundCategory).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        //filters the sounds name in the auto suggest box
        public static void GetSoundsByName(ObservableCollection<Sound> sounds, String name)
        {
            var allSounds = getSounds();
            sounds.Clear();
            var filteredSounds = allSounds.Where(p => p.Name == name).ToList();
            filteredSounds.ForEach(p => sounds.Add(p));
        }

        //gets sounds
        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();

            sounds.Add(new Sound("Bright", SoundCategory.Needs));
            sounds.Add(new Sound("Cold", SoundCategory.Needs));
            sounds.Add(new Sound("Hungry", SoundCategory.Needs));
            sounds.Add(new Sound("Hot", SoundCategory.Needs));
            sounds.Add(new Sound("Loud", SoundCategory.Needs));
            sounds.Add(new Sound("Sick", SoundCategory.Needs));
            sounds.Add(new Sound("Sleepy", SoundCategory.Needs));
            sounds.Add(new Sound("Thirsty", SoundCategory.Needs));
            sounds.Add(new Sound("Toilet", SoundCategory.Needs));

            sounds.Add(new Sound("Dad", SoundCategory.Wants));
            sounds.Add(new Sound("Mom", SoundCategory.Wants));
            sounds.Add(new Sound("Music", SoundCategory.Wants));
            sounds.Add(new Sound("Outside", SoundCategory.Wants));
            sounds.Add(new Sound("Play", SoundCategory.Wants));
            sounds.Add(new Sound("TV", SoundCategory.Wants));

            sounds.Add(new Sound("Dislike", SoundCategory.Interaction));
            sounds.Add(new Sound("Goodbye", SoundCategory.Interaction));
            sounds.Add(new Sound("Hello", SoundCategory.Interaction));
            sounds.Add(new Sound("Like", SoundCategory.Interaction));
            sounds.Add(new Sound("Love", SoundCategory.Interaction));

            return sounds;

        }
    }
}
