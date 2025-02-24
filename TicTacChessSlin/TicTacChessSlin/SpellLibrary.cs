using System.Collections.Generic;
using System.Linq;

namespace TicTacChessSlin
{
    internal static class SpellLibrary
    {
        private static Dictionary<string, Spell> spellCollection = new Dictionary<string, Spell>();

        // Add or update a spell in the library
        public static void AddOrUpdateSpell(string key, Spell spell)
        {
            spellCollection[key] = spell;
        }

        // Remove a spell from the library
        public static void RemoveSpell(string key)
        {
            if (spellCollection.ContainsKey(key))
            {
                spellCollection.Remove(key);
            }
        }

        // Retrieve a spell by its name
        public static Spell GetSpell(string key)
        {
            return spellCollection.TryGetValue(key, out Spell spell) ? spell : null;
        }

        // Get all spells in a list
        public static List<Spell> GetAllSpells()
        {
            return spellCollection.Values.ToList();
        }
    }
}
