using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Abilities;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Characters {
    public abstract class Character {
        public Player User { get; }
        public int Level { get; protected set; }
        public string CharacterName { get; protected set; }
        public bool[,] TalentArray { get; protected set; }
        public int xpPerLevel = 100;
        public int Experience { get; protected set; }
        public List<Ability> Abilities { get; protected set; }

        public virtual Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }

        public virtual CharacterIdentity identity {
            get => CharacterIdentity.Base;
        }
        public virtual string FullName {
            get => "Default Character";
        }

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

        public Character(Player user) {
            User = user;
            
            vanityHead = new Item();
            dyeHead = new Item();
            vanityBody = new Item();
            dyeBody = new Item();
            vanityLeg = new Item();
            dyeLeg = new Item();
            primary = new Item();
            
            /*
            abilities = new List<Ability>(8);
            for(int i = 0; i < abilities.Capacity; i++) {
                abilities.Add(new Ability());
            }
            */
            TalentArray = new bool[7, 4];
        }
        
        public void SetCharacter() {
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();
            TerrariaMobaUtils.ClearInventory(mobaPlayer);
            InitializeCharacter();
            SetPlayer();
            SetStats();
            
            User.inventory[0] = primary;
            User.armor[10] = vanityHead;
            User.armor[11] = vanityBody;
            User.armor[12] = vanityLeg;

            User.dye[0] = dyeHead;
            User.dye[1] = dyeBody;
            User.dye[2] = dyeLeg;

            User.statLifeMax2 = baseMaxLife;
            User.statLife = baseMaxLife;
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
            Experience += xp;

            while (Experience >= xpPerLevel) {
                LevelUp();
                Experience -= xpPerLevel;
            }
        }
        
        public virtual void TalentSelect() { }
        public virtual void LevelUp() { }
        public virtual void InitializeCharacter() { }

        public virtual void LevelTalentOne() {
            /*
            Main.NewText(canSelectTalent);
            Main.NewText(Level);
            if (canSelectTalent) {
                switch (Level) {
                    case 1:
                        Main.NewText("Level up one!");
                        TalentArray[0, 0] = true;
                        break;
                    case 4:
                        TalentArray[1, 0] = true;
                        break;
                    case 7:
                        TalentArray[2, 0] = true;
                        break;
                    case 10:
                        TalentArray[3, 0] = true;
                        break;
                    case 13:
                        TalentArray[4, 0] = true;
                        break;
                    case 16:
                        TalentArray[5, 0] = true;
                        break;
                    case 20:
                        TalentArray[6, 0] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }
            */
        }
        
        public virtual void LevelTalentTwo() {
            /*if (canSelectTalent) {
                switch (Level) {
                    case 1:
                        Main.NewText("Level up two!");
                        TalentArray[0, 1] = true;
                        break;
                    case 4:
                        TalentArray[1, 1] = true;
                        break;
                    case 7:
                        TalentArray[2, 1] = true;
                        break;
                    case 10:
                        TalentArray[3, 1] = true;
                        break;
                    case 13:
                        TalentArray[4, 1] = true;
                        break;
                    case 16:
                        TalentArray[5, 1] = true;
                        break;
                    case 20:
                        TalentArray[6, 1] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }*/
        }
        
        public virtual void LevelTalentThree() {
            /*if (canSelectTalent) {
                switch (Level) {
                    case 1:
                        Main.NewText("Level up three!");
                        TalentArray[0, 2] = true;
                        break;
                    case 4:
                        TalentArray[1, 2] = true;
                        break;
                    case 7:
                        TalentArray[2, 2] = true;
                        break;
                    case 10:
                        TalentArray[3, 2] = true;
                        break;
                    case 13:
                        TalentArray[4, 2] = true;
                        break;
                    case 16:
                        TalentArray[5, 2] = true;
                        break;
                    case 20:
                        TalentArray[6, 2] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }*/
        }

        public virtual void LevelTalentFour() {
            /*if (canSelectTalent) {
                switch (Level) {
                    case 1:
                        TalentArray[0, 3] = true;
                        break;
                    case 4:
                        TalentArray[1, 3] = true;
                        break;
                    case 7:
                        TalentArray[2, 3] = true;
                        break;
                    case 10:
                        TalentArray[3, 3] = true;
                        break;
                    case 13:
                        TalentArray[4, 3] = true;
                        break;
                    case 16:
                        TalentArray[5, 3] = true;
                        break;
                    case 20:
                        TalentArray[6, 3] = true;
                        break;
                }
                canSelectTalent = false;
                SyncTalents();
            }*/
        }

        public virtual void SyncTalents() {

        }

        public virtual void ReadCharacter(BinaryReader reader) {
            Level = reader.ReadInt32();
        }

        public virtual void WriteCharacter(BinaryWriter writer) {
            writer.Write(Level);
        }
        
        /*
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
        */
        
        public virtual void HandleAbility(Ability ability) {
            /*
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            if (ability.cooldownTimer == 0 && mobaPlayer.currentResource >= ability.ResourceCost && !ability.IsActive) {
                mobaPlayer.currentResource -= ability.ResourceCost;
                ability.OnCast();
            }
            */
        }
        
        public virtual void UpdateBaseStats() {
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();

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