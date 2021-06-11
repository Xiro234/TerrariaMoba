﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public abstract class Character {
        public abstract string Name { get; }
        public Player User { get; private set; }
        public string CharacterName { get; protected set; }
        public abstract Texture2D CharacterIcon { get; }

        //TODO - Change the way talents work. public bool[,] TalentArray { get; protected set; }
        
        public const int XP_PER_LEVEL = 100;
        public int Experience { get; protected set; }
        public List<Ability> Abilities { get; protected set; }
        public Statistics BaseStatistics { get; private set; }
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
        public Ability SlotOne {
            get { return Abilities[0]; }
            protected set { Abilities[0] = value; }
        }
        
        public Ability SlotTwo {
            get { return Abilities[1]; }
            protected set { Abilities[1] = value; }
        }
        
        public Ability SlotThree {
            get { return Abilities[2]; }
            protected set { Abilities[2] = value; }
        }
        
        public Ability SlotFour {
            get { return Abilities[3]; }
            protected set { Abilities[3] = value; }
        }
        
        public Ability SlotFive {
            get { return Abilities[4]; }
            protected set { Abilities[4] = value; }
        }

        public Character(Player user, Statistics baseStatistics, params Ability[] abilities) {
            User = user;
            BaseStatistics = baseStatistics;
            Abilities = abilities.ToList();
        }

        public virtual void GainExperience(int xp) {
            Experience += xp;

            while (Experience >= XP_PER_LEVEL) {
                LevelUp();
                Experience -= XP_PER_LEVEL;
            }
        }
        
        public virtual void LevelUp() { }
        
        public virtual void StartGame() { }

        public virtual void RegenResource() { //Base is mana
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();
            float manaRegenFromMax = ((BaseStatistics.MaxResource + mobaPlayer.Stats.MaxResource) * 0.125f / 60f);
            mobaPlayer.CurrentResource += manaRegenFromMax + BaseStatistics.ResourceRegen + mobaPlayer.Stats.ResourceRegen;
        }
    }
}