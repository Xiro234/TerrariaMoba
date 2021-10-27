using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public abstract class Character {
        public abstract string Name { get; }
        public Player User { get; private set; }
        public abstract Asset<Texture2D> CharacterIcon { get; }

        //TODO - Change the way talents work. public bool[,] TalentArray { get; protected set; }
        
        public const int XP_PER_LEVEL = 100;
        public int Experience { get; protected set; }
        public abstract Ability[] Skills { get; }
        public abstract Dictionary<AttributeType, float> BaseAttributes { get; }
        public int Level { get; protected set; }

        //Appearance
        public virtual bool IsMale { get => true; }
        public virtual int HairID { get => 0; }
        public virtual Color HairColor { get => Color.Tomato; }
        public virtual Color SkinColor { get => Color.Tomato; }
        public virtual Color EyeColor { get => Color.Tomato; }

        //Items
        public virtual int PrimaryWeaponID { get => 0; }
        public virtual int HeadVanityID { get => 0; }
        public virtual int HeadDyeID { get => 0; }
        public virtual int BodyVanityID { get => 0; }
        public virtual int BodyDyeID { get => 0; }
        public virtual int LegVanityID { get => 0; }
        public virtual int LegDyeID { get => 0; }
        
        //Ability Properties
        public Ability BasicAbilityOne {
            get { return Skills[0]; }
            protected set { Skills[0] = value; }
        }
        
        public Ability BasicAbilityTwo {
            get { return Skills[1]; }
            protected set { Skills[1] = value; }
        }
        
        public Ability BasicAbilityThree {
            get { return Skills[2]; }
            protected set { Skills[2] = value; }
        }
        
        public Ability Ultimate {
            get { return Skills[3]; }
            protected set { Skills[3] = value; }
        }
        
        public Ability Trait {
            get { return Skills[4]; }
            protected set { Skills[4] = value; }
        }

        public Character(Player user) {
            User = user;
        }
        
        public Character() { } //For reflection

        public virtual void GainExperience(int xp) {
            Experience += xp;

            while (Experience >= XP_PER_LEVEL) {
                LevelUp();
                Experience -= XP_PER_LEVEL;
            }
        }
        
        public virtual void LevelUp() { }

        public virtual void StartGame() { }

        public virtual void RegenResource(float maxMana) { //Base is mana
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();
            
            if (mobaPlayer.CurrentResource > maxMana) {
                mobaPlayer.CurrentResource = (int)Math.Floor(maxMana);
            }
        }

        public virtual void InitializePlayer() { 
            Item primary = new Item();
            primary.SetDefaults(PrimaryWeaponID);
            Item vanityHead = new Item();
            vanityHead.SetDefaults(HeadVanityID);
            Item vanityBody = new Item();
            vanityBody.SetDefaults(BodyVanityID);
            Item vanityLegs = new Item();
            vanityLegs.SetDefaults(LegVanityID);
            Item dyeHead = new Item();
            dyeHead.SetDefaults(HeadDyeID);
            Item dyeBody = new Item();
            dyeBody.SetDefaults(BodyDyeID);
            Item dyeLegs = new Item();
            dyeLegs.SetDefaults(LegDyeID);
            
            User.inventory[0] = primary;
            User.armor[10] = vanityHead;
            User.armor[11] = vanityBody;
            User.armor[12] = vanityLegs;
            User.dye[0] = dyeHead;
            User.dye[1] = dyeBody;
            User.dye[2] = dyeLegs;

            User.Male = IsMale;
            User.hair = HairID;
            User.hairColor = HairColor;
            User.eyeColor = EyeColor;
            User.skinColor = SkinColor;
        }
    }
}