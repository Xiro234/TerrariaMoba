using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Packets;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Characters {
    public class Character {
        public Player player;
        public int level = 1;
        public bool canSelectTalent = false;
        public string characterName = "none";
        public bool[,] talentArray;
        public int xpPerLevel = 100;
        public int experience = 0;
        public CharacterEnum CharacterEnum;
        public List<Ability> abilities;
        public Texture2D CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Lock");

        //Stats
        public int baseMaxLife;
        public float baseLifeRegen;
        public float baseLifeDegen;
        public int baseMaxResource;
        public float baseResourceRegen;
        public float baseResourceDegen;
        public int baseArmor;
        
        //Items
        public Item primary;
        
        public Item vanityHead;
        public Item dyeHead;
        public Item vanityBody;
        public Item dyeBody;
        public Item vanityLeg;
        public Item dyeLeg;
        
        //Ability Properties
        public Ability QAbility {
            get { return abilities[0]; }
            set { abilities[0] = value; }
        }
        
        public Ability EAbility {
            get { return abilities[1]; }
            set { abilities[1] = value; }
        }
        
        public Ability FAbility {
            get { return abilities[2]; }
            set { abilities[2] = value; }
        }
        
        public Ability RAbility {
            get { return abilities[3]; }
            set { abilities[3] = value; }
        }
        
        public Ability CAbility {
            get { return abilities[4]; }
            set { abilities[4] = value; }
        }

        public Character(Player myPlayer) {
            player = myPlayer;
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            
            vanityHead = new Item();
            dyeHead = new Item();
            vanityBody = new Item();
            dyeBody = new Item();
            vanityLeg = new Item();
            dyeLeg = new Item();
            primary = new Item();

            abilities = new List<Ability>(8);
            for(int i = 0; i < abilities.Capacity; i++) {
                abilities.Add(new Ability());
            }
            talentArray = new bool[7, 4];
            mobaPlayer.CharacterPicked = true;
        }
        
        public void SetCharacter() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            TerrariaMobaUtils.ClearInventory(mobaPlayer);
            InitializeCharacter();
            SetPlayer();
            SetStats();
            
            player.inventory[0] = primary;
            player.armor[10] = vanityHead;
            player.armor[11] = vanityBody;
            player.armor[12] = vanityLeg;

            player.dye[0] = dyeHead;
            player.dye[1] = dyeBody;
            player.dye[2] = dyeLeg;

            player.statLifeMax2 = baseMaxLife;
            player.statLife = baseMaxLife;
        }

        public virtual void SetPlayer() {}

        public virtual void SetStats() {
            baseMaxLife = 1000;
            baseLifeRegen = 4;
            baseLifeDegen = 0;
            baseMaxResource = 500;
            baseResourceRegen = 4;
            baseResourceDegen = 0;
            baseArmor = 0;
        }
        
        public virtual void GainExperience(int xp) {
            experience += xp;

            while (experience >= xpPerLevel) {
                LevelUp();
                experience -= xpPerLevel;
            }
        }
        
        public virtual void TalentSelect() { }
        public virtual void LevelUp() { }

        public virtual void InitializeCharacter() {
            
        }

        public virtual void LevelTalentOne() {
            Main.NewText(canSelectTalent);
            Main.NewText(level);
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        Main.NewText("Level up one!");
                        talentArray[0, 0] = true;
                        break;
                    case 4:
                        talentArray[1, 0] = true;
                        break;
                    case 7:
                        talentArray[2, 0] = true;
                        break;
                    case 10:
                        talentArray[3, 0] = true;
                        break;
                    case 13:
                        talentArray[4, 0] = true;
                        break;
                    case 16:
                        talentArray[5, 0] = true;
                        break;
                    case 20:
                        talentArray[6, 0] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }
        }
        
        public virtual void LevelTalentTwo() {
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        Main.NewText("Level up two!");
                        talentArray[0, 1] = true;
                        break;
                    case 4:
                        talentArray[1, 1] = true;
                        break;
                    case 7:
                        talentArray[2, 1] = true;
                        break;
                    case 10:
                        talentArray[3, 1] = true;
                        break;
                    case 13:
                        talentArray[4, 1] = true;
                        break;
                    case 16:
                        talentArray[5, 1] = true;
                        break;
                    case 20:
                        talentArray[6, 1] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }
        }
        
        public virtual void LevelTalentThree() {
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        Main.NewText("Level up three!");
                        talentArray[0, 2] = true;
                        break;
                    case 4:
                        talentArray[1, 2] = true;
                        break;
                    case 7:
                        talentArray[2, 2] = true;
                        break;
                    case 10:
                        talentArray[3, 2] = true;
                        break;
                    case 13:
                        talentArray[4, 2] = true;
                        break;
                    case 16:
                        talentArray[5, 2] = true;
                        break;
                    case 20:
                        talentArray[6, 2] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }
        }

        public virtual void LevelTalentFour() {
            if (canSelectTalent) {
                switch (level) {
                    case 1:
                        talentArray[0, 3] = true;
                        break;
                    case 4:
                        talentArray[1, 3] = true;
                        break;
                    case 7:
                        talentArray[2, 3] = true;
                        break;
                    case 10:
                        talentArray[3, 3] = true;
                        break;
                    case 13:
                        talentArray[4, 3] = true;
                        break;
                    case 16:
                        talentArray[5, 3] = true;
                        break;
                    case 20:
                        talentArray[6, 3] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }
        }

        public virtual void SyncTalents() {
            TalentsPacket.Write(Main.LocalPlayer.whoAmI, CharacterEnum, talentArray);
        }

        public virtual void ReadCharacter(BinaryReader reader) {
            level = reader.ReadInt32();
        }

        public virtual void WriteCharacter(BinaryWriter writer) {
            writer.Write(level);
        }
        
        public virtual void ResetEffects() {}
        public virtual void PreUpdate() {}
        public virtual void PostUpdateBuffs() {}
        public virtual bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) { return true; }
        public virtual float UseTimeMultiplier(Item item) { return 1f; }
        public virtual void ModifyDrawLayers(List<PlayerLayer> layers) {}
        public virtual void PreUpdateMovement() {}
        public virtual void PostUpdateRunSpeeds() {}
        public virtual void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {}
        public virtual void SetControls() {}
        public virtual void PostUpdateEquips() {}
        public virtual void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {}
        
        public virtual void HandleAbility(Ability ability) {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            if (ability.Cooldown == 0 && mobaPlayer.currentResource >= ability.ResourceCost && !ability.IsActive) {
                mobaPlayer.currentResource -= ability.ResourceCost;
                Packets.AbilityCastPacket.Write(abilities.IndexOf(ability), player.whoAmI);
                ability.Cast();
            }
        }
        
        public virtual void HealMe(ref int amount) {}

        public virtual void UpdateBaseStats() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();

            mobaPlayer.maxLife += baseMaxLife;
            mobaPlayer.lifeRegen += baseLifeRegen;
            mobaPlayer.lifeDegen += baseLifeDegen;
            mobaPlayer.maxResource += baseMaxResource;
            mobaPlayer.resourceRegen = baseResourceRegen;
            mobaPlayer.resourceDegen = baseResourceDegen;
            mobaPlayer.armor += baseArmor;
        }

        //Etc.
        public virtual void SlayEffect(Player deadPlayer) {}
        public virtual void TeamSlayEffect(Player deadPlayer) {}
    }
}