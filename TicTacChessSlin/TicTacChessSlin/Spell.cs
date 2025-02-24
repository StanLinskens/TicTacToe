using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    internal class Spell
    {
        public string SpellName { get; set; }
        public Panel SpellPanel { get; set; }

        public Spell(string spellName, Panel spellPanel)
        {
            SpellName = spellName;
            SpellPanel = spellPanel; // The image representing the spell
        }

        // Abstract method that will be implemented by all specific spells
       // public abstract void ApplyEffect(object target);
    }
}